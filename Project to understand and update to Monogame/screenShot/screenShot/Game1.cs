using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace screenShot
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        #region Variables and properties

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MouseState _oldMouse, _currentMouse;
        KeyboardState _oldKeyboard, _currentKeyboard;
        Vector2 _currentMousePosition, _oldMousePosition;
        SpriteFont _defaultFont;
        private bool _allKeysReleased;
        Random _rnd = new Random();
        public enum MouseButtons { Left, Middle, Right, }

        //easy way of getting the windowsize
        public Vector2 WindowSize { get { return new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height); } }
        public Vector2 WindowCenter { get { return WindowSize / 2; } }

        #endregion


        #region Constructor and LoadContent

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _defaultFont = Content.Load<SpriteFont>("DefaultFont");
        }

        #endregion


        #region Update and Draw

        protected override void Update(GameTime gameTime)
        {
            UpdateStates();

            //this is a bit of a hack :)
            //Because pressing the ENTER key to start the hostgame in Windows may be perceived as the 
            //"start the currently selected game" ENTER press, we ensure that the ENTER key has been up by
            //by setting a variable to true the first time an Update with no keys pressed has been detected
            if (!_allKeysReleased) { _allKeysReleased = _currentKeyboard.GetPressedKeys().Length == 0; }

            // Allows the game to exit
            if (WasJustPressed(Keys.Escape)) { this.Exit(); }

            //toggles fullscreen
            if (WasJustPressed(Keys.F11)) { graphics.ToggleFullScreen(); }
            doScreenshot = WasJustPressed(Keys.F9);
            base.Update(gameTime);

            SaveStates();
        }


        bool doScreenshot = false;  //set this to true to perform screenshot

        protected override void Draw(GameTime gameTime)
        {

            //if necessary, we prepare for a screenshot 
            if (doScreenshot)
            {
                GraphicsDevice.PrepareScreenShot();
            }

            //Clear graphicsdevice with blue background
            GraphicsDevice.Clear(Color.Blue);

            //begin drawing
            spriteBatch.Begin();

            //write test string
            spriteBatch.DrawString(_defaultFont, "Screenshot test! :)", new Vector2(100, 100), Color.Yellow);

            //write whether we are running in HiDef or Reach graphics profile
            spriteBatch.DrawString(_defaultFont, "Profile: " + graphics.GraphicsProfile, new Vector2(100, 200), Color.Yellow);

            //call superclass' Draw()
            base.Draw(gameTime);

            //end drawing 
            spriteBatch.End();

            //if necessary, we save the image to a screenshot
            if (doScreenshot)
            {
                GraphicsDevice.SaveScreenshot();
                doScreenshot = false;
            }
        }

        #endregion


        #region Helpermethods

        bool WasJustPressed(Keys key) { return _currentKeyboard.IsKeyDown(key) && _oldKeyboard.IsKeyUp(key); }

        bool WasJustReleased(Keys key) { return _currentKeyboard.IsKeyUp(key) && _oldKeyboard.IsKeyDown(key); }

        /// <summary>
        /// Lets you know whether a mousebutton was just pressed, as opposed to having been held down for some time
        /// </summary>
        /// <param name="button">The button you want to check for</param>
        /// <returns>Whether the button was pressed since last Update</returns>
        bool WasJustPressed(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return _currentMouse.LeftButton == ButtonState.Pressed && _oldMouse.LeftButton == ButtonState.Released;

                case MouseButtons.Middle:
                    return _currentMouse.MiddleButton == ButtonState.Pressed && _oldMouse.MiddleButton == ButtonState.Released;

                case MouseButtons.Right:
                    return _currentMouse.RightButton == ButtonState.Pressed && _oldMouse.RightButton == ButtonState.Released;
            }

            return false;
        }

        /// <summary>
        /// Lets you know whether a mousebutton was just released, as opposed to having been untouched for some time
        /// </summary>
        /// <param name="button">The button you want to check for</param>
        /// <returns>Whether the button was released since last Update</returns>
        bool WasJustReleased(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return _currentMouse.LeftButton == ButtonState.Released && _oldMouse.LeftButton == ButtonState.Pressed;

                case MouseButtons.Middle:
                    return _currentMouse.MiddleButton == ButtonState.Released && _oldMouse.MiddleButton == ButtonState.Pressed;

                case MouseButtons.Right:
                    return _currentMouse.RightButton == ButtonState.Released && _oldMouse.RightButton == ButtonState.Pressed;
            }

            return false;
        }



        private void UpdateStates()
        {
            _currentKeyboard = Keyboard.GetState();
            _currentMouse = Mouse.GetState();
            _currentMousePosition = new Vector2(_currentMouse.X, _currentMouse.Y);
        }

        private void SaveStates()
        {
            _oldKeyboard = _currentKeyboard;
            _oldMouse = _currentMouse;
            _oldMousePosition = _currentMousePosition;
        }

        #endregion
    }
}
