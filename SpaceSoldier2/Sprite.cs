using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceSoldier2
{
    public class Sprite
    {
        public Texture2D texture;
        public Rectangle sourceRectangle;
        public Vector2 origin;
        public float scale;

        public Sprite(Texture2D texture, Rectangle sourceRectangle, Vector2 origin, float scale)
        {
            this.texture = texture;
            this.sourceRectangle = sourceRectangle;
            this.origin = origin;
            this.scale = scale;
        }
  
    }
}
