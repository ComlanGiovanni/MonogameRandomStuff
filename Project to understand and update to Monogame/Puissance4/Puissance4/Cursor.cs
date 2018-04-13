using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Puissance4
{
    class Cursor
    {
        private Player current;
        private Player p1, p2;
        private int cursorPosition = 1;
        private bool lefted = false, righted = false, space = false;

        public Cursor(Player p1, Player p2)
        {
            this.current = p1;
            this.p1 = p1;
            this.p2 = p2;
        }

        public void Reinit()
        {
            current = p1;
            cursorPosition = 1;
        }

        public void Update()
        {
            if (!lefted && Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                cursorPosition = Math.Max(cursorPosition - 1, 1);
                lefted = true;
            }
            else if (!righted && Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                cursorPosition = Math.Min(cursorPosition + 1, 7);
                righted = true;
            }

            if (lefted && Keyboard.GetState().IsKeyUp(Keys.Left))
            {
                lefted = false;
            }
            else if (righted && Keyboard.GetState().IsKeyUp(Keys.Right))
            {
                righted = false;
            }

            if (Grid.Instance.Winner == null && !space && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (p1.Turn)
                {
                    if (p1.AddToX(cursorPosition - 1))
                    {
                        p1.Turn = false;
                        p2.Turn = true;
                        current = p2;
                    }
                }
                else if (p2.Turn)
                {
                    if (p2.AddToX(cursorPosition - 1))
                    {
                        p1.Turn = true;
                        p2.Turn = false;
                        current = p1;
                    }
                }
                space = true;
            }

            if (space && Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                space = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            Texture2D c = current.GetJeton().GetTexture();
            spriteBatch.Draw(c, new Vector2(offset.X - (c.Width + 5) + cursorPosition * 70, 
                offset.Y - c.Height - 10), Color.White);
        }
    }
}
