using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Text.Json;
using System.IO;

namespace SpaceSoldier2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        SaveData saveData;
        string saveDataPath = "saveData.json";
        
        String gameState = "MAIN MENU";
        MainMenu mainMenu = new MainMenu();
        OptionsMenu optionsMenu = new OptionsMenu();
        PausedMenu pausedMenu = new PausedMenu();
        SelectLevelMenu selectLevelMenu = new SelectLevelMenu();
        BestTimesMenu bestTimesMenu = new BestTimesMenu();

        public Texture2D textureAtlas;
        public List<GameObject> scene;

        public Sprite playerSprite;
        public Sprite alienSprite;
        public Sprite backgroundSprite;
        public Sprite projectileSprite;
        public Sprite alienShooterSprite;
        public Sprite alienProjectileSprite;
        public Sprite soundBarUnitSprite;
        public Sprite heartSprite;
        public Sprite verticalWallSprite;
        public Sprite horizontalWallSprite;
        public Sprite alienMedicSprite;
        public Sprite bombSprite;
        public Sprite explosionSprite;
        public Sprite barrelSprite;

        public SpriteFont gameFont;

        public Player player;
        public GameObject background;

        MouseState mouseState;
        bool mouseReleased = true;

        public float projectileVelocity = 800f;
        public float bombVelocity = 600f;
        public int aliensAlive = 0;
        public bool bombsEquipped = true;

        float gameplayTimer = 0;
        bool escapeWasDown = false;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            saveData = Load();
            MySounds.soundVolume = saveData.SoundVolume;
            MySounds.musicVolume = saveData.MusicVolume;
          
            textureAtlas = Content.Load<Texture2D>("vsebine");
            gameFont = Content.Load<SpriteFont>("gameFont");

            MySounds.projectileShot = Content.Load<SoundEffect>("Sounds/projectileShot");
            MySounds.projectileHit = Content.Load<SoundEffect>("Sounds/projectileHit");
            MySounds.alienKilled = Content.Load<SoundEffect>("Sounds/alienKilled");
            MySounds.healing = Content.Load<SoundEffect>("Sounds/healing");
            MySounds.bombShot = Content.Load<SoundEffect>("Sounds/bombShot");
            MySounds.explosion = Content.Load<SoundEffect>("Sounds/explosion");
            MySounds.scifiMusic = Content.Load<Song>("Sounds/scifiMusic");
           
            playerSprite = new AnimatedSprite(textureAtlas, new Rectangle(474, 308, 544, 584), new Vector2(272, 292), 0.25f, 4, new int[]{ 544, 586, 543, 582 }, 615, new Vector2[] {new Vector2(272, 292), new Vector2(272, 292), new Vector2(272, 292), new Vector2(272, 292)}, 3.0f);           
            alienSprite = new AnimatedSprite(textureAtlas, new Rectangle(470, 1260, 446, 560), new Vector2(223, 280), 0.25f, 4, new int[] { 443, 468, 443, 478 }, 615, new Vector2[] { new Vector2(223, 280), new Vector2(223, 280), new Vector2(223, 280), new Vector2(223, 280) }, 3.0f);        
            alienShooterSprite = new AnimatedSprite(textureAtlas, new Rectangle(470, 2040, 476, 550), new Vector2(238, 275), 0.25f, 4, new int[] { 475, 526, 501, 515 }, 615, new Vector2[] { new Vector2(238, 275), new Vector2(238, 275), new Vector2(238, 275), new Vector2(238, 275) }, 3.0f);
            verticalWallSprite = new Sprite(textureAtlas, new Rectangle(3893, 1213, 127, 2060), new Vector2(0, 0), 0.25f);
            horizontalWallSprite = new Sprite(textureAtlas, new Rectangle(3894, 916, 2060, 127), new Vector2(0, 0), 0.25f);
            backgroundSprite = new Sprite(textureAtlas, new Rectangle(4332, 1212, 2047, 2047), new Vector2(0, 0), 1.0f);
            projectileSprite = new Sprite(textureAtlas, new Rectangle(3872, 448, 60, 60), new Vector2(30, 31), 0.25f);           
            alienProjectileSprite = new Sprite(textureAtlas, new Rectangle(4122, 460, 44, 42), new Vector2(22, 21), 0.35f);
            soundBarUnitSprite = new Sprite(textureAtlas, new Rectangle(3896, 918, 100, 100), new Vector2(0, 0), 0.25f);
            heartSprite = new Sprite(textureAtlas, new Rectangle(3512, 426, 98, 104), new Vector2(49, 52), 0.25f);
            alienMedicSprite = new AnimatedSprite(textureAtlas, new Rectangle(4716, 185, 443, 548), new Vector2(223, 280), 0.25f, 4, new int[] { 443, 468, 443, 478 }, 615, new Vector2[] { new Vector2(223, 280), new Vector2(223, 280), new Vector2(223, 280), new Vector2(223, 280) }, 3.0f);
            bombSprite = new Sprite(textureAtlas, new Rectangle(3731, 440, 82, 82), new Vector2(41, 41), 0.25f);
            explosionSprite = new AnimatedSprite(textureAtlas, new Rectangle(26, 2759, 93, 700), new Vector2(46, 350), 0.4f, 7, new int[] { 93, 171, 257, 518, 700, 700, 741 }, 700, new Vector2[] { new Vector2(46, 350), new Vector2(85, 350), new Vector2(128, 350), new Vector2(259, 350), new Vector2(350, 350), new Vector2(350, 350), new Vector2(370, 350) }, 21.0f);
            barrelSprite = new Sprite(textureAtlas, new Rectangle(4199, 311, 175, 325), new Vector2(87, 162), 0.25f);

            background = new GameObject(new Vector2(0, 0), backgroundSprite);
            optionsMenu.SoundBarUnitSprite = soundBarUnitSprite;
            
            scene = new List<GameObject>();

            player = new Player(new Vector2(960, 540), playerSprite);          
            scene.Add(player);
           
            Camera.Position = new Vector2(0, 0);

            MediaPlayer.Volume = (float)(0.1 * MySounds.musicVolume);
            MediaPlayer.Play(MySounds.scifiMusic);
          
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.RightControl))
                Exit();            

            if (gameState.Equals("MAIN MENU"))
            {
                gameState = mainMenu.Update(gameTime);
                if (gameState.Equals("EXIT")) {
                    Exit();
                } 
            }
            else if (gameState.Equals("OPTIONS MENU"))
            {
                gameState = optionsMenu.Update(gameTime);
            }
            else if (gameState.Equals("PAUSED MENU"))
            {
                gameState = pausedMenu.Update(gameTime);
            }
            else if (gameState.Equals("SELECT LEVEL MENU"))
            {
                gameState = selectLevelMenu.Update(gameTime);
            }
            else if (gameState.Equals("BEST TIMES MENU"))
            {
                gameState = bestTimesMenu.Update(gameTime);
            }
            else if (gameState.Equals("LEVEL 1"))
            {
                Level1.buildLevel(this);
                gameState = "GAMEPLAY";
            }
            else if (gameState.Equals("LEVEL 2"))
            {
                Level2.buildLevel(this);
                gameState = "GAMEPLAY";
            }
            else if (gameState.Equals("LEVEL 3"))
            {
                Level3.buildLevel(this);
                gameState = "GAMEPLAY";
            }
            else if (gameState.Equals("LEVEL 4"))
            {
                Level4.buildLevel(this);
                gameState = "GAMEPLAY";
            }
            else if (gameState.Equals("GAMEPLAY"))
            {
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                gameplayTimer += elapsedTime;

                //check if paused
                KeyboardState keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    escapeWasDown = true;
                }
                if (escapeWasDown && keyboardState.IsKeyUp(Keys.Escape))
                {
                    escapeWasDown = false;
                    gameState = "PAUSED MENU";
                }
               
                //player shooting projectiles or bombs
                mouseState = Mouse.GetState();
                if (mouseState.LeftButton == ButtonState.Pressed && mouseReleased)
                {
                    Vector2 gunPosition = player.Position + new Vector2(185, -238) * player.Sprite.scale;          
                    Vector2 mousePosition = mouseState.Position.ToVector2() + Camera.Position;
                    Vector2 projectileDirection = mousePosition - gunPosition;
                    projectileDirection.Normalize();

                    if (!bombsEquipped)
                    {
                        Projectile projectile = new Projectile(gunPosition, projectileSprite, (int)(30 * projectileSprite.scale), projectileDirection, false);
                        scene.Add(projectile);
                        MySounds.projectileShot.Play(MySounds.soundVolume, 1f, 1f);
                    }
                    else
                    {
                        Bomb bomb = new Bomb(gunPosition, bombSprite, (int)(41 * projectileSprite.scale), projectileDirection);
                        scene.Add(bomb);
                        MySounds.bombShot.Play(MySounds.soundVolume, 1f, 1f);
                    }

                    mouseReleased = false;
                }
                if (mouseState.LeftButton == ButtonState.Released)
                {
                    mouseReleased = true;
                }


                //moving the player
                player.Update(gameTime);

                //moving projectiles, bombs and aliens; making alien projectiles; updating explosions               
                List<Projectile> alienProjectiles = new List<Projectile>();               
                foreach (GameObject gameObject in scene)
                {
                    if (gameObject is Projectile)
                    {
                        gameObject.Position += elapsedTime * projectileVelocity * ((Projectile)gameObject).ProjectileDirection;                        
                    }
                    else if (gameObject is Bomb)
                    {
                        gameObject.Position += elapsedTime * bombVelocity * ((Bomb)gameObject).ProjectileDirection;
                    }
                    else if (gameObject is Explosion)
                    {
                        ((Explosion)gameObject).Update(gameTime);
                    }
                    //get alien projectiles by updating Aliens
                    else if (gameObject is Alien)
                    {
                        Projectile projectile = ((Alien)gameObject).Update(gameTime, player);
                        if (projectile != null)
                        {
                            alienProjectiles.Add(projectile);
                        }
                    }
                }               

                foreach (Projectile projectile in alienProjectiles)
                {
                    scene.Add(projectile);
                }
                alienProjectiles.Clear();


                //going through the scene           
                List<GameObject> gameObjectsToRemove = new List<GameObject>();
                List<GameObject> gameObjectsToAdd = new List<GameObject>();
                foreach (GameObject gameObject in scene)
                {
                    //removing explosions                                           
                    if (gameObject is Explosion)
                    {
                        Explosion explosion = (Explosion)gameObject;
                        if (explosion.explosionOver(gameTime))
                        {
                            gameObjectsToRemove.Add(gameObject);
                        }
                    }

                    //updating barrel color                                          
                    if (gameObject is Barrel)
                    {
                        Barrel barrel = (Barrel)gameObject;
                        barrel.Update(gameTime);
                    }

                    //checking for and resolving collisions
                    foreach (GameObject gameObject2 in scene)
                    {
                        //alien is hit by projectile
                        if (gameObject is Alien && gameObject2 is Projectile)
                        {
                            if (!((Projectile)gameObject2).ShotByAlien)
                            {
                                //if alien and projectile collision
                                if (((Alien)gameObject).Rectangle.Contains(((Projectile)gameObject2).Position))
                                {
                                    MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
                                    gameObjectsToRemove.Add(gameObject2);
                                    ((Alien)gameObject).Health--;
                                    ((Alien)gameObject).WasHitTimer = 0.1f;
                                    gameObject.ObjectColor = Color.Green;
                                    if (((Alien)gameObject).Health < 3)
                                    {
                                        gameObject.ObjectColor = Color.Red;
                                    }
                                    if (((Alien)gameObject).Health == 0)
                                    {
                                        gameObjectsToRemove.Add(gameObject);
                                        aliensAlive--;
                                        MySounds.alienKilled.Play(MySounds.soundVolume, 1f, 1f);
                                    }   
                                }
                            }
                        }

                        //alien is directly hit by bomb
                        if (gameObject is Alien && gameObject2 is Bomb)
                        {                          
                            if (((Alien)gameObject).Rectangle.Contains(((Bomb)gameObject2).Position))
                            {
                                MySounds.explosion.Play(MySounds.soundVolume, 1f, 1f);
                                Explosion explosion = new Explosion(gameObject2.Position, makeExplosionSprite());
                                gameObjectsToAdd.Add(explosion);
                                gameObjectsToRemove.Add(gameObject2);
                                ((Alien)gameObject).Health = 0;
                                ((Alien)gameObject).WasHitTimer = 0.1f;
                                gameObjectsToRemove.Add(gameObject);
                                aliensAlive--;
                                MySounds.alienKilled.Play(MySounds.soundVolume, 1f, 1f);                                                              
                            }
                        }

                        //alien in explosion radius
                        if (gameObject is Alien && gameObject2 is Explosion)
                        {
                            Alien alien = (Alien)gameObject;
                            Explosion explosion = (Explosion)gameObject2;
                            if (Vector2.Distance(alien.Position, explosion.Position) < explosion.Radius)
                            {
                                MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
                                alien.CrashTimer = 0.3f;
                                alien.CrashDirection = alien.Position - explosion.Position;
                                alien.CrashDirection.Normalize();
                                alien.Health--;
                                alien.WasHitTimer = 0.1f;
                                alien.ObjectColor = Color.Green;
                                if (alien.Health < 3)
                                {
                                    alien.ObjectColor = Color.Red;
                                }
                                if (alien.Health == 0)
                                {
                                    gameObjectsToRemove.Add(alien);
                                    aliensAlive--;
                                    MySounds.alienKilled.Play(MySounds.soundVolume, 1f, 1f);
                                }
                            }
                        }

                        //player in explosion radius
                        if (gameObject is Player && gameObject2 is Explosion)
                        {
                            Player player = (Player)gameObject;
                            Explosion explosion = (Explosion)gameObject2;
                            if (Vector2.Distance(player.Position, explosion.Position) < explosion.Radius)
                            {
                                MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
                                
                                player.CrashTimer = 0.3f;
                                player.CrashDirection = player.Position - explosion.Position;
                                player.CrashDirection.Normalize();

                                if (Vector2.Distance(player.Position, explosion.Position) < explosion.Radius - 100 * explosion.Sprite.scale)
                                {
                                    player.Health -= 1;
                                }
                            }
                        }

                        //barrel hit by projectile
                        if (gameObject is Barrel && gameObject2 is Projectile)
                        {
                            Barrel barrel = (Barrel)gameObject;
                            Projectile projectile = (Projectile)gameObject2;                          
                            if (barrel.Rectangle.Contains(projectile.Position))
                            {
                                MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
                                gameObjectsToRemove.Add(projectile);
                                barrel.Health--;
                                barrel.ObjectColor = Color.Gray;
                                barrel.WasHitTimer = 0.1f;
                                if (barrel.Health <= 0)
                                {
                                    gameObjectsToRemove.Add(barrel);
                                    Explosion explosion = new Explosion(barrel.Position, makeExplosionSprite());
                                    gameObjectsToAdd.Add(explosion);
                                }
                            }
                        }

                        //barrel hit by BOMB
                        if (gameObject is Barrel && gameObject2 is Bomb)
                        {
                            Barrel barrel = (Barrel)gameObject;
                            Bomb bomb = (Bomb)gameObject2;
                            if (barrel.Rectangle.Contains(bomb.Position))
                            {                              
                                gameObjectsToRemove.Add(bomb);
                                gameObjectsToRemove.Add(barrel);
                                Explosion explosion = new Explosion(barrel.Position, makeExplosionSprite());
                                gameObjectsToAdd.Add(explosion);
                            }
                        }

                        //barrel in explosion radius
                        if (gameObject is Barrel && gameObject2 is Explosion)
                        {
                            Barrel barrel = (Barrel)gameObject;
                            Explosion explosion = (Explosion)gameObject2;
                            if (Vector2.Distance(barrel.Position, explosion.Position) < explosion.Radius)
                            {
                                MySounds.explosion.Play(MySounds.soundVolume, 1f, 1f);
                                gameObjectsToRemove.Add(barrel);
                                Explosion explosion2 = new Explosion(barrel.Position, makeExplosionSprite());
                                gameObjectsToAdd.Add(explosion2);
                            }
                        }

                        //player and walls
                        if (gameObject is Player && gameObject2 is Wall)
                        {
                            Wall wall = (Wall)gameObject2;
                            Player player = (Player)gameObject;
                            if (wall.Rectangle.Intersects(player.OuterRectangle))
                            {
                                player.Position = player.LastPosition;
                                Camera.Position = Camera.LastPosition;
                            }
                        }

                        //player and barrel
                        if (gameObject is Player && gameObject2 is Barrel)
                        {
                            Barrel barrel = (Barrel)gameObject2;
                            Player player = (Player)gameObject;
                            if (barrel.Rectangle.Intersects(player.OuterRectangle))
                            {
                                player.Position = player.LastPosition;
                                Camera.Position = Camera.LastPosition;
                            }
                        }

                        //aliens and walls
                        if (gameObject is Alien && gameObject2 is Wall)
                        {
                            Wall wall = (Wall)gameObject2;
                            Alien alien = (Alien)gameObject;
                            if (wall.Rectangle.Intersects(alien.OuterRectangle))
                            {
                                alien.Position = alien.LastPosition;
                                //alien.ChangeDirectionTimer = 0;
                                alien.RandomMovementDirection = Vector2.Negate(alien.RandomMovementDirection);
                            }
                        }

                        //projectiles and walls
                        if (gameObject is Projectile && gameObject2 is Wall)
                        {
                            Wall wall = (Wall)gameObject2;
                            Projectile projectile = (Projectile)gameObject;
                            if (wall.Rectangle.Contains(projectile.Position))
                            {
                                gameObjectsToRemove.Add(gameObject);
                            }
                        }

                        //bombs and walls
                        if (gameObject is Bomb && gameObject2 is Wall)
                        {
                            Wall wall = (Wall)gameObject2;
                            Bomb bomb = (Bomb)gameObject;
                            if (wall.Rectangle.Contains(bomb.Position))
                            {
                                gameObjectsToRemove.Add(gameObject);
                                MySounds.explosion.Play(MySounds.soundVolume, 1f, 1f);
                                Explosion explosion = new Explosion(gameObject.Position, makeExplosionSprite());
                                gameObjectsToAdd.Add(explosion);
                            }
                        }

                        //two projectiles clashing
                        if (gameObject is Projectile && gameObject2 is Projectile && gameObject != gameObject2)
                        {
                            Projectile projectile1 = (Projectile)gameObject;
                            Projectile projectile2 = (Projectile)gameObject2;
                            if (Vector2.Distance(projectile1.Position, projectile2.Position) < (projectile1.Radius + projectile2.Radius))
                            {
                                gameObjectsToRemove.Add(gameObject);
                                gameObjectsToRemove.Add(gameObject2);
                                MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
                            }
                        }

                        //player and alien                   
                        if (gameObject is Player && gameObject2 is Alien)
                        {
                            Player player = (Player)gameObject;
                            Alien alien = (Alien)gameObject2;
                            if (player.Rectangle.Intersects(alien.Rectangle))
                            {
                                player.Health -= 2;
                                player.CrashTimer = 0.3f;
                                player.CrashDirection = player.Position - alien.Position;
                                player.CrashDirection.Normalize();
                                alien.CrashTimer = 0.3f;
                                alien.CrashDirection = alien.Position - player.Position;
                                alien.CrashDirection.Normalize();
                                MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
                            }
                        }

                        //player is hit by projectile
                        if (gameObject is Player && gameObject2 is Projectile)
                        {
                            Player player = (Player)gameObject;
                            Projectile projectile = (Projectile)gameObject2;
                            if (projectile.ShotByAlien)
                            {
                                //if player and projectile collision
                                if (player.Rectangle.Contains(projectile.Position))
                                {
                                    MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
                                    gameObjectsToRemove.Add(gameObject2);
                                    player.Health--;
                                    player.CrashTimer = 0.1f;
                                    player.CrashDirection = new Vector2(0, 0);                                  
                                }
                            }
                        }

                        //two aliens                 
                        if (gameObject is Alien && gameObject2 is Alien && gameObject != gameObject2)
                        {
                            Alien alien1 = (Alien)gameObject;
                            Alien alien2 = (Alien)gameObject2;
                            //if one alien is medic
                            if (alien1 is AlienMedic)
                            {
                                AlienMedic alienMedic = (AlienMedic)alien1;
                                //medic next to alien
                                if (alienMedic.OuterRectangle.Intersects(alien2.OuterRectangle))
                                {
                                    if (alienMedic.HealingTimer <= 0 && alien2.Health < 4)
                                    {
                                        alien2.Health = 6;
                                        alien2.ObjectColor = Color.White;
                                        MySounds.healing.Play(MySounds.soundVolume, 1f, 1f);
                                        alienMedic.HealingTimer = 5;
                                    }
                                }                                                             
                            }
                            if (alien1.Rectangle.Intersects(alien2.Rectangle))
                            {                              
                                alien1.CrashTimer = 0.1f;
                                alien1.CrashDirection = alien1.Position - alien2.Position;
                                alien1.CrashDirection.Normalize();
                                alien2.CrashTimer = 0.1f;
                                alien2.CrashDirection = alien2.Position - alien1.Position;
                                alien2.CrashDirection.Normalize();
                            }
                        }


                    }
                }

                foreach (GameObject gameObject in gameObjectsToRemove)
                {
                    scene.Remove(gameObject);
                }
                gameObjectsToRemove.Clear();

                foreach (GameObject gameObject in gameObjectsToAdd)
                {
                    scene.Add(gameObject);
                }
                gameObjectsToAdd.Clear();
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            

            if (gameState.Equals("MAIN MENU"))
            {
                GraphicsDevice.Clear(Color.Black);               
                mainMenu.Draw(gameTime, _spriteBatch, gameFont, player);
            }
            else if (gameState.Equals("OPTIONS MENU"))
            {
                GraphicsDevice.Clear(Color.Black);
                optionsMenu.Draw(gameTime, _spriteBatch, gameFont, player);
            }
            else if (gameState.Equals("PAUSED MENU"))
            {
                GraphicsDevice.Clear(Color.Black);
                pausedMenu.Draw(gameTime, _spriteBatch, gameFont, player);
            }
            else if (gameState.Equals("SELECT LEVEL MENU"))
            {
                GraphicsDevice.Clear(Color.Black);
                selectLevelMenu.Draw(gameTime, _spriteBatch, gameFont, player);
            }
            else if (gameState.Equals("BEST TIMES MENU"))
            {
                GraphicsDevice.Clear(Color.Black);
                bestTimesMenu.Draw(gameTime, _spriteBatch, gameFont, player);
            }          

            else if (gameState.Equals("GAMEPLAY"))
            {
                GraphicsDevice.Clear(Color.Black);

                _spriteBatch.Begin();

                //draw background
                _spriteBatch.Draw(texture: backgroundSprite.texture, position: new Vector2(-2046.5f, -2046.5f) - Camera.Position, sourceRectangle: backgroundSprite.sourceRectangle, color: background.ObjectColor, rotation: 0.0f, origin: new Vector2(0, 0), scale: backgroundSprite.scale, effects: SpriteEffects.None, layerDepth: 1);
                _spriteBatch.Draw(texture: backgroundSprite.texture, position: new Vector2(0, -2046.5f) - Camera.Position, sourceRectangle: backgroundSprite.sourceRectangle, color: background.ObjectColor, rotation: 0.0f, origin: new Vector2(0, 0), scale: backgroundSprite.scale, effects: SpriteEffects.None, layerDepth: 1);
                _spriteBatch.Draw(texture: backgroundSprite.texture, position: new Vector2(-2046.5f, 0) - Camera.Position, sourceRectangle: backgroundSprite.sourceRectangle, color: background.ObjectColor, rotation: 0.0f, origin: new Vector2(0, 0), scale: backgroundSprite.scale, effects: SpriteEffects.None, layerDepth: 1);
                _spriteBatch.Draw(texture: backgroundSprite.texture, position: new Vector2(0, 0) - Camera.Position, sourceRectangle: backgroundSprite.sourceRectangle, color: background.ObjectColor, rotation: 0.0f, origin: new Vector2(0, 0), scale: backgroundSprite.scale, effects: SpriteEffects.None, layerDepth: 1);


                foreach (GameObject gameObject in scene)
                {
                    //_spriteBatch.Draw(gameObject.Sprite.texture, gameObject.Position, gameObject.Sprite.sourceRectangle, Color.White);
                    _spriteBatch.Draw(texture: gameObject.Sprite.texture, position: gameObject.Position - Camera.Position - gameObject.Sprite.origin * gameObject.Sprite.scale, sourceRectangle: gameObject.Sprite.sourceRectangle, color: gameObject.ObjectColor, rotation: 0.0f, origin: new Vector2(0, 0), scale: gameObject.Sprite.scale, effects: SpriteEffects.None, layerDepth: 1);
                    //gameObject.ObjectColor = Color.White;
                }

                _spriteBatch.DrawString(gameFont, "Time: " + Math.Round(gameplayTimer, 1).ToString(), new Vector2(1700, 20), Color.White);
                _spriteBatch.DrawString(gameFont, "Level 1", new Vector2(900, 20), Color.White);
                _spriteBatch.DrawString(gameFont, "Aliens alive: " + aliensAlive, new Vector2(1400, 20), Color.White);

                for (int i = 0; i < player.Health; i++)
                {
                    _spriteBatch.Draw(texture: heartSprite.texture, new Vector2(10 + i * 30, 20), sourceRectangle: heartSprite.sourceRectangle, color: Color.White, rotation: 0.0f, origin: new Vector2(0, 0), scale: heartSprite.scale, effects: SpriteEffects.None, layerDepth: 1);

                }

                //debugging
                //Vector2 gunPosition = player.Position + new Vector2(185, -238) * player.Sprite.scale;
                /*Vector2 gunPosition = player.Position;

                Vector2 mousePosition = mouseState.Position.ToVector2() - Camera.Position - projectileSprite.origin * projectileSprite.scale;
                Vector2 projectileDirection = mousePosition - gunPosition;
                _spriteBatch.Draw(texture: projectileSprite.texture, gunPosition - Camera.Position - projectileSprite.origin * projectileSprite.scale, sourceRectangle: projectileSprite.sourceRectangle, color: Color.White, rotation: 0.0f, origin: new Vector2(0, 0), scale: projectileSprite.scale, effects: SpriteEffects.None, layerDepth: 1);
                _spriteBatch.Draw(texture: alienProjectileSprite.texture, mousePosition, sourceRectangle: alienProjectileSprite.sourceRectangle, color: Color.White, rotation: 0.0f, origin: new Vector2(0, 0), scale: alienProjectileSprite.scale, effects: SpriteEffects.None, layerDepth: 1);
                */
                
                //draw player rectangle
                //_spriteBatch.Draw(texture: backgroundSprite.texture, position: player.Rectangle.Location.ToVector2() - Camera.Position, sourceRectangle: new Rectangle(4340, 1220, player.Rectangle.Width, player.Rectangle.Height), color: player.ObjectColor, rotation: 0.0f, origin: new Vector2(0, 0), scale: 1, effects: SpriteEffects.None, layerDepth: 1);
                //draw alien rectangle
                //_spriteBatch.Draw(texture: backgroundSprite.texture, position: alien.Rectangle.Location.ToVector2() - Camera.Position, sourceRectangle: new Rectangle(4340, 1220, alien.Rectangle.Width, alien.Rectangle.Height), color: player.ObjectColor, rotation: 0.0f, origin: new Vector2(0, 0), scale: 1, effects: SpriteEffects.None, layerDepth: 1);




                _spriteBatch.End();
            }
                
            

            base.Draw(gameTime);
        }

        public AnimatedSprite makeExplosionSprite()
        {
            AnimatedSprite explosionSprite = new AnimatedSprite(textureAtlas, new Rectangle(26, 2759, 93, 700), new Vector2(46, 350), 0.4f, 7, new int[] { 93, 171, 257, 518, 700, 700, 741 }, 700, new Vector2[] { new Vector2(46, 350), new Vector2(85, 350), new Vector2(128, 350), new Vector2(259, 350), new Vector2(350, 350), new Vector2(350, 350), new Vector2(370, 350) }, 21.0f);
            return explosionSprite;
        }

        void Save(SaveData saveData)
        {
            string serializedText = JsonSerializer.Serialize<SaveData>(saveData);
            File.WriteAllText(saveDataPath, serializedText);
        }

        SaveData Load()
        {
            string saveDataString = File.ReadAllText(saveDataPath);
            return JsonSerializer.Deserialize<SaveData>(saveDataString);
        }
    }
}
