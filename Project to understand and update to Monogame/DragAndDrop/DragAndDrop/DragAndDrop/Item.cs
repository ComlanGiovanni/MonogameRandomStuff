using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DragAndDrop
{

    public class Item : IDragAndDropItem
    {

        #region IDragAndDropItem implementation

        public Vector2 Position { get; set; }
        public bool IsSelected { get; set; }
        public bool IsMouseOver { get; set; }
        public bool Contains(Vector2 pointToCheck)
        {
            Point mouse = new Point((int)pointToCheck.X, (int)pointToCheck.Y);
            return Border.Contains(mouse);
        }

        #endregion

        #region Properties and variables

        public Texture2D Texture { get; set; }
        private readonly SpriteBatch _spriteBatch;

        public Rectangle Border
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        #endregion

        #region Constructor and Draw

        public Item(SpriteBatch spriteBatch, Texture2D texture, Vector2 position)
        {
            _spriteBatch = spriteBatch;
            Texture = texture;
            Position = position;
        }

        public void Draw(GameTime gameTime)
        {
            Color colorToUse = Color.White;
            if (IsSelected)
            {
                colorToUse = Color.Orange;
            }
            else
            {
                if (IsMouseOver) { colorToUse = Color.Cyan; }
            }
            _spriteBatch.Draw(Texture, Position, colorToUse);
        }

        #endregion

    }
}