using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using scoreSystem.Managers;

namespace scoreSystem.Sprites
{
    public class Player : Sprite 
    {
        public int Score;

        public Player(Texture2D texture) : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();

            foreach (var sprite in sprites)
            {
                if (sprite is Player)
                    continue;

                if(sprite.Rectangle.Intersects(this.Rectangle))
                {
                    Score++;
                    sprite.IsRemoved = true;
                }
            }
        }

        private void Move()
        {

            //base.Update(gameTime, sprites);

            if (Input == null)//if nto key is pressed
                return;//just and coutinue the instruction

            //Diagonal direction possible
            if (Keyboard.GetState().IsKeyDown(Input.Up))
                Position.Y -= Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
                Position.Y += Speed;

            if (Keyboard.GetState().IsKeyDown(Input.Left))
                Position.X -= Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
                Position.X += Speed;

            //try to implement this on First game project
            Position = Vector2.Clamp(Position, new Vector2(0, 0), new Vector2(Models.Settings.SCREEN_WIDTH - this.Rectangle.Width, Models.Settings.SCREEN_HEIGHT - this.Rectangle.Height));

        }
    }
}
