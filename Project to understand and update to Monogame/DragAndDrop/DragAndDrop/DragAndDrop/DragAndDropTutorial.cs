using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DragAndDrop
{
    /// <summary>
    /// Sample code which shows how to use the DragAndDropControllerClass
    /// July 2013 - Jakob Krarup
    /// HTTP://WWW.XNAFAN.NET
    /// </summary>
    public class DragAndDropTutorial : Game
    {

        #region Variables and properties

        private SpriteBatch _spriteBatch;
        private SpriteFont _font, _bigFont;
        private DragAndDropController<Item> _dragDropController;
        private KeyboardState _keyboardState;

        private bool AControlKeyIsPressed { get { return _keyboardState.IsKeyDown(Keys.LeftControl) || _keyboardState.IsKeyDown(Keys.RightControl); } }
       
        #endregion

        #region Constructor and setup

        public DragAndDropTutorial()
        {
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("DefaultFont");
            _bigFont = Content.Load<SpriteFont>("BigFont");
            _dragDropController = new DragAndDropController<Item>(this, _spriteBatch);
            Components.Add(_dragDropController);
            SetupDraggableItems();
        }

        private void SetupDraggableItems()
        {
            Texture2D itemTexture = Content.Load<Texture2D>("item");
            _dragDropController.Clear();
            for (int i = 0; i < 10; i++)
            {
                Item item = new Item(_spriteBatch, itemTexture, new Vector2(50 + i * 70, 100));
                _dragDropController.Add(item);
            }
        }

        #endregion

        #region Update and related

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            HandleKeyboardInput();
        }

        private void HandleKeyboardInput()
        {
            _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyDown(Keys.Escape)) { Exit(); }
            if (_keyboardState.IsKeyDown(Keys.F5)) { SetupDraggableItems(); }
            if (_keyboardState.IsKeyDown(Keys.A) && AControlKeyIsPressed) { _dragDropController.SelectAll(); }
        } 

        #endregion

        #region Draw and related

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);
            _spriteBatch.Begin();
            foreach (var item in _dragDropController.Items) { item.Draw(gameTime); }
            WriteInfo();
            base.Draw(gameTime);
            _spriteBatch.End();
        }

        private void WriteInfo()
        {
            _spriteBatch.DrawString(_bigFont, "Drag and drop controller sample", new Vector2(100, 10), Color.LightGreen);
            _spriteBatch.DrawString(_font, "Items selected : " + _dragDropController.SelectedCount, new Vector2(20, 450), Color.LightGreen);
            _spriteBatch.DrawString(_font, "www.xnafan.net ", new Vector2(630, 450), Color.WhiteSmoke);
        }

        #endregion
        
    }
}