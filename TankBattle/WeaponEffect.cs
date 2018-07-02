using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    /// <summary>
    /// This abstract class represents a generic effect created by a GameplayTank's attack. Both Boom and Shell come under this umbrella.
    /// </summary>
    public abstract class WeaponEffect
    {
        protected Battle game;

        /// <param name="game">The game where the weapon effect will be used</param>
        public void SetCurrentGame(Battle game)
        {
            this.game = game;
        }

        public abstract void Step();
        public abstract void Render(Graphics graphics, Size displaySize);
    }
}
