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
        /// Checks if player has run into another GameObject
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool CheckCollision(GameObject other)
        {
            return false;
        }

        /// <summary>
        /// player dies
        /// </summary>
        public void Die()
        {

        }

        #endregion
    }
}
