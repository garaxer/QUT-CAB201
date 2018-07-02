using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    /// <summary>
    /// This class represents the landscape, the arena on which the tanks battle. 
    /// The terrain is randomly generated and can be destroyed during the round. 
    /// A new Battlefield with a newly-generated terrain is created for each round.
    /// </summary>
    public class Battlefield
    {
        public const int WIDTH = 160;
        public const int HEIGHT = 120;

        bool[,] field; // represents the terrain (where 'true' means there is terrain at that location)

        /// <summary>
        /// If there is battle it then it will randoml generates the terrain on which the tanks will battle.
        /// </summary>
        public Battlefield()
        {
            field = new bool[Battlefield.WIDTH, Battlefield.HEIGHT];
            Random random = new Random();
            int randTop = random.Next(0, WIDTH/2); //where to start drawing land
            for (int height = 0; height < Battlefield.HEIGHT; height++)
            {
                int randWidth = random.Next(0, WIDTH-1); //the start of the random block
                int randLeft = random.Next(-2, 0); //to expand left/right of the edge of the mountain
                int randRight = random.Next(1, 3);
                
                for (int width = 0; width < WIDTH; width++)
                {
                    if (!field[width, height])
                    {
                        if (height <= Chassis.HEIGHT + randTop)
                        {
                            field[width, height] = false;
                        }
                        else if (height == HEIGHT-1)
                        {
                            field[width, height] = true;
                        }
                        else if (field[width, height - 1] || width == randWidth)
                        {
                            field[width, height] = true; //expand left and right for the values chosen
                            for (int i = randLeft; i <= randRight; i++)
                            {
                                if (!(width + i > WIDTH-1) && !(width + i < 0))
                                {
                                    //if its on the edge only, just make it true. This in here in case you make expansion start from 0
                                    if (width == WIDTH-1 || width == 0)
                                    {
                                        field[width + i, height] = true;
                                    } //only expand if on an edge. This is too avoid expanding on the block next to it. The second part after the or could be omitted.
                                    else if (!field[width - 1, height - 1] || !field[width + 1, height - 1])
                                    {
                                        field[width + i, height] = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            field[width, height] = false;
                        }
                    }
                } // end for loop
            }
        }


        /// <summary>
        /// Finds out if there is a tile on the battlefield
        /// x will never be less than 0 or greater than Battlefield.WIDTH - 1.
        /// y will never be less than 0 or greater than Battlefield.HEIGHT - 1.
        /// </summary>
        /// <param name="x">the coordiante of the field</param>
        /// <returns>0 or 1</returns>
        public bool IsTileAt(int x, int y)
        {
            return field[x, y];
        }

        /// <summary>
        /// Finds out if there will be space for the tank
        /// 
        /// Neither x nor y will ever be less than 0.
        /// x will never be greater than Battlefield.WIDTH - Chassis.WIDTH
        /// y will never be greater than Battlefield.HEIGHT - Chassis.HEIGHT
        /// </summary>
        /// <param name="x">x coordinate of future location</param>
        /// <param name="y">y coordinate of future locations</param>
        /// <returns>false if tank fits, true if not</returns>
        public bool TankFits(int x, int y)
        {
            bool foundLand = false;
            for (int i = 0; i < Chassis.WIDTH; i++)
            {
                for (int j = 0; j < Chassis.HEIGHT; j++)
                {
                    // if haven't already found land and there is a tile to check beyond boundaries
                    if (!foundLand && !(x + i > Battlefield.WIDTH - 1) && !(y + j > Battlefield.HEIGHT - 1))
                    {
                        foundLand = field[x + i, y + j];
                    }
                }
            }
            return foundLand;
        }

        /// <summary>
        /// Finds out the tanks initial vertical position
        /// </summary>
        /// <param name="x">Tanks horizontal position</param>
        /// <returns> location of the nearest ground of the tank, y</returns>
        public int TankVerticalPosition(int x)
        {
            bool groundfound = false;
            int y = 0;

            while (!groundfound)
            {
                y++;
                groundfound = TankFits(x, y);
            }
            return y - 1; //-1 because we want to be above the found ground
        }

        /// <summary>
        /// Destroys part of the terrain
        /// </summary>
        public void TerrainDestruction(float destroyX, float destroyY, float radius)
        {
            for (int height = 0; height < Battlefield.HEIGHT; height++)
            {
                for (int width = 0; width < Battlefield.WIDTH; width++)
                {

                    float distance = (float)Math.Sqrt(Math.Pow(destroyX - width, 2) + Math.Pow(destroyY - height, 2));
                    if (distance < radius)
                    {
                        field[width, height] = false;
                    }
                }
            }
        }

        /// <summary>
        /// Moves everything without something below it down.
        /// </summary>
        /// <returns>Returns true if something moved</returns>
        public bool ProcessGravity()
        {
            bool moved = false;
            for (int width = 0; width < Battlefield.WIDTH - 1; width++)
            {
                bool temp = false;
                for (int height = Battlefield.HEIGHT -2 ; height >=  0; height--)
                {
                    //bottom most tile isn't getting moved
                    if (field[width,height] && !field[width, height+1])
                    {
                        temp = field[width, height + 1];
                        field[width, height + 1] = field[width, height];
                        field[width, height] = temp;
                        if (!moved)
                        {
                            moved = true;
                        }
                    }
                }
            }
            return moved;
        } 
    }
}
