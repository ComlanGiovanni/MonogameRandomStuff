using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace inputKMG
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 originM;
        Texture2D persoN, persoC, cursor1, btnExit;
        Vector2 posPersoN, posPersoC, poscursor1, posBtnExit;
        KeyboardState previousState;

        Point mousePoint;
        Rectangle rectangle;

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
        }

        protected override void Initialize()
        {
            previousState = Keyboard.GetState();
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            persoN = this.Content.Load<Texture2D>("Textures/Personnages/persoMouv");
            persoC= this.Content.Load<Texture2D>("Textures/Personnages/persoMouv1");
            cursor1 = this.Content.Load<Texture2D>("Textures/Cursor/cursor1");
            btnExit = this.Content.Load<Texture2D>("Textures/Ux/exit");


           // origin = new Vector2(persoN.Height / 2, persoN.Width / 2);
            posPersoN = new Vector2((ScreenSettings.SCREEN_HEIGHT / 2) - (persoN.Height / 2),
            (ScreenSettings.SCREEN_WIDTH / 2) - (persoN.Width / 2));
            posPersoC = new Vector2(100, 100);
            poscursor1 = new Vector2(700, 700);
            posBtnExit = new Vector2((ScreenSettings.SCREEN_HEIGHT - btnExit.Height),0);
            originM = new Vector2(cursor1.Height / 2, cursor1.Width / 2);
        }

        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            /*
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();*/
            KeyboardState stateK = Keyboard.GetState();
            MouseState stateM = Mouse.GetState();

            
            mousePoint = new Point(stateM.X, stateM.Y);
            rectangle = new Rectangle((ScreenSettings.SCREEN_HEIGHT - btnExit.Height), 0, btnExit.Width, btnExit.Height);

            if (rectangle.Contains(mousePoint) && stateM.LeftButton == ButtonState.Pressed) //&& stateM.LeftButton == ButtonState.Released
                Exit();
            
            if (stateK.IsKeyDown(Keys.Escape))
                Exit();

            //Print it on the windows
            System.Text.StringBuilder sb = new StringBuilder();
            foreach (var key in stateK.GetPressedKeys())
                sb.Append("Keys :").Append(key).Append(" Pressed");

            if (sb.Length > 0)
                System.Diagnostics.Debug.WriteLine(sb.ToString());
            else
                System.Diagnostics.Debug.WriteLine("NO KEYS PRESSED");

            System.Diagnostics.Debug.WriteLine(stateM.X.ToString() + "," + stateM.Y.ToString());

            if (stateK.IsKeyDown(Keys.Right))
                posPersoN.X += 10;
            else if (stateK.IsKeyDown(Keys.Left))
                posPersoN.X -= 10;
            else if (stateK.IsKeyDown(Keys.Up))
                posPersoN.Y -= 10;
            else if (stateK.IsKeyDown(Keys.Down))
                posPersoN.Y += 10;

            //NEED SOME CLASSE HERE
            
            if (stateK.IsKeyDown(Keys.NumPad6) && !previousState.IsKeyDown(Keys.NumPad6))
                posPersoC.X += 10;
            else if (stateK.IsKeyDown(Keys.NumPad4) && !previousState.IsKeyDown(Keys.NumPad4))
                posPersoC.X -= 10;
            else if (stateK.IsKeyDown(Keys.NumPad8) && !previousState.IsKeyDown(Keys.NumPad8))
                posPersoC.Y -= 10;
            else if (stateK.IsKeyDown(Keys.NumPad2) && !previousState.IsKeyDown(Keys.NumPad2))
                posPersoC.Y += 10;
                

            previousState = stateK;

            poscursor1.X = stateM.X;
            poscursor1.Y = stateM.Y;

            if(stateM.LeftButton == ButtonState.Pressed && this.IsActive)// or just make the game in pause
                cursor1= this.Content.Load<Texture2D>("Textures/Cursor/cursor2");
            else if (stateM.LeftButton == ButtonState.Released && this.IsActive)
                cursor1 = this.Content.Load<Texture2D>("Textures/Cursor/cursor1");

            //tateM.LeftButton == ButtonState.Pressed && stateM.LeftButton == ButtonState.Released fast click lol

            /* IN THE WINDOWS AREA
            && stateM.X >= 0 && stateM.X < graphics.PreferredBackBufferWidth
             && stateM.Y >= 0 && stateM.Y < graphics.PreferredBackBufferHeight
            */

            /*WINDOWS FORM
             && System.Windows.Forms.Form.ActiveForm != null
             && System.Windows.Forms.Form.ActiveForm.Text.Equals(this.Window.Title))
             */
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ScreenSettings.DONT_AFFECT_COLOR_SPRITE);

            spriteBatch.Begin();
            spriteBatch.Draw(persoN, posPersoN, null, ScreenSettings.DONT_AFFECT_COLOR_SPRITE);
            spriteBatch.Draw(persoC, posPersoC, null, ScreenSettings.DONT_AFFECT_COLOR_SPRITE);
            spriteBatch.Draw(btnExit, posBtnExit, null, ScreenSettings.DONT_AFFECT_COLOR_SPRITE);

            //spriteBatch.Draw(cursor1, new Rectangle(100, 400, 100, 100), null, ScreenSettings.DONT_AFFECT_COLOR_SPRITE, 0, new Vector2(250, 250), SpriteEffects.None, 0);
            //spriteBatch.Draw(cursor1, new Rectangle(0, 0, 100, 100), null, ScreenSettings.DONT_AFFECT_COLOR_SPRITE, 0, new Vector2(250, 250), SpriteEffects.None, 0);
            //spriteBatch.Draw(cursor1, poscursor1,null,null, originM, 0, null, null, SpriteEffects.None,0);
            //spriteBatch.Draw(cursor1, poscursor1,null,null, originM, 0, null, null, SpriteEffects.None,0);
            //spriteBatch.Draw(cursor1, poscursor1, null, ScreenSettings.DONT_AFFECT_COLOR_SPRITE, 0, originM, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(cursor1, poscursor1, null, ScreenSettings.DONT_AFFECT_COLOR_SPRITE, 0, originM, 1, SpriteEffects.None, 0);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
