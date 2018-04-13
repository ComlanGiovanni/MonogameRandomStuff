using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jumpAndGravity
{
    public class Character
    {
        public Texture2D texture;

        public Vector2 position;
        public Vector2 velocity;

        public bool hasJumped;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }

        public Character(Texture2D newTexture, Vector2 newPosistion)
        {
            texture = newTexture;
            position = newPosistion;
            hasJumped = true;
        }

        public void Update(GameTime gameTime, SoundEffect effect, SoundEffect effect1)
        {
            position += velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X = 3f;
                effect1.Play();
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -3f;
                effect1.Play();
            }

            else velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 10f;
                velocity.Y = -8f;
                hasJumped = true;
                effect.Play();
            }

            float i = 1;
            velocity.Y += 0.15f * i;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
