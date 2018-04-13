using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace scoreSystem.Managers
{
    public class Sprite //Can be access by anywhere
    {
        protected Texture2D _texture;
        public Vector2 Position;
        public Color Colour = Models.Settings.DONT_AFFECT_COLOR_SPRITE;
        public float Speed;
        public Input Input;

        public bool IsRemoved;

        //Because the _texture is private u need to get  width and heigh with a get
        // for my game maybe get get the w/h into the animation manager ? _texture is null maybe we ask for
        //the w/h when is not get yet
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        //One constructor
        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Colour);
        }


    }
}
