using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media; //Song
namespace verySimplePongSystem
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Song song;

        private Texture2D paddleTex, ballTex, gameOverTex, background;
        private Vector2 paddlePos, ballPos, gameOverPos;
        
        bool isGameOver;
        float xBallSpeed, yBallSpeed, paddleSpeed;

        //Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.ApplyChanges();
            //graphics.IsFullScreen = true;//or Default
            // TODO: Add your initialization logic here
            Window.AllowAltF4 = false; //No Rage quit
            //Window.AllowUserResizing = true;
            xBallSpeed = 5; yBallSpeed = 5; paddleSpeed = 30;
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            song = Content.Load<Song>("songbackground");

            MediaPlayer.Play(song);
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            background = Content.Load<Texture2D>("background");
            paddleTex = Content.Load<Texture2D>("Paddle");
            ballTex = Content.Load<Texture2D>("Ball");
            gameOverTex = Content.Load<Texture2D>("GameOver");

            ballPos = new Vector2(GraphicsDevice.Viewport.Width / 2 - ballTex.Width / 2, 0);
            paddlePos = new Vector2(GraphicsDevice.Viewport.Width / 2 - paddleTex.Width / 2,
            GraphicsDevice.Viewport.Height - paddleTex.Height * 1.5f);
            gameOverPos = new Vector2(GraphicsDevice.Viewport.Width / 2 - gameOverTex.Width / 2,
                        GraphicsDevice.Viewport.Height / 2 - gameOverTex.Height / 2);

            // TODO: use this.Content to load your game content here
        }

        private void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            MediaPlayer.Volume -= -0.1f;
            MediaPlayer.Play(song);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            if(!isGameOver)
            {
                ballPos.X += xBallSpeed;
                ballPos.Y += yBallSpeed;

                if(Keyboard.GetState().IsKeyDown(Keys.Left))
                    paddlePos.X -= paddleSpeed;

                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    paddlePos.X += paddleSpeed;

                if (ballPos.X <= 0)
                {
                    //GraphicsDevice.Clear(Color.Orange);
                    xBallSpeed *= -1;
                }



                if (ballPos.Y <= 0)
                {
                    //GraphicsDevice.Clear(Color.Purple);
                    yBallSpeed *= -1;
                }

                if (ballPos.X >= GraphicsDevice.Viewport.Width - ballTex.Width)
                {
                    //GraphicsDevice.Clear(Color.Yellow);
                    xBallSpeed *= -1;
                }
                    
                if (ballPos.Y >= paddlePos.Y - ballTex.Width)
                {
                    if(ballPos.X + ballTex.Width >= paddlePos.X & ballPos.X <= paddlePos.X + paddleTex.Width)
                    {
                        //GraphicsDevice.Clear(Color.Gray);
                        yBallSpeed *= -1;
                    }
                        
                }

                if (ballPos.Y >= GraphicsDevice.Viewport.Height - ballTex.Width)
                    isGameOver = true;
            }
            else
            {
                if(Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    ballPos = new Vector2(GraphicsDevice.Viewport.Width / 2 - ballTex.Width / 2, 0);
                    paddlePos = new Vector2(GraphicsDevice.Viewport.Width / 2 - paddleTex.Width / 2,
                    GraphicsDevice.Viewport.Height - paddleTex.Height * 1.5f);
                    isGameOver = false;
                }
            }

            // TODO: Add your update logic here 

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Firebrick);

            ///wierdaf
            ///spriteBatch.Draw(ballTex, new Rectangle(xBallPos, yBallPos, wBall, hBall), Color.White);
            ///spriteBatch.Draw(paddleTex, new Rectangle(xPaddlePos, yPaddlePos, wPaddle, hPaddle), Color.White);
            ///spriteBatch.Draw(gameOverTex, gameOverPos,Color.White);
            ///spriteBatch.Draw(gameOverTex, gameOverPos, new Rectangle(0, 0, 100, 100), Color.White);

            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 1000, 500), Color.White);

            if (!isGameOver)
            {
                spriteBatch.Draw(ballTex, ballPos, Color.White);//White don't affect ur sprite
                spriteBatch.Draw(paddleTex, paddlePos, Color.White);
            }
            else
            {
                spriteBatch.Draw(gameOverTex, gameOverPos, Color.White);
            }
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
