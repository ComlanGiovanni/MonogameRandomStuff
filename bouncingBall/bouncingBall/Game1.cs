using FF.Models;
using FF.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace bouncingBall
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<MmtAnimatedSprite> _mmtAnimatedSprite;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = Settings.SCREEN_HEIGHT,
                PreferredBackBufferWidth = Settings.SCREEN_WIDTH
            };
            graphics.ApplyChanges();

            this.IsMouseVisible = Settings.IS_MOUSE_VISIBLE;
            this.Window.IsBorderless = Settings.IS_BORDERLESS;

            this.Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (graphics.PreferredBackBufferWidth / 2),
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - ((graphics.PreferredBackBufferHeight + 40) / 2));//40 :^)

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var cBall = new List<Animation>() { { new Animation(Content.Load<Texture2D>("SpriteSheet/colored-balls"), 5, 0.1f) }, };

            _mmtAnimatedSprite = new List<MmtAnimatedSprite>()
            {
                new MmtAnimatedSprite(cBall){
                    /*Position = new Vector2(
                    ((graphics.PreferredBackBufferHeight/2) - (128/2)),
                    ((graphics.PreferredBackBufferWidth/2) - (128 / 2))),*/
                    Position = new Vector2(20,380),
                    BallSpeed = 0.1f
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

            foreach (var sprite in _mmtAnimatedSprite)
                sprite.Update(gameTime, _mmtAnimatedSprite);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (var sprite in _mmtAnimatedSprite)
                sprite.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
