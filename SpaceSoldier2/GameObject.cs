using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceSoldier2
{
    public class GameObject
    {
        public Vector2 Position { get; set; }
        public Sprite Sprite { get; set; }
        public Color ObjectColor {get; set;}

        public GameObject(Vector2 position, Sprite sprite)
        {
            this.Position = position;
            this.Sprite = sprite;
            this.ObjectColor = Color.White;
        }

    }
}
