using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SpaceSoldier2
{
    class SelectLevelMenu
    {
        int selectLevelMenuSelection = 1;
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
                if (selectLevelMenuSelection != 1)
                {
                    selectLevelMenuSelection -= 1;
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
                if (selectLevelMenuSelection != 5)
                {
                    selectLevelMenuSelection += 1;
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
                if (selectLevelMenuSelection != 1)
                {
                    selectLevelMenuSelection -= 1;
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
                if (selectLevelMenuSelection != 5)
                {
                    selectLevelMenuSelection += 1;
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
                if (selectLevelMenuSelection == 1)
                {
                    MySounds.alienKilled.Play(MySounds.soundVolume, 1f, 1f);
                    return "LEVEL 1";
                }
                else if (selectLevelMenuSelection == 2)
                {
                    MySounds.alienKilled.Play(MySounds.soundVolume, 1f, 1f);
                    return "LEVEL 2";
                }
                else if (selectLevelMenuSelection == 3)
                {
                    MySounds.alienKilled.Play(MySounds.soundVolume, 1f, 1f);
                    return "LEVEL 3";
                }
                else if (selectLevelMenuSelection == 4)
                {
                    MySounds.alienKilled.Play(MySounds.soundVolume, 1f, 1f);
                    return "LEVEL 4";
                }                
                else if (selectLevelMenuSelection == 5)
                {
                    MySounds.alienKilled.Play(MySounds.soundVolume, 1f, 1f);
                    return "MAIN MENU";
                }
            }
            return "SELECT LEVEL MENU";
        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch, SpriteFont gameFont, Player player)
        {
            _spriteBatch.Begin();

            _spriteBatch.DrawString(gameFont, "Select Level Menu", new Vector2(700, 200), Color.White, 0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(texture: player.Sprite.texture, position: new Vector2(1150, 150), sourceRectangle: player.Sprite.sourceRectangle, color: Color.White, rotation: 0.0f, origin: new Vector2(0, 0), scale: player.Sprite.scale, effects: SpriteEffects.None, layerDepth: 1);

            if (selectLevelMenuSelection == 1)
            {
                _spriteBatch.DrawString(gameFont, "Level 1", new Vector2(800, 400), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Level 1", new Vector2(800, 400), Color.White);
            }
            if (selectLevelMenuSelection == 2)
            {
                _spriteBatch.DrawString(gameFont, "Level 2", new Vector2(800, 500), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Level 2", new Vector2(800, 500), Color.White);
            }
            if (selectLevelMenuSelection == 3)
            {
                _spriteBatch.DrawString(gameFont, "Level 3", new Vector2(800, 600), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Level 3", new Vector2(800, 600), Color.White);
            }
            if (selectLevelMenuSelection == 4)
            {
                _spriteBatch.DrawString(gameFont, "Level 4", new Vector2(800, 700), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Level 4", new Vector2(800, 700), Color.White);
            }
            if (selectLevelMenuSelection == 5)
            {
                _spriteBatch.DrawString(gameFont, "Back to Main menu", new Vector2(800, 800), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Back to Main menu", new Vector2(800, 800), Color.White);
            }

            _spriteBatch.End();
        }
    }
}
