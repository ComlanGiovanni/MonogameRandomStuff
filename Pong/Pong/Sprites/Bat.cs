using FF.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using FF.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Sprites
{
    public class Bat : Sprite
    {
        public Bat(Texture2D texture) : base(texture)
        {
            Speed = 5f;
        }

        public override void Update(GameTime gameTime, List<Sprite> twoDirMmtStaticSprite)
        {
            //base.Update(gameTime, twoDirMmtStaticSprite);
            if (Input == null)
                throw new Exception("A value for Input");

            if (Keyboard.GetState().IsKeyDown(Input.Up))
                Velocity.Y = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
                Velocity.Y = Speed;

            Position += Velocity;

            Position.Y = MathHelper.Clamp(Position.X, 0, Game1.ScreenWidth - _texture.Height);

            Velocity = Settings.THE_ORIGIN;
        }
    }
}
