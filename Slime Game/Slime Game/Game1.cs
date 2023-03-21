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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            gameState = GameState.Menu;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            tileMap = Content.Load<Texture2D>("tileset");
            fire = Content.Load<Texture2D>("fire");
            ice = Content.Load<Texture2D>("ice");

            // TODO: use this.Content to load your game content here
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

            // draw GameState
            switch (gameState)
            {
                case GameState.Menu:
                    break;

                case GameState.LoadingScreen:
                    break;

                case GameState.InGame:
                    break;

                case GameState.WinScreen:
                    break;
            }

            base.Draw(gameTime);
        }
    }
}