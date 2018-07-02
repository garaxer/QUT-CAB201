using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    /// <summary>
    /// Concrete class that extends the GenericPlayer class, providing functionality specific to computer-controlled GenericPlayers. 
    /// Random angles and power are set for the computer player to use
    /// </summary>
    public class ComputerPlayer : GenericPlayer
    {

        private String name;
        private Chassis tank;
        private Color colour;

        /// <summary>
        ///  exists to pass its parameters to the base constructor
        /// </summary>
        /// <param name="name">Name to be displayed</param>
        /// <param name="tank">Computers tank</param>
        /// <param name="colour">Tanks colour</param>
        public ComputerPlayer(string name, Chassis tank, Color colour) : base(name, tank, colour)
        {
            this.name = name;
            this.tank = tank;
            this.colour = colour;
        }

        /// <summary>
        ///This method is called each round, allowing the ComputerPlayer to refresh its knowledge of the gameplay state, 
        ///So it now knows the tanks have been placed in a different order.
        /// </summary>
        public override void BeginRound()
        {
            //where do I find the order, do I need to reference the current game some how
        }

        /// <summary>
        ///This method is called when it's this player's turn
        ///The player will need to call methods in gameplayForm 
        ///such as SelectWeapon(), SetAngle(), SetForce() and finally Launch() to aim and fire the weapon.
        /// </summary>
        public override void NewTurn(GameplayForm gameplayForm, Battle currentGame)
        {
            GameplayTank gameplayTank = currentGame.GetCurrentGameplayTank();

            int numPlayers = currentGame.NumPlayers();
            //for each player find the first player who still exists and aim at them. //There has to be an easier way

            Random random = new Random();
            int angle = random.Next(-90, 91);
            int power = random.Next(25, 101);

            gameplayForm.SetForce(power);
            gameplayForm.SetAngle(angle);
            gameplayForm.Launch();
        }

        /// <summary>
        ///This method is called each time a shot fired by this player hits, allowing the computer to adjust its aim
        ///Note that this method is not always called- if the shot goes off-screen it will never hit anything and this method will not be called.
        /// </summary>
        public override void ReportHit(float x, float y)
        {
            
        }
    }
}
