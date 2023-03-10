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
    /// <summary>
    /// Represents the player that the user controls
    /// </summary>
    internal class Player: GameObject
    {
        #region fields

        //player states
        PlayerMatterState currentMatterState;
        PlayerMovementState currentMoveState;

        //movement
        float speed;
        float jumpHeight;
        Vector2 velocity;

        //animation
        private Rectangle frame;
        // Animation data
        private int currentFrame;
        private double fps;
        private double secondsPerFrame;
        private double timeCounter;
        // Sprite sheet data
        private int numPlayerSprites;
        private int widthOfPlayerSprite;

        #endregion

        //properties



        //constructor
        public Player(Texture2D texture, Rectangle pos):base(texture, pos)
        {
            speed = 5.0f;
            jumpHeight = -15.0f;
            currentMatterState = PlayerMatterState.Solid;
            currentMoveState = PlayerMovementState.IdleRight;
            velocity = Vector2.Zero;

            // Set up animation data:
            fps = 8.0;                      // Animation frames to cycle through per second
            secondsPerFrame = 1.0 / fps;    // How long each animation frame lasts
            timeCounter = 0;                // Time passed since animation
            currentFrame = 1;         // Sprite sheet's first animation frame is 1 (not 0)
        }


        #region methods

        /// <summary>
        /// checks all player logic per frame
        /// will be called in Game1
        /// </summary>
        public void Update()
        {

        }

        /// <summary>
        /// Draws the player
        /// </summary>
        /// <param name="sb"></param>
        public override void Draw(SpriteBatch sb)
        {

        }

        /// <summary>
        /// registers what key is being pressed and will move the player in the correct direction
        /// </summary>
        public void ProcessInput()
        {

        } 

        /// <summary>
        /// called when w or space is pressed to make the player jump
        /// </summary>
        public void Jump()
        {

        }

        /// <summary>
        /// changes PlayerMatterState
        /// </summary>
        /// <param name="hotter"></param>
        public void ChangeTemperature(bool hotter)
        {

        }

        /// <summary>
        /// player dies
        /// </summary>
        public void Die()
        {

        }

        public void UpdateAnimation(GameTime gameTime)
        {
            // ElapsedGameTime is the duration of the last GAME frame
            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

            // Has enough time passed to flip to the next frame?
            if (timeCounter >= secondsPerFrame)
            {
                // Change which frame is active, ensuring the frame is reset back to the first 
                playerCurrentFrame++;
                if (playerCurrentFrame >= 7)
                {
                    playerCurrentFrame = 1;
                }

                // Reset the time counter
                timeCounter -= secondsPerFrame;
            }
        }

        private void DrawPlayerWalking(SpriteEffects flip)
        {
            // This version of draw can flip (mirror) the image horizontally or vertically,
            // depending on the method's SpriteEffects parameter.

            // Mario is animated with this method.
            // He is drawn starting at the second animation frame in the sprite sheet 
            //   and cycles through animation frames 1, 2, and 3.
            //   (i.e. the second through fourth images in the sheet)
            _spriteBatch.Draw(
                player.PlayerTexture,                                   // Whole sprite sheet
                player.Position,                                  // Position of the Mario sprite
                new Rectangle(                                  // Which portion of the sheet is drawn:
                    playerCurrentFrame * widthOfPlayerSprite + 2 * widthOfPlayerSprite,   // - Left edge
                    0,                                          // - Top of sprite sheet
                    widthOfPlayerSprite,                        // - Width 
                    yellowDinoSpriteSheet.Height),              // - Height
                Color.White,                                    // No change in color
                0.0f,                                           // No rotation
                Vector2.Zero,                                   // Start origin at (0, 0) of sprite sheet 
                1.0f,                                           // Scale
                flip,                                           // Flip it horizontally or vertically?    
                0.0f);                                          // Layer depth
        }

        private void DrawPlayerStanding(SpriteEffects flip)
        {
            // This version of draw can flip (mirror) the image horizontally or vertically,
            // depending on the method's SpriteEffects parameter.

            // Mario is animated with this method.
            // He is drawn starting at the second animation frame in the sprite sheet 
            //   and cycles through animation frames 1, 2, and 3.
            //   (i.e. the second through fourth images in the sheet)
            _spriteBatch.Draw(
                texture,                                   // Whole sprite sheet
                player.Position,                                  // Position of the Mario sprite
                new Rectangle(                                  // Which portion of the sheet is drawn:
                    (playerCurrentFrame % 3) * widthOfPlayerSprite,   // - Left edge
                    0,                                          // - Top of sprite sheet
                    widthOfPlayerSprite,                        // - Width 
                    yellowDinoSpriteSheet.Height),              // - Height
                Color.White,                                    // No change in color
                0.0f,                                           // No rotation
                Vector2.Zero,                                   // Start origin at (0, 0) of sprite sheet 
                1.0f,                                           // Scale
                flip,                                           // Flip it horizontally or vertically?    
                0.0f);                                          // Layer depth
        }

        #endregion
    }
}
