using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FF.Models;
using Microsoft.Xna.Framework;

namespace Pong.Managers
{
    public class Score
    {
        public int Score1;
        public int Score2;
        public Vector2 positionS1 = new Vector2(320, 70);
        public Vector2 positionS2 = new Vector2(430, 70);

        private SpriteFont _font;

        public Score(SpriteFont font)
        {
            _font = font;
        }

        public void  Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, Score1.ToString(), positionS1, Settings.DONT_AFFECT_COLOR_SPRITE);
            spriteBatch.DrawString(_font, Score1.ToString(), positionS1, Settings.DONT_AFFECT_COLOR_SPRITE);
        }
    }
}
