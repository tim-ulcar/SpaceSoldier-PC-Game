using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceSoldier2
{
    public class Wall : GameObject
    {
        int HalfWidth { get; set; }
        int HalfHeight { get; set; }
        public bool IsVertical { get; set; }
        public Rectangle Rectangle { get; set; }
        public Wall(Vector2 position, Sprite sprite, bool isVertical) : base(position, sprite)
        {
            this.IsVertical = isVertical;
            if (isVertical)
            {
                this.HalfWidth = (int)(64 * this.Sprite.scale);
                this.HalfHeight = (int)(1030 * this.Sprite.scale);
                this.Rectangle = new Rectangle((int)position.X, (int)position.Y, 2 * HalfWidth, 2 * HalfHeight);
            }
            else
            {
                this.HalfWidth = (int)(1030 * this.Sprite.scale);
                this.HalfHeight = (int)(64 * this.Sprite.scale);
                this.Rectangle = new Rectangle((int)position.X, (int)position.Y, 2 * HalfWidth, 2 * HalfHeight);
            }
            
        }

    }
}
