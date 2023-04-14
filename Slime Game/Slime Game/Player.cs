// Written by Jake Wardell, Dylan Clauson, Will Slyman

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Slime_Game
{
    /// <summary>
    /// Class: Player
    /// Represents the player! Can move, jump, and change states
    /// </summary>
    internal class Player : GameObject
    {
        #region fields

        //player states
        private PlayerMatterState currentMatterState;
        private PlayerMovementState currentMoveState;
        private PlayerMatterState lastMatterState;
        private bool debugModeActive;

        //movement
        private float speed;
        private float jumpHeight;
        private Vector2 velocity;
        private bool isGrounded;
        private Vector2 gravity;
        private bool gravityOff;

        private Rectangle groundRect;

        //Keyboard states
        private KeyboardState prevKeyState;
        private KeyboardState currentKeyState;

        // Animation data
        private int currentFrame;
        private double fps;
        private double secondsPerFrame;
        private double timeCounter;

        // Debug sprites
        private Texture2D debugSolid;


        private double deathTime;
        public event ResetLevel ResetLevelEvent;

        //SOund effects
        SoundEffect sfx_Fire;
        SoundEffect sfx_Ice;
        
        #endregion

        #region properties

        /// <summary>
        /// Gets and sets the velocity vector
        /// </summary>
        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }

        /// <summary>
        /// Gets and can set whether debug mode is active
        /// </summary>
        public bool DebugModeActive
        {
            get
            {
                return debugModeActive;
            }
            set
            {
                debugModeActive = value;
            }
        }

        /// <summary>
        /// Gets the current matter state only
        /// </summary>
        public PlayerMatterState CurrentMatterState
        {
            get
            {
                return currentMatterState;
            }
        }


        /// <summary>
        /// Get/Set for isGrounded
        /// </summary>
        public bool IsGrounded
        {
            get { return isGrounded; }
            set { isGrounded = value; }
        }

        public Rectangle GroundRect
        {
            get { return groundRect; }
        }


        /// <summary>
        /// Get/Set for speed
        /// </summary>
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }


        /// <summary>
        /// Gets and sets the turning gravity on
        /// </summary>
        public bool GravityOff
        {
            get { return gravityOff; }
            set { gravityOff = value; }
        }
        #endregion

        //constructor
        public Player() : base(null, new Rectangle(50, 50, 24, 16))
        {
            this.debugSolid = Art.Instance.LoadTexture2D("debug_solid");

            this.texture = Art.Instance.LoadTexture2D("slime");
            debugModeActive = false;

            speed = 5.0f;
            jumpHeight = -13.0f;
            currentMatterState = PlayerMatterState.Liquid;
            currentMoveState = PlayerMovementState.IdleRight;
            velocity = Vector2.Zero;
            gravity = new Vector2(0, 0.5f);
            isGrounded = false;
            gravityOff = false;

            deathTime = 1;

            groundRect = new Rectangle(14, 34, 24, 12);

            // Set up animation data:
            fps = 8.0;                      // Animation frames to cycle through per second
            secondsPerFrame = 1.0 / fps;    // How long each animation frame lasts
            timeCounter = 0;                // Time passed since animation
            currentFrame = 1;               // Sprite sheet's first animation frame is 1 (not 0)

            //Sound Effect
            sfx_Fire = Art.Instance.LoadSoundEffect("sfx_fire");
            sfx_Ice = Art.Instance.LoadSoundEffect("sfx_ice");
            
        }


        #region methods

        /// <summary>
        /// checks all player logic per frame
        /// will be called in Game1
        /// </summary>
        public void Update(GameTime gameTime)
        {
            // record the currenky keyboard state
            currentKeyState = Keyboard.GetState();

            // Move the player!
            ProcessMovement(gameTime);
            UpdateAnimation(gameTime);

            // Only apply gravity if the player is not dead
            if (currentMatterState != PlayerMatterState.Dead || GravityOff == false)
            {
                ApplyGravity();
            }

            //Checks single key R is pressed then resets the level
            if (Keyboard.GetState().IsKeyDown(Keys.R) && prevKeyState.IsKeyUp(Keys.R))
            {
                ResetStage();
            }

            // record previous keyboard state
            prevKeyState = currentKeyState;
        }

        /// <summary>
        /// Draws the player
        /// </summary>
        /// <param name="sb"></param>
        public override void Draw(SpriteBatch sb)
        {
            // Draw some hitboxes while debug mode is on
            if (debugModeActive)
            {
                // Draws the jump box if debug mode is active
                sb.Draw(debugSolid, groundRect, Color.White);
                // Also draw the intersectRect
                sb.Draw(debugSolid, GetCollisionHelperRect(), Color.Green);
            }
            

            // draw MatterState
            // Currently draws debug textures
            switch (currentMatterState)
            {
                case PlayerMatterState.Gas:
                    // draw MovementState
                    switch (currentMoveState)
                    {
                        case PlayerMovementState.IdleLeft:
                        case PlayerMovementState.MoveLeft:
                            if (isGrounded)
                            {
                                DrawPlayer(sb, SpriteEffects.FlipHorizontally, 4, 7);
                            }
                            else
                            {
                                DrawPlayer(sb, SpriteEffects.FlipHorizontally, 4, 6);
                            }
                            break;
                        case PlayerMovementState.IdleRight:
                        case PlayerMovementState.MoveRight:
                            if (isGrounded)
                            {
                                DrawPlayer(sb, SpriteEffects.None, 4, 7);
                            }
                            else
                            {
                                DrawPlayer(sb, SpriteEffects.None, 4, 6);
                            }
                            break;
                    }
                    break;
                //============================================================================
                case PlayerMatterState.Liquid:
                    // draw MovementState
                    switch (currentMoveState)
                    {
                        case PlayerMovementState.IdleLeft:
                        case PlayerMovementState.MoveLeft:
                            if (isGrounded)
                            {
                                DrawPlayer(sb, SpriteEffects.FlipHorizontally, 4, 1);
                            }
                            else
                            {
                                DrawPlayer(sb, SpriteEffects.FlipHorizontally, 2, 2);
                            }
                            break;
                        case PlayerMovementState.IdleRight:
                        case PlayerMovementState.MoveRight:
                            if (isGrounded)
                            {
                                DrawPlayer(sb, SpriteEffects.None, 4, 1);
                            }
                            else
                            {
                                DrawPlayer(sb, SpriteEffects.None, 4, 2);
                            }
                            break;
                    }
                    break;
                //============================================================================
                case PlayerMatterState.Solid:
                    // draw MovementState
                    switch (currentMoveState)
                    {
                        case PlayerMovementState.IdleLeft:
                        case PlayerMovementState.MoveLeft:
                            DrawPlayer(sb, SpriteEffects.FlipHorizontally, 1, 4);
                            break;
                        case PlayerMovementState.IdleRight:
                        case PlayerMovementState.MoveRight:
                            DrawPlayer(sb, SpriteEffects.None, 1, 4);
                            break;
                    }
                    break;
                //============================================================================
                case PlayerMatterState.Dead:
                    // draw dead
                    switch (lastMatterState)
                    {
                        case PlayerMatterState.Gas:
                            DrawPlayer(sb, SpriteEffects.None, 3, 8);
                            break;
                        case PlayerMatterState.Liquid:
                            DrawPlayer(sb, SpriteEffects.None, 4, 3);
                            break;
                        case PlayerMatterState.Solid:
                            DrawPlayer(sb, SpriteEffects.None, 1, 5);
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Handles movement and player transitions.
        /// </summary>
        /// <param name="gameTime">World time</param>
        public void ProcessMovement(GameTime gameTime)
        {
            //Checks to make sure player isn't dead
            if (currentMatterState != PlayerMatterState.Dead)
            {
                switch (currentMoveState)
                {
                    case PlayerMovementState.MoveLeft:
                        // ===== LOGIC =====
                        // If you are not a solid...
                        if (currentMatterState != PlayerMatterState.Solid)
                        {
                            // Move at a constant speed
                            position.X -= (int)(speed);
                        }
                        // If you are a solid (ice)
                        else
                        {
                            // Accelerate at a negative dir
                            if (speed > -15)
                            {
                                speed -= 1;
                            }
                            position.X += (int)speed;
                        }

                        // ===== TRANSITIONS =====
                        if (currentKeyState.IsKeyDown(Keys.D))
                        {
                            currentMoveState = PlayerMovementState.MoveRight;
                        }
                        else if (currentKeyState.IsKeyUp(Keys.A))
                        {
                            currentMoveState = PlayerMovementState.IdleLeft;
                        }
                        break;
                    case PlayerMovementState.MoveRight:
                        // ===== LOGIC =====
                        // If you are not a solid...
                        if (currentMatterState != PlayerMatterState.Solid)
                        {
                            // Move at a constant speed
                            position.X += (int)(speed);
                        }
                        // If you are a solid (ice)
                        else
                        {
                            // Accelerate at a negative dir
                            if (speed < 15)
                            {
                                speed += 1;
                            }
                            position.X += (int)speed;
                        }

                        // ===== TRANSITIONS =====
                        if (currentKeyState.IsKeyDown(Keys.A))
                        {
                            currentMoveState = PlayerMovementState.MoveLeft;
                        }
                        else if (currentKeyState.IsKeyUp(Keys.D))
                        {
                            currentMoveState = PlayerMovementState.IdleRight;
                        }
                        break;
                    case PlayerMovementState.IdleLeft:
                    case PlayerMovementState.IdleRight:
                        // ===== LOGIC =====

                        if (currentMatterState == PlayerMatterState.Solid)
                        {
                            speed = (speed * 0.95f);
                            position.X += (int)(speed);
                        }

                        // ===== TRANSITIONS =====
                        if (currentKeyState.IsKeyDown(Keys.A))
                        {
                            currentMoveState = PlayerMovementState.MoveLeft;
                        }
                        else if (currentKeyState.IsKeyDown(Keys.D))
                        {
                            currentMoveState = PlayerMovementState.MoveRight;
                        }
                        break;
                }

                // Possible to jump from any state
                if (debugModeActive == false)
                {
                    // Jump if space or W is pressed
                    if (currentKeyState.IsKeyDown(Keys.W) && prevKeyState.IsKeyUp(Keys.W) && prevKeyState.IsKeyUp(Keys.Space))
                    {
                        Jump();
                    }
                    else if (currentKeyState.IsKeyDown(Keys.Space) && prevKeyState.IsKeyUp(Keys.W) && prevKeyState.IsKeyUp(Keys.Space))
                    {
                        Jump();
                    }
                }
                //For in debugMode
                else
                {
                    if (currentKeyState.IsKeyDown(Keys.W))
                    {
                        Jump();
                    }
                    if (currentKeyState.IsKeyDown(Keys.S))
                    {
                        position.Y += 5;
                    }

                }
            }
            // If player is dead...
            else
            {
                // Wait 2 seconds
                if (deathTime > 0)
                {
                    // Decrease deathTime by elapsed total seconds
                    deathTime -= gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    // Reset the Stage
                    ResetStage();
                }
            }
            
        }

        /// <summary>
        /// called when w or space is pressed to make the player jump
        /// </summary>
        public void Jump()
        {
            //If statement for debug mode sees if active
            if (GravityOff == false)
            {
                // Can only jump if grounded
                if (isGrounded)
                {
                    switch (currentMatterState)
                    {
                        //if player is a solid they cannot jump
                        case PlayerMatterState.Solid:
                            break;

                        //If player is gas the jump is invered
                        case PlayerMatterState.Gas:
                            velocity.Y = (int)-jumpHeight;
                            break;

                        //If the player is liquid jump is normal
                        case PlayerMatterState.Liquid:
                            velocity.Y = (int)jumpHeight;
                            break;
                    }
                }
            }
            else
            {
                // Allow player to fly
                position.Y -= 5;
            }
        }

        /// <summary>
        /// Applys gravity to the player
        /// </summary>
        public void ApplyGravity()
        {
            //When debug isn't active
            if (GravityOff == false)
            {
                //Gas has inverse jump
                if (currentMatterState != PlayerMatterState.Gas)
                {
                    velocity += gravity;

                    // Cap y velocity to avoid clipping through floor
                    if (velocity.Y > 15)
                    {
                        velocity.Y = 15;
                    }
                }
                else
                {
                    velocity -= gravity;

                    // Cap player y velocity if gas
                    if (velocity.Y < -5)
                    {
                        velocity.Y = -5;
                    }
                }

                position.Y += (int)velocity.Y;
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
            // Don't do anything if we died
            if (currentMatterState == PlayerMatterState.Dead) { return; }

            // If we hit a hot collectable...
            if (hotter)
            {
                sfx_Fire.Play();
                // Change form based on state
                switch (currentMatterState)
                {
                    // Solid to Liquid
                    case PlayerMatterState.Solid:
                        currentMatterState = PlayerMatterState.Liquid;
                        speed = 5f;
                        break;
                    // Liqid to Gas
                    case PlayerMatterState.Liquid:
                        currentMatterState = PlayerMatterState.Gas;
                        break;
                    // Gas to Dead
                    case PlayerMatterState.Gas:
                        //Makes it so player can not die in debug mode
                        if (debugModeActive == false)
                        {
                            Die();
                        }
                        break;
                }
            }
            // If we hit a cold collectable...
            else
            {
                sfx_Ice.Play();

                // Change form based on state
                switch (currentMatterState)
                {
                    // Solid to Dead
                    case PlayerMatterState.Solid:
                        //Makes it so player can not die in debug mode
                        if (debugModeActive == false)
                        {
                            Die();
                        }
                        break;
                    // Liqid to Solid
                    case PlayerMatterState.Liquid:
                        //Changes player speed to 0 so no extra sliding
                        speed = 0;
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
        /// Player dies
        /// </summary>
        public void Die()
        {
            lastMatterState = currentMatterState;
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

                // Reset the time counter
                timeCounter -= secondsPerFrame;
            }
        }

        /// <summary>
        /// Updates the groundRect so it is adjusted by position
        /// </summary>
        /// <param name="pos">Position to adjust by</param>
        public void UpdateGroundRect(Vector2 pos)
        {
            // Append groundRect to player's position

            // Change y position based on player matter state
            if (currentMatterState == PlayerMatterState.Gas)
            {
                // Above top of player for gas
                groundRect.Y = -24 + (int)pos.Y;
            }
            else
            {
                // Below player for any other state
                groundRect.Y = 16 + (int)pos.Y;
            }
            // Stay with player's x position
            groundRect.X = (int)pos.X;
        }

        /// <summary>
        /// draws player standing
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="flip"></param>
        private void DrawPlayer(SpriteBatch sb, SpriteEffects flip, int frameCycle, int sheetLine)
        {
            int yDifference = -16;
            if (currentMatterState == PlayerMatterState.Gas) { yDifference = 0; }

            sb.Draw(
                texture,                                        // Whole sprite sheet
                new Vector2(position.X - 4, position.Y + yDifference),            // Position of the Mario sprite
                new Rectangle(                                  // Which portion of the sheet is drawn:
                    (currentFrame % frameCycle) * 32,           // - Left edge
                    32*(sheetLine - 1),                           // - Top of sprite frame
                    32,                                         // - Width 
                    32),                                        // - Height
                Color.White,                                    // No change in color
                0.0f,                                           // No rotation
                Vector2.Zero,                                   // Start origin at (0, 0) of sprite sheet 
                1.0f,                                           // Scale
                flip,                                           // Flip it horizontally or vertically?    
                0.0f);                                          // Layer depth
        }

        /// <summary>
        /// Returns a rectangle ready to be used to determine collision.
        /// </summary>
        /// <returns>A rectangle to be used by level for determining collision directions.</returns>
        public Rectangle GetCollisionHelperRect()
        {
            // Determine where the bottom of the rectangle should be
            
            // The bottom should be where the player meets the tile
            // Thus, flip the bottom and top based on y velocity
            // (Notice how it flips when you jump in debug mode)

            // Default is bottom
            int yDifference = -16;
            // If we are going up, put the box on top
            if (Math.Round(velocity.Y) < 0) { yDifference = 0; }

            // It's a little wonky with gas, but it hasn't broken anything, so it's all ok

            // Return this helper rectangle!
            return new Rectangle(position.X - 4, position.Y + yDifference, 32, 32);
        }

        /// <summary>
        /// Sends a signal to reset the level, and resets the player back to liquid
        /// </summary>
        public void ResetStage()
        {
            // Call reset level event (calls level.ReadLevel again)
            if (ResetLevelEvent != null)
            {
                ResetLevelEvent();
            }
            // Reset death timer (waits 1 sec before calling this method)
            deathTime = 1;

            // Reset player stats back to liquid
            Reset();
        }

        /// <summary>
        /// Resets the player to their default states
        /// </summary>
        public void Reset()
        {
            // Reset player stats back to liquid
            currentMatterState = PlayerMatterState.Liquid;
            gravityOff = false;
            speed = 5;
            velocity.Y = 0;
            velocity.X = 0;
        }
    } 
}

#endregion