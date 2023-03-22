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
    internal class Player: GameObject
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
        /// Get/Set for isGrounded
        /// </summary>
        public bool IsGrounded
        {
            get { return isGrounded; }
            set { isGrounded = value; }
        }

        #endregion

        //constructor
        public Player(Texture2D debugSolid, Texture2D debugLiquid, Texture2D debugGas, Rectangle pos):base(debugSolid, pos)
        {
            this.debugSolid = debugSolid;
            this.debugLiquid = debugLiquid;
            this.debugGas = debugGas;
            debugModeActive = false;

            speed = 5.0f;  
            jumpHeight = -15.0f;
            currentMatterState = PlayerMatterState.Liquid;
            currentMoveState = PlayerMovementState.IdleRight;
            velocity = Vector2.Zero;
            gravity = new Vector2(0, 0.5f);
            isGrounded = false;

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
            // record the currenky keyboard state
            currentKeyState = Keyboard.GetState();

            ProcessMovement();
            ApplyGravity();

            // record previous keyboard state
            prevKeyState = currentKeyState;
        }

        /// <summary>
        /// Draws the player
        /// </summary>
        /// <param name="sb"></param>
        public override void Draw(SpriteBatch sb)
        {
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
        
        
        

        public void ProcessMovement()
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
            // (Except dead, duh)
            if (currentMatterState != PlayerMatterState.Dead)
            {
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
                }
                position.Y += (int)velocity.Y;
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

        

        /* ==== Old Movement ====
        /// <summary>
        /// registers what key is being pressed and will move the player in the correct direction
        /// </summary>
        public void ProcessInput()
        {

            //If space or W are hit then the jump method is called
            if(currentKeyState.IsKeyDown(Keys.W) && prevKeyState.IsKeyUp(Keys.W) && prevKeyState.IsKeyUp(Keys.Space)) {
                Jump();
            }
            if(currentKeyState.IsKeyDown(Keys.Space) && prevKeyState.IsKeyUp(Keys.W) && prevKeyState.IsKeyUp(Keys.Space))
            {
                Jump();
            }

            //This logic is for all states not the solid state
            if (currentMatterState != PlayerMatterState.Solid)
            {
                if (currentKeyState.IsKeyDown(Keys.D))
                {
                    position.X += (int)(speed);
                }
                if (currentKeyState.IsKeyDown(Keys.A))
                {
                    position.X -= (int)(speed);
                }
            }
            //For solid movement which has a slide
            else
            {
                //acceleration
                float acceleration = 2;
                
                if (currentKeyState.IsKeyDown(Keys.D))
                {
                    //Sets speed to 1 so acceleration isn't multiplyed by 0. Then multiplys
                    //acceleration and speed and adds it to the previous speed
                    speed += (speed + acceleration);
                    
                }
                else if (currentKeyState.IsKeyDown(Keys.A))
                {
                    //Sets speed to -1 so acceleration isn't multiplyed by 0. Then multiplys
                    //acceleration and speed and adds it to the previous speed
                    speed -= (speed + acceleration);
                    
                }
                else
                {
                 speed *= 0.9f;
                }

                position.X += (int)speed;
            }
        }
        */

        #endregion
    }
}
