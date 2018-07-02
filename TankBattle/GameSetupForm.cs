using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class GameSetupForm : Form
    {
        private Battle game;
        private int numPlayers;
        private int playerNumber = 1;
        int tankType = 1;
        public GameSetupForm(Battle game)
        {
            this.game = game;
            numPlayers = game.NumPlayers();
            InitializeComponent();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            bool humanPLayer = false; //is human player
            if (human.Checked == true)
            {
                humanPLayer = true;
            }

            //int tankType = int.Parse(tank1.Text); //how to find as a group

            if (playerNumber <= numPlayers)
            {
                if (humanPLayer)
                {
                    GenericPlayer player = new PlayerController(nameBox.Text, Chassis.GetTank(tankType), Battle.PlayerColour(playerNumber));
                    game.RegisterPlayer(playerNumber, player);
                }
                else
                {
                    GenericPlayer player = new ComputerPlayer(nameBox.Text, Chassis.GetTank(tankType), Battle.PlayerColour(playerNumber));
                    game.RegisterPlayer(playerNumber, player);
                }
                
   
                if (playerNumber == numPlayers-1)
                {
                    nextButton.Text = "Done!";
                }
                    playerNumber++;
                    playerLabel.Text = String.Format("Player #{0}'s name", playerNumber);
                    nameBox.Text = String.Format("Player {0}", playerNumber);

            }
            if(playerNumber > numPlayers )
            {
                Hide();
                game.NewGame();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            tankType = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            tankType = 2;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
