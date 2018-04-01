using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace manipulationOfTexture
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D coinMario, birdFly, canardBain/*, idlleMan*/, starMario;
        Vector2 posCoinMario, posCanardBain, posStarMario/*,posIdlleMan*/;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = ScreenSettings.SCREEN_HEIGHT,
                PreferredBackBufferWidth = ScreenSettings.SCREEN_WIDTH
            };
            graphics.ApplyChanges();

            this.IsMouseVisible = ScreenSettings.IS_MOUSE_VISIBLE;
            this.Window.IsBorderless = ScreenSettings.IS_BORDERLESS;
            
            this.Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) 
                - (graphics.PreferredBackBufferWidth / 2),
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) 
                - (graphics.PreferredBackBufferHeight / 2));

            Content.RootDirectory = "Content";

            posCoinMario = new Vector2(-16, 872);
            posCanardBain = new Vector2(842, 842);
            //posIdlleMan = new Vector2(600, 600);
            posStarMario = new Vector2(0, 0);
        }
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            coinMario = this.Content.Load<Texture2D>("Textures/png/coinM");
            birdFly = this.Content.Load<Texture2D>("Textures/jpg/birdF");
            canardBain = this.Content.Load<Texture2D>("Textures/png/canardB");
            //idlleMan = this.Content.Load<Texture2D>("Textures/gifs/idlleMan");
            starMario = this.Content.Load<Texture2D>("Textures/png/starM");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //Infinte movement LUL
            posCoinMario.Y--;
            posCanardBain.X--;

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(coinMario, posCoinMario, ScreenSettings.DONT_AFFECT_COLOR_SPRITE);
            //spriteBatch.Draw(birdFly, new Rectangle(50,50, birdFly.Width - + / * 500, birdFly.Height - + / *500), ScreenSettings.DONT_AFFECT_COLOR_SPRITE);
            spriteBatch.Draw(birdFly, new Rectangle(
                ((ScreenSettings.SCREEN_HEIGHT/2) - (300 /2)),
                ((ScreenSettings.SCREEN_WIDTH/2) - (300 / 2))
                , 300,300), Color.Red);
            //spriteBatch.Draw(canardBain, posCanardBain, null, ScreenSettings.DONT_AFFECT_COLOR_SPRITE, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally|SpriteEffects.FlipVertically, 0);
            spriteBatch.Draw(canardBain, posCanardBain, null, ScreenSettings.DONT_AFFECT_COLOR_SPRITE, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
            //spriteBatch.Draw(idlleMan, posIdlleMan, null, ScreenSettings.DONT_AFFECT_COLOR_SPRITE, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(starMario, posStarMario, null, ScreenSettings.DONT_AFFECT_COLOR_SPRITE, -45f, new Vector2(starMario.Height/2,starMario.Width/2), 1, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
