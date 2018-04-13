using FixAnimatedSprite.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FixAnimatedSprite
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<Sprite> _sprites;
        //private float _timer;

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
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (graphics.PreferredBackBufferHeight / 2));

            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var animations = new Dictionary<string, Animation>()
            {
                {"WalkUp", new Animation(Content.Load<Texture2D>("sprites/MarioCoinAnimated"), 6)},
            };

            _sprites = new List<Sprite>()
            {
                new Sprite(animations)
                {
                    Position = new Vector2(500, 500),
                },
            };

        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //_timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var sprite in _sprites)
                sprite.Update(gameTime, _sprites);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            foreach (var sprite in _sprites)
                sprite.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
