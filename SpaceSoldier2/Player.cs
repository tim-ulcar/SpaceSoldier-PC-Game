using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceSoldier2
{
    public class Player : GameObject
    {
        float playerVelocity = 300f;
        public int Health { get; set; }
        public int HalfWidth { get; set; }
        public int HalfHeight { get; set; }
        public Rectangle Rectangle { get; set; }
        public Rectangle OuterRectangle { get; set; }

        public Vector2 LastPosition { get; set; }
        public Vector2 CrashDirection { get; set; }
        public float CrashTimer { get; set; }

        public Player(Vector2 position, Sprite sprite) : base(position, sprite)
        {
            this.Health = 6;
            this.HalfWidth = (int)(113 * this.Sprite.scale);
            this.HalfHeight = (int)(212 * this.Sprite.scale);           
            this.Rectangle = new Rectangle((int)position.X + (int)(108 * this.Sprite.scale) - (int)(this.Sprite.origin.X * this.Sprite.scale), (int)position.Y - (int)(this.Sprite.origin.Y * this.Sprite.scale), 2 * HalfWidth, 2 * HalfHeight);
            this.OuterRectangle = new Rectangle((int)position.X - (int)(this.Sprite.origin.X * this.Sprite.scale), (int)position.Y - (int)(this.Sprite.origin.Y * this.Sprite.scale), (int)(544 * this.Sprite.scale), (int)(584 * this.Sprite.scale));
            this.CrashTimer = 0;
        }

        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyboardState = Keyboard.GetState();

            //moving the player
            LastPosition = this.Position;
            Camera.LastPosition = Camera.Position;
            if (keyboardState.IsKeyDown(Keys.W))
            {
                this.Position += new Vector2(0, -playerVelocity * elapsedTime);
                Camera.Position += new Vector2(0, -playerVelocity * elapsedTime);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                this.Position += new Vector2(0, playerVelocity * elapsedTime);
                Camera.Position += new Vector2(0, playerVelocity * elapsedTime);
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                this.Position += new Vector2(playerVelocity * elapsedTime, 0);
                Camera.Position += new Vector2(playerVelocity * elapsedTime, 0);
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                this.Position += new Vector2(-playerVelocity * elapsedTime, 0);
                Camera.Position += new Vector2(-playerVelocity * elapsedTime, 0);               
            }

            //if crashed with alien
            if (CrashTimer > 0)
            {
                this.Position += CrashDirection * playerVelocity * 0.05f * elapsedTime;
                Camera.Position += CrashDirection * playerVelocity * 0.05f * elapsedTime;
                CrashTimer -= elapsedTime;
                this.ObjectColor = Color.Red;
            }
            else
            {
                this.ObjectColor = Color.White;
            }

            this.Rectangle = new Rectangle((int)this.Position.X + (int)(108 * this.Sprite.scale) - (int)(this.Sprite.origin.X * this.Sprite.scale), (int)this.Position.Y - (int)(this.Sprite.origin.Y * this.Sprite.scale), 2 * HalfWidth, 2 * HalfHeight);
            this.OuterRectangle = new Rectangle((int)this.Position.X - (int)(this.Sprite.origin.X * this.Sprite.scale), (int)this.Position.Y - (int)(this.Sprite.origin.Y * this.Sprite.scale), (int)(544 * this.Sprite.scale), (int)(584 * this.Sprite.scale));


            //animations
            if (this.Sprite is AnimatedSprite)
            {
                if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.A))
                {
                    ((AnimatedSprite)this.Sprite).Update(gameTime, true);
                }
                else
                {
                    ((AnimatedSprite)this.Sprite).Update(gameTime, false);
                }
                
            }
               
        }
    }
}
