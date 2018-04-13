using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Puissance4
{
    class Grid
    {
        public Jeton[,] GridElems { get; set; }
        private static Grid m_instance;
        private Texture2D blank;
        private Player winner;
        public Player Winner { get { return this.winner; } }

        public static Grid Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new Grid();
                return m_instance;
            }
        }

        private Grid()
        {
            GridElems = new Jeton[7, 6];
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    GridElems[x, y] = null;
                }
            }

            winner = null;
        }

        public int GetY(int x)
        {
            int _y = 5;
            while (_y >= 0 && GridElems[x, _y] != null)
            {
                _y--;
            }
            return _y;
        }

        public bool AddToX(int x, Jeton j)
        {
            int y = GetY(x);
            if (y != -1)
            {
                GridElems[x, y] = j;
                return true;
            }
            return false;    
        }

        public void Clean()
        {
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    GridElems[x, y] = null;
                }
            }

            winner = null;
        }

        public void Load(ContentManager Content)
        {
            blank = Content.Load<Texture2D>("grid");
        }

        public bool IsFull()
        {
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    if (GridElems[x, y] == null) return false;
                }
            }
            return true;
        }

        public void FindWinner(int x, int y, Player p)
        {
            if (DiagonaleW(x, y, p) || LineW(x, y, p))
            {
                winner = p;
            }
        }

        private Jeton GetElement(int x, int y)
        {
            if (x < 0 || x > 6 || y < 0 || y > 5)
            {
                return null;
            }
            return GridElems[x, y];
        }

        private bool LineW(int x, int y, Player p)
        {
            Jeton c = GetElement(x, y);
            int count = 0, tmp = 0, tx = x, ty = y;
            // Horizontal line right => x + 1, y
            while (c != null && c.Player == p)
            {
                tx = (tx + 1) % 7;
                c = GetElement(tx, ty);
                tmp++;
            }

            // Horizontal line left => x - 1, y
            tx = (x - 1) % 7;
            c = GetElement(tx, ty);
            while (c != null && c.Player == p)
            {
                tx = (tx - 1) % 7;
                c = GetElement(tx, ty);
                tmp++;
            }

            count = tmp;

            // Reinitialize
            tx = x;
            ty = y;
            tmp = 0;
            c = GetElement(tx, ty);

            // Vertical line top => x, y - 1
            while (c != null && c.Player == p)
            {
                ty = (ty - 1) % 6;
                c = GetElement(tx, ty);
                tmp++;
            }

            // Vertical line bottom => x, y + 1
            ty = (y + 1) % 6;
            c = GetElement(tx, ty);
            while (c != null && c.Player == p)
            {
                ty = (ty + 1) % 6;
                c = GetElement(tx, ty);
                tmp++;
            }

            count = Math.Max(tmp, count);
            if (count >= 4) return true;
            return false;
        }

        private bool DiagonaleW(int x, int y, Player p)
        {
            Jeton c = GetElement(x, y);
            int count = 0, tmp = 0, tx = x, ty = y;

            // Descendant droite => x + 1, y - 1
            while (c != null && c.Player == p)
            {
                tx = (tx + 1) % 7;
                ty = (y - 1) % 6;
                c = GetElement(tx, ty);
                tmp++;
            }

            // Ascendant gauche => x - 1, y + 1
            tx = (x - 1) % 7;
            ty = (y + 1) % 6;
            c = GetElement(tx, ty);
            while (c != null && c.Player == p)
            {
                tx = (tx - 1) % 7;
                ty = (ty + 1) % 6;
                c = GetElement(tx, ty);
                tmp++;
            }

            count = tmp;

            // Reinitialize
            tmp = 0;
            c = GetElement(tx, ty);
            tx = x; ty = y;

            // Descendant gauche => x - 1, y - 1
            while (c != null && c.Player == p)
            {
                tx = (tx - 1) % 7;
                ty = (ty - 1) % 6;
                c = GetElement(tx, ty);
                tmp++;
            }

            // Ascendant droite => x + 1, y + 1
            tx = (x + 1) % 7;
            ty = (y + 1) % 6;
            c = GetElement(tx, ty);
            while (c != null && c.Player == p)
            {
                tx = (tx + 1) % 7;
                ty = (ty + 1) % 6;
                c = GetElement(tx, ty);
                tmp++;
            }

            count = Math.Max(tmp, count);

            if (count >= 4) return true;
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    Vector2 position = new Vector2(offset.X + x * 70, offset.Y + y * 70);
                    spriteBatch.Draw(blank, position, Color.White);
                    if (GridElems[x, y] != null)
                    {
                        position += new Vector2(5, 5);
                        spriteBatch.Draw(GridElems[x, y].GetTexture(), position, Color.White);
                    }
                }
            }
        }
    }
}
