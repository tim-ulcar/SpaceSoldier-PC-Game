using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceSoldier2
{
    public class Projectile : GameObject
    {
        public Vector2 ProjectileDirection { get; set; }
        public int Radius { get; set; }
        public bool ShotByAlien { get; set; }       
        public Projectile(Vector2 position, Sprite sprite, int radius, Vector2 projectileDirection, bool shotByAlien) : base(position, sprite)
        {
            ProjectileDirection = projectileDirection;
            this.ShotByAlien = shotByAlien;
            this.Radius = radius;
        }

        public void update(GameTime gameTime)
        {

        }
    }
}
