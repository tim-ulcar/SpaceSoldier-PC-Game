using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Media;
using System.Text.Json;
using System.IO;

namespace SpaceSoldier2
{
    class OptionsMenu
    {
        public Sprite SoundBarUnitSprite { get; set; }
        string saveDataPath = "saveData.json";

        int optionsSelection = 1;
        float musicVolume = 1f;
        float soundVolume = 1f;
        bool wWasDown = false;
        bool sWasDown = false;
        bool upWasDown = false;
        bool downWasDown = false;
        bool enterWasDown = false;
        bool leftWasDown = false;
        bool rightWasDown = false;
        bool aWasDown = false;
        bool dWasDown = false;

        public String Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            //moving up and down in menu
            if (keyboardState.IsKeyDown(Keys.W))
            {
                wWasDown = true;
            }
            if (wWasDown && keyboardState.IsKeyUp(Keys.W))
            {
                if (optionsSelection != 1)
                {
                    optionsSelection -= 1;
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
                if (optionsSelection != 3)
                {
                    optionsSelection += 1;
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
                if (optionsSelection != 1)
                {
                    optionsSelection -= 1;
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
                if (optionsSelection != 3)
                {
                    optionsSelection += 1;
                }
                downWasDown = false;
                MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
            }

            //changing music and sound volume in menu
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                leftWasDown = true;
            }
            if (leftWasDown && keyboardState.IsKeyUp(Keys.Left))
            {
                leftWasDown = false;
                if (optionsSelection == 2)
                {
                    if (MySounds.soundVolume > 0.19)
                    {
                        MySounds.soundVolume -= 0.2f;
                    }
                    MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);                   
                }
                if (optionsSelection == 3)
                {
                    if (MySounds.musicVolume > 0.19)
                    {
                        MySounds.musicVolume -= 0.2f;
                        MediaPlayer.Volume = (float)(0.1 * MySounds.musicVolume);
                    }
                    MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
                }
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                aWasDown = true;
            }
            if (aWasDown && keyboardState.IsKeyUp(Keys.A))
            {
                aWasDown = false;
                if (optionsSelection == 2)
                {
                    if (MySounds.soundVolume > 0.19)
                    {
                        MySounds.soundVolume -= 0.2f;
                    }
                    MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
                }
                if (optionsSelection == 3)
                {
                    if (MySounds.musicVolume > 0.19)
                    {
                        MySounds.musicVolume -= 0.2f;
                        MediaPlayer.Volume = (float)(0.1 * MySounds.musicVolume);
                    }
                    MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
                }
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                rightWasDown = true;
            }
            if (rightWasDown && keyboardState.IsKeyUp(Keys.Right))
            {
                rightWasDown = false;
                if (optionsSelection == 2)
                {
                    if (MySounds.soundVolume < 1)
                    {
                        MySounds.soundVolume += 0.2f;
                        MediaPlayer.Volume = (float)(0.1 * MySounds.musicVolume);
                    }
                    MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
                }
                if (optionsSelection == 3)
                {
                    if (MySounds.musicVolume < 1)
                    {
                        MySounds.musicVolume += 0.2f;
                        MediaPlayer.Volume = (float)(0.1 * MySounds.musicVolume);
                    }
                    MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
                }
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                dWasDown = true;
            }
            if (dWasDown && keyboardState.IsKeyUp(Keys.D))
            {
                dWasDown = false;
                if (optionsSelection == 2)
                {
                    if (MySounds.soundVolume < 1)
                    {
                        MySounds.soundVolume += 0.2f;
                    }
                    MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
                }
                if (optionsSelection == 3)
                {
                    if (MySounds.musicVolume < 1)
                    {
                        MySounds.musicVolume += 0.2f;
                        MediaPlayer.Volume = (float)(0.1 * MySounds.musicVolume);
                    }
                    MySounds.projectileHit.Play(MySounds.soundVolume, 1f, 1f);
                }
            }

            //making a selection in menu
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                enterWasDown = true;
            }
            if (enterWasDown && keyboardState.IsKeyUp(Keys.Enter))
            {
                enterWasDown = false;
                if (optionsSelection == 1)
                {                  
                    SaveData newSaveData = new SaveData();
                    newSaveData.SoundVolume = MySounds.soundVolume;
                    newSaveData.MusicVolume = MySounds.musicVolume;
                    Save(newSaveData);

                    MySounds.alienKilled.Play(MySounds.soundVolume, 1f, 1f);
                    return "MAIN MENU";
                }               
            }
            return "OPTIONS MENU";
        }


        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch, SpriteFont gameFont, Player player)
        {
            _spriteBatch.Begin();

            _spriteBatch.DrawString(gameFont, "Options", new Vector2(700, 200), Color.White, 0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(texture: player.Sprite.texture, position: new Vector2(1050, 150), sourceRectangle: player.Sprite.sourceRectangle, color: Color.White, rotation: 0.0f, origin: new Vector2(0, 0), scale: player.Sprite.scale, effects: SpriteEffects.None, layerDepth: 1);

            if (optionsSelection == 1)
            {
                _spriteBatch.DrawString(gameFont, "Back to Main Menu", new Vector2(800, 400), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Back to Main Menu", new Vector2(800, 400), Color.White);
            }
            if (optionsSelection == 2)
            {
                _spriteBatch.DrawString(gameFont, "Sound Volume", new Vector2(800, 500), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Sound Volume", new Vector2(800, 500), Color.White);
            }
            if (optionsSelection == 3)
            {
                _spriteBatch.DrawString(gameFont, "Music Volume", new Vector2(800, 600), Color.Gray);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Music Volume", new Vector2(800, 600), Color.White);
            }

            for (float i = 0f; i <= MySounds.soundVolume; i += 0.2f)
            {                
                _spriteBatch.Draw(texture: SoundBarUnitSprite.texture, position: new Vector2(1050 + 150 * i, 508), sourceRectangle: SoundBarUnitSprite.sourceRectangle, color: Color.WhiteSmoke, rotation: 0.0f, origin: new Vector2(0, 0), scale: SoundBarUnitSprite.scale, effects: SpriteEffects.None, layerDepth: 1);
            }

            for (float i = 0f; i <= MySounds.musicVolume; i += 0.2f)
            {
                _spriteBatch.Draw(texture: SoundBarUnitSprite.texture, position: new Vector2(1050 + 150 * i, 608), sourceRectangle: SoundBarUnitSprite.sourceRectangle, color: Color.WhiteSmoke, rotation: 0.0f, origin: new Vector2(0, 0), scale: SoundBarUnitSprite.scale, effects: SpriteEffects.None, layerDepth: 1);
            }


            _spriteBatch.End();
        }


        void Save(SaveData saveData)
        {
            string serializedText = JsonSerializer.Serialize<SaveData>(saveData);
            File.WriteAllText(saveDataPath, serializedText);
        }
    }
}
