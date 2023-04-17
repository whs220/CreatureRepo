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
using Microsoft.Xna.Framework.Audio;

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
        public List<Spring> springs;

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
            this.springs = new List<Spring>();
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
            springs.Clear();

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
                    if (data[2] == "filler")
                    {
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(90, 90, 32, 32)));
                    }
                    if (data[2] == "CGright")
                    {
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(512, 288, 32, 32)));
                    }
                    if (data[2] == "CGleft")
                    {
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(448, 288, 32, 32)));
                    }
                    if (data[2] == "pillar")
                    {
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(928, 448, 32, 32)));
                    }
                    if (data[2] == "corner1")
                    {
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(800, 160, 32, 32)));
                    }
                    if (data[2] == "corner2")
                    {
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(864, 288, 32, 32)));
                    }
                    if (data[2] == "corner3")
                    {
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(289, 161, 32, 32)));
                        //225, 161
                    }
                    if (data[2] == "corner4")
                    {
                        tiles.Add(new Tile(tilemap, new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32), new Rectangle(353, 161, 32, 32)));
                        //417, 161
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

                    // misc objects
                    if (data[2] == "spring")
                    {
                        springs.Add(new Spring(new Rectangle((int.Parse(data[1])) * 32, (int.Parse(data[0])) * 32, 32, 32)));
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
                        collectable.DrawHot(sb, Color.Red);
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

            // each spring is drawn
            foreach (Spring spring in springs)
            {
                // Flip spring if gas
                spring.Flip = player.CurrentMatterState == PlayerMatterState.Gas;
                spring.DrawBounce(sb, Color.White);
            }
            
        }

        /// <summary>
        /// Checks collectible collisions and tile collisions
        /// </summary>
        public void Update()
        {
            //Calls all collectible collisions
            CollectibleColision();
            // Calls all misc collision
            MiscCollision();
            

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
                if (item.IsExit != true)
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

                        
                        //turns off the collectable
                        item.IsActive = false;
                    }
                }
                //sends you to the next level
                if (item.IsExit)
                {

                    NextLevelEvent();
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
            bool currentIsGrounded = player.IsGrounded;

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

                // For square collision detection
                Rectangle actualRect = player.GetCollisionHelperRect();

                //loops through all intersects
                foreach (Tile tile in intersections)
                {
                    Rectangle rect = tile.Position;

                    // NEW & IMPROVED COLLISION!

                    // This is basically the same collision we had before,
                    // the player being a full box.
                    // HOWEVER... only change is that it only updates to this old box
                    // collision if the new hitbox hits something!!
                    // Leading to a working smaller player hitbox and (hopefully) no more clipping!

                    // ----- checkIntersection -----
                    // This rectangle is a 32 x 32 bounding box around the player
                    // (the same rect the animation uses)
                    // This is the intersection that
                    // DETERMINES WHETER THE PLAYER ADJUSTS UP/DOWN & LEFT/RIGHT

                    // The new player hitbox is too small to determine this,
                    // leading to clipping through the floor
                    // This happens due to the player already
                    // being in the floor when checking collision...
                    // ...leading to the code pushing the player through the
                    // ground thinking they are more below than above

                    // The larger rectangle fixes this since the area to check is greater,
                    // leading to more accurate collisions
                    Rectangle checkIntersection = Rectangle.Intersect(rect, actualRect);

                    // ----- addInterestion -----
                    // This is the actual player hitbox intersection
                    // It will determine HOW FAR the player adjusts (just like mario!!)
                    Rectangle addIntersection = Rectangle.Intersect(rect, posCopy);

                    Vector2 velCopy = player.Velocity;

                    //If the check rectangle is taller than it is wide
                    // 4 Pixel buffer to avoid getting stuck
                    if (checkIntersection.Height > checkIntersection.Width && checkIntersection.Width > 4)
                    {
                        //the player is moved left or right
                        if (posCopy.X - rect.X < 0)
                        {
                            posCopy.X -= addIntersection.Width;
                        }
                        else
                        {
                            posCopy.X += addIntersection.Width;
                        }
                        // If player is solid, bounce the ice physics!
                        if (player.CurrentMatterState == PlayerMatterState.Solid && checkIntersection.Height > 6)
                        {
                            player.Speed = -player.Speed;
                        }
                    }

                    //if wider than it is tall
                    // Buffer of 10 pixels to avoid getting stuck on the wall
                    if (checkIntersection.Height < checkIntersection.Width && checkIntersection.Width > 10)
                    {
                        //tolerance and cap
                        velCopy.Y = 0;

                        //copy alter replace
                        player.Velocity = velCopy;

                        //move up or down
                        if (posCopy.Y - rect.Y < 0)
                        {
                            posCopy.Y -= addIntersection.Height;
                        }
                        else
                        {
                            posCopy.Y += addIntersection.Height;
                        }
                    }
                }

                //resolves intersections
                player.Position = posCopy;
            }
            // Update the player's groundRect on the new Player Position
            player.UpdateGroundRect(new Vector2(player.Position.X, player.Position.Y));
        }

        public void MiscCollision()
        {
            //checks for intersections
            foreach (Spring spring in springs)
            {
                // Boost player up!!
                // Have to increase y position to avoid bumping!

                int flip = 1;
                if (player.GetCollisionHelperRect().Intersects(spring.Position))
                {
                    // Boost downwards if gas
                    if (player.CurrentMatterState == PlayerMatterState.Gas) { flip = -1; }

                    Rectangle posCopy = player.Position;
                    Vector2 velCopy = player.Velocity;
                    posCopy.Y -= 4 * flip;
                    velCopy.Y = -14 * flip;
                    player.Velocity = velCopy;
                    player.Position = posCopy;
                    spring.StartAnimation();
                    break;
                }
            }
        }
    }
}
