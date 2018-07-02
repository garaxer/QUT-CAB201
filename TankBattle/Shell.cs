using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    public class Shell : WeaponEffect
    {
        private float x; // the x coordinate of the shell
        private float y;
        private float gravity;
        private Boom explosion;
        private GenericPlayer player;

        private float xVelocity;
        private float yVelocity;

        /// <summary>
        /// Constructs a new Shell.
        /// </summary>
        /// <param name="x">the x coordinate of the tank</param>
        /// <param name="y">y position of the tank</param>
        /// <param name="angle">angle shell is leaving</param>
        /// <param name="power">power of the shell</param>
        /// <param name="gravity">The gravity the shell experiences</param>
        /// <param name="explosion">the explosion to produce</param>
        /// <param name="player">the player who fired the shell</param>
        public Shell(float x, float y, float angle, float power, float gravity, Boom explosion, GenericPlayer player)
        {
            this.x = x;
            this.y = y;
            this.gravity = gravity;
            this.explosion = explosion;
            this.player =  player;

            float angleRadians = (90 - angle) * (float)Math.PI / 180;
            float magnitude = power / 50;
            
            //use the angle to determine which way to send the shell
            xVelocity = (float)Math.Cos(angleRadians) * magnitude;
            yVelocity = (float)Math.Sin(angleRadians) * -magnitude;
        }

        /// <summary>
        /// This method draws the Shell as a small white circle.
        /// </summary>
        public override void Step()
        {
            for(int i = 0; i < 10; i++)
            {
                x += xVelocity;
                y += yVelocity;
                x += game.WindSpeed() / 1000.0f;

                //if we go out of bounds, cancel the whole thing
                if ( x > Battlefield.WIDTH || x <0 || y > Battlefield.HEIGHT)
                {
                    game.CancelEffect(this);
                }
                // if we detect a collision hit the tank
                else if(game.CheckCollidedTank(x,y))
                {
                    player.ReportHit(x, y); //used for the computer player
                    explosion.Detonate(x, y);
                    game.AddWeaponEffect(explosion);
                    game.CancelEffect(this);
                    return;
                }
                yVelocity += gravity;
            }
        }

        /// <summary>
        /// Renders the shell onto the screen
        /// </summary>
        public override void Render(Graphics graphics, Size size)
        {
            float x = (float)this.x * size.Width / Battlefield.WIDTH;
            float y = (float)this.y * size.Height / Battlefield.HEIGHT;
            float s = size.Width / Battlefield.WIDTH;

            RectangleF r = new RectangleF(x - s / 2.0f, y - s / 2.0f, s, s);
            Brush b = new SolidBrush(Color.WhiteSmoke);

            graphics.FillEllipse(b, r);
        }
    }
}
