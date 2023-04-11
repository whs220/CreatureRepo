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
    ///
    internal class Level
    {
        //Fields

        //directory
        public string fileName;

        //Lists of game objects
        public List<GameObject> gameObjects;
        public List<Tile> tiles;
        public List<Collectable> collectables;

        //player
        private Player player;

        //Textures
        private Texture2D tilemap;
        private Texture2D collect;
        private Texture2D exit;
        
        //borders for future use
        private Rectangle groundFrame;
        private Rectangle ceilingFrame;
        private Rectangle leftFrame;
        private Rectangle rightFrame;
        private Tile backTile;

        //misc
        private KeyboardState prevKeyState;
        private bool collisionsOn;

        //exit
        public event NextLevel NextLevelEvent;

        //For debug mode
        private bool debugModeActive;

        // Properties

        /// <summary>
        /// Gets and sets the debugmodeactive bool
        /// </summary>
        public bool DebugModeActive
        {
            get { return debugModeActive; }
            set { debugModeActive = value; }
        }

        /// <summary>
        /// Makes ColisionsOn have a set value
        /// </summary>
        public bool CollisionsOn
        {
            get { return collisionsOn; }
            set { collisionsOn = value; }
        }


        // 1, 352
        // 31, 383

        //Constructor
        public Level(string fileName, Player player) 
        {
            collisionsOn = true;
            debugModeActive = false;
            this.fileName = fileName;
            this.player = player;
            tilemap = Art.Instance.LoadTexture2D("tileset");
            collect = Art.Instance.LoadTexture2D("collectables");
            exit = Art.Instance.LoadTexture2D("pipe");


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

            //make stream reader and set the done bool to false
            StreamReader input;
            bool done = false;

            //clear any previous tiles
            tiles.Clear();
            collectables.Clear();
            gameObjects.Clear();

            //File IO
            try
            {
                //connects to file
                input = new StreamReader("../../../" + fileName);
                string line;

                //ceiling line
                tiles.Add(new Tile(tilemap, new Rectangle(0, 0, 1024, 32), new Rectangle(0, 224, 32, 32)));

                //Reads through every line
                while ((line = input.ReadLine()) != null)
                {
                    //splits the line into its data
                    string[] data = line.Split(',');

                    //All tile cases
                    if (data[2] == "top")
                    {
                        //ceiling tile
                        //uses second rectangle to frame the part of the tilemap
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(0, 224, 32, 32)));
                    }
                    if (data[2] == "ground")
                    {
                        //uses second rectangle to frame the part of the tilemap
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(0, 351, 32, 32)));
                    }
                    if (data[2] == "leftWall")
                    {
                        //uses second rectangle to frame the part of the tilemap
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(800, 416, 32, 32)));
                    }
                    if (data[2] == "rightWall")
                    {
                        //uses second rectangle to frame the part of the tilemap
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(864, 416, 32, 32)));
                    }
                    if (data[2] == "platform")
                    {
                        //uses second rectangle to frame the part of the tilemap
                        //Standard size of four blocks long
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(0, 288, 32, 32)));
                    }

                    // all collectable cases
                    if (data[2] == "hot")
                    {
                        //new collectable with bool to determine type
                        collectables.Add(new Collectable(collect, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), true));
                    }
                    if (data[2] == "cold")
                    {
                        collectables.Add(new Collectable(collect, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), false));
                    }
                    if (data[2] == "exit")
                    {
                        //Creates both a tile and a collectable to make the entrance
                        //of the pipe only at the front
                        tiles.Add(new Tile(exit, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(0, 0, 32, 32)));
                        collectables.Add(new Collectable(exit, new Rectangle((int.Parse(data[1])) * 32 - 4, (int.Parse(data[0])) * 32 + 8, 32, 16), false, true));
                    }

                    //players position is set to where it starts in the level file
                    if (data[2] == "player")
                    {
                        player.Reset();
                        Rectangle copy = player.Position;
                        copy.X = (int.Parse(data[1])) * 32;
                        copy.Y = (int.Parse(data[0])) * 32;
                        player.Position = copy;
                    }
                }
            }
            catch
            {
                throw new Exception("Level couldnt read properly");
            }
        }
        
        /// <summary>
        /// Draws all tiles and collectables
        /// </summary>
        public void Draw(SpriteBatch sb)
        {
            //Prints all background tiles 
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    backTile.Position = new Rectangle(i * 32, j * 32, 32, 32);
                    backTile.Draw(sb);
                }
            }

            //Each collectable is drawn
            foreach (Collectable collectable in collectables)
            {
                //besides for the pipe entrance
                if (!collectable.IsExit)
                {
                    if (player.CurrentMatterState == PlayerMatterState.Gas && collectable.IsHot) {
                        collectable.DrawHot(sb, Color.Orange);
                    }
                    else if(player.CurrentMatterState == PlayerMatterState.Solid && collectable.IsHot == false)
                    {
                        collectable.DrawCold(sb, Color.Purple);
                    }
                    else
                    {
                        if (collectable.IsExit)
                        {
                            collectable.Draw(sb);
                        }
                        else if (collectable.IsHot)
                        {
                            collectable.DrawHot(sb, Color.White);
                        }
                        else
                        {
                            collectable.DrawCold(sb, Color.White);
                        }
                    }
                }
            }

            //each tile is drawn
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
            //Calls all collectible collisions
            CollectibleColision();
            

            //for debug mode
            if (collisionsOn) 
            {
                TileCollision();
            }


            //Gets previous key clicked for single key pressed
            prevKeyState = Keyboard.GetState();
        }

        /// <summary>
        /// Handles collectible collisions
        /// </summary>
        public void CollectibleColision()
        {

            //list of all tiles intersecting
            List<Collectable> intersections = new List<Collectable>();

            //adds each tile that its touching to its own list
            foreach (Collectable collectable in collectables)
            {
                if (player.Position.Intersects(collectable.Position))
                {
                    intersections.Add(collectable);
                }

            }

            //loops through all intersecting collectables
            foreach(Collectable item in intersections)
            {
                //checks to see if the item hasnt been used already
                if (item.IsActive)
                {
                    //Changes the players state of matter
                    if (item.IsHot)
                    {
                        player.ChangeTemperature(true);
                    }
                    if (!item.IsHot)
                    {
                        player.ChangeTemperature(false);
                    }

                    //sends you to the next level
                    if (item.IsExit)
                    {

                        NextLevelEvent();
                    }

                    //turns off the collectable
                    item.IsActive = false;



                }

            }
        }

        /// <summary>
        /// handles tile collisions
        /// </summary>
        public void TileCollision()
        {
            //Lists all intersections
            List<Tile> intersections = new List<Tile>();

            //is not grounded
            bool isGrounded = false;

            //checks for intersections
            foreach(Tile tile in tiles) 
            {
                if (player.Position.Intersects(tile.Position))
                {
                    intersections.Add(tile);
                }

                //grounds the player
                if (!isGrounded && player.GroundRect.Intersects(tile.Position))
                {
                    isGrounded = true;
                }
            }

            //update player
            player.IsGrounded = isGrounded;

            if(intersections.Count > 0)
            {
                //for copy alter replace
                Rectangle posCopy = player.Position;

                //loops through all intersects
                foreach (Tile tile in intersections)
                {
                    Rectangle rect = tile.Position;
                    Rectangle intersection = Rectangle.Intersect(rect, posCopy);
                    Vector2 velCopy = player.Velocity;

                    //If the rectangle is taller than it is wide
                    if (intersection.Height > intersection.Width && intersection.Width > 4)
                    {
                        //the player is moved left or right
                        if (posCopy.X - rect.X < 0)
                        {
                            posCopy.X -= intersection.Width;
                        }
                        else
                        {
                            posCopy.X += intersection.Width;
                        }
                        // If player is solid, bounce the ice physics!
                        if (player.CurrentMatterState == PlayerMatterState.Solid)
                        {
                            player.Speed = -player.Speed;
                        }
                    }

                    //if wider than it is tall
                    if (intersection.Height < intersection.Width)
                    {
                        //tolerance and cap
                        if (intersection.Width > 10)
                        {
                            velCopy.Y = 0;
                        }
                        
                        //copy alter replace
                        player.Velocity = velCopy;

                        //move up or down
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
            // Update the player's groundRect on the new Player Position
            player.UpdateGroundRect(new Vector2(player.Position.X, player.Position.Y));
        }
    }
}
