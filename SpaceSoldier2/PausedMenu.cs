using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SpaceSoldier2
{
    class PausedMenu
    {
        int pausedMenuSelection = 1;
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
                if (pausedMenuSelection != 1)
                {
                    pausedMenuSelection -= 1;
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
                if (pausedMenuSelection != 2)
                {
                    pausedMenuSelection += 1;
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
                if (pausedMenuSelection != 1)
                {
                    pausedMenuSelection -= 1;
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
                if (pausedMenuSelection != 2)
                {
                    pausedMenuSelection += 1;
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
                if (pausedMenuSelection == 1)
                {
                    MySounds.alienKilled.Play(MySounds.soundVolume, 1f, 1f);
                    return "GAMEPLAY";
                }
                else if (pausedMenuSelection == 2)
                {
                    MySounds.alienKilled.Play(MySounds.soundVolume, 1f, 1f);
                    return "MAIN MENU";
                }
            }
            return "PAUSED MENU";
        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch, SpriteFont gameFont, Player player)
        {
            _spriteBatch.Begin();

            _spriteBatch.DrawString(gameFont, "Paused Menu", new Vector2(700, 200), Color.White, 0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(texture: player.Sprite.texture, position: new Vector2(1050, 150), sourceRectangle: player.Sprite.sourceRectangle, color: Color.White, rotation: 0.0f, origin: new Vector2(0, 0), scale: player.Sprite.scale, effects: SpriteEffects.None, layerDepth: 1);

            if (pausedMenuSelection == 1)
            {
                _spriteBatch.DrawString(gameFont, "Continue", new Vector2(800, 400), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Continue", new Vector2(800, 400), Color.White);
            }
            if (pausedMenuSelection == 2)
            {
                _spriteBatch.DrawString(gameFont, "Back to Main Menu", new Vector2(800, 500), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Back to Main Menu", new Vector2(800, 500), Color.White);
            }           

            _spriteBatch.End();
        }
    }
}
