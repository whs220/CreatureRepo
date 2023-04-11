//This is the collectable class that handles collectable data
//Jake Wardell, Will Slyman
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Slime_Game
{
    internal class Collectable : GameObject
    {
        // ==== Fields ====
        private bool isHot;
        private bool isActive;
        private bool isExit;

        // Animation data
        private int currentFrame;
        private double fps;
        private double secondsPerFrame;
        private double timeCounter;


        // Properties

        /// <summary>
        /// Get/set for isHot
        /// </summary>
        public bool IsHot
        {
            get { return isHot; }
            set { isHot = value; }
        }

        /// <summary>
        /// Get/set for isActive
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        /// <summary>
        /// Get/set for isExit
        /// </summary>
        public bool IsExit
        {
            get { return isExit; }
            set { isExit = value; }
        }


        // ==== Constructors ====

        /// <summary>
        /// Creates a collectable
        /// </summary>
        /// <param name="texture">The texture of the collectable</param>
        /// <param name="pos">Postion rectangle</param>
        /// <param name="isHot">Whether it's hot or not</param>
        public Collectable(Texture2D texture, Rectangle pos, bool isHot, bool isExit = false) :
            base(texture, pos)
        {
            this.isHot = isHot;
            isActive = true;
            this.isExit = isExit;

            // Set up animation data:
            fps = 8.0;                      // Animation frames to cycle through per second
            secondsPerFrame = 1.0 / fps;    // How long each animation frame lasts
            timeCounter = 0;                // Time passed since animation
            currentFrame = 1;               // Sprite sheet's first animation frame is 1 (not 0)
        }

        /// <summary>
        /// only draws hot if active
        /// </summary>
        /// <param name="sb">The sprite batch</param>
        public void DrawHot(SpriteBatch sb, Color color)
        {
            if (isActive == true || isExit)
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
        /// only draws Cold if active
        /// </summary>
        /// <param name="sb">The sprite batch</param>
        public void DrawCold(SpriteBatch sb, Color color)
        {
            if (isActive == true || isExit)
            {
                sb.Draw(
                texture,                                        // Whole sprite sheet
                new Vector2(position.X, position.Y),            // Position of the Mario sprite
                new Rectangle(                                  // Which portion of the sheet is drawn:
                    ((currentFrame % 4)+4) * 32,                // - Left edge
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
    }
}
