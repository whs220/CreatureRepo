using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Slime_Game
{
    /// <summary>
    /// Will, Josie
    /// To act as a platform or a deterred depending on the player's state
    /// </summary>
    internal class Tile : GameObject
    {
        // ===== Fields =====

        private PlayerMatterState[] collidableStates;
        private PlayerMatterState[] killStates;
        private Rectangle frame;


        // ===== Constructors =====

        /// <summary>
        /// Creates a new tile!
        /// </summary>
        /// <param name="texture">Texture of the tile.</param>
        /// <param name="pos">Position of the tile.</param>
        /// <param name="collidableStates">Array of PlayerMatterState's that the tile will collide with.</param>
        /// <param name="killStates">Array of PlayerMatterState's that will kill the player on contact.</param>
        public Tile(Texture2D texture, Rectangle pos, PlayerMatterState[] collidableStates, PlayerMatterState[] killStates, Rectangle frame) : base(texture, pos)
        {
            this.collidableStates = collidableStates;
            this.killStates = killStates;
            this.frame = frame;
        }

        /// <summary>
        /// Creates a new tile, with a defult killStates of none!
        /// (Won't kill the player on contact in any state)
        /// </summary>
        /// <param name="texture">Texture of the tile.</param>
        /// <param name="pos">Position of the tile.</param>
        /// <param name="collidableStates">Array of PlayerMatterState's that the tile will collide with.</param>
        public Tile(Texture2D texture, Rectangle pos, PlayerMatterState[] collidableStates, Rectangle frame) : base(texture, pos)
        {
            this.collidableStates = collidableStates;
            killStates = new PlayerMatterState[0];
            this.frame = frame;
        }

        /// <summary>
        /// Creates a default tile!
        /// (Collides with all PlayerMatterState's and won't kill the player on contact in any state)
        /// </summary>
        /// <param name="texture">Texture of the tile.</param>
        /// <param name="pos">Position of the tile.</param>
        public Tile(Texture2D texture, Rectangle pos, Rectangle frame) : base(texture, pos)
        {
            collidableStates = new PlayerMatterState[] { PlayerMatterState.Liquid, PlayerMatterState.Gas, PlayerMatterState.Solid, PlayerMatterState.Dead };
            killStates = new PlayerMatterState[0];
            this.frame = frame;
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

        /// <summary>
        /// Draws the tile!
        /// </summary>
        /// <param name="sb">Spritebatch</param>
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, frame, Color.White);
        }
    }
}
