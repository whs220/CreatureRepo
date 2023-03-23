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
    /// works
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
        private Tile backTile;
        private KeyboardState prevKeyState;
        private bool collisionsOn;

        //For debyg mode
        private bool debugModeActive;

        // Properties

        /// <summary>
        /// Gets the debugmodeactive bool
        /// </summary>
        public bool DebugModeActive
        {
            get { return debugModeActive; }
        }

        // 1, 352
        // 31, 383

        //Constructor
        public Level(string fileName, Player player, Texture2D tilemap, Texture2D fire, Texture2D ice) 
        {
            collisionsOn = true;
            debugModeActive = false;
            this.fileName = fileName;
            this.player = player;
            this.tilemap = tilemap;
            this.fire = fire;
            this.ice = ice;

            // Add reset level event
            player.ResetLevelEvent += ReadLevel;

            this.tiles = new List<Tile>();
            this.collectables = new List<Collectable>();
            this.gameObjects = new List<GameObject>();
            backTile = new Tile(tilemap, new Rectangle(0, 0, 32, 32), new Rectangle(480, 480, 32, 32));
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
            tiles.Clear();
            collectables.Clear();
            gameObjects.Clear();

            try
            {
                input = new StreamReader("../../../" + fileName);
                string line;

                tiles.Add(new Tile(tilemap, new Rectangle(0, 0, 1024, 32), new Rectangle(0, 224, 32, 32)));
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
                    if (data[2] == "hot")
                    {
                        collectables.Add(new Collectable(fire, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), true));
                    }
                    if (data[2] == "cold")
                    {
                        collectables.Add(new Collectable(ice, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), false));
                    }

                    //player
                    if (data[2] == "player")
                    {
                        Rectangle copy = player.Position;
                        copy.X = (int.Parse(data[1])) * 32;
                        copy.Y = (int.Parse(data[0])) * 32;
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
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    backTile.Position = new Rectangle(i * 32, j * 32, 32, 32);
                    backTile.Draw(sb);
                }
            }
            foreach (Collectable collectable in collectables)
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
            
            if (collisionsOn) 
            {
                TileCollision();
            }

            //Checks if the F1 key is clicked and swtiches if it on or off
            if(Keyboard.GetState().IsKeyDown(Keys.F1) && prevKeyState.IsKeyUp(Keys.F1))
            {
                debugModeActive = !debugModeActive;
                player.DebugModeActive = debugModeActive;
            }

            if(Keyboard.GetState().IsKeyDown(Keys.F2) && prevKeyState.IsKeyUp(Keys.F2) && debugModeActive)
            {
                collisionsOn = !collisionsOn;
            }

            //Gets previous key clicked for single key pressed
            prevKeyState = Keyboard.GetState();
        }

        /// <summary>
        /// Handles collectible collisions
        /// </summary>
        public void CollectibleColision()
        {
            // Loop all collect
            // if (player.pos.intersects(collectables[i].pos)){
            //Calls change temperature and sets collectable to inactive

            List<Collectable> intersections = new List<Collectable>();

            foreach (Collectable collectable in collectables)
            {
                if (player.Position.Intersects(collectable.Position))
                {
                    intersections.Add(collectable);
                }

            }

            foreach(Collectable item in intersections)
            {

                if (item.IsActive)
                {
                    if (item.IsHot)
                    {
                        player.ChangeTemperature(true);
                    }
                    if (!item.IsHot)
                    {
                        player.ChangeTemperature(false);
                    }
                    item.IsActive = false;
                }

            }




        }

        /// <summary>
        /// handles tile collisions
        /// </summary>
        public void TileCollision()
        {
            List<Tile> intersections = new List<Tile>();
            bool isGrounded = false;
            foreach(Tile tile in tiles) 
            {
                if (player.Position.Intersects(tile.Position))
                {
                    intersections.Add(tile);
                }
                // If the groundrect intersects a tile...
                if (!isGrounded && player.GroundRect.Intersects(tile.Position))
                {
                    // You're grounded!!
                    isGrounded = true;
                }
            }
            // Set player IsGrounded
            player.IsGrounded = isGrounded;

            if(intersections.Count > 0)
            {
                Rectangle posCopy = player.Position;

                foreach (Tile tile in intersections)
                {
                    Rectangle rect = tile.Position;
                    Rectangle intersection = Rectangle.Intersect(rect, posCopy);
                    Vector2 velCopy = player.Velocity;

                    if (intersection.Height > intersection.Width && intersection.Width > 4)
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

                    if (intersection.Height < intersection.Width)
                    {
                        if (intersection.Width > 10)
                        {
                            velCopy.Y = 0;
                        }
                        
                        player.Velocity = velCopy;

                        if (posCopy.Y - rect.Y < 0)
                        {
                            posCopy.Y -= intersection.Height;
                        }
                        else
                        {

                            posCopy.Y += intersection.Height;
                        }
                    }


                }

                

                //resolves intersections
                player.Position = posCopy;
            }
            // Update the player's groundRect on thew new Player Position
            player.UpdateGroundRect(new Vector2(player.Position.X, player.Position.Y));

        }

    }
}
