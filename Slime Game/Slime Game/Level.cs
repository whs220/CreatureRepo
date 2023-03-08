using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        string fileName;
        List<GameObject> gameObjects;
        List<Tile> tiles;
        List<Collectable> collectables;
        Player player;

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
