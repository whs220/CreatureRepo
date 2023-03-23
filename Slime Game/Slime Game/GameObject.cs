//Handles setting up the parent for gameobject
//Jake Wardell
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
    internal class GameObject
    {
        // ==== Field ====
        protected Texture2D texture;
        protected Rectangle position;


        // ==== Properties ====

        /// <summary>
        /// Makes the rectangle field accessable and changeable
        /// </summary>
        public Rectangle Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }


        // ==== Constuctor ====

        /// <summary>
        /// Creates a game object
        /// </summary>
        /// <param name="texture">The texture</param>
        /// <param name="pos">Rectangle pos</param>
        public GameObject(Texture2D texture, Rectangle pos)
        {
            this.texture = texture;
            position = pos;
        }


        // ==== Method ====
        
        /// <summary>
        /// Draws gameobject
        /// </summary>
        /// <param name="sb">The sprite batch</param>
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
        
    }
}
