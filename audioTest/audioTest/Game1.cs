using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace audioTest
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Song song;
        //SoundEffect songFX;
        List<SoundEffect> soundFxList;
        //SongCollection songcollec;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            soundFxList = new List<SoundEffect>();
        }
        
        protected override void Initialize()
        {
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //song = Content.Load<Song>("Song/Short/inception");
            //MediaPlayer.Play(song);
            //MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            soundFxList.Add(Content.Load<SoundEffect>("Song/Short/Explosion6"));
            soundFxList.Add(Content.Load<SoundEffect>("Song/Short/Pickup_Coin8"));
            soundFxList.Add(Content.Load<SoundEffect>("Song/Short/Randomize3"));

            //soundFxList[0].Play();
            //soundFxList[1].Play(); /*Same time lol*/

            var instance = soundFxList[0].CreateInstance();
            //instance.IsLooped = true;
            //instance.Play();

        }
        /*
        private void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            //MediaPlayer.Volume -= -0.1f;
            MediaPlayer.Play(song);
        }
        */

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad1))
                soundFxList[0].CreateInstance().Play();
            else if (Keyboard.GetState().IsKeyDown(Keys.NumPad2))
                soundFxList[1].CreateInstance().Play();
            else if (Keyboard.GetState().IsKeyDown(Keys.NumPad3))
                soundFxList[2].CreateInstance().Play();

            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (SoundEffect.MasterVolume == 0.0f)//Muet
                    SoundEffect.MasterVolume = 1.0f;//FullVolume
                else
                    SoundEffect.MasterVolume = 0.0f;//No Volume
            }


            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
