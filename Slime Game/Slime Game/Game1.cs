//Jake Wardell
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slime_Game
{
    public enum GameState
    {
        Menu,
        LoadingScreen,
        InGame,
        WinScreen
    }

    public enum PlayerMovementState
    {
        IdleLeft,
        IdleRight,
        MoveLeft,
        MoveRight
    }

    public enum PlayerMatterState
    {
        Gas,
        Liquid,
        Solid,
        Dead
    }

    public delegate void ResetLevel();
    public delegate void NextLevel();
    

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameState gameState;
        private Texture2D tileMap;
        private Texture2D fire;
        private Texture2D ice;

        private Player player;
        private Texture2D debugSolid;
        private Texture2D debugLiquid;
        private Texture2D debugGas;
        private Level level1;
        KeyboardState prevKeyState;


        //Level List
        private List<Level> levels;
        private int currentLevel;

        // menu
        private Texture2D startTexture;
        private Texture2D quitTexture;
        private Button startButton;
        private Button quitButton;
        private SpriteFont mainFont;
        private SpriteFont titleFont;
        

        // loading
        private double timer;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 1024;
        }

        protected override void Initialize()
        {
            // menu
            gameState = GameState.Menu; // CHANGE THIS TO GameState.InGame IF YOU WANT TO BYPASS THE MENU AND LOADING SCREENS

            // loading
            timer = 1;

            base.Initialize();

            

            // Debug: Creating the player here
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            tileMap = Content.Load<Texture2D>("tileset");
            fire = Content.Load<Texture2D>("fire");
            ice = Content.Load<Texture2D>("ice");

            debugSolid = Content.Load<Texture2D>("debug_solid");
            debugLiquid = Content.Load<Texture2D>("debug_liquid");
            debugGas = Content.Load<Texture2D>("debug_gas");

            mainFont = Content.Load<SpriteFont>("bankgothiclight16");
            titleFont = Content.Load<SpriteFont>("comicSans36");

            player = new Player(debugSolid, debugLiquid, debugGas, new Rectangle(50, 50, 32, 32));
            level1 = new Level("Content/jaketestlevel.level", player, tileMap, fire, ice);

            //Level List

            levels = new List<Level>();
            levels.Add(level1);
            currentLevel = 0;
            foreach(Level level in levels)
            {
                level.NextLevelEvent += NextLevel;
            }
            levels[0].ReadLevel();
            

            // menu
            startTexture = Content.Load<Texture2D>("startButton");
            quitTexture = Content.Load<Texture2D>("quitButton");

            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // update GameState
            switch (gameState)
            {
                case GameState.Menu:
                    // initialize buttons
                    startButton = new Button(startTexture, new Rectangle(150, 550, 300, 100));
                    quitButton = new Button(quitTexture, new Rectangle(574, 550, 300, 100));

                    // click on start game -> loading screen
                    if(startButton.MousePosition() && startButton.MouseClick())
                    {
                        gameState = GameState.LoadingScreen;
                    }

                    // click on quit game -> CLOSE GAME
                    if (quitButton.MousePosition() && quitButton.MouseClick())
                    {
                        System.Environment.Exit(0);
                    }

                    break;

                case GameState.LoadingScreen:
                    if (levels[currentLevel].DebugModeActive == false)
                    {
                        timer -= gameTime.ElapsedGameTime.TotalSeconds;

                        // loading screen ends -> in game
                        if (timer <= 0)
                        {
                            gameState = GameState.InGame;
                            timer = 2;
                        }
                    }

                    base.Update(gameTime);
                    break;

                case GameState.InGame:
                    player.Update(gameTime);
                    levels[currentLevel].Update();

                    
                    /*
                    if (levels[currentLevel].DebugModeActive == true)
                    {
                        if(Keyboard.GetState().IsKeyDown(Keys.N) && prevKeyState.IsKeyUp(Keys.N))
                        {
                            NextLevel();
                        }
                    }

                    if(currentLevel > levels.Count)
                    {
                        gameState = GameState.WinScreen;
                    }
                    */
                    prevKeyState = Keyboard.GetState();
                    // beat a level -> loading screen
                    // beat the last level -> win screen
                    break;

                case GameState.WinScreen:
                    // click on close game -> CLOSE GAME
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            // draw GameState
            switch (gameState)
            {
                case GameState.Menu:
                    GraphicsDevice.Clear(Color.LimeGreen);

                    _spriteBatch.DrawString(titleFont, "Sebastian Slime!", new Vector2(275, 300), Color.DarkOliveGreen);

                    startButton.Draw(_spriteBatch);
                    quitButton.Draw(_spriteBatch);

                    break;

                case GameState.LoadingScreen:
                    GraphicsDevice.Clear(Color.DarkOliveGreen);

                    _spriteBatch.DrawString(titleFont, "Loading...", new Vector2(30, 920), Color.LimeGreen);

                    break;

                case GameState.InGame:
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    levels[currentLevel].Draw(_spriteBatch);
                    player.Draw(_spriteBatch);

                    //If in debug mode then it draws specific stuff
                    if (player.DebugModeActive == true)
                    {
                        _spriteBatch.DrawString(mainFont, "Player X, Y: " + player.Position.X + ", " + player.Position.Y +
                            "\nCurrent State: " + player.CurrentMatterState.ToString(), new Vector2(30, 50), Color.White);
                    }
                    break;

                case GameState.WinScreen:
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }


        public void NextLevel()
        {
            currentLevel++;
            levels[currentLevel].ReadLevel();
        }
    }
}