using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceSoldier2
{
    public class AlienShooter : Alien
    {
        float alienVelocity = 100f;
        float shootingTimerMax = 1;
        float ShootingTimer { get; set; }      
        Vector2 GunPosition { get; set; }
        Sprite AlienProjectileSprite { get; set; }
        List<GameObject> scene { get; set; }
        public AlienShooter(Vector2 position, Sprite sprite, Sprite alienProjectileSprite, List<GameObject> scene) : base(position, sprite, scene)
        {
            this.Health = 6;
            this.GunPosition = this.Position + new Vector2(176, -135) * this.Sprite.scale;
            this.AlienProjectileSprite = alienProjectileSprite;
            this.ShootingTimer = shootingTimerMax;
            this.scene = scene;
        }

        public override Projectile Update(GameTime gameTime, Player player)
        {
            
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.GunPosition = this.Position + new Vector2(176, -135) * this.Sprite.scale;
            LastPosition = this.Position;
            bool isMoving = false;

            this.WasHitTimer -= elapsedTime;
            if (this.WasHitTimer <= 0 && this.Health > 2)
            {
                this.ObjectColor = Color.White;
            }

            //if player detected
            if (Vector2.Distance(this.Position, player.Position) < 1000 && noWallInBetween(player))
            {              
                Vector2 playerDirection = player.Position - this.GunPosition;
                playerDirection.Normalize();

                //moving towards or away from player
                if (this.Health < 3)
                {
                    this.Position -= elapsedTime * alienVelocity * playerDirection;
                    isMoving = true;
                }
                else
                {
                    float distanceFromPlayer = Vector2.Distance(this.Position, player.Position);
                    if (distanceFromPlayer > 500)
                    {
                        this.Position += elapsedTime * alienVelocity * playerDirection;
                        isMoving = true;
                    }                  
                }
                //shooting at the player
                if (ShootingTimer <= 0)
                {
                    Projectile projectile = new Projectile(this.GunPosition, AlienProjectileSprite, (int)(22 * AlienProjectileSprite.scale), playerDirection, true);
                    ShootingTimer = shootingTimerMax;
                    return projectile;
                }
                else
                {
                    ShootingTimer -= elapsedTime;
                }
            }
            else
            {
                //moving in a random pattern
                isMoving = true;
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
                ((AnimatedSprite)this.Sprite).Update(gameTime, isMoving);
            }
            
            return null;
        }
    }
}
