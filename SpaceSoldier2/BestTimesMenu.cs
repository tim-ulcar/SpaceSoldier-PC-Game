using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SpaceSoldier2
{
    class BestTimesMenu
    {
        int bestTimesMenuSelection = 1;
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
                if (bestTimesMenuSelection != 1)
                {
                    bestTimesMenuSelection -= 1;
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
                if (bestTimesMenuSelection != 5)
                {
                    bestTimesMenuSelection += 1;
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
                if (bestTimesMenuSelection != 1)
                {
                    bestTimesMenuSelection -= 1;
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
                if (bestTimesMenuSelection != 5)
                {
                    bestTimesMenuSelection += 1;
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
                if (bestTimesMenuSelection == 5)
                {
                    MySounds.alienKilled.Play(MySounds.soundVolume, 1f, 1f);
                    return "MAIN MENU";
                }
            }
            return "BEST TIMES MENU";
        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch, SpriteFont gameFont, Player player)
        {
            _spriteBatch.Begin();

            _spriteBatch.DrawString(gameFont, "Best Times Menu", new Vector2(700, 200), Color.White, 0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(texture: player.Sprite.texture, position: new Vector2(1150, 150), sourceRectangle: player.Sprite.sourceRectangle, color: Color.White, rotation: 0.0f, origin: new Vector2(0, 0), scale: player.Sprite.scale, effects: SpriteEffects.None, layerDepth: 1);

            if (bestTimesMenuSelection == 1)
            {
                _spriteBatch.DrawString(gameFont, "Level 1: " + "31.2" + " s", new Vector2(800, 400), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Level 1: " + "31.2" + " s", new Vector2(800, 400), Color.White);
            }
            if (bestTimesMenuSelection == 2)
            {
                _spriteBatch.DrawString(gameFont, "Level 2: " + "131.4" + " s", new Vector2(800, 500), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Level 2: " + "131.4" + " s", new Vector2(800, 500), Color.White);
            }
            if (bestTimesMenuSelection == 3)
            {
                _spriteBatch.DrawString(gameFont, "Level 3: " + "213.6" + " s", new Vector2(800, 600), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Level 3: " + "213.6" + " s", new Vector2(800, 600), Color.White);
            }
            if (bestTimesMenuSelection == 4)
            {
                _spriteBatch.DrawString(gameFont, "Level 4: " + "135.8" + " s", new Vector2(800, 700), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Level 4: " + "135.8" + " s", new Vector2(800, 700), Color.White);
            }
            if (bestTimesMenuSelection == 5)
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
