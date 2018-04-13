using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//ctrl + .

namespace SpriteObject
{
    public class Sprite
    {
        private Texture2D _texture;
        public Vector2 Position;

        public float Speed = 2f;

        //Constructor
        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public void  Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Z))//Up
            {
                Position.Y -= Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))//Down
            {
                Position.Y += Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Q))//left
            {
                Position.X -= Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))//right
            {
                Position.X += Speed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
