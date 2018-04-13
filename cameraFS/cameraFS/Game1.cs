using cameraFS.Core;
using cameraFS.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace cameraFS
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Camera _camera;

        private List<Component> _components;

        private Player _player;

        public static int ScreenH;

        public static int screenw;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = Models.Settings.SCREEN_HEIGHT,
                PreferredBackBufferWidth = Models.Settings.SCREEN_WIDTH
            };
            graphics.ApplyChanges();
            
            this.IsMouseVisible = Models.Settings.IS_MOUSE_VISIBLE;
            this.Window.IsBorderless = Models.Settings.IS_BORDERLESS;

            this.Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (graphics.PreferredBackBufferWidth / 2),
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - ((graphics.PreferredBackBufferHeight + 40) / 2));//80 :^)
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            ScreenH = graphics.PreferredBackBufferHeight;
            screenw = graphics.PreferredBackBufferWidth;

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _camera = new Camera();

            _player = new Player(Content.Load<Texture2D>("Textures/png"));

            _components = new List<Component>()
            {
                new Sprite(Content.Load<Texture2D>("Textures/pixel-art-background-8")),
                _player,
                new Sprite(Content.Load<Texture2D>("Textures/bl")),
            };
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var component in _components)
                component.Update(gameTime);

            _camera.Follow(_player);
            
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: _camera.Transform);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
