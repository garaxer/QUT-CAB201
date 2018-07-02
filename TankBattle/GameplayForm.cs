using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class GameplayForm : Form
    {

        //Offsets
        int displayPanelOffest;
        int ControlPanelOffest;


        private Color landscapeColour;
        private Random rng = new Random();
        private Image backgroundImage = null;
        private int levelWidth = 160;
        private int levelHeight = 120;
        private Battle currentGame;

        private BufferedGraphics backgroundGraphics;
        private BufferedGraphics gameplayGraphics;

        public GameplayForm(Battle game)
        {
            currentGame = game;
            string[] imageFilenames = { "Images\\background1.jpg",
                            "Images\\background2.jpg",
                            "Images\\background3.jpg",
                            "Images\\background4.jpg"};
            Color[] landscapeColours = { Color.FromArgb(255, 0, 0, 0),
                             Color.FromArgb(255, 73, 58, 47),
                             Color.FromArgb(255, 148, 116, 93),
                             Color.FromArgb(255, 133, 119, 109) };
            Random random = new Random();

            landscapeColour =  landscapeColours[random.Next(0, 4)];
            backgroundImage = Image.FromFile(imageFilenames[random.Next(0, 4)]);


            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            InitializeComponent();

            backgroundGraphics = InitDisplayBuffer();
            gameplayGraphics = InitDisplayBuffer();

            DrawBackground();
            DrawGameplay();
            NewTurn();


            //Create offsets
            ControlPanelOffest = this.Width - controlPanel.Width;
            displayPanelOffest = this.Width - displayPanel.Width;
        }
        

        // From https://stackoverflow.com/questions/13999781/tearing-in-my-animation-on-winforms-c-sharp
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        public void EnableTankControls()
        {
            controlPanel.Enabled = true;
        }

        public void SetAngle(float angle)
        {
            angleSelector.Value = (int)angle;
        }

        public void SetForce(int power)
        {
            powerSelector.Value = power;
            powerLabel.Text = String.Format("{0}", powerSelector.Value);
        }
        public void SelectWeapon(int weapon)
        {
            weaponSelector.SelectedIndex = weapon;
        }

        public void Launch()
        {
            currentGame.GetCurrentGameplayTank().Launch();
            controlPanel.Enabled = false;
            timer1.Enabled = true;
        }
        private void DrawGameplay()
        {
            //Renders the backgroundGraphics buffer to the gameplayGraphics buffer: 
            backgroundGraphics.Render(gameplayGraphics.Graphics);
            currentGame.DrawTanks(gameplayGraphics.Graphics, displayPanel.Size);
            currentGame.DisplayEffects(gameplayGraphics.Graphics, displayPanel.Size);
        }


        private void NewTurn()
        {
            GameplayTank tank = currentGame.GetCurrentGameplayTank();
            GenericPlayer player = tank.GetPlayerNumber();

            //Sets the form caption
            Text = String.Format("Tank Battle - Round {0} of {1}", currentGame.GetCurrentRound(), currentGame.GetRounds());

            controlPanel.BackColor = player.GetColour();

            playerLabel.Text = player.PlayerName();
            
            SetAngle(tank.GetTankAngle());
            SetForce(tank.GetTankPower());

            int wind = currentGame.WindSpeed();
            if (wind >= 0 )
            {
                windLabel.Text = String.Format("{0} E", wind);
            } else
            {
                windLabel.Text = String.Format("{0} W", Math.Abs(wind));
            }

            //Remove all the current selectable weapons and replace them with the current tanks weapons.
            weaponSelector.Items.Clear();
            String[] weapons = tank.GetTank().ListWeapons();
            foreach (String weapon in weapons) {
                weaponSelector.Items.Add(weapon);
            }
            //tank.SelectWeapon
            SelectWeapon(tank.GetCurrentWeapon());

            player.NewTurn(this, currentGame);
        }


        private void DrawBackground()
        {
            Graphics graphics = backgroundGraphics.Graphics;
            Image background = backgroundImage;
            graphics.DrawImage(backgroundImage, new Rectangle(0, 0, displayPanel.Width, displayPanel.Height));

            Battlefield battlefield = currentGame.GetMap();
            Brush brush = new SolidBrush(landscapeColour);

            for (int y = 0; y < Battlefield.HEIGHT; y++)
            {
                for (int x = 0; x < Battlefield.WIDTH; x++)
                {
                    if (battlefield.IsTileAt(x, y))
                    {
                        int drawX1 = displayPanel.Width * x / levelWidth;
                        int drawY1 = displayPanel.Height * y / levelHeight;
                        int drawX2 = displayPanel.Width * (x + 1) / levelWidth;
                        int drawY2 = displayPanel.Height * (y + 1) / levelHeight;
                        graphics.FillRectangle(brush, drawX1, drawY1, drawX2 - drawX1, drawY2 - drawY1);
                    }
                }
            }
        }

        public BufferedGraphics InitDisplayBuffer()
        {
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            Graphics graphics = displayPanel.CreateGraphics();
            Rectangle dimensions = new Rectangle(0, 0, displayPanel.Width, displayPanel.Height);
            BufferedGraphics bufferedGraphics = context.Allocate(graphics, dimensions);
            return bufferedGraphics;
        }

        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = displayPanel.CreateGraphics();
            gameplayGraphics.Render(graphics);
        }

        private void fireButton_Click(object sender, EventArgs e)
        {
            currentGame.GetCurrentGameplayTank().Launch();
            controlPanel.Enabled = false;
            timer1.Enabled = true;
        }

        private void angleSelector_ValueChanged(object sender, EventArgs e)
        {
            currentGame.GetCurrentGameplayTank().SetAngle((float)angleSelector.Value);
            DrawGameplay();
            displayPanel.Invalidate();

        }

        private void powerSelector_Scroll(object sender, EventArgs e)
        {
            currentGame.GetCurrentGameplayTank().SetForce(powerSelector.Value);
            powerLabel.Text = String.Format("{0}", powerSelector.Value);
        }


        /// <summary>
        /// Handles the animation and physics logic
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //if all attack animations have ended 
            if (!currentGame.ProcessWeaponEffects())
            {
                currentGame.ProcessGravity(); //handle all the after - attack gravity cleanup.
                //redraw everything after potentially moving terrain.
                DrawBackground();
                DrawGameplay();
                displayPanel.Invalidate();// method to trigger a redraw.

                //if some tanks were moved
                if (currentGame.ProcessGravity())
                {
                    return;
                }
                // if nothing moved then the turn will end, if there is no more turns the round is over.
                else
                {
                    timer1.Enabled = false;
                    if (currentGame.TurnOver())
                    {
                        NewTurn();
                    } else
                    {
                        Dispose();
                        currentGame.NextRound();
                        return;
                    }
                }
            }
            //Otherwise, attack animations are still ongoing. 
            else
            {
                DrawGameplay();
                displayPanel.Invalidate();
                return;
            }
        }



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Down || keyData == Keys.Up || keyData == Keys.Right || keyData == Keys.Left)
            {
                // Process keys
                if (keyData == Keys.Right)
                {
                    int newValue = (int)angleSelector.Value + 5;
                    if (newValue > 90)
                    {
                        angleSelector.Value = 90;
                    }
                    else
                    {
                        angleSelector.Value = newValue;
                    }
                }

                if (keyData == Keys.Left)
                {
                    int newValue = (int)angleSelector.Value - 5;
                    if (newValue < -90)
                    {
                        angleSelector.Value = -90;
                    }
                    else
                    {
                        angleSelector.Value = newValue;
                    }
                }

                if (keyData == Keys.Up)
                {
                    int newValue = powerSelector.Value + 1;
                    if (newValue > 100)
                    {
                        powerSelector.Value = 100;
                    }
                    else
                    {
                        powerSelector.Value = newValue;
                    }
                    powerLabel.Text = String.Format("{0}", powerSelector.Value);
                    currentGame.GetCurrentGameplayTank().SetForce(powerSelector.Value);
                }

                if (keyData == Keys.Down)
                {
                    int newValue = powerSelector.Value - 1;
                    if (newValue < 5)
                    {
                        powerSelector.Value = 5;
                    }
                    else
                    {
                        powerSelector.Value = newValue;
                    }
                    currentGame.GetCurrentGameplayTank().SetForce(powerSelector.Value);
                    powerLabel.Text = String.Format("{0}", powerSelector.Value);
                }

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void GameplayForm_Resize(object sender, EventArgs e)
        {
            //controlPanel.Width = this.Width - ControlPanelOffest;
           // displayPanel.Width = this.Width - displayPanelOffest;

        }
    }
}
