using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace inputManagerClass
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //using System.Collections.Generic;
        private List<Sprite> _sprites;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var texture = Content.Load<Texture2D>("block");

            //list of Sprite
            _sprites = new List<Sprite>()
            {
                //First Sprite
                new Sprite(texture)//initialisation........Can be animation
                {
                    Position = new Vector2(100, 100),
                    Input = new Input()//New instance
                    {
                        //assigne value.....Default is keys.None
                        Up = Keys.Z,
                        Down = Keys.S,
                        Left = Keys.Q,
                        Right = Keys.D,
                    }
                },


                //Second Sprite
                new Sprite(texture)
                 {
                    Position = new Vector2(200, 200),
                    Input = new Input()//New instance
                    {
                        //assigne value.....Default is keys.None
                        Up = Keys.NumPad8,
                        Down = Keys.NumPad5,
                        Left = Keys.NumPad4,
                        Right = Keys.NumPad6,
                    }
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

            // we can use a actual sprite but sprite can be animations for exemple
            foreach (var sprite in _sprites) //var auto detect the type
                sprite.Update();

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
