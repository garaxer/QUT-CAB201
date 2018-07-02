using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    /// <summary>
    /// Chassis is an abstract class representing a generic tank model.
    /// </summary>
    public abstract class Chassis
    {
        public const int WIDTH = 4;
        public const int HEIGHT = 3;
        public const int NUM_TANKS = 2;

        public abstract int[,] DisplayTank(float angle);

        /// <summary>
        /// If given a field, draws a line with the specified coordinates
        /// </summary>
        /// <param name="graphic">where to draw line</param>
        /// <param name="X1">start of line</param>
        /// <param name="Y1"></param>
        /// <param name="X2">end of line</param>
        /// <param name="Y2"></param>
        public static void SetLine(int[,] graphic, int X1, int Y1, int X2, int Y2)
        {
                        
            int dx = X2 - X1;
            int dy = Y2 - Y1;

            if (dx < 3 && dx > -3)
            {
                for (int y = Y2; y <= Y1; y++)
                {
                    int x = X1 + dx * (y -Y1) / dy;
                    graphic[y, x] = 1;
                }

            }
            else if (X1 <= X2)
            {
                for (int x = X1; x <= X2; x++)
                {
                    int y = Y1 + dy * (x - X1) / dx;
                    graphic[y, x] = 1;
                }
            }
            else
            {
                for (int x = X2; x <= X1; x++)
                {
                    int y = Y1 + dy * (x - X1) / dx;
                    graphic[y,x] = 1;
                }

            }
            
            /*
            for (int x = 0; x < graphic.GetLength(0); x++)
            {
                for (int y = 0;y < graphic.GetLength(1); y++)
                {
                    Console.Write(graphic[x,y]);
                }
                Console.WriteLine("");
            } */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tankColour"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public Bitmap CreateTankBMP(Color tankColour, float angle)
        {
            int[,] tankGraphic = DisplayTank(angle);
            int height = tankGraphic.GetLength(0);
            int width = tankGraphic.GetLength(1);

            Bitmap bmp = new Bitmap(width, height);
            Color transparent = Color.FromArgb(0, 0, 0, 0);
            Color tankOutline = Color.FromArgb(255, 0, 0, 0);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (tankGraphic[y, x] == 0)
                    {
                        bmp.SetPixel(x, y, transparent);
                    }
                    else
                    {
                        bmp.SetPixel(x, y, tankColour);
                    }
                }
            }

            // Outline each pixel
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    if (tankGraphic[y, x] != 0)
                    {
                        if (tankGraphic[y - 1, x] == 0)
                            bmp.SetPixel(x, y - 1, tankOutline);
                        if (tankGraphic[y + 1, x] == 0)
                            bmp.SetPixel(x, y + 1, tankOutline);
                        if (tankGraphic[y, x - 1] == 0)
                            bmp.SetPixel(x - 1, y, tankOutline);
                        if (tankGraphic[y, x + 1] == 0)
                            bmp.SetPixel(x + 1, y, tankOutline);
                    }
                }
            }

            return bmp;
        }

        /// <summary>
        /// This abstract method gets the starting durability of this type of tank.
        /// </summary>
        /// <returns>the initial value of the tank's armour</returns>
        public abstract int GetTankArmour();

        public abstract string[] ListWeapons();

        public abstract void ShootWeapon(int weapon, GameplayTank playerTank, Battle currentGame);

        /// <summary>
        /// Generates a new tank depending which tank is chosen
        /// </summary>
        /// <param name="tankNumber">the tank to choose</param>
        /// <returns>returns a new tank type</returns>
        public static Chassis GetTank(int tankNumber)
        {
            if (tankNumber == 2)
            {
                return new coolTank();
            }
            else
            {
                return new MyTank();
            }
        }
    }
}

