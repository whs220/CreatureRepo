//Jake Wardell, Dylan Clauson, Will Slyman - This makes it so player is functioning

using System;
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
    /// Class: Player
    /// Represents the player! Can move, jump, and change states
    /// </summary>
    internal class Player : GameObject
    {
        #region fields

        //player states
        private PlayerMatterState currentMatterState;
        private PlayerMovementState currentMoveState;
        private bool debugModeActive;

        //movement
        private float speed;
        private float jumpHeight;
        private Vector2 velocity;
        private float prevSpeed;
        private bool isGrounded;
        private Vector2 gravity;

        private Rectangle groundRect;

        //Keyboard states
        private KeyboardState prevKeyState;
        private KeyboardState currentKeyState;

        // Animation
        private Rectangle frame;
        // Animation data
        private int currentFrame;
        private double fps;
        private double secondsPerFrame;
        private double timeCounter;
        // Sprite sheet data
        private int numPlayerSprites;
        private int widthOfPlayerSprite;

        // Debug sprites
        private Texture2D debugSolid;
        private Texture2D debugLiquid;
        private Texture2D debugGas;


        private double deathTime;
        public event ResetLevel ResetLevelEvent;
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
        #endregion

        //constructor
        public Player(Texture2D debugSolid, Texture2D debugLiquid, Texture2D debugGas, Rectangle pos) : base(debugSolid, pos)
        {
            this.debugSolid = debugSolid;
            this.debugLiquid = debugLiquid;
            this.debugGas = debugGas;
            debugModeActive = false;

            speed = 5.0f;
            jumpHeight = -13.0f;
            currentMatterState = PlayerMatterState.Liquid;
            currentMoveState = PlayerMovementState.IdleRight;
            velocity = Vector2.Zero;
            gravity = new Vector2(0, 0.5f);
            isGrounded = false;

            deathTime = 1;

            groundRect = new Rectangle(14, 34, 24, 24);

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
        public void Update(GameTime gameTime)
        {
            // record the currenky keyboard state
            currentKeyState = Keyboard.GetState();

            ProcessMovement(gameTime);
            ApplyGravity();

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
            // Draws the jump box if debug mode is active
            if (debugModeActive)
            {
                sb.Draw(debugSolid, groundRect, Color.White);
            }

            // draw MatterState
            // Currently draws debug textures
            switch (currentMatterState)
            {
                case PlayerMatterState.Gas:
                    sb.Draw(debugGas, position, Color.White);
                    break;

                case PlayerMatterState.Liquid:
                    sb.Draw(debugLiquid, position, Color.White);
                    break;

                case PlayerMatterState.Solid:
                    sb.Draw(debugSolid, position, Color.White);
                    break;

                case PlayerMatterState.Dead:
                    break;
            }

            // draw MovementState
            switch (currentMoveState)
            {
                case PlayerMovementState.IdleLeft:
                    break;

                case PlayerMovementState.IdleRight:
                    break;

                case PlayerMovementState.MoveLeft:
                    break;

                case PlayerMovementState.MoveRight:
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
                // Wait 2 sseconds
                if (deathTime > 0)
                {
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
            if (debugModeActive == false)
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
                position.Y -= 5;
            }
        }

        /// <summary>
        /// Applys gravity to the player
        /// </summary>
        public void ApplyGravity()
        {
            //When debug isn't active
            if (debugModeActive == false)
            {
                //Gas has inverse jump
                if (currentMatterState != PlayerMatterState.Gas)
                {
                    velocity += gravity;
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
            // If we hit a hot collectable...
            if (hotter)
            {
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
        /// Updates the groundRect so it is adjusted by position
        /// </summary>
        /// <param name="pos">Position to adjust by</param>
        public void UpdateGroundRect(Vector2 pos)
        {
            // Append groundRect to player's bottom
            if (currentMatterState == PlayerMatterState.Gas)
            {
                groundRect.Y = -6 + (int)pos.Y;
            }
            else
            {
                groundRect.Y = 34 + (int)pos.Y;
            }
            groundRect.X = (int)pos.X + 6;
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
            currentMatterState = PlayerMatterState.Liquid;
            speed = 5;
            velocity.Y = 0;
        }

        /// <summary>
        /// Resets the player to their default states
        /// </summary>
        public void Reset()
        {
            // Reset player stats back to liquid
            currentMatterState = PlayerMatterState.Liquid;
            speed = 5;
            velocity.Y = 0;
            velocity.X = 0;
        }
    } 
}

#endregion