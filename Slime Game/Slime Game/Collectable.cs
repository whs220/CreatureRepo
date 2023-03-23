//This is the collectable class that handles collectable data
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

        // Properties

        /// <summary>
        /// Get/set for isHot
        /// </summary>
        public bool IsHot
        {
            get { return isHot; }
            set { isHot = false; }
        }

        /// <summary>
        /// Get/set for isActive
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = false; }
        }

        /// <summary>
        /// Get/set for isExit
        /// </summary>
        public bool IsExit
        {
            get { return isExit; }
            set { isExit = false; }
        }


        // ==== Constructors ====

        /// <summary>
        /// Creates a collectable
        /// </summary>
        /// <param name="texture">The texture of the collectable</param>
        /// <param name="pos">Postion rectangle</param>
        /// <param name="isHot">Whether it's hot or not</param>
        public Collectable(Texture2D texture, Rectangle pos, bool isHot) :
            base(texture, pos)
        {
            this.isHot = isHot;
            isActive = true;
        }

        /// <summary>
        /// only draws is active
        /// </summary>
        /// <param name="sb">The sprite batch</param>
        public override void Draw(SpriteBatch sb)
        {
            if (isActive == true)
            {
                sb.Draw(texture, position, Color.White); ;
            }
        }
    }
}
