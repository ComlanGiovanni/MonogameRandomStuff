using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace inputManagerClass
{

    public class Sprite //Can be access by anywhere
    {
        /*
         * Texture2D -> using Microsoft.Xna.Framework.Graphics;
         * Vector2 -> using Microsoft.Xna.Framework.Graphics;
         * Keyboard -> using Microsoft.Xna.Framework.Input;
         * SpriteBatch -> using Microsoft.Xna.Framework.Graphics
         *
         *
         *
         **/
        private Texture2D _texture;
        public Vector2 Position;
        public float Speed = 3f;
        public Input Input;
        
        //Constructor take the texture for the Draw Method

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }


        public void Update()
        {
            Move();
        }

        //Method for mouvement
        private void Move()//Only for this class
        {
            if (Input == null)//if nto key is pressed
                return;//just and coutinue the instruction

            //if the keys.Up/Down/Left/Right is just down do the mouvement
            if (Keyboard.GetState().IsKeyDown(Input.Up))//Up
            {
                Position.Y -= Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Down))//Down
            {
                Position.Y += Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Left))//Left
            {
                Position.X -= Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Right))//Right
            {
                Position.X += Speed;
            }
        }

        //For the Draw we need the  to draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);//make a Settigns classfor those varaible
        }
    }
}
