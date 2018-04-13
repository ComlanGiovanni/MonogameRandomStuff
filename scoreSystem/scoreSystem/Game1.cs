using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using scoreSystem.Managers;
using scoreSystem.Sprites;
using System;
using System.Collections.Generic;

namespace scoreSystem
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Random Random;

        private List<Sprite> _sprites;

        private SpriteFont _font;

        private float _timer;

        private Texture2D _appleTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = Models.Settings.SCREEN_HEIGHT,
                PreferredBackBufferWidth = Models.Settings.SCREEN_WIDTH
            };
            graphics.ApplyChanges();

            Random = new Random();

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

            var playerTexture = Content.Load<Texture2D>("ML");

            _sprites = new List<Sprite>()
            {
                new Player(playerTexture)
                {
                    Input = new Input()
                    {
                        Left = Keys.Q,
                        Right = Keys.D,
                        Up = Keys.Z,
                        Down = Keys.S,
                    },

                    Position = new Vector2(100,100),
                    Colour = Color.Red,
                    Speed = 10f,
                },

                new Player(playerTexture)
                {
                    Input = new Input()
                    {
                        Left = Keys.Left,
                        Right = Keys.Right,
                        Up = Keys.Up,
                        Down = Keys.Down,
                    },

                    //SAME DISTANCE BTW THE SCRENN
                    //Position = new Vector2(Models.Settings.SCREEN_WIDTH - 100 - playerTexture.Width,100),
                    Position = new Vector2(400,100),
                    Colour = Color.Blue,
                    Speed = 10f,
                },
            };

            _font = Content.Load<SpriteFont>("Font");
            _appleTexture = Content.Load<Texture2D>("MarioCoin");
        }

        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var sprite in _sprites)
                sprite.Update(gameTime, _sprites);

            PostUpdate();

            SpawnApple();

            base.Update(gameTime);
        }

        private void SpawnApple()
        {
            if (_timer > 1)
            {
                _timer = 0;

                var xPos = Random.Next(0, Models.Settings.SCREEN_WIDTH - _appleTexture.Width);
                var yPos = Random.Next(0, Models.Settings.SCREEN_HEIGHT - _appleTexture.Height);

                _sprites.Add(new Sprite(_appleTexture)
                {
                    Position = new Vector2(xPos, yPos),
                });
            }
        }

        private void PostUpdate()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            spriteBatch.Begin();

            foreach (var sprite in _sprites)
                sprite.Draw(spriteBatch);

            var fontY = 10;
            var i = 0;

            foreach (var sprite in _sprites)
            {
                if (sprite is Player)
                    spriteBatch.DrawString(_font, string.Format("Player {0}: {1}", ++i, ((Player)sprite).Score), new Vector2(10, fontY += 20), Color.Black);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}