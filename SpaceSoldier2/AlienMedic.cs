using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceSoldier2
{
    public class AlienMedic : Alien
    {
        float alienVelocity = 200f;       
        List<GameObject> scene { get; set; }
        public float HealingTimer { get; set; }
        public AlienMedic(Vector2 position, Sprite sprite, List<GameObject> scene) : base(position, sprite, scene)
        {
            this.Health = 6;           
            this.scene = scene;
            this.HealingTimer = 0;
        }
        
        public override Projectile Update(GameTime gameTime, Player player)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            HealingTimer -= elapsedTime;
            LastPosition = this.Position;

            this.WasHitTimer -= elapsedTime;
            if (this.WasHitTimer <= 0 && this.Health > 2)
            {
                this.ObjectColor = Color.White;
            }

            //check if hurt alien detected
            bool foundHurtAlien = false;
            foreach (GameObject gameObject in scene)
            { 
                if (gameObject is Alien && gameObject != this)
                {
                    Alien alien = (Alien)gameObject;
                    if (Vector2.Distance(this.Position, alien.Position) < 1000 && noWallInBetween(alien)) 
                    {
                        if (alien.Health <= 3)
                        {
                            //moving towards the hurt alien
                            foundHurtAlien = true;
                            Vector2 movementDirection = alien.Position - this.Position;
                            movementDirection.Normalize();
                            this.Position += elapsedTime * alienVelocity * movementDirection;
                            break;
                        }
                    }  
                }
                       
            }           
            if (!foundHurtAlien)
            {
                //moving in a random pattern
                if (ChangeDirectionTimer > 0)
                {
                    this.Position += elapsedTime * alienVelocity * RandomMovementDirection;
                    ChangeDirectionTimer -= elapsedTime;
                }
                else
                {
                    Random rnd = new Random();
                    int x = rnd.Next(-100, 101);
                    int y = rnd.Next(-100, 101);
                    Vector2 randomVector = new Vector2(x, y);
                    randomVector.Normalize();
                    RandomMovementDirection = randomVector;
                    ChangeDirectionTimer = 5;
                }
            }

            //if crashed with player or another alien
            if (CrashTimer > 0)
            {
                this.Position += CrashDirection * alienVelocity * 0.05f * elapsedTime;
                CrashTimer -= elapsedTime;
            }

            //updating rectangles
            this.Rectangle = new Rectangle((int)this.Position.X + (int)(110 * this.Sprite.scale) - (int)(this.Sprite.origin.X * this.Sprite.scale), (int)this.Position.Y - (int)(this.Sprite.origin.Y * this.Sprite.scale), 2 * HalfWidth, 2 * HalfHeight);
            this.OuterRectangle = new Rectangle((int)this.Position.X - (int)(this.Sprite.origin.X * this.Sprite.scale), (int)this.Position.Y - (int)(this.Sprite.origin.Y * this.Sprite.scale), (int)(446 * this.Sprite.scale), (int)(560 * this.Sprite.scale));

            //animations
            if (this.Sprite is AnimatedSprite)
            {
                ((AnimatedSprite)this.Sprite).Update(gameTime, true);
            }

            return null;
        }
    }
}
