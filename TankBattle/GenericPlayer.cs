using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    abstract public class GenericPlayer
    {
        private Chassis tank;
        private String name;
        private Color colour;

        private int roundsWon = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the player</param>
        /// <param name="tank">Chassis the player uses</param>
        /// <param name="colour">Colour the player uses</param>
        public GenericPlayer(string name, Chassis tank, Color colour)
        {
            this.name = name;
            this.tank = tank;
            this.colour = colour;
        }

        /// <returns>Returns the chassis that the player uses</returns>
        public Chassis GetTank()
        {
            return tank;
        }

        /// <returns>Returns a String containing the name of this player</returns>
        public string PlayerName()
        {
            return name;
        }

        /// <returns>Returns the colour of the player</returns>
        public Color GetColour()
        {
            return colour;
        }

        /// <summary>
        /// Increments the amount rounds a player has won
        /// </summary>
        public void Winner()
        {
            roundsWon++;
        }

        /// <returns>returns an int the player has won</returns>
        public int GetScore()
        {
            return roundsWon;
        }

        public abstract void BeginRound();

        public abstract void NewTurn(GameplayForm gameplayForm, Battle currentGame);

        public abstract void ReportHit(float x, float y);
    }
}
