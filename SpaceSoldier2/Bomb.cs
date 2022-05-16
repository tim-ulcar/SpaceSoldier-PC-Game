using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceSoldier2
{
    public class Bomb : GameObject
    {
        public Vector2 ProjectileDirection { get; set; }
        public int Radius { get; set; }
        public Bomb(Vector2 position, Sprite sprite, int radius, Vector2 projectileDirection) : base(position, sprite)
        {
            ProjectileDirection = projectileDirection;          
            this.Radius = radius;
        }

        public void update(GameTime gameTime)
        {

        }
    }
}
