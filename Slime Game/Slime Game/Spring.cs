using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime_Game
{
    /// <summary>
    /// Will
    /// Gives player a bounce on collision 
    /// </summary>
    internal class Spring : GameObject
    {
        // ===== Fields =====
        // Animation data
        private int currentFrame;
        private double fps;
        private double secondsPerFrame;
        private double timeCounter;
        private bool flip;

        public bool Flip
        {
            set { flip = value; }
        }

        public Spring(Rectangle rect) : base(null, new Rectangle())
        {
            this.position = rect;
            this.texture = Art.Instance.LoadTexture2D("spring");

            // Set up animation data:
            currentFrame = 0;
            fps = 12.0;                      // Animation frames to cycle through per second
            secondsPerFrame = 1.0 / fps;    // How long each animation frame lasts
            timeCounter = 0;                // Time passed since animation
            currentFrame = 0;
        }

        /// <summary>
        /// Draws the spring!
        /// </summary>
        /// <param name="sb">Spritebatch</param>
        /// <param name="color">Color of spring</param>
        public void DrawBounce(SpriteBatch sb, Color color)
        {
            if (flip)
            {
                sb.Draw(
               texture,                                        // Whole sprite sheet
               new Vector2(position.X, position.Y),            // Position of the Mario sprite
               new Rectangle(                                  // Which portion of the sheet is drawn:
                   (currentFrame % 4) * 32,                    // - Left edge
                   0,                                          // - Top of sprite frame
                   32,                                         // - Width 
                   32),                                        // - Height
               color,                                          // No change in color
               0.0f,                                           // No rotation
               Vector2.Zero,                                   // Start origin at (0, 0) of sprite sheet 
               1.0f,                                           // Scale
               SpriteEffects.FlipVertically,                   // Flip it horizontally or vertically?    
               0.0f);                                          // Layer depth
            }
            else
            {
                sb.Draw(
               texture,                                        // Whole sprite sheet
               new Vector2(position.X, position.Y),            // Position of the Mario sprite
               new Rectangle(                                  // Which portion of the sheet is drawn:
                   (currentFrame % 4) * 32,                    // - Left edge
                   0,                                          // - Top of sprite frame
                   32,                                         // - Width 
                   32),                                        // - Height
               color,                                          // No change in color
               0.0f,                                           // No rotation
               Vector2.Zero,                                   // Start origin at (0, 0) of sprite sheet 
               1.0f,                                           // Scale
               SpriteEffects.None,                             // Flip it horizontally or vertically?    
               0.0f);                                          // Layer depth
            }
           
        }

        /// <summary>
        /// updates the current frame of the given movement style
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateAnimation(GameTime gameTime)
        {
            if (currentFrame != 0)
            {
                // ElapsedGameTime is the duration of the last GAME frame
                timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

                // Has enough time passed to flip to the next frame?
                if (timeCounter >= secondsPerFrame)
                {
                    // Change which frame is active, ensuring the frame is reset back to the first 
                    currentFrame++;

                    // Reset the time counter
                    timeCounter -= secondsPerFrame;
                }

                if (currentFrame == 5) { currentFrame = 0; }
            }
        }

        public void StartAnimation()
        {
            currentFrame = 1;
        }
    }
}
