using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceSoldier2
{
    public class Alien : GameObject
    {
        public int Health { get; set; }
        public int HalfWidth { get; set; }
        public int HalfHeight { get; set; }
        public Rectangle Rectangle { get; set; }
        public Rectangle OuterRectangle { get; set; }
        public Vector2 LastPosition { get; set; }
        float alienVelocity = 100f;
        public Vector2 CrashDirection { get; set; }
        public float CrashTimer { get; set; }
        public float ChangeDirectionTimer { get; set; }
        public float WasHitTimer { get; set; }
        public Vector2 RandomMovementDirection { get; set; }
        List<GameObject> scene { get; set; }
        public Alien(Vector2 position, Sprite sprite, List<GameObject> scene) : base(position, sprite)
        {
            this.Health = 6;
            this.HalfWidth = (int)(115 * this.Sprite.scale);
            this.HalfHeight = (int)(209 * this.Sprite.scale);
            this.Rectangle = new Rectangle((int)position.X + (int)(110 * this.Sprite.scale) - (int)(this.Sprite.origin.X * this.Sprite.scale), (int)position.Y - (int)(this.Sprite.origin.Y * this.Sprite.scale), 2 * HalfWidth, 2 * HalfHeight);
            this.OuterRectangle = new Rectangle((int)position.X - (int)(this.Sprite.origin.X * this.Sprite.scale), (int)position.Y - (int)(this.Sprite.origin.Y * this.Sprite.scale), (int)(446 * this.Sprite.scale), (int)(560 * this.Sprite.scale));
            this.CrashTimer = 0;
            this.ChangeDirectionTimer = 0;
            this.WasHitTimer = 0;
            this.scene = scene;
        }

        public virtual Projectile Update(GameTime gameTime, Player player)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            LastPosition = this.Position;
            
            this.WasHitTimer -= elapsedTime;
            if (this.WasHitTimer <= 0 && this.Health > 2)
            {
                this.ObjectColor = Color.White;
            }

            //if player detected
            if (Vector2.Distance(this.Position, player.Position) < 1000 && noWallInBetween(player))
            {
                //moving towards the player              
                Vector2 playerDirection = player.Position - this.Position;
                playerDirection.Normalize();
                if (this.Health < 3)
                {
                    this.Position -= elapsedTime * alienVelocity * playerDirection;
                }
                else
                {
                    this.Position += elapsedTime * alienVelocity * playerDirection;
                }               
            }
            else
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

        public bool noWallInBetween(GameObject target)
        {
            foreach (GameObject gameObject in scene)
            {
                if (gameObject is Wall)
                {
                    Wall wall = (Wall)gameObject;
                    if (LineIntersectsRect(this.Position, target.Position, wall.Rectangle))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool LineIntersectsRect(Vector2 p1, Vector2 p2, Rectangle r)
        {
            return LineIntersectsLine(p1, p2, new Vector2(r.X, r.Y), new Vector2(r.X + r.Width, r.Y)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X + r.Width, r.Y), new Vector2(r.X + r.Width, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X + r.Width, r.Y + r.Height), new Vector2(r.X, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X, r.Y + r.Height), new Vector2(r.X, r.Y)) ||
                   (r.Contains(p1) && r.Contains(p2));
        }

        private static bool LineIntersectsLine(Vector2 l1p1, Vector2 l1p2, Vector2 l2p1, Vector2 l2p2)
        {
            float q = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float d = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);

            if (d == 0)
            {
                return false;
            }

            float r = q / d;

            q = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float s = q / d;

            if (r < 0 || r > 1 || s < 0 || s > 1)
            {
                return false;
            }

            return true;
        }

        public bool projectileInsideRectangle(Vector2 projectilePosition)
        {
            if ((projectilePosition.X > this.Rectangle.X) && (projectilePosition.X < this.Rectangle.X + 2*HalfWidth) && (projectilePosition.Y > this.Rectangle.Y) && (projectilePosition.Y < this.Rectangle.Y + 2*HalfHeight))
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
