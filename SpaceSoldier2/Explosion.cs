using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceSoldier2
{
    public class Explosion : GameObject
    {
        public int Radius { get; set; }
        public float ExistsTimer { get; set; }
        public Explosion(Vector2 position, Sprite sprite) : base(position, sprite)
        {           
            this.Radius = (int)(600 * this.Sprite.scale);
            this.ExistsTimer = 0.333f;
        }

        public void Update(GameTime gameTime)
        {
            ((AnimatedSprite)this.Sprite).Update(gameTime, true);
        }

        public bool explosionOver(GameTime gameTime)
        {           
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            ExistsTimer -= elapsedTime;
            
            if (ExistsTimer <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }                   
        }
    }
}