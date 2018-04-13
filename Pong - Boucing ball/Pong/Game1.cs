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

            var ballTexture = Content.Load<Texture2D>("Textures/Ball");
            var Dur = Content.Load<Texture2D>("Textures/Paddle");

            _score = new Score(Content.Load<SpriteFont>("Font"));//C:/Windows/Fonts,

            _twoDirMmtStaticSprite = new List<Sprite>
            {
                new Sprite(Dur)
                {
                    Position = new Vector2(80,100),
                    Colour = Color.Violet,
                },

                new Sprite(Dur)
                {
                    Position = new Vector2(210,100),
                    Colour = Color.Violet,
                },

                new Sprite(Dur)
                {
                    Position = new Vector2(200,398),
                    Colour = Color.Violet,
                },

                new Sprite(Dur)
                {
                    Position = new Vector2(368,100),
                    Colour = Color.Violet,
                },

                new Ball(ballTexture)
                {
                    Position = new Vector2((ScreenWidth / 2) - (ballTexture.Width / 2), (ScreenHeight /2) - (ballTexture.Height /20)),
                    Score = _score,
                    Speed = 10f,
                    Colour = Color.Red,
                },

                new Ball(ballTexture)
                {
                    Position = new Vector2(500, 8),
                    Score = _score,
                    Speed = 10f,
                },

                new Ball(ballTexture)
                {
                    Position = new Vector2(200, 300),
                    Score = _score,
                    Speed = 10f,
                },

                new Ball(ballTexture)
                {
                    Position = new Vector2(250, 400),
                    Score = _score,
                    Speed = 10f,
                },

                new Ball(ballTexture)
                {
                    Position = new Vector2(400, 400),
                    Score = _score,
                    Speed = 10f,
                },

                new Ball(ballTexture)
                {
                    Position = new Vector2(400, 400),
                    Score = _score,
                    Speed = 10f,
                },

                new Ball(ballTexture)
                {
                    Position = new Vector2(10,50),
                    Score = _score,
                    Speed = 25f,
                },

                new Ball(ballTexture)
                {
                    Position = new Vector2(50,87),
                    Score = _score,
                    Speed = 7f,
                },

                new Ball(ballTexture)
                {
                    Position = new Vector2(7,100),
                    Score = _score,
                    Speed = 9f,
                },

                new Ball(ballTexture)
                {
                    Position = new Vector2(90,80),
                    Score = _score,
                    Speed = 20f,
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
