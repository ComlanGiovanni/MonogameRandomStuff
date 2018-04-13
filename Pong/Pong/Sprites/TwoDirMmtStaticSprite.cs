using FF.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FF.Sprites
{
    public class Sprite
    {
        protected Texture2D _texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public Color Colour = Settings.DONT_AFFECT_COLOR_SPRITE;
        public float Speed;
        public Input Input;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(_texture, Position, Colour);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> twoDirMmtStaticSprite)//Vitrual can be ovveride
        {
           
        }

        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
                   this.Rectangle.Left < sprite.Rectangle.Left &&
                   this.Rectangle.Bottom > sprite.Rectangle.Top &&
                   this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
                   this.Rectangle.Right > sprite.Rectangle.Right &&
                   this.Rectangle.Bottom > sprite.Rectangle.Top &&
                   this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
                   this.Rectangle.Top < sprite.Rectangle.Top &&
                   this.Rectangle.Right > sprite.Rectangle.Left &&
                   this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
                   this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
                   this.Rectangle.Right > sprite.Rectangle.Left &&
                   this.Rectangle.Left < sprite.Rectangle.Right;
        }
    }
}
