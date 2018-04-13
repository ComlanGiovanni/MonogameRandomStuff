using Microsoft.Xna.Framework.Graphics;
using FF.Models;
using Microsoft.Xna.Framework;

namespace Pong.Managers
{
    public class Score
    {
        public int Score1;
        public SpriteFont _font;

        public Vector2 positionS1 = new Vector2(100,100);
        //public Vector2 positionS1 = new Vector2((int)(MiddlePoint.X - textSize.X), (int)(MiddlePoint.Y - textSize.Y));

        public Score(SpriteFont font)
        {
            _font = font;
        }

        public void  Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, Score1.ToString(), positionS1, Settings.DONT_AFFECT_COLOR_SPRITE);
        }
    }
}
