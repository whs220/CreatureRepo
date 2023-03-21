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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            gameState = GameState.Menu;

            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 1024;

            base.Initialize();

            // Debug: Creating the player here
            player = new Player(debugSolid, debugLiquid, debugGas, new Rectangle(50, 50, 32, 32));
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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // update GameState
            switch (gameState)
            {
                case GameState.Menu:
                    // click on start game -> loading screen
                    // click on quit game -> CLOSE GAME

                    player.Update();
                    break;

                case GameState.LoadingScreen:
                    // loading screen ends -> in game
                    break;

                case GameState.InGame:
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // draw GameState
            switch (gameState)
            {
                case GameState.Menu:
                    player.Draw(_spriteBatch);
                    break;

                case GameState.LoadingScreen:
                    break;

                case GameState.InGame:
                    break;

                case GameState.WinScreen:
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}