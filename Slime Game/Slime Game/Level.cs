using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime_Game
{
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
