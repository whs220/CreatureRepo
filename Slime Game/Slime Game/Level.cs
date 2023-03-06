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

        //Methods
       
        /// <summary>
        /// Returns a list of gameobjects based on the recieved files.
        /// </summary>
        /// <returns></returns>
        public List<GameObject> ReadLevel()
        {
            return gameObjects;
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

        }

        /// <summary>
        /// Handles collectible collisions
        /// </summary>
        public void CollectibleColision()
        {

        }

        /// <summary>
        /// handles tile collisions
        /// </summary>
        public void TileCollision()
        {

        }

    }
}
