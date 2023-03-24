﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        Vector2 gravity;

        //Keyboard states
        KeyboardState prevKeyState;
        KeyboardState keyboard;

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
            gravity = new Vector2(0, 0.5f);

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
            ProcessInput();
            ApplyGravity();

            prevKeyState = keyboard;
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

            keyboard = Keyboard.GetState();

            //If space or W are hit then the jump method is called
            if(keyboard.IsKeyDown(Keys.W) && prevKeyState.IsKeyUp(Keys.W) && prevKeyState.IsKeyUp(Keys.Space)) {
                Jump();
            }
            if(keyboard.IsKeyDown(Keys.Space) && prevKeyState.IsKeyUp(Keys.W) && prevKeyState.IsKeyUp(Keys.Space))
            {
                Jump();
            }

            //This logic is for all states not the solid state
            if (currentMatterState != PlayerMatterState.Solid)
            {
                if (keyboard.IsKeyDown(Keys.D))
                {
                    position.X += (int)(speed);
                }
                if (keyboard.IsKeyDown(Keys.A))
                {
                    position.X -= (int)(speed);
                }
            }
            //For solid movement which has a slide
            else
            {
                //acceleration
                float acceleration = 2;

                while (keyboard.IsKeyDown(Keys.D))
                {
                    //Sets speed to 1 so acceleration isn't multiplyed by 0. Then multiplys
                    //acceleration and speed and adds it to the previous speed
                    speed = 1;
                    speed += (speed * acceleration);
                    position.X += (int)speed;
                }
                while (keyboard.IsKeyDown(Keys.A))
                {
                    //Sets speed to -1 so acceleration isn't multiplyed by 0. Then multiplys
                    //acceleration and speed and adds it to the previous speed
                    speed = -1;
                    speed += (speed * acceleration);
                    position.X += (int)speed;
                }

                //Then while the speed isn't 0
                while (speed != 0)
                {
                    position.X += (int)speed;
                    speed -= 1;
                }
            }
        }

        /// <summary>
        /// called when w or space is pressed to make the player jump
        /// </summary>
        public void Jump()
        {
            switch (currentMatterState)
            {
                //if player is a solid they cannot jump
                case PlayerMatterState.Solid:
                    break;

                //If player is gas the jump is invered
                case PlayerMatterState.Gas:
                    position.Y = (int)-jumpHeight;
                    break;

                //If the player is liquid jump is normal
                case PlayerMatterState.Liquid:
                    position.Y = (int)jumpHeight;
                    break;
            }
        }

        /// <summary>
        /// Applys gravity to the player
        /// </summary>
        public void ApplyGravity()
        {
            //Gas has inverse jump
            if(currentMatterState != PlayerMatterState.Gas)
            {
                position.Y += (int)gravity.Y;
            }
            //For gas gravity
            else
            {
                position.Y -= (int)gravity.Y;
            }
        }

        /// <summary>
        /// Changes PlayerMatterState based on the type of collectable hit.
        /// </summary>
        /// <param name="hotter">Is a hot collectable?</param>
        public void ChangeTemperature(bool hotter)
        {
            // If we hit a hot collectable...
            if (hotter)
            {
                // Change form based on state
                switch (currentMatterState)
                {
                    // Solid to Liquid
                    case PlayerMatterState.Solid:
                        currentMatterState = PlayerMatterState.Liquid;
                        break;
                    // Liqid to Gas
                    case PlayerMatterState.Liquid:
                        currentMatterState = PlayerMatterState.Gas;
                        break;
                    // Gas to Dead
                    case PlayerMatterState.Gas:
                        currentMatterState = PlayerMatterState.Dead;
                        break;
                }
            }
            // If we hit a cold collectable...
            else
            {
                // Change form based on state
                switch (currentMatterState)
                {
                    // Solid to Dead
                    case PlayerMatterState.Solid:
                        currentMatterState = PlayerMatterState.Dead;
                        break;
                    // Liqid to Solid
                    case PlayerMatterState.Liquid:
                        currentMatterState = PlayerMatterState.Solid;
                        break;
                    // Gas to Liquid
                    case PlayerMatterState.Gas:
                        currentMatterState = PlayerMatterState.Liquid;
                        break;
                }
            }
        }

        /// <summary>
        /// player dies
        /// </summary>
        public void Die()
        {
            currentMatterState = PlayerMatterState.Dead;
        }

        /// <summary>
        /// updates the current frame of the given movement style
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateAnimation(GameTime gameTime)
        {
            // ElapsedGameTime is the duration of the last GAME frame
            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

            // Has enough time passed to flip to the next frame?
            if (timeCounter >= secondsPerFrame)
            {
                // Change which frame is active, ensuring the frame is reset back to the first 
                currentFrame++;
                if (currentFrame >= 7)
                {
                    currentFrame = 1;
                }

                // Reset the time counter
                timeCounter -= secondsPerFrame;
            }
        }

        /// <summary>
        /// draws player walking
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="flip"></param>
        private void DrawPlayerWalking(SpriteBatch sb, SpriteEffects flip)
        {
            sb.Draw(
                texture,                                   // Whole sprite sheet
                new Vector2(position.X, position.Y),                                  // Position of the sprite
                new Rectangle(                                  // Which portion of the sheet is drawn:
                    currentFrame * widthOfPlayerSprite + 2 * widthOfPlayerSprite,                             // - Left edge
                    0,                                          // - Top of sprite sheet
                    widthOfPlayerSprite,                        // - Width 
                    texture.Height),              // - Height
                Color.White,                                    // No change in color
                0.0f,                                           // No rotation
                Vector2.Zero,                                   // Start origin at (0, 0) of sprite sheet 
                1.0f,                                           // Scale
                flip,                                           // Flip it horizontally or vertically?    
                0.0f);                                          // Layer depth
        }

        /// <summary>
        /// draws player standing
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="flip"></param>
        private void DrawPlayerStanding(SpriteBatch sb, SpriteEffects flip)
        {
            sb.Draw(
                texture,                                        // Whole sprite sheet
                new Vector2(position.X, position.Y),            // Position of the Mario sprite
                new Rectangle(                                  // Which portion of the sheet is drawn:
                    (currentFrame % 3) * widthOfPlayerSprite,       // - Left edge
                    0,                                              // - Top of sprite sheet
                    widthOfPlayerSprite,                            // - Width 
                    texture.Height),                                // - Height
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
