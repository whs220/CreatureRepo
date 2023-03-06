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

        /// <summary>
        /// Creates a new tile, with a defult killStates of none!
        /// (Won't kill the player on contact in any state)
        /// </summary>
        /// <param name="texture">Texture of the tile.</param>
        /// <param name="pos">Position of the tile.</param>
        /// <param name="collidableStates">Array of PlayerMatterState's that the tile will collide with.</param>
        public Tile(Texture2D texture, Rectangle pos, PlayerMatterState[] collidableStates) : base(texture, pos)
        {
            this.collidableStates = collidableStates;
            killStates = new PlayerMatterState[0];
        }

        /// <summary>
        /// Creates a default tile!
        /// (Collides with all PlayerMatterState's and won't kill the player on contact in any state)
        /// </summary>
        /// <param name="texture">Texture of the tile.</param>
        /// <param name="pos">Position of the tile.</param>
        public Tile(Texture2D texture, Rectangle pos) : base(texture, pos)
        {
            collidableStates = new PlayerMatterState[] { PlayerMatterState.Liquid, PlayerMatterState.Gas, PlayerMatterState.Solid, PlayerMatterState.Dead };
            killStates = new PlayerMatterState[0];
        }

        // ===== Methods =====

        /// <summary>
        /// Returns true if the player's current matter state is inside the tile's collidableStates array!
        /// </summary>
        /// <param name="playerMatterState">The player's current matter state.</param>
        /// <returns>True/False if playerMatterState is in collidableStates.</returns>
        public bool CheckCollide(PlayerMatterState playerMatterState)
        {
            return collidableStates.Contains(playerMatterState);
        }

        /// <summary>
        /// Returns true if the player's current matter state is inside the tile's killStates array!
        /// </summary>
        /// <param name="playerMatterState">The player's current matter state.</param>
        /// <returns>True/False if playerMatterState is in killStates.</returns>
        public bool CheckKill(PlayerMatterState playerMatterState)
        {
            return killStates.Contains(playerMatterState);
        }
    }
}
