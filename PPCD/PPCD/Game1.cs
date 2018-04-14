using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FF.Models;
using System;
using System.Collections.Generic;
using FF.Sprites;
using System.Diagnostics;

namespace PPCD
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //private List<MmtAnimatedSprite> _mmtAnimatedSprite;
        private List<MmtStaticSprite> _mmtStaticSprite;

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
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - ((graphics.PreferredBackBufferHeight + 40) / 2));
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //var cBall = new List<Animation>() { { new Animation(Content.Load<Texture2D>("SpriteSheet/colored-balls"), 5, 0.1f) }, };
            var aball = Content.Load<Texture2D>("Sprite/aBall");
            /*
            _mmtAnimatedSprite = new List<MmtAnimatedSprite>()
            {
                new MmtAnimatedSprite(cBall){Position = new Vector2(650, 800), Speed = 10f},
                new MmtAnimatedSprite(cBall){Position = new Vector2(49, 140), Speed = 5f},
                new MmtAnimatedSprite(cBall){Position = new Vector2(139, 512), Speed = 2f},
            };
            */

            _mmtStaticSprite = new List<MmtStaticSprite>()
            {
                new MmtStaticSprite(aball){Position = new Vector2(650, 800), Speed = 10f},
                new MmtStaticSprite(aball){Position = new Vector2(49, 140), Speed = 5f},
                new MmtStaticSprite(aball){Position = new Vector2(139, 512), Speed = 2f},
            };
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            /*
            foreach (var sprite in _mmtAnimatedSprite)
                sprite.Update(gameTime, _mmtAnimatedSprite);
            */

            foreach (var sprite in _mmtStaticSprite)
                sprite.Update(gameTime, _mmtStaticSprite);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            /*
            foreach (var sprite in _mmtAnimatedSprite)
                sprite.Draw(spriteBatch);
            */

            foreach (var sprite in _mmtStaticSprite)
                sprite.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
