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
    /// Josie Caradonna
    /// Reads the file and makes a tile map
    /// does not wokr rn
    /// </summary>
    internal class Level
    {
        //Fields
        public string fileName;
        public List<GameObject> gameObjects;
        public List<Tile> tiles;
        public List<Collectable> collectables;
        private Player player;
        private Texture2D tilemap;
        private Rectangle groundFrame;
        private Rectangle ceilingFrame;
        private Rectangle leftFrame;
        private Rectangle rightFrame;

        // 1, 352
        // 31, 383

        
        //Constructor
        public Level(string fileName, Player player, Texture2D tilemap)
        {
            this.fileName = fileName;
            this.player = player;
            this.tilemap = tilemap;

        }


        //Methods
       
        /// <summary>
        /// Returns a list of gameobjects based on the recieved files.
        /// </summary>
        /// <returns></returns>
        public void ReadLevel()
        {
            //File IO

        }
        
        /// <summary>
        /// Draws all tiles and collectables
        /// </summary>
        public void Draw()
        {

        }

        /// <summary>
        /// Checks collectible collisions and tile collisions
        /// </summary>
        public void Update()
        {
           CollectibleColision();
           TileCollision();
        }

        /// <summary>
        /// Handles collectible collisions
        /// </summary>
        public void CollectibleColision()
        {
            // Loop all collect
            // if (player.pos.intersects(collectables[i].pos)){
            //Calls change temperature and sets collectable to inactive
        }

        /// <summary>
        /// handles tile collisions
        /// </summary>
        public void TileCollision()
        {

        }

    }
}
