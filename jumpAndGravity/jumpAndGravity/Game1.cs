using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace jumpAndGravity
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Character player;

        List<Platform> platforms = new List<Platform>();

        SoundEffect effect, grassWalk;
        Song song;

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

            player = new Character(Content.Load<Texture2D>("batman"), new Vector2(50,50));
            
            platforms.Add(new Platform(Content.Load<Texture2D>("Platform"), new Vector2(0, 400)));
            platforms.Add(new Platform(Content.Load<Texture2D>("Platform"), new Vector2(200, 400)));
            platforms.Add(new Platform(Content.Load<Texture2D>("Platform"), new Vector2(400, 400)));
            platforms.Add(new Platform(Content.Load<Texture2D>("Platform"), new Vector2(600, 400)));
            platforms.Add(new Platform(Content.Load<Texture2D>("Platform"), new Vector2(250, 200)));
            platforms.Add(new Platform(Content.Load<Texture2D>("Platform"), new Vector2(450, 100)));

            effect = Content.Load<SoundEffect>("Jump");
            grassWalk = Content.Load<SoundEffect>("grassWalk");

            song = Content.Load<Song>("themeSong");

            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Platform platform in platforms)
                if(player.Rectangle.IsOnTopOf(platform.rectangle))
                {
                    player.velocity.Y = 0f;
                    player.hasJumped = false;
                }

            player.Update(gameTime, effect, grassWalk);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            foreach (Platform platform in platforms)
                platform.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

static class RectangleHelper
{
    const int penetrationMargin = 5;

    public static bool IsOnTopOf(this Rectangle r1, Rectangle r2)
    {
        return (r1.Bottom >= r2.Top - penetrationMargin &&
            r1.Bottom <= r2.Top + (r2.Height / 2) &&
            r1.Right >= r2.Left + 5 &&
            r1.Left <= r2.Right - 5);
    }

    public static bool IsOnLeft(this Rectangle r1, Rectangle r2)
    {
        return (
            r1.Left >= r2.Right - 1 &&
            r1.Left <= r2.Right + 1 &&
            r1.Bottom >= r2.Top + penetrationMargin &&
            r1.Top <= r2.Bottom - penetrationMargin
            );
    }

    public static bool IsOnRight(this Rectangle r1, Rectangle r2)
    {
        return (
            r1.Right >= r2.Left - 1 &&
            r1.Right <= r2.Left + 1 &&
            r1.Bottom >= r2.Top + penetrationMargin &&
            r1.Top <= r2.Bottom - penetrationMargin
            );
    }
}

