using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace appTestAndOther
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D texture;
        Vector2 position;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = Settings.SCREEN_HEIGHT,
                PreferredBackBufferWidth = Settings.SCREEN_WIDTH,
                IsFullScreen = Settings.IS_FULLSCREEN,
                SynchronizeWithVerticalRetrace = Settings.V_SYNC
            };
            graphics.ApplyChanges();

            this.IsMouseVisible = Settings.IS_MOUSE_VISIBLE;
            this.Window.AllowUserResizing = Settings.ALLOW_USER_RESIZING;
            this.Window.IsBorderless = Settings.IS_BORDERLESS;
            this.Window.AllowAltF4 = Settings.ALLOW_RAGE_QUIT;
            this.IsFixedTimeStep = Settings.IS_FIXED_TIME_STEP;
            this.TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 33);

            //CENTER THE APPLICATION, PEFECTLY CENTER WHEN THE APP IS BORDERLESS
            this.Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (graphics.PreferredBackBufferWidth / 2),
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (graphics.PreferredBackBufferHeight / 2));

            this.Activated += (sender, args) => { this.Window.Title = "♥ Active ♥"; };
            this.Deactivated += (sender, args) => { this.Window.Title = "♥ UnActive ♥"; };

            Content.RootDirectory = "Content";
        }
        
        //Only specific shit
        protected override void OnActivated(object sender, EventArgs args)
        {
            Window.IsBorderless = true;
            base.OnActivated(sender, args);
        }

        //Only specific shit
        protected override void OnDeactivated(object sender, EventArgs args)
        {
            Window.IsBorderless = false;
            base.OnDeactivated(sender, args);
        }

        //Try to play a song LUL
        /*
        protected override void OnExiting(object sender, EventArgs args)
        {
            base.OnExiting(sender, args);
        }*/

        protected override void Initialize()
        {
            position = new Vector2(500, 0);

            //texture = new Texture2D(this.GraphicsDevice, 500, 500);
            /*
            Color[] colorData = new Color[500 * 500];

            for (int i = 0; i < 250000; i++)
                colorData[i] = Settings.LOADING_TEXTURE_COLOR_SPRITE;

            texture.SetData<Color>(colorData);
            */

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("Textures/ghost");// deux ghost png/jpeg -> png go first ??
        }
        
        protected override void UnloadContent()
        {
            texture.Dispose();
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (IsActive)//pause the game if not focus
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                position.X -= 60.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                /*
                if (position.X > 450)
                    position.X -= 10;

                if (position.X > this.GraphicsDevice.Viewport.Width)
                    position.X = 500;
                    */
                /*
            if (position.X > 400)
                position.X -= 3;
            else if (position.X < 300)
                position.X -= 1;
            */
                if (position.X < -400)
                    position.X = 400;

                //gameTime.IsRunningSlowly
                base.Update(gameTime);
            }
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Settings.BACKGROUND_COLOR);
            //gameTime.TotalGameTime -> on pause still increased
            //this.InactiveSleepTime - gameTime.TotalGameTime for the true one

            var fps = 1 / gameTime.ElapsedGameTime.TotalSeconds;
            Window.Title = fps.ToString();

            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Settings.DONT_AFFECT_COLOR_SPRITE);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
