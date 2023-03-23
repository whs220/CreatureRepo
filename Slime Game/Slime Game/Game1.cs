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

        // win screen
        private Texture2D restartTexture;
        private Button restartButton;

        // loading
        private double timer;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // set the size of the window
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
            // loading in tiles and collectibles
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            tileMap = Content.Load<Texture2D>("tileset");
            fire = Content.Load<Texture2D>("fire");
            ice = Content.Load<Texture2D>("ice");

            // loading in debug mode content
            debugSolid = Content.Load<Texture2D>("debug_solid");
            debugLiquid = Content.Load<Texture2D>("debug_liquid");
            debugGas = Content.Load<Texture2D>("debug_gas");

            // loading in fonts
            mainFont = Content.Load<SpriteFont>("bankgothiclight16");
            titleFont = Content.Load<SpriteFont>("comicSans36");

            // loading in player and level
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
            startButton = new Button(startTexture, new Rectangle(150, 550, 300, 100));
            quitButton = new Button(quitTexture, new Rectangle(574, 550, 300, 100));

            // win screen
            restartTexture = Content.Load<Texture2D>("restartButton");
            restartButton = new Button(restartTexture, new Rectangle(150, 650, 300, 100));

            level1.ReadLevel();
            // Add reset level event
            player.ResetLevelEvent += level1.ReadLevel;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // update GameState
            switch (gameState)
            {
                case GameState.Menu:
                    quitButton.Y = 550;

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
                    // bypasses loading screen if debug mode is active
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

                    // increment gameTime
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
                    quitButton.Y = 650;

                    // click on restart -> menu
                    if (restartButton.MousePosition() && restartButton.MouseClick())
                    {
                        gameState = GameState.Menu;
                    }

                    // click on quit -> CLOSE GAME
                        if (quitButton.MousePosition() && quitButton.MouseClick())
                    {
                        System.Environment.Exit(0);
                    }

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
                    // background
                    GraphicsDevice.Clear(Color.LimeGreen);

                    // font(s)
                    _spriteBatch.DrawString(titleFont, "Sebastian Slime!", new Vector2(275, 300), Color.DarkOliveGreen);

                    // button(s)
                    startButton.Draw(_spriteBatch);
                    quitButton.Draw(_spriteBatch);

                    break;

                case GameState.LoadingScreen:
                    // background
                    GraphicsDevice.Clear(Color.DarkOliveGreen);

                    // font(s)
                    _spriteBatch.DrawString(titleFont, "Loading...", new Vector2(30, 920), Color.LimeGreen);

                    break;

                case GameState.InGame:
                    // background
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    // level and player
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
                    // background
                    GraphicsDevice.Clear(Color.LimeGreen);

                    // font(s)
                    _spriteBatch.DrawString(titleFont, "You found your family!", new Vector2(225, 300), Color.DarkOliveGreen);
                    _spriteBatch.DrawString(titleFont, "Congratulations! :)", new Vector2(275, 400), Color.DarkOliveGreen);

                    // button(s)
                    restartButton.Draw(_spriteBatch);
                    quitButton.Draw(_spriteBatch);

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