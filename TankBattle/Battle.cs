//assignment ID: 328609139
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TankBattle
{
    /// <summary>
    /// Represents the main data of the game and initialises how many are playing, 
    /// Keeps track of the game elements
    /// </summary>
    public class Battle
    {
        private GenericPlayer[] GenericPlayers; //All the players of the game
        private int numRounds;
        private int numPlayers;
        private List<WeaponEffect> weaponEffects;

        //used in new game
        private int currentRound;
        private int startingGenericPlayer; //The player that is going to start their turn first.

        // used in Commence round
        private int currentPlayer; //The player who is interacting with the current tank
        private GameplayTank[] GameplayTanks;
        private int windSpeed;

        private Battlefield battlefield;

        /// <summary>
        /// Battle's contructor.
        /// Creates a numPlayer size array of generic player
        /// Creates a collection of weapon effects
        /// numPlayers will always be between 2 and 8 (inclusive)
        ///numRounds will always be between 1 and 100 (inclusive)
        /// </summary>
        /// <param name="numPlayers"> number of players in the game</param>
        /// <param name="numRounds">the number of rounds that will be played.</param>
        public Battle(int numPlayers, int numRounds)
        {
            // throw new NotImplementedException();

            GenericPlayers = new GenericPlayer[numPlayers];
            this.numRounds = numRounds;
            this.numPlayers = numPlayers;
            weaponEffects = new List<WeaponEffect>();
        }

        /// <summary>
        /// Returns the total number of players for this game.
        /// </summary>
        /// <returns>int between 2 and 8</returns>
        public int NumPlayers()
        {
            return numPlayers;
        }

        /// <summary>
        /// Returns the current round
        /// </summary>
        /// <returns>int between 1 and 100</returns>
        public int GetCurrentRound()
        {
            return currentRound;
        }

        /// <summary>
        /// Returns the total number of rounds that are going to be played
        /// </summary>
        /// <returns>int between 1 and 100</returns>
        public int GetRounds()
        {
            return numRounds;
        }

        /// <summary>
        /// Adds a player to the game
        /// </summary>
        /// <param name="playerNum">Which position the player is in. Player num must be between 1 and the number of players</param>
        /// <param name="player">The player to add the player array</param>
        public void RegisterPlayer(int playerNum, GenericPlayer player)
        {
            GenericPlayers[playerNum - 1] = player;
        }

        /// <summary>
        /// Gives the specified player from the array using the player's number
        /// </summary>
        /// <param name="playerNum">a player number (between 1 and the number of players)</param>
        /// <returns>returns the appropriate GenericPlayer from the GenericPlayer array</returns>
        public GenericPlayer GetPlayerNumber(int playerNum)
        {
            return GenericPlayers[playerNum - 1];
        }

        /// <summary>
        /// Returns the game playtank for the given player
        /// </summary>
        /// <param name="playerNum">The player number (between 1 and the number of players) </param>
        /// <returns>returns the GameplayTank associated with the GenericPlayer of that number. </returns>
        public GameplayTank GetGameplayTank(int playerNum)
        {          
            return GameplayTanks[playerNum - 1];
        }

        /// <summary>
        /// Returns the colour chosen for the specified player number
        /// </summary>
        /// <param name="playerNum">The player number (between 1 and the number of players) </param>
        /// <returns> returns an appropriate colour to be used to represent that player.</returns>
        public static Color PlayerColour(int playerNum)
        {
            Color[] arrayColors = { Color.Aqua, Color.LimeGreen, Color.Red, Color.RoyalBlue, Color.Orange, Color.Pink, Color.Yellow, Color.LightGoldenrodYellow };
            return arrayColors[playerNum - 1];
        }

        /// <summary>
        /// Given a number of players, this static method calculates evenly spread out horizontal positions for players
        /// </summary>
        /// <param name="numPlayers">the number of players in the game</param>
        /// <returns>Returns an array of ints containing the horizontal positions of those players on the map. </returns>
        public static int[] CalculatePlayerPositions(int numPlayers)
        {
            int[]Positions = new int[numPlayers];
            int firstPosition = (Battlefield.WIDTH / numPlayers) / 2; //calcutes position of the first player
            Positions[0] = firstPosition; 
            for (int i = 1; i < Positions.Length; i++)
            {
                Positions[i] = (Battlefield.WIDTH / numPlayers) * i + firstPosition; 
            }
            return Positions;
        }

        /// <summary>
        /// Given an array of at least 1, randomises the other of the numbers in it:
        /// </summary>
        /// <param name="array">the array of numbers to randomize</param>
        public static void Rearrange(int[] array)
        {
            Random rng = new Random();
            for (int t = 0; t < array.Length; t++)
            {
                int tmp = array[t];
                int r = rng.Next(t, array.Length);
                array[t] = array[r];
                array[r] = tmp; 
            }
        }

        /// <summary>
        /// Begins a new game and calls the first round to be started
        /// </summary>
        public void NewGame()
        {
            currentRound = 1;
            startingGenericPlayer = 0;
            CommenceRound();
        }

        /// <summary>
        /// begins a new round of gameplay. A game consists of multiple rounds, and a round consists of multiple turns.
        /// </summary>
        public void CommenceRound()
        {
            //Initialising a private field of Battle representing the current player to the value of the starting GenericPlayer field (see NewGame).
            currentPlayer = startingGenericPlayer;
            //Creating a new Battlefield, which is also stored as a private field of Battle.
             battlefield = new Battlefield();
            //Creating an array of GenericPlayer positions by calling CalculatePlayerPositions with the number of GenericPlayers playing the game
            int[] positions = CalculatePlayerPositions(GenericPlayers.Length);
            //Looping through each GenericPlayer and calling its BeginRound method.
            foreach (GenericPlayer genricPlayer in GenericPlayers)
            {
                genricPlayer.BeginRound(); //is only currently used for computer players
            }
            
            Rearrange(positions);//Shuffling that array of positions with the Rearrange method.

            GameplayTanks = new GameplayTank[GenericPlayers.Length];
            for (int i = 0; i < GameplayTanks.Length; i++)
            {
                GameplayTanks[i] = new GameplayTank(GenericPlayers[i],positions[i], battlefield.TankVerticalPosition(positions[i]),this); 
            }
            Random random = new Random();
            windSpeed = random.Next(-100, 100); //create a wind that will be used to move the shell

            GameplayForm gameForm = new GameplayForm(this);
            gameForm.Show();
        }

        /// <returns> Returns A battle field that the game uses</returns>
        public Battlefield GetMap()
        {
            return battlefield;
        }

        /// <summary>
        ///  tells all the GameplayTanks to draw themselves. 
        /// </summary>
        public void DrawTanks(Graphics graphics, Size displaySize)
        {
            for (int i = 0; i < GameplayTanks.Length; i++)
            {
                if (GameplayTanks[i].TankExists()) {
                    GameplayTanks[i].Render(graphics,displaySize);
                };

            }
        }
        
        /// <returns>returns the GameplayTank associated with the current player.</returns>
        public GameplayTank GetCurrentGameplayTank()
        {
            return GameplayTanks[currentPlayer];
        }

        /// <summary>
        /// This method adds the given WeaponEffect to its list/array of WeaponEffects.
        /// </summary>
        /// <param name="weaponEffect">The WeaponEffect to be added to this game</param>
        public void AddWeaponEffect(WeaponEffect weaponEffect)
        {
            weaponEffects.Add(weaponEffect);
            weaponEffects[weaponEffects.IndexOf(weaponEffect)].SetCurrentGame(this);
        }

        /// <summary>
        /// This method loops through all WeaponEffects in the array, calling Step() on each.
        /// </summary>
        /// <returns> returns true if there were any WeaponEffects to call Step() on, and false otherwise.</returns>
        public bool ProcessWeaponEffects()
        {
            if (weaponEffects.Count() > 0 ) { 
                for (int i = 0; i <weaponEffects.Count(); i++) {
                    weaponEffects.ElementAt(i).Step();
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// This method loops through all WeaponEffects in the array, calling Render() on each
        /// </summary>
        /// <param name="graphics">To be passed to WeaponEffects render</param>
        /// <param name="displaySize">To be passed to WeaponEffects render</param>
        public void DisplayEffects(Graphics graphics, Size displaySize)
        {
            foreach (WeaponEffect weaponEffect in weaponEffects)
            {
                weaponEffect.Render(graphics,displaySize);
            }
        }

        /// <summary>
        /// This method removes the WeaponEffect referenced by weaponEffect from the array or list used by Battle to store active WeaponEffects.
        /// </summary>
        /// <param name="weaponEffect">weaponeffect to remove</param>
        public void CancelEffect(WeaponEffect weaponEffect)
        {
            weaponEffects.Remove(weaponEffect);
        }

        /// <param name="projectileX">x coordinate of the shell</param>
        /// <param name="projectileY">y coordinate of the shell</param>
        /// <returns>This method returns true if a Shell at projectileX, projectileY will hit something.</returns>
        public bool CheckCollidedTank(float projectileX, float projectileY)
        {
            //If the coordinates given are outside the map boundaries return false
            if (projectileX < 0 || projectileX > Battlefield.WIDTH || projectileY < 0 || projectileY > Battlefield.HEIGHT)
            {
                return false;
            }
            //If the Battlefield contains something at that location return true
            else if (battlefield.IsTileAt((int)projectileX, (int)projectileY))
            {
                return true;
            }
            //If there is a GameplayTank at that location and that GameplayTank returns true when TankExists is called on it, return true.
            for (int i = 0; i < GameplayTanks.Length; i++)
            {
                if (projectileX >= GameplayTanks[i].XPos() && projectileX <= GameplayTanks[i].XPos() + Chassis.WIDTH
                    && projectileY >= GameplayTanks[i].GetY() && projectileY <= GameplayTanks[i].GetY() + Chassis.HEIGHT
                    && GameplayTanks[0].TankExists() && currentPlayer != i)
                {
                    return true;
                }
            }
            return false; 
        }

        /// <summary>
        /// This method inflicts up to explosionDamage damage on any GameplayTanks within the circle described by damageX, damageY and radius.
        /// </summary>
        /// <param name="damageX">X coordinate of Center of the damage circle</param>
        /// <param name="damageY">Y coordinate of Center of the damage circle</param>
        /// <param name="explosionDamage">Damage to inflict on the tank</param>
        /// <param name="radius">the size of the damage circle</param>
        public void DamagePlayer(float damageX, float damageY, float explosionDamage, float radius)
        {
            for (int i = 0; i < GameplayTanks.Length; i++)
            {
                if (GameplayTanks[i].TankExists())
                {
                    float centreTankX = GameplayTanks[i].XPos() + (Chassis.WIDTH/2);
                    float centreTankY = GameplayTanks[i].GetY() + (Chassis.HEIGHT); // 2?
                    float distance = (float)Math.Sqrt(Math.Pow(damageX - centreTankX, 2) + Math.Pow(damageY - centreTankY, 2));
                    if (distance > radius)
                    {
                        GameplayTanks[i].DamagePlayer(0);
                    }
                    //if its not a direct hit, calculate a percent to damage the tank
                    if (distance < radius && distance > radius/2)
                    {
                        float damage = (explosionDamage * (radius - distance)) / radius;
                        GameplayTanks[i].DamagePlayer((int)damage);
                    }
                    else if(distance < radius /2 ) ///2
                    {
                        GameplayTanks[i].DamagePlayer((int)explosionDamage);
                    }
                    
                }
            }
        }

        /// <summary>
        /// This method is called after all WeaponEffect animations have finished and moves any terrain and/or GameplayTanks that are floating in the air down. 
        /// It will be called in a loop by the GameplayForm.
        /// </summary>
        /// <returns>this method returns false once there is nothing left to move, and true until then. </returns>
        public bool ProcessGravity()
        {
            bool moved = false;

            moved = battlefield.ProcessGravity();
            for (int i = 0; i < GameplayTanks.Length; i++)
            {
                if (GameplayTanks[i].ProcessGravity())
                {
                    moved = true;
                }
            }
            return moved;
        }

        /// <summary>
        /// This method is called once the current turn is over. 
        /// It checks how many GameplayTanks are still in battle, 
        /// makes a determination as to whether the round is over or not, 
        /// and if not changes the current player to the next player that's still in the running. 
        /// </summary>
        /// <returns>This method returns true if the round is still going, and false if it's over.</returns>
        public bool TurnOver()
        {
            int noOfTanks = 0;
            for (int i = 0; i < GameplayTanks.Length; i++)
            {
                if (GameplayTanks[i].TankExists())
                {
                    noOfTanks++;
                }
            }
            if (noOfTanks > 1)
            {
                bool foundPlayer = false;
                while (!foundPlayer)
                {
                    currentPlayer++;
                    if (currentPlayer >= numPlayers)
                    {
                        currentPlayer = 0;
                    }
                    if (GameplayTanks[currentPlayer].TankExists())
                    {
                        foundPlayer = true;
                    }  
                }

                Random random = new Random();
                windSpeed = windSpeed + random.Next(-10, 10);
                if (windSpeed < -100)
                {
                    windSpeed = -100;
                }
                if (windSpeed > 100)
                {
                    windSpeed = 100;
                }

                return true;
            }else  {
                this.FindWinner();
                return false; 
            }

        }

        /// <summary>
        /// It finds out which player won the round and rewards that player with a point.
        /// </summary>
        public void FindWinner()
        {
            for (int i = 0; i < GameplayTanks.Length; i++)
            {
                if (GameplayTanks[i].TankExists())
                {
                    GameplayTanks[i].GetPlayerNumber().Winner();
                }
            }
        }

        /// <summary>
        /// This method is called by GameplayForm after the round is over 
        /// (GameplayForm decides whether the round is over based on whether TurnOver() returned true or false).
        /// </summary>
        public void NextRound()
        {
            currentRound++;
            if(currentRound <= numRounds)
            {
                startingGenericPlayer++;
                if (startingGenericPlayer > numPlayers)
                {
                    startingGenericPlayer = 0;
                }
                CommenceRound();
            } else
            {

                //GameplayForm gameForm = new GameplayForm(this);
                //gameForm.Hide();

                Winner winner = new Winner(this);
                winner.Show();
            }
        }
        
        /// <returns>Returns a int between -100 and 100</returns>
        public int WindSpeed()
        {
            return windSpeed;
        }
    }
}
