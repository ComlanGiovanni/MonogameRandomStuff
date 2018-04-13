using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Puissance4
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Grid grid;
        Player p1, p2;
        Cursor cursor;
        SpriteFont font;
        Song back_song;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            grid = Grid.Instance;
            p1 = new Player("Player 1", Content.Load<Texture2D>("zero"));
            p2 = new Player("Player 2", Content.Load<Texture2D>("cross"));

            p1.Turn = true;

            grid.Load(Content);
            cursor = new Cursor(p1, p2);

            font = Content.Load<SpriteFont>("information");
            back_song = Content.Load<Song>("piano_loop");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(back_song);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                grid.Clean();
                p1.Turn = true;
                p2.Turn = false;
                cursor.Reinit();
            }

            cursor.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Vector2 viewport = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            Vector2 offset = new Vector2((viewport.X - 490) / 2, (viewport.Y - 420) / 2);

            // Texts
            string player1 = p1.Name + " turn";
            string player2 = p2.Name + " turn";

            GraphicsDevice.Clear(Color.WhiteSmoke);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            cursor.Draw(spriteBatch, offset);

            if (grid.IsFull() && grid.Winner == null)
            {
                string won = "Tie ! Press R to restart";
                Vector2 size = font.MeasureString(won);
                spriteBatch.DrawString(font, won, new Vector2((viewport.X - size.X) / 2, viewport.Y - size.Y - 20), Color.Black);
            }
            else
            {
                if (grid.Winner == null)
                {
                    if (p1.Turn)
                    {
                        Vector2 size = font.MeasureString(player1);
                        spriteBatch.DrawString(font, player1, new Vector2((viewport.X - size.X) / 2, viewport.Y - size.Y - 20), Color.Black);
                    }
                    else if (p2.Turn)
                    {
                        Vector2 size = font.MeasureString(player2);
                        spriteBatch.DrawString(font, player2, new Vector2((viewport.X - size.X) / 2, viewport.Y - size.Y - 20), Color.Black);
                    }
                }
                else
                {
                    string won = grid.Winner.Name + " won the game ! Press R to restart";
                    Vector2 size = font.MeasureString(won);
                    spriteBatch.DrawString(font, won, new Vector2((viewport.X - size.X) / 2, viewport.Y - size.Y - 20), Color.Black);
                }
            }

            grid.Draw(spriteBatch, offset);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
