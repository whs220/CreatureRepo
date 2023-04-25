//Jake Wardell
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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

        private Player player;
        KeyboardState prevKeyState;


        //Level List
        private List<Level> levels;
        private string[] levelNames;
        private int currentLevel;

        // menu
        private Texture2D startTexture;
        private Texture2D quitTexture;
        private Texture2D creditsTexture;
        private Texture2D startScreen;
        private Button startButton;
        private Button quitButton;
        private Button creditsButton;
        private SpriteFont debugFont;
        private SpriteFont titleFont;
        private SpriteFont gameFont;

        // win screen
        private Texture2D restartTexture;
        private Texture2D winScreen;
        private Button restartButton;

        // loading
        private Texture2D loadingScreen;
        private double timer;

        // in game
        private Texture2D background;

        //Music
        private Song themeSong;
        private Song secondSong;
        private int currentSong = -1;

        // Speedrun timer
        private GameTime speedrunTime;
        private bool speedRunTimerActive; 

        //Sound Effect
        SoundEffect sfx_NextLevel;


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
            gameState = GameState.Menu;

            // loading
            timer = 0.5f;

            // ===== List of levels! =====d
            // This is the order of levels that appear!
            levelNames = new string[]
            {
                //Tutorial Levels
                "Content/firstLevel.level",
                "Content/secondLevel.level",
                "Content/thirdLevel.level",
                "Content/fourthLevel.level",

                //Middle levels?
                "Content/welcome_slime.level",
                "Content/epic_slide.level",
                

                //spring tutorial
                "Content/springTutoiral.level",
                "Content/pinball.level",
                "Content/Bounce.level",
                "Content/maze.level",
                "Content/need_for_speed.level",
                "Content/spring_hell.level"

                //"Content/level1.level",
            };

            levels = new List<Level>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // loading in tiles and collectibles
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Art.Instance.SetContentLoader(Content);

            // Load in sounds
            themeSong = Content.Load<Song>("slimegame");
            secondSong = Content.Load<Song>("slimegame2");

            // loading in fonts
            debugFont = Content.Load<SpriteFont>("bankgothiclight16");
            titleFont = Content.Load<SpriteFont>("comicSans36");
            gameFont = Content.Load<SpriteFont>("arial-35");

            // loading in player
            player = new Player();

            // Adding levels to the level list
            foreach (string levelName in levelNames)
            {
                Level level = new Level(levelName, player);
                levels.Add(level);
            }

            foreach (Level level in levels)
            {
                level.NextLevelEvent += NextLevel;
            }

            // menu
            startTexture = Content.Load<Texture2D>("newStartButton");
            quitTexture = Content.Load<Texture2D>("newQuitButton");
            creditsTexture = Content.Load<Texture2D>("creditsButton");
            startScreen = Content.Load<Texture2D>("startScreen");
            startButton = new Button(startTexture, new Rectangle(640, 600, 300, 100));
            quitButton = new Button(quitTexture, new Rectangle(640, 725, 300, 100));
            //creditsButton = new Button(creditsTexture, new Rectangle(640, 850, 300, 100));

            // loading
            loadingScreen = Content.Load<Texture2D>("loadScreen");

            // in game
            background = Content.Load<Texture2D>("background");

            // win screen
            restartTexture = Content.Load<Texture2D>("newRestartButton");
            winScreen = Content.Load<Texture2D>("winScreen");
            restartButton = new Button(restartTexture, new Rectangle(30, 300, 300, 100));

            //Play Song
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.2f;
            PlaySong(0);

            //Speed run timer
            speedRunTimerActive = false;

            //Sound Effects
            sfx_NextLevel = Content.Load<SoundEffect>("win");
        }

        protected override void Update(GameTime gameTime)
        {

            // update GameState
            switch (gameState)
            {
                case GameState.Menu:
                    // Play main theme
                    PlaySong(0);

                    // moves quit button from win position to start position (in case player restarts from the end)
                    quitButton.X = 640;
                    quitButton.Y = 725;

                    // click on start game -> loading screen
                    if(startButton.MousePosition() && startButton.MouseClick())
                    {
                        gameState = GameState.LoadingScreen;

                        // Set currentLevel to zero and set add the readLevel event
                        currentLevel = 0;
                        player.ResetLevelEvent += levels[0].ReadLevel;
                        // Reset speedrun timer
                        speedrunTime = new GameTime();
                        // Then the level is read in the loading screen state!
                    }

                    // click on quit game -> CLOSE GAME
                    if (quitButton.MousePosition() && quitButton.MouseClick())
                    {
                        System.Environment.Exit(0);
                    }

                    //Turns speed run timer on
                    if(Keyboard.GetState().IsKeyDown(Keys.Tab) && prevKeyState.IsKeyUp(Keys.Tab))
                    {
                        speedRunTimerActive = !speedRunTimerActive;
                    }

                    break;

                case GameState.LoadingScreen:
                    // bypasses loading screen if debug mode is active
                    if (levels[currentLevel].DebugModeActive)
                    {
                        gameState = GameState.InGame;
                        break;
                    }
                    timer -= gameTime.ElapsedGameTime.TotalSeconds;

                    // loading screen ends -> in game
                    if (timer <= 0)
                    {
                        gameState = GameState.InGame;
                        timer = 0.5f;
                        // Read in the current level
                        levels[currentLevel].ReadLevel();
                    }

                    

                    // increment gameTime
                    base.Update(gameTime);
                    break;

                //In Game State
                case GameState.InGame:

                    //If escape is clciked in game then it will take you to menu
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        gameState = GameState.Menu;

                    // Switch song if on stage 6
                    if (currentLevel == 6)
                    {
                        PlaySong(1);
                    }

                    // Speedrun timer
                    if (speedRunTimerActive)
                    {
                        speedrunTime.ElapsedGameTime += gameTime.ElapsedGameTime;
                    }

                    player.Update(gameTime);
                    // Collectable animation
                    foreach(Collectable c in levels[currentLevel].collectables)
                    {
                        c.UpdateAnimation(gameTime);
                    }
                    // Spring animation
                    foreach (Spring s in levels[currentLevel].springs)
                    {
                        s.UpdateAnimation(gameTime);
                    }
                    //Calls tge current level update method for current level logic
                    levels[currentLevel].Update();

                    
                    //Checks if the F1 key is clicked and debugMode is turned on or off
                    if (Keyboard.GetState().IsKeyDown(Keys.F1) && prevKeyState.IsKeyUp(Keys.F1))
                    {
                        levels[currentLevel].DebugModeActive = !levels[currentLevel].DebugModeActive;
                        player.DebugModeActive = levels[currentLevel].DebugModeActive;
                    }

                    //If the game is in debug mode
                    if (levels[currentLevel].DebugModeActive)
                    {
                        //If key N is pressed once nextlevel is xalled
                        if(Keyboard.GetState().IsKeyDown(Keys.N) && prevKeyState.IsKeyUp(Keys.N))
                        {
                            NextLevel();
                        }
                        //Checks for single key press on C to change colder
                        if (Keyboard.GetState().IsKeyDown(Keys.C) && prevKeyState.IsKeyUp(Keys.C))
                        {
                            player.ChangeTemperature(false);
                        }
                        //Checks for single key press on H to change hotter
                        if (Keyboard.GetState().IsKeyDown(Keys.H) && prevKeyState.IsKeyUp(Keys.H))
                        {
                            player.ChangeTemperature(true);
                        }
                        //When F2 is clicked then collisions get turned off
                        if (Keyboard.GetState().IsKeyDown(Keys.F2) && prevKeyState.IsKeyUp(Keys.F2))
                        {
                            levels[currentLevel].CollisionsOn = !levels[currentLevel].CollisionsOn;
                        }
                        //Switches the gravity to the opposite
                        if(Keyboard.GetState().IsKeyDown(Keys.G) && prevKeyState.IsKeyUp(Keys.G))
                        {
                            player.GravityOff = !player.GravityOff;
                        }
                    }

                    
                    
                    prevKeyState = Keyboard.GetState();

                    break;

                //For when on the game win screen
                case GameState.WinScreen:

                    // moves quit button from win position to start position (in case player restarts from the end)
                    quitButton.X = 700;
                    quitButton.Y = 300;

                    NextLevel(); //Then calls nextLevel to reset to the first level

                    // click on restart -> menu
                    if (restartButton.MousePosition() && restartButton.MouseClick())
                    {
                        // Go to menu and set current level back to 0
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


        /// <summary>
        /// Draws the game depending on state and other factors
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            // draw GameState
            switch (gameState)
            {
                //For Menu state
                case GameState.Menu:

                    // background image
                    _spriteBatch.Draw(startScreen, new Rectangle(0, 0, 1024, 1024), Color.White);

                    // button(s)
                    startButton.Draw(_spriteBatch);
                    quitButton.Draw(_spriteBatch);
                    //creditsButton.Draw(_spriteBatch);

                    break; 


                //For in the Loading screen
                case GameState.LoadingScreen:

                    // background image
                    _spriteBatch.Draw(loadingScreen, new Rectangle(0, 0, 1024, 1024), Color.White);

                    break;


                //In game state
                case GameState.InGame:

                    // background
                    _spriteBatch.Draw(background, new Rectangle(0, 0, 1024, 1024), Color.White);

                    // level and player
                    levels[currentLevel].Draw(_spriteBatch);
                    player.Draw(_spriteBatch);

                    //If in debug mode then it draws specific stuff
                    if (player.DebugModeActive)
                    {
                        //Debug mode writing on left side with stats about things
                        _spriteBatch.DrawString(debugFont, "Player X, Y: " + player.Position.X + ", " + player.Position.Y + // Writes player X and Y
                            "\nPlayer Velocity: " + player.Velocity.X + ", " + player.Velocity.Y + // Writes player velocity
                            "\nCurrent State: " + player.CurrentMatterState.ToString() + // Writes players current state
                            "\nCurrent Level: " + currentLevel + // Writes current level number
                            "\nCollisions On: " + levels[currentLevel].CollisionsOn + //States wherther collisions are on
                            "\nGravity off: " + player.GravityOff // states whether gravity is on
                            , new Vector2(30, 50), Color.White);

                        //Debug mode writing on the right side of the screen how to use it 
                        _spriteBatch.DrawString(debugFont, "Use 'N' to go to next level \nUse 'H' to go hotter \nUse 'C' for colder \nUse 'F2' to toggle collisions \nUse 'G' to toggle gravity", new Vector2(730, 50),Color.White);


                    }

                    //Draws text for tutorial
                    OnBoarding(_spriteBatch);

                    // Draw speedrun timer
                    if (speedRunTimerActive)
                    {
                        _spriteBatch.DrawString(debugFont, string.Format("{0:00}:{1:00}:{2:00}", speedrunTime.ElapsedGameTime.Minutes, speedrunTime.ElapsedGameTime.Seconds, speedrunTime.ElapsedGameTime.Milliseconds),
                            new Vector2(915, 995), Color.White);
                    }

                    break;


                //Win screen state
                case GameState.WinScreen:
                    // background image
                    _spriteBatch.Draw(winScreen, new Rectangle(0, 0, 1024, 1024), Color.White);;

                    // button(s)
                    restartButton.Draw(_spriteBatch);
                    quitButton.Draw(_spriteBatch);

                    // Final speedrun time!
                    if (speedRunTimerActive)
                    {
                        _spriteBatch.DrawString(debugFont, string.Format("{0:00}:{1:00}.{2:00}", speedrunTime.ElapsedGameTime.Minutes, speedrunTime.ElapsedGameTime.Seconds, speedrunTime.ElapsedGameTime.Milliseconds),
                            new Vector2(915, 995), Color.Cyan);
                    }
                    break;
            }

            

            _spriteBatch.End();

            base.Draw(gameTime);
        }


        /// <summary>
        /// For going to the next level
        /// </summary>
        public void NextLevel()
        {
            //If the currentLevel + 1 is less than level count
            if (currentLevel + 1 < levels.Count && currentLevel >= 0)
            {
                //Unadds the current level from level readlevel event so that when game is reset the correct level is read
                player.ResetLevelEvent -= levels[currentLevel].ReadLevel;
                //Turns off debug mode and re-enable collisions
                levels[currentLevel].DebugModeActive = false;
                levels[currentLevel].CollisionsOn = true;
                player.DebugModeActive = false;
                //Current level is increased
                currentLevel++;

                //Adds current level to the event
                player.ResetLevelEvent += levels[currentLevel].ReadLevel;

                // Go to loading screen, which then will read the new level
                gameState = GameState.LoadingScreen;
                sfx_NextLevel.Play();

            }
            //if this is the last level switches to the win screen
            else
            {
                // Remove event from the last level
                player.ResetLevelEvent -= levels[levels.Count - 1].ReadLevel;
                //Turns off debug mode for last level and re-enable collisions for last level
                levels[levels.Count - 1].DebugModeActive = false;
                levels[currentLevel].CollisionsOn = true;
                player.DebugModeActive = false;
                // Change to the win screen
                gameState = GameState.WinScreen;
            }
        }

        public void OnBoarding(SpriteBatch sb)
        {
            // Level 1 text for tutorial 
            if(currentLevel == 0)
            {
                sb.DrawString(gameFont, " Use \'A\' and \'D\' to move\nUse \'W\' or \'Space\' to jump", 
                    new Vector2(220, 500), Color.White);
            }
            // Level 1 text for tutorial 
            else if (currentLevel == 1)
            {
                sb.DrawString(gameFont, "Hit fire collectables to change \ntemperature and \nbecome a gas... this \nwill allow you to \nfloat ;)",
                    new Vector2(500, 650), Color.White);
            }
            //Level 3 tutorial text
            else if (currentLevel == 2)
            {
                sb.DrawString(gameFont, "    Hit Ice collectables\n and become a Solid... \n Solids can't jump but can slide",
                    new Vector2(130, 600), Color.White);
            }
            //Level 4 tutorial text (Talks about resetting and all the matter states)
            else if (currentLevel == 3)
            {
                sb.DrawString(gameFont, "  If you ever get stuck hit 'R' \n      to reset The 3 matter\nstates are solid -> liquid -> gas",
                    new Vector2(130, 600), Color.White);
            }
            //Text for spring tutorial 
            else if (currentLevel == 6)
            {
                sb.DrawString(gameFont, "A spring can make you bounce \n     in any matter state!",
                    new Vector2(130, 600), Color.White);
            }
        }

        /// <summary>
        /// Changes which song is playing
        /// </summary>
        /// <param name="id">The id of the song to play an int value</param>
        public void PlaySong(int id)
        {
            //To pick which song it is
            switch (id)
            {
                case 0:
                    if (currentSong != 0)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(themeSong);
                        currentSong = 0;
                    }
                    break;
                case 1:
                    if (currentSong != 1)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(secondSong);
                        currentSong = 1;
                    }
                    break;
            }
        }
    }
}