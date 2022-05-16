using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceSoldier2
{
    public class Barrel : GameObject
    {
        public int Health { get; set; }
        public int HalfWidth { get; set; }
        public int HalfHeight { get; set; }
        public Rectangle Rectangle { get; set; }
        public float WasHitTimer { get; set; }

        public Barrel(Vector2 position, Sprite sprite) : base(position, sprite)
        {
            this.Health = 3;
            this.HalfWidth = (int)(87 * this.Sprite.scale);
            this.HalfHeight = (int)(149 * this.Sprite.scale);
            this.Rectangle = new Rectangle((int)position.X - (int)(this.Sprite.origin.X * this.Sprite.scale), (int)position.Y + (int)(27 * this.Sprite.scale) - (int)(this.Sprite.origin.Y * this.Sprite.scale), 2 * HalfWidth, 2 * HalfHeight);
            this.WasHitTimer = 0;
        }

        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;          

            this.WasHitTimer -= elapsedTime;
            if (this.WasHitTimer <= 0)
            {
                this.ObjectColor = Color.White;
            }
        }
    }
}
