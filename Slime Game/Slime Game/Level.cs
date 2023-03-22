using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Microsoft.Xna.Framework.Content;

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
        private Texture2D fire;
        private Texture2D ice;
        private Rectangle groundFrame;
        private Rectangle ceilingFrame;
        private Rectangle leftFrame;
        private Rectangle rightFrame;
        private Game1 game1;

        // 1, 352
        // 31, 383

        //Constructor
        public Level(string fileName, Player player, Texture2D tilemap, Texture2D fire, Texture2D ice) 
        { 

            this.fileName = fileName;
            this.player = player;
            this.tilemap = tilemap;
            this.fire = fire;
            this.ice = ice;

            this.tiles = new List<Tile>();
            this.collectables = new List<Collectable>();
            this.gameObjects = new List<GameObject>();
            this.game1 = game1;
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
                input = new StreamReader("../../../" + fileName);
                string line;

                while ((line = input.ReadLine()) != null)
                {
                    string[] data = line.Split(',');

                    //tiles
                    if (data[2] == "top")
                    {
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(0, 224, 32, 32)));
                    }
                    if (data[2] == "ground")
                    {
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(0, 351, 32, 32)));
                    }
                    if (data[2] == "leftWall")
                    {
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(800, 416, 32, 32)));
                    }
                    if (data[2] == "rightWall")
                    {
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(864, 416, 32, 32)));
                    }
                    if (data[2] == "platform")
                    {
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 128, 32), new Rectangle(640, 480, 128, 32)));
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
                        Rectangle copy = player.Position;
                        copy.X = (int.Parse(data[0]) - 1) * 32;
                        copy.Y = (int.Parse(data[1]) - 1) * 32;
                        player.Position = copy;
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
        public void Draw(SpriteBatch sb)
        {
            foreach(Collectable collectable in collectables)
            {
                collectable.Draw(sb);
            }
            foreach(Tile tile in tiles)
            {
                tile.Draw(sb);
            }
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
            List<Tile> intersections = new List<Tile>();
            foreach(Tile tile in tiles) 
            {
                if (player.Position.Intersects(tile.Position))
                {
                    intersections.Add(tile);
                    Rectangle intersection = Rectangle.Intersect(player.Position, tile.Position);

                }
            }

            if(intersections.Count > 0)
            {
                Rectangle posCopy = player.Position;

                foreach (Tile tile in intersections)
                {
                    Rectangle rect = tile.Position;
                    Rectangle intersection = Rectangle.Intersect(rect, posCopy);

                    if (intersection.Height > intersection.Width)
                    {
                        if (posCopy.X - rect.X < 0)
                        {
                            posCopy.X -= intersection.Width;
                        }
                        else
                        {
                            posCopy.X += intersection.Width;
                        }
                    }

                }

                foreach (Tile tile in intersections)
                {
                    Rectangle intersection = Rectangle.Intersect(rect, playerRect);
                    if (intersection.Height < intersection.Width)
                    {
                        playerVelocity.Y = 0;
                        if (playerRect.Y - rect.Y < 0)
                        {
                            playerRect.Y -= intersection.Height;
                        }
                        else
                        {
                            playerRect.Y += intersection.Height;
                        }
                    }

                }

                //resolves intersections
                playerPosition.X = playerRect.X;
                playerPosition.Y = playerRect.Y;
            }


        }

    }
}
