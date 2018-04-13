using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FF.Models;
using System;
using Pong.Managers;
using System.Collections.Generic;
using FF.Sprites;
using Pong.Sprites;

namespace Pong
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;

        public static Random Random;

        private Score _score;
        private List<Sprite> _twoDirMmtStaticSprite;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            ScreenWidth = graphics.PreferredBackBufferWidth;
            ScreenHeight = graphics.PreferredBackBufferHeight;

            Random = new Random();
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var batTexture = Content.Load<Texture2D>("Textures/Paddle");
            var ballTexture = Content.Load<Texture2D>("Textures/Ball");

            _score = new Score(Content.Load<SpriteFont>("Font"));

            _twoDirMmtStaticSprite = new List<Sprite>
            {
                //new TwoDirMmtStaticSprite(Content.Load<Texture2D>("Textures/Background")),

                new Bat(batTexture)
                {
                    Position = new Vector2(20, (ScreenHeight /2) - (batTexture.Height/2)),

                    Input = new Input()
                    {
                        Up = Keys.Up,
                        Down = Keys.Down,
                    }
                },

                new Bat(batTexture)
                {//oRIGINE OF A TEXTURE
                    Position = new Vector2(ScreenWidth - 20 - batTexture.Width, (ScreenHeight /2) - (batTexture.Height/2)),

                    Input = new Input()
                    {
                        Up = Keys.Z,
                        Down = Keys.S,
                    }
                },

                new Ball(ballTexture)
                {
                    Position = new Vector2((ScreenWidth / 2) - (ballTexture.Width / 2), (ScreenHeight /2) - (ballTexture.Height /20)),
                    Score = _score,
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

            foreach (var sprite in _twoDirMmtStaticSprite)
                sprite.Update(gameTime, _twoDirMmtStaticSprite);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (var sprite in _twoDirMmtStaticSprite)
                sprite.Draw(spriteBatch);

            _score.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
