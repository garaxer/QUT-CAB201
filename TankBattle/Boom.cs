using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    /// <summary>
    /// The Boom class is a type of WeaponEffect that represents the payload attached to a Shell. 
    /// An Boom will inflict damage on tanks and destroy terrain within a radius.
    /// </summary>
    public class Boom : WeaponEffect
    {
        private int explosionDamage;
        private int explosionRadius;
        private int earthDestructionRadius;

        private float x;
        private float y;
        private float lifetime;//The amount of time the boom will happen

        /// <summary>
        /// Boom Constructor
        /// </summary>
        /// <param name="explosionDamage">The amount of damage to inflict</param>
        /// <param name="explosionRadius">The area to destroy</param>
        /// <param name="earthDestructionRadius">The amount of earth to destroy</param>
        public Boom(int explosionDamage, int explosionRadius, int earthDestructionRadius)
        {
        this.explosionDamage = explosionDamage;
        this.explosionRadius =explosionRadius;
        this.earthDestructionRadius = earthDestructionRadius;
        }

        /// <summary>
        /// This method detonates the Boom at the specified location.
        /// The reason this is performed in a method and not the constructor
        /// is so a Boom can be created and stored in a Shell before it is known where the Boom will actually appear.
        /// </summary>
        public void Detonate(float x, float y)
        {
            this.x = x;
            this.y = y;
            lifetime = 1.0f;
        }

        /// <summary>
        /// This method reduces the Boom's lifespan by 0.05, and if it reaches 0 (or lower)
        /// damage the player and remove the terrain and cancel the effect
        /// </summary>
        public override void Step()
        {
            lifetime -= 0.05f;
            if (lifetime <= 0)
            {
                game.DamagePlayer(x, y, explosionDamage, explosionRadius);
                game.GetMap().TerrainDestruction(x, y, explosionRadius);
                game.CancelEffect(this);
            }
        }

        /// <summary>
        /// Renders the explosion
        /// </summary>
        public override void Render(Graphics graphics, Size displaySize)
        {
            float x = (float)this.x * displaySize.Width / Battlefield.WIDTH;
            float y = (float)this.y * displaySize.Height / Battlefield.HEIGHT;
            float radius = displaySize.Width * (float)((1.0 - lifetime) * explosionRadius * 3.0 / 2.0) / Battlefield.WIDTH;

            int alpha = 0, red = 0, green = 0, blue = 0;

            if (lifetime < 1.0 / 3.0)
            {
                red = 255;
                alpha = (int)(lifetime * 3.0 * 255);
            }
            else if (lifetime < 2.0 / 3.0)
            {
                red = 255;
                alpha = 255;
                green = (int)((lifetime * 3.0 - 1.0) * 255);
            }
            else
            {
                red = 255;
                alpha = 255;
                green = 255;
                blue = (int)((lifetime * 3.0 - 2.0) * 255);
            }

            if (Math.Abs(alpha) > 255)
            {
                alpha = 255;
            }
            RectangleF rect = new RectangleF(x - radius, y - radius, radius * 2, radius * 2);
            Brush b = new SolidBrush(Color.FromArgb(Math.Abs(alpha), red, green, blue));

            graphics.FillEllipse(b, rect);
        }
    }
}
