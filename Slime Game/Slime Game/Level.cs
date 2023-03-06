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

        //Method
        public List<GameObject> ReadLevel()
        {
            return gameObjects;
        } 

    }
}
