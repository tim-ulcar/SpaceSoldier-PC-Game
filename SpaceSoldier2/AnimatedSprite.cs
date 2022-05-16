using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceSoldier2
{
    public class AnimatedSprite : Sprite
    {
        int currentFrame;
        int totalFrames;
        int[] frameWidths;
        int frameHeight;
        float fps;
        float secondsSinceLastFrameUpdate;
        Vector2 framesStartPosition;
        Vector2[] originsArray;

        public AnimatedSprite(Texture2D texture, Rectangle sourceRectangle, Vector2 origin, float scale, int totalFrames, int[] frameWidths, int frameHeight, Vector2[] originsArray, float fps) : base(texture, sourceRectangle, origin, scale)
        {
            //current frame goes from 0 to (totalFrames - 1)
            this.currentFrame = 0;
            this.totalFrames = totalFrames;
            this.frameWidths = frameWidths;
            this.frameHeight = frameHeight;
            this.fps = fps;
            this.secondsSinceLastFrameUpdate = 0f;
            this.framesStartPosition = new Vector2(sourceRectangle.X, sourceRectangle.Y);
            this.originsArray = originsArray;
        }

        public void Update(GameTime gameTime, bool isMoving)
        {          
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            secondsSinceLastFrameUpdate += elapsedTime;
            if (secondsSinceLastFrameUpdate >= 1/fps)
            {
                if (!isMoving)
                {
                    currentFrame = 0;
                    secondsSinceLastFrameUpdate = 0;
                    sourceRectangle = new Rectangle((int)framesStartPosition.X, (int)framesStartPosition.Y, frameWidths[0], frameHeight);
                    origin = originsArray[currentFrame];
                }
                else
                {
                    if (currentFrame + 1 < totalFrames)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        currentFrame = 0;
                    }
                    secondsSinceLastFrameUpdate = 0;

                    int extraWidth = 0;
                    for (int i = 0; i < currentFrame; i++)
                    {
                        extraWidth += frameWidths[i];
                    }
                    sourceRectangle = new Rectangle((int)framesStartPosition.X + extraWidth, (int)framesStartPosition.Y, frameWidths[currentFrame], frameHeight);
                    origin = originsArray[currentFrame];
                }                              
            }
        }
    }
}
