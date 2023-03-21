using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

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
            this.tiles = new List<Tile>();
            this.collectables = new List<Collectable>();
            this.gameObjects = new List<GameObject>();
        }


        //Methods
       
        /// <summary>
        /// Returns a list of gameobjects based on the recieved files.
        /// </summary>
        /// <returns></returns>
        public void ReadLevel()
        {
            StreamReader input;
            bool done = false;

            try
            {
                input = new StreamReader(fileName);

                while (!done)
                {
                    try
                    {
                        string line = input.ReadLine();
                        string[] data = line.Split(',');

                        //tiles
                        if (data[2] == "top")
                        {
                            tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[0]) - 1) * 32, (int.Parse(data[1]) - 1) * 32, 32, 32), new Rectangle(0, 224, 32, 32)));
                        }
                        if (data[2] == "ground")
                        {
                            tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[0]) - 1) * 32, (int.Parse(data[1]) - 1) * 32, 32, 32), new Rectangle(0, 383, 32, 32)));
                        }
                        if (data[2] == "leftWall")
                        {
                            tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[0]) - 1) * 32, (int.Parse(data[1]) - 1) * 32, 32, 32), new Rectangle(800, 416, 32, 32)));
                        }
                        if (data[2] == "rightWall")
                        {
                            tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[0]) - 1) * 32, (int.Parse(data[1]) - 1) * 32, 32, 32), new Rectangle(864, 416, 32, 32)));
                        }
                        if (data[2] == "platform")
                        {
                            tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[0]) - 1) * 32, (int.Parse(data[1]) - 1) * 32, 128, 32), new Rectangle(640, 480, 128, 32)));
                        }

                        //collectable
                        if(data[2] == "hot")
                        {
                            collectables.Add(new Collectable(tilemap, new Rectangle((int.Parse(data[0]) - 1) * 32, (int.Parse(data[1]) - 1) * 32, 32, 32), true));
                        }
                        if (data[2] == "cold")
                        {
                            collectables.Add(new Collectable(tilemap, new Rectangle((int.Parse(data[0]) - 1) * 32, (int.Parse(data[1]) - 1) * 32, 32, 32), true));
                        }

                        //player
                        if (data[2] == "player")
                        {

                        }



                    }
                    catch
                    {
                        done = true;
                        break;
                    }
                }
            }
            catch
            {
                
            }

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
