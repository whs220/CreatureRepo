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

        // menu
        private Texture2D startTexture;
        private Texture2D quitTexture;
        private Button startButton;
        private Button quitButton;

        private SpriteFont mainFont;
        private SpriteFont titleFont;

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
            gameState = GameState.Menu;

            

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

            // menu
            startTexture = Content.Load<Texture2D>("startButton");
            quitTexture = Content.Load<Texture2D>("quitButton");

            level1.ReadLevel();
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
                        gameState = GameState.InGame;
                    }

                    // click on quit game -> CLOSE GAME
                    if (quitButton.MousePosition() && quitButton.MouseClick())
                    {
                        System.Environment.Exit(0);
                    }

                    break;

                case GameState.LoadingScreen:
                    // loading screen ends -> in game
                    break;

                case GameState.InGame:
                    player.Update(gameTime);
                    level1.Update();

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
                    break;

                case GameState.InGame:
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    level1.Draw(_spriteBatch);
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
    }
}