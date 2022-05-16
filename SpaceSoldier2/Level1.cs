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
    public static class Level1
    {
        public static void buildLevel(Game1 game)
        {
            List<GameObject> scene = game.scene;
            game.background.ObjectColor = Color.CornflowerBlue;
            game.player.Position = new Vector2(0, 1550);
            Camera.Position = new Vector2(-960, 966);
         
            Wall verticalWall;
            Wall horizontalWall;
            //building outer walls
            int x = -2046;
            int y = -2046;
            for (int i = y; i < 2046; i += 515)
            {
                verticalWall = new Wall(new Vector2(x, i), game.verticalWallSprite, true);
                scene.Add(verticalWall);
            }
            x = 2046;
            y = -2046;
            for (int i = y; i < 2046; i += 515)
            {
                verticalWall = new Wall(new Vector2(x, i), game.verticalWallSprite, true);
                scene.Add(verticalWall);
            }
            x = -2046;
            y = -2046;
            for (int i = x; i < 2046; i += 515)
            {
                horizontalWall = new Wall(new Vector2(i, y), game.horizontalWallSprite, false);
                scene.Add(horizontalWall);
            }
            x = -2046;
            y = 2046;
            for (int i = x; i < 2046; i += 515)
            {
                horizontalWall = new Wall(new Vector2(i, y), game.horizontalWallSprite, false);
                scene.Add(horizontalWall);
            }

            //building inner vertical walls
            verticalWall = new Wall(new Vector2(-1200, -2046), game.verticalWallSprite, true);
            scene.Add(verticalWall);
            verticalWall = new Wall(new Vector2(-400, -2046), game.verticalWallSprite, true);
            scene.Add(verticalWall);
            verticalWall = new Wall(new Vector2(400, -2046), game.verticalWallSprite, true);
            scene.Add(verticalWall);
            verticalWall = new Wall(new Vector2(1200, -2046), game.verticalWallSprite, true);
            scene.Add(verticalWall);
            verticalWall = new Wall(new Vector2(-1200, 1532), game.verticalWallSprite, true);
            scene.Add(verticalWall);
            verticalWall = new Wall(new Vector2(-400, 1532), game.verticalWallSprite, true);
            scene.Add(verticalWall);
            verticalWall = new Wall(new Vector2(400, 1532), game.verticalWallSprite, true);
            scene.Add(verticalWall);
            verticalWall = new Wall(new Vector2(1200, 1532), game.verticalWallSprite, true);
            scene.Add(verticalWall);

            verticalWall = new Wall(new Vector2(-1200, -1532), game.verticalWallSprite, true);
            scene.Add(verticalWall);
            verticalWall = new Wall(new Vector2(-400, -1532), game.verticalWallSprite, true);
            scene.Add(verticalWall);
            verticalWall = new Wall(new Vector2(400, -1532), game.verticalWallSprite, true);
            scene.Add(verticalWall);
            verticalWall = new Wall(new Vector2(1200, -1532), game.verticalWallSprite, true);
            scene.Add(verticalWall);
            verticalWall = new Wall(new Vector2(-1200, 1018), game.verticalWallSprite, true);
            scene.Add(verticalWall);
            verticalWall = new Wall(new Vector2(-400, 1018), game.verticalWallSprite, true);
            scene.Add(verticalWall);
            verticalWall = new Wall(new Vector2(400, 1018), game.verticalWallSprite, true);
            scene.Add(verticalWall);
            verticalWall = new Wall(new Vector2(1200, 1018), game.verticalWallSprite, true);
            scene.Add(verticalWall);

            //building inner horizontal walls
            horizontalWall = new Wall(new Vector2(-2046, -500), game.horizontalWallSprite, false);
            scene.Add(horizontalWall);
            horizontalWall = new Wall(new Vector2(-772, -500), game.horizontalWallSprite, false);
            scene.Add(horizontalWall);
            horizontalWall = new Wall(new Vector2(-257, -500), game.horizontalWallSprite, false);
            scene.Add(horizontalWall);
            horizontalWall = new Wall(new Vector2(257, -500), game.horizontalWallSprite, false);
            scene.Add(horizontalWall);
            horizontalWall = new Wall(new Vector2(1200, -500), game.horizontalWallSprite, false);
            scene.Add(horizontalWall);
            horizontalWall = new Wall(new Vector2(-2046, 500), game.horizontalWallSprite, false);
            scene.Add(horizontalWall);
            horizontalWall = new Wall(new Vector2(-772, 500), game.horizontalWallSprite, false);
            scene.Add(horizontalWall);
            horizontalWall = new Wall(new Vector2(-257, 500), game.horizontalWallSprite, false);
            scene.Add(horizontalWall);
            horizontalWall = new Wall(new Vector2(257, 500), game.horizontalWallSprite, false);
            scene.Add(horizontalWall);
            horizontalWall = new Wall(new Vector2(1200, 500), game.horizontalWallSprite, false);
            scene.Add(horizontalWall);

            //creating aliens
            Alien alien;
            AnimatedSprite alienSprite;
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(-1000, 1000), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game); 
            alien = new Alien(new Vector2(-600, 1000), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game); 
            alien = new Alien(new Vector2(0, 1000), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game); 
            alien = new Alien(new Vector2(600, 1000), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game); 
            alien = new Alien(new Vector2(1000, 1000), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game); 
            alien = new Alien(new Vector2(-1600, 1700), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game); 
            alien = new Alien(new Vector2(-1000, 1700), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(1000, 1700), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(1600, 1700), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(-1600, -300), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(-400, -300), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(400, -300), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(1600, -300), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(-1600, 300), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(-400, 300), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(400, 300), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(1600, 300), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(-1600, -1800), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(-1000, -1800), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(0, -1800), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(1000, -1800), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(1600, -1800), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(-600, -800), alienSprite, game.scene);
            scene.Add(alien);
            alienSprite = makeAlienSprite(game);
            alien = new Alien(new Vector2(600, -800), alienSprite, game.scene);
            scene.Add(alien);

            game.aliensAlive = 24;

            //barrels
            Barrel barrel;
            barrel = new Barrel(new Vector2(0, 1200), game.barrelSprite);
            scene.Add(barrel);
            barrel = new Barrel(new Vector2(-100, 1200), game.barrelSprite);
            scene.Add(barrel);
            barrel = new Barrel(new Vector2(-200, 1200), game.barrelSprite);
            scene.Add(barrel);
            barrel = new Barrel(new Vector2(-300, 1200), game.barrelSprite);
            scene.Add(barrel);
            barrel = new Barrel(new Vector2(0, 700), game.barrelSprite);
            scene.Add(barrel);
            barrel = new Barrel(new Vector2(-100, 700), game.barrelSprite);
            scene.Add(barrel);
            barrel = new Barrel(new Vector2(-200, 700), game.barrelSprite);
            scene.Add(barrel);
            barrel = new Barrel(new Vector2(-300, 700), game.barrelSprite);
            scene.Add(barrel);


            /*
            AlienShooter alienShooter;
            AnimatedSprite alienShooterSprite = new AnimatedSprite(game.textureAtlas, new Rectangle(470, 2040, 476, 550), new Vector2(238, 275), 0.25f, 4, new int[] { 475, 526, 501, 515 }, 615, 3.0f);
            alienShooter = new AlienShooter(new Vector2(150, 1700), alienShooterSprite, game.alienProjectileSprite, scene);
            scene.Add(alienShooter);
            */

            AlienMedic alienMedic;
            AnimatedSprite alienMedicSprite = makeAlienMedicSprite(game);
            alienMedic = new AlienMedic(new Vector2(0, 1700), alienMedicSprite, scene);
            scene.Add(alienMedic);

        }

        public static AnimatedSprite makeAlienSprite(Game1 game)
        {          
            AnimatedSprite alienSprite = new AnimatedSprite(game.textureAtlas, new Rectangle(470, 1260, 446, 560), new Vector2(223, 280), 0.25f, 4, new int[] { 443, 468, 443, 478 }, 615, new Vector2[] { new Vector2(223, 280), new Vector2(223, 280), new Vector2(223, 280), new Vector2(223, 280) }, 3.0f);
            return alienSprite;
        }

        public static AnimatedSprite makeAlienMedicSprite(Game1 game)
        {           
            AnimatedSprite alienMedicSprite = new AnimatedSprite(game.textureAtlas, new Rectangle(4716, 185, 443, 548), new Vector2(223, 280), 0.25f, 4, new int[] { 443, 468, 443, 478 }, 615, new Vector2[] { new Vector2(223, 280), new Vector2(223, 280), new Vector2(223, 280), new Vector2(223, 280) }, 3.0f);
            return alienMedicSprite;
        }
    }
}
