using System;

namespace TankBattle
{
    internal class MyTank : Chassis
    {
        /// <summary>
        /// This method draws the tank into an array and returns it. 
        /// </summary>
        /// <param name="angle">The angle to draw the line</param>
        /// <returns>returns an int[12,16] array containing a 1 for each pixel that makes up the tank's shape.</returns>
        public override int[,] DisplayTank(float angle)
        {
            int[,] graphic = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 },
                   { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                   { 0, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0 },
                   { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            /* //this looks fine with the Basic line drawer
            if ( angle < -67.5)
            {
                SetLine(graphic, 7, 6, 2, 6);
            } else if ( angle >= -67.5 && angle < -22.5) //-90 degrees
            {
                SetLine(graphic, 7, 6, 3, 2);
            } else if (angle <= 22.5 && angle >= -22.5)
            {
                SetLine(graphic, 7, 6, 7, 1); //0, straight up
            }
            else if (angle > 22.5 && angle <= 67.5)
            {
                SetLine(graphic, 7, 6, 12, 2);
            } else if (angle >= 67.5)
            {
                SetLine(graphic, 7, 6, 12, 6); //90
            } else
            {
                //
            }*/

            //Find the correct line to draw
            float angleRadians = (90 - angle) * (float)Math.PI / 180;
            float xVelocity = (float)Math.Cos(angleRadians) * 6;
            float yVelocity = (float)Math.Sin(angleRadians) * -6;
            SetLine(graphic, 7, 6, 7+(int)Math.Round(xVelocity), 6 + (int)Math.Round(yVelocity));

            return graphic;
        }

        /// <summary>
        /// gets the starting durability of this type of tank.
        /// </summary>
        /// <returns>Returns the initial int armour value of this tank.</returns>
        public override int GetTankArmour()
        {
            return 100;
        }

        /// <summary>
        /// returns an array containing a list of weapons that this tank has. 
        /// </summary>
        public override string[] ListWeapons()
        {
            return new string[] { "Standard shell", "Double Shot"};
        }

        /// <summary>
        /// used to handle firing the specified weapon from the tank playerTank.
        /// </summary>
        /// <param name="weapon">The weapon chosen from ListWeapons</param>
        /// <param name="playerTank">The tank that is shooting the weapons</param>
        /// <param name="currentGame">The battle the shot takes place in</param>
        public override void ShootWeapon(int weapon, GameplayTank playerTank, Battle currentGame)
        {
            float x = playerTank.XPos();
            float y = playerTank.GetY();
            //find the center of the tank
            x = x + Chassis.WIDTH / 2;
            y = y + Chassis.HEIGHT / 2;

            GenericPlayer player =  playerTank.GetPlayerNumber();
            Boom boom = new Boom(100, 4, 4);
            Shell shell = new Shell(x, y, playerTank.GetTankAngle(), playerTank.GetTankPower(), 0.01f, boom, player);

            currentGame.AddWeaponEffect(shell);

            //if doubleshot is selected, fire a second shot
            if (weapon == 1)
            {
                Shell shell2 = new Shell(x, y, playerTank.GetTankAngle()-5, playerTank.GetTankPower(), 0.01f, boom, player);
                currentGame.AddWeaponEffect(shell2);
            }
        }
    }
}