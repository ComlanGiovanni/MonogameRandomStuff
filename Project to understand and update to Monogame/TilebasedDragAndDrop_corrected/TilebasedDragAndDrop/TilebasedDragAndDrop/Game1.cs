using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Created in 2012 by Jakob Krarup (www.xnafan.net).
// Use, alter and redistribute this code freely,
// but please leave this comment :)

namespace TilebasedDragAndDrop
{
    /// <summary>
    /// Sample code for tilebased drag and drop in XNA
    /// </summary>
    public class Game1 : Game
    {

        #region Variables

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D _whiteSquare;                 //the white 64x64 pixels bitmap to draw with
        Texture2D _xnafan;                      //the "xnafan" 64x64 pixels bitmap to drag and drop

        Vector2 _currentMousePosition;          //the current position of the mouse
        Vector2 _draggableSquarePosition;       //the draggable tile
        Vector2 _boardPosition;                 //where to position the board
        Vector2 _mouseDownPosition;             //where the mouse was clicked down
        Rectangle _draggableSquareBorder;       //the boundaries of the draggable tile

        readonly bool[,] _board = new bool[8, 6];        //stores whether there is something in a square

        const int _tileSize = 80;               //how wide/tall the tiles are

        bool _isDragging = false;               //remembers whether the mouse is currently dragging something

        SpriteFont _defaultFont;                //font to write info with

        //stores the previous and current states of the mouse
        //makes it possible to know if a button was just clicked
        //or whether it was up/down previously as well.
        MouseState _oldMouse, _currentMouse; 
        
        #endregion

        #region Constructor and LoadContent

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //set the screen size
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 900;

            //positions the top left corner of the board - change this to move the board
            _boardPosition = new Vector2(100, 75);

            //positions the square to drag
            _draggableSquarePosition = new Vector2(800, 100);


            //show the mouse
            IsMouseVisible = true;
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load the textures
            _whiteSquare = Content.Load<Texture2D>("white_64x64");
            _xnafan = Content.Load<Texture2D>("xnafan_64x64");

            //load the font
            _defaultFont = Content.Load<SpriteFont>("DefaultFont");

            //remembers the draggable squares position, so we can easily test for mouseclicks on it
            _draggableSquareBorder = new Rectangle((int)_draggableSquarePosition.X, (int)_draggableSquarePosition.Y, _tileSize, _tileSize);

        }

        #endregion

        #region Update and related methods

        protected override void Update(GameTime gameTime)
        {
            //get the current state of the mouse (position, buttons, etc.)
            _currentMouse = Mouse.GetState();

            // Allows the game to exit on an ESC press
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) { this.Exit(); }

            //remember the mouseposition for use in this Update and subsequent Draw
            _currentMousePosition = new Vector2(_currentMouse.X, _currentMouse.Y);

            CheckForLeftButtonDown();

            CheckForLeftButtonRelease();
            
            CheckForRightButtonReleaseOverBoard();

            //call the Game class' Update
            base.Update(gameTime);

            //store the current state of the mouse as the old
            _oldMouse = _currentMouse;
        }

        private void CheckForLeftButtonDown()
        {
            if (_currentMouse.LeftButton == ButtonState.Pressed )
            {
                //if this Update() is a new click - store the mouse-down position
                if (_oldMouse.LeftButton == ButtonState.Released)
                {
                    _mouseDownPosition = _currentMousePosition;    
                }

                //if the mousedown was within the draggable tile 
                //and the mouse has been moved more than 10 pixels:
                //start dragging!
                if ((_mouseDownPosition - _currentMousePosition).Length() > 10 && _draggableSquareBorder.Contains((int)_mouseDownPosition.X, (int)_mouseDownPosition.Y))
                {
                    _isDragging = true;
                }
            }
        }
        
        private void CheckForLeftButtonRelease()
        {
            //if the user just released the mousebutton - set _isDragging to false, and check if we should add the tile to the board
            if (_oldMouse.LeftButton == ButtonState.Pressed && _currentMouse.LeftButton == ButtonState.Released && _isDragging)
            {
                _isDragging = false;

                //if the mousebutton was released inside the board
                if (IsMouseInsideBoard())
                {
                    //find out which square the mouse is over
                    Vector2 tile = GetSquareFromCurrentMousePosition();
                    //and set that square to true (has a piece)
                    _board[(int)tile.X, (int)tile.Y] = true;
                }
            }
        }

        private void CheckForRightButtonReleaseOverBoard()
        {
            //find out if right button was just clicked over the board - and remove a tile from that square
            if (_oldMouse.RightButton == ButtonState.Released && _currentMouse.RightButton == ButtonState.Pressed && IsMouseInsideBoard())
            {
                Vector2 boardSquare = GetSquareFromCurrentMousePosition();
                _board[(int)boardSquare.X, (int)boardSquare.Y] = false;
            }
        }

        #endregion

        #region Draw and related methods

        protected override void Draw(GameTime gameTime)
        {

            //add a green background
            GraphicsDevice.Clear(Color.DarkGreen);

            //start drawing
            spriteBatch.Begin();

            DrawText();             //draw helptext
            DrawBoard();            //draw the board
            DrawDraggableTile();    //draw the draggable tile, wherever it may be

            //end drawing
            spriteBatch.End();

            base.Draw(gameTime);
        }


        //Draws the text on the screen
        private void DrawText()
        {
            spriteBatch.DrawString(_defaultFont, "Tilebased XNA drag and drop sample", new Vector2(100, 20), Color.White);
            spriteBatch.DrawString(_defaultFont, "Drag new tile onto board with left mousebutton", new Vector2(100, 560), Color.White);
            spriteBatch.DrawString(_defaultFont, "Remove tiles with right-click", new Vector2(100, 590), Color.White);
            spriteBatch.DrawString(_defaultFont, "www.Xnafan.net", new Vector2(725, 665), Color.White);
        }


        //draws the draggable tile either under the mouse, if it is currently being dragged, or in its default position
        private void DrawDraggableTile()
        {
            if (_isDragging)
            {
                spriteBatch.Draw(_xnafan, new Rectangle((int)(_currentMousePosition.X - _whiteSquare.Width / 2), (int)(_currentMousePosition.Y - _whiteSquare.Height / 2), _tileSize, _tileSize), Color.White);
            }
            else
            {
                spriteBatch.Draw(_xnafan, _draggableSquarePosition, Color.White);
            }
        }


        // Draws the game board
        private void DrawBoard()
        {
            float opacity;                                      //how opaque/transparent to draw the square
            Color colorToUse = Color.White;                     //background color to use
            Rectangle squareToDrawPosition = new Rectangle();   //the square to draw (local variable to avoid creating a new variable per square)

            //for all columns
            for (int x = 0; x < _board.GetLength(0); x++)
            {
                //for all rows
                for (int y = 0; y < _board.GetLength(1); y++)
                {

                    //figure out where to draw the square
                    squareToDrawPosition = new Rectangle((int)(x * _tileSize + _boardPosition.X), (int)(y * _tileSize + _boardPosition.Y), _tileSize, _tileSize);

                    //the code below will make the board checkered using only a single, white square:

                    //if we add the x and y value of the tile
                    //and it is even, we make it one third opaque
                    if ((x + y) % 2 == 0)
                    {
                        opacity = .33f;
                    }
                    else
                    {
                        //otherwise it is one tenth opaque
                        opacity = .1f;
                    }

                    //make the square the mouse is over red
                    if (IsMouseInsideBoard() && IsMouseOnTile(x, y))
                    {
                        colorToUse = Color.Red;
                        opacity = .5f;
                    }
                    else
                    {
                        colorToUse = Color.White;
                    }


                    //draw the white square at the given position, offset by the x- and y-offset, in the opacity desired
                    spriteBatch.Draw(_whiteSquare, squareToDrawPosition, colorToUse * opacity);

                    //if the square has a tile - draw it
                    if (_board[x, y])
                    {
                        spriteBatch.Draw(_xnafan, squareToDrawPosition, Color.White * 0.8F);
                    }
                }

            }
        }

        #endregion

        #region Mouse and board helpermethods

        // Checks to see whether a given coordinate is within the board
        private bool IsMouseOnTile(int x, int y)
        {
            //do an integerdivision (whole-number) of the coordinates relative to the board offset with the tilesize in mind
            return (int)(_currentMousePosition.X - _boardPosition.X) / _tileSize == x && (int)(_currentMousePosition.Y - _boardPosition.Y) / _tileSize == y;
        }

        //find out whether the mouse is inside the board
        bool IsMouseInsideBoard()
        {
            if (_currentMousePosition.X >= _boardPosition.X && _currentMousePosition.X <= _boardPosition.X + _board.GetLength(0) * _tileSize && _currentMousePosition.Y >= _boardPosition.Y && _currentMousePosition.Y <= _boardPosition.Y + _board.GetLength(1) * _tileSize)
            {
                return true;
            }
            else
            { return false; }
        }

        //get the column/row on the board for a given coordinate
        Vector2 GetSquareFromCurrentMousePosition()
        {
            //adjust for the boards offset (_boardPosition) and do an integerdivision
            return new Vector2((int)(_currentMousePosition.X - _boardPosition.X) / _tileSize, (int)(_currentMousePosition.Y - _boardPosition.Y) / _tileSize);
        }

        #endregion

    }
}