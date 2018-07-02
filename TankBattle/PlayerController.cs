using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    public class PlayerController : GenericPlayer
    {
        /// <summary>
        /// Not used by human player
        /// </summary>
        public PlayerController(string name, Chassis tank, Color colour) : base(name, tank, colour)
        {
 
        }

        /// <summary>
        /// Not used by human player
        /// </summary>
        public override void BeginRound()
        {

        }

        /// <summary>
        /// Allows the current player to control the gamePlayform
        /// </summary>
        /// <param name="gameplayForm">The form the player interacts with</param>
        /// <param name="currentGame">Not used by human player</param>
        public override void NewTurn(GameplayForm gameplayForm, Battle currentGame)
        {
            gameplayForm.EnableTankControls(); 
        }

        /// <summary>
        /// Not used by human player
        /// </summary>
        public override void ReportHit(float x, float y)
        {
 
        }
    }
}
