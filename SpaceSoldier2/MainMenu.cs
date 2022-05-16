using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Reflection;

namespace SpaceSoldier2
{
    class MainMenu
    {
        int mainMenuSelection = 1;
        bool wWasDown = false;
        bool sWasDown = false;
        bool upWasDown = false;
        bool downWasDown = false;
        bool enterWasDown = false;

        public String Update(GameTime gameTime)
        {            
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                wWasDown = true;
            }
            if (wWasDown && keyboardState.IsKeyUp(Keys.W))
            {
                if (mainMenuSelection != 1)
                {
                    mainMenuSelection -= 1;
                }
                wWasDown = false;
                MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                sWasDown = true;
            }
            if (sWasDown && keyboardState.IsKeyUp(Keys.S))
            {
                if (mainMenuSelection != 4)
                {
                    mainMenuSelection += 1;
                }
                sWasDown = false;
                MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                upWasDown = true;
            }
            if (upWasDown && keyboardState.IsKeyUp(Keys.Up))
            {
                if (mainMenuSelection != 1)
                {
                    mainMenuSelection -= 1;
                }
                upWasDown = false;
                MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                downWasDown = true;
            }
            if (downWasDown && keyboardState.IsKeyUp(Keys.Down))
            {
                if (mainMenuSelection != 4)
                {
                    mainMenuSelection += 1;
                }
                downWasDown = false;
                MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
            }

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                enterWasDown = true;             
            }
            if (enterWasDown && keyboardState.IsKeyUp(Keys.Enter))
            {
                enterWasDown = false;
                if (mainMenuSelection == 1)
                {
                    MySounds.alienKilled.Play(MySounds.soundVolume, 1f, 1f);
                    return "SELECT LEVEL MENU";
                }
                else if (mainMenuSelection == 2)
                {
                    MySounds.alienKilled.Play(MySounds.soundVolume, 1f, 1f);
                    return "BEST TIMES MENU";
                }
                else if (mainMenuSelection == 3)
                {
                    MySounds.alienKilled.Play(MySounds.soundVolume, 1f, 1f);
                    return "OPTIONS MENU";
                }
                else if (mainMenuSelection == 4)
                {
                    MySounds.alienKilled.Play(MySounds.soundVolume, 1f, 1f);
                    return "EXIT";
                }
            }
            return "MAIN MENU";           
        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch, SpriteFont gameFont, Player player)
        {
            _spriteBatch.Begin();

            _spriteBatch.DrawString(gameFont, "Space Soldier", new Vector2(700, 200), Color.White, 0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(texture: player.Sprite.texture, position: new Vector2(1050, 150), sourceRectangle: player.Sprite.sourceRectangle, color: Color.White, rotation: 0.0f, origin: new Vector2(0, 0), scale: player.Sprite.scale, effects: SpriteEffects.None, layerDepth: 1);

            if (mainMenuSelection == 1)
            {
                _spriteBatch.DrawString(gameFont, "Select Level", new Vector2(800, 400), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Select Level", new Vector2(800, 400), Color.White);
            }
            if (mainMenuSelection == 2)
            {
                _spriteBatch.DrawString(gameFont, "Best Times", new Vector2(800, 500), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Best Times", new Vector2(800, 500), Color.White);
            }
            if (mainMenuSelection == 3)
            {
                _spriteBatch.DrawString(gameFont, "Options", new Vector2(800, 600), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Options", new Vector2(800, 600), Color.White);
            }
            if (mainMenuSelection == 4)
            {
                _spriteBatch.DrawString(gameFont, "Exit Game", new Vector2(800, 700), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Exit Game", new Vector2(800, 700), Color.White);
            }


            //_spriteBatch.DrawString(gameFont, "Folder: ", new Vector2(100, 100), Color.White);


            _spriteBatch.End();
        }
    }
}
