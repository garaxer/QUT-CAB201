using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    /// <summary>
    /// This class represents a tank on the battlefield, as distinct from Chassis which represents a particular model of tank.
    /// </summary>
    public class GameplayTank
    {
        private GenericPlayer player;
        private int tankX;
        private int tankY;
        private Battle game;
        private int durability;

        private float angle;
        private int power;
        private int currentWeapon;
        private Bitmap tankBitmap;

        /// <summary>
        /// This constructor stores player, tankX, tankY and game as private fields of GameplayTank. 
        /// It then gets the Chassis by using the GenericPlayer's GetTank() method,
        /// then calls GetTankArmour() on it and stores this as the GameplayTank's current durability. 
        /// This will go down as the tank takes damage.
        /// </summary>
        /// <param name="player">the player using the tank</param>
        /// <param name="tankX">coordinate</param>
        /// <param name="tankY">coordinate</param>
        /// <param name="game">the battle the tank is apart of</param>
        public GameplayTank(GenericPlayer player, int tankX, int tankY, Battle game)
        {
            this.player = player;
            this.tankX = tankX;
            this.tankY = tankY;
            this.game = game;

            durability = player.GetTank().GetTankArmour();
            angle = 0;
            power = 25;
            currentWeapon = 0;

            tankBitmap = player.GetTank().CreateTankBMP(player.GetColour(), angle);

        }

        /// <returns>Returns the GenericPlayer using the tank</returns>
        public GenericPlayer GetPlayerNumber()
        {
            return player;
        }

        /// <returns>Returns the chassis type of the tank</returns>
        public Chassis GetTank()
        {
            return player.GetTank();
        }

        /// <returns>Returns the angle the tank is currently pointing at</returns>
        public float GetTankAngle()
        {
            return angle;
        }

        /// <param name="angle">The angle of the line the tank will use</param>
        public void SetAngle(float angle)
        {
            this.angle = angle;
            Color c = player.GetColour();
            tankBitmap = player.GetTank().CreateTankBMP(c, angle);
        }

        /// <returns>Returns the power of the tank, int between 5 and 100</returns>
        public int GetTankPower()
        {
            return power;
        }

        /// <param name="power">The power of the tank to set</param>
        public void SetForce(int power)
        {
            this.power = power;
        }

        /// <returns>Returns the Weapon number the tank has selecteed</returns>
        public int GetCurrentWeapon()
        {
            return currentWeapon;
        }

        /// <param name="newWeapon">The weapon number to set as the the current weapon</param>
        public void SelectWeapon(int newWeapon)
        {
            currentWeapon = newWeapon;
        }

        /// <summary>
        /// Displays the tank on the screen
        /// </summary>
        public void Render(Graphics graphics, Size displaySize)
        {
            int drawX1 = displaySize.Width * tankX / Battlefield.WIDTH;
            int drawY1 = displaySize.Height * tankY / Battlefield.HEIGHT;
            int drawX2 = displaySize.Width * (tankX + Chassis.WIDTH) / Battlefield.WIDTH;
            int drawY2 = displaySize.Height * (tankY + Chassis.HEIGHT) / Battlefield.HEIGHT;
            graphics.DrawImage(tankBitmap, new Rectangle(drawX1, drawY1, drawX2 - drawX1, drawY2 - drawY1));

            int drawY3 = displaySize.Height * (tankY - Chassis.HEIGHT) / Battlefield.HEIGHT;
            Font font = new Font("Arial", 8);
            Brush brush = new SolidBrush(Color.White);

            int pct = durability * 100 / player.GetTank().GetTankArmour() ;
            if (pct < 100)
            {
                graphics.DrawString(pct + "%", font, brush, new Point(drawX1, drawY3));
            }
        }

        /// <returns>int x coordiante</returns>
        public int XPos()
        {
            return tankX;
        }

        /// <returns>y coordinate</returns>
        public int GetY()
        {
            return tankY;
        }

        /// <summary>
        /// This causes the GameplayTank to fire its current weapon.
        /// This method calls its own GetTank() method, then calls ShootWeapon() on that Chassis, passing in the current weapon, 
        /// the this reference and the private Battle field of GameplayTank.
        /// </summary>
        public void Launch()
        {
            GetTank().ShootWeapon(currentWeapon, this, game);
        }

        /// <param name="damageAmount">the ammount to damage the player</param>
        public void DamagePlayer(int damageAmount)
        {
            durability = durability - damageAmount;
        }


        /// <returns>Returns true if the tank has durability left (>0), otherwise fals</returns>
        public bool TankExists()
        {
            if (durability > 0)
            {
                return true;
            } else
            {
                return false;
            }
        }

        /// <summary>
        /// Calling this method calls the GameplayTank to fall down one tile, if possible.
        /// </summary>
        /// <returns>Returns true if the tank moved, false otherwise</returns>
        public bool ProcessGravity()
        {
            if (!TankExists())
            {
                return false;
            }else
            {
                //If there is any terrain within the tank area, this method returns true. Otherwise it returns false
                if (game.GetMap().TankFits(tankX, tankY + 1))
                {
                    return false;
                } else
                {
                    tankY++;
                    durability--;
                    if (tankY == Battlefield.HEIGHT - Chassis.HEIGHT)
                    {
                        durability = 0;
                    }
                    return true;
                }
            }
        }
    }
}
