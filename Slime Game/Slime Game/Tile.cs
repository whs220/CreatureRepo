// Will Slyman
// 3/6/2023
// Class: Tile
// Purpose: To act as a platform or a deterred depending on the player's state

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
    internal class Tile : GameObject
    {
        // ===== Fields =====
        private PlayerMatterState[] collidableStates;
        private PlayerMatterState[] killStates;

        // ===== Constructors =====
        /// <summary>
        /// Creates a new tile!
        /// </summary>
        /// <param name="texture">Texture of the tile.</param>
        /// <param name="pos">Position of the tile.</param>
        /// <param name="collidableStates">Array of PlayerMatterState's that the tile will collide with.</param>
        /// <param name="killStates">Array of PlayerMatterState's that will kill the player on contact.</param>
        public Tile(Texture2D texture, Rectangle pos, PlayerMatterState[] collidableStates, PlayerMatterState[] killStates) : base(texture, pos)
        {
            this.collidableStates = collidableStates;
            this.killStates = killStates;
        }

        public Tile(Texture2D texture, Rectangle pos) : base(texture, pos)
        {
            collidableStates = new PlayerMatterState[] { PlayerMatterState.Liquid, PlayerMatterState.Gas, PlayerMatterState.Solid, PlayerMatterState.Dead };
        }
    }
}
