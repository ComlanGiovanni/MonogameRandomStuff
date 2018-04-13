using Microsoft.Xna.Framework.Graphics;

namespace Puissance4
{
    class Jeton
    {
        private Texture2D texture;
        public Player Player
        {
            get; set;
        }

        public Jeton(Texture2D t, Player p)
        {
            this.texture = t;
            Player = p;
        }

        public Texture2D GetTexture()
        {
            return texture;
        }
    }
}
