using Microsoft.Xna.Framework.Graphics;

namespace Puissance4
{
    class Player
    {
        public bool Turn { get; set; }
        private string m_name;
        private Jeton jeton;

        public string Name { get { return this.m_name; } }

        public Player(string name, Texture2D t)
        {
            m_name = name;
            Turn = false;
            jeton = new Jeton(t, this);
        }

        public bool AddToX(int x)
        {
            if (Turn)
            {
                int y = Grid.Instance.GetY(x);
                if (Grid.Instance.AddToX(x, jeton))
                {
                    Grid.Instance.FindWinner(x, y, this);
                    return true;
                }
                return false;
            }
            return false;
        }

        public Jeton GetJeton()
        {
            return this.jeton;
        }

    }
}
