using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DieRoller
{

    // Created in 2012 by Jakob Krarup (www.xnafan.net).
    // Use, alter and redistribute this code freely,
    // but please leave this comment :)

    public class Die : DrawableGameComponent
    {

        #region Variables and properties

        private const double _msForRoll = 1000;                 //how long the die should roll

        private static int _dieFaceSize;                        //the width/height in pixels of the individual diefaces
        private static Random _rnd = new Random();              //for creating random values
        private static SpriteBatch _spriteBatch;                //stores a reference to the game's SpriteBatch

        private int _dieValue = 1;                              //the current value of the die

        public double MsLeftInRoll { get; private set; }        //how long is left before this die stops rolling
        public DieState State { get; private set; }             // The current state of the die, rolling or stationary
        public Texture2D DieFaces { get; private set; }         //strip containing all the faces for the die. Should be six times as wide as it is tall
        public Vector2 OriginalPosition { get; private set; }   //where the die is positioned    
        public Vector2 CurrentPosition { get; set; }


        /// <summary>
        /// The current value of the die
        /// </summary>
        public int Value
        {
            get { return _dieValue; }
            set
            {
                //only sets the value if it is from 1 to 6
                if (value >= 1 && value <= 6)
                {
                    _dieValue = value;
                }
            }
        }

        // Allows access to the SpriteBatch in the Game class
        private SpriteBatch SpriteBatch
        {

            get
            {

                //if we haven't gotten the spritebatch yet
                if (Die._spriteBatch == null)
                {
                    //get it from the Game's service
                    Die._spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
                }

                return Die._spriteBatch; //now return it
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Creates a new Die
        /// </summary>
        /// <param name="game">The running Game</param>
        public Die(Game game, Vector2 position)
            : base(game)
        {
            //remember where to begin each roll from
            OriginalPosition = position;
            CurrentPosition = OriginalPosition;
        }

        //Loads the dice graphics
        protected override void LoadContent()
        {
            //load the texture - it should be six times as wide as it is tall
            DieFaces = Game.Content.Load<Texture2D>("dice");
            //store the height
            _dieFaceSize = DieFaces.Height;
            base.LoadContent();
        }

        #endregion


        #region Update

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //if the die is rolling
            if (this.State == DieState.Rolling)
            {
                //subtract the time since last Update from the rolltime
                MsLeftInRoll -= gameTime.ElapsedGameTime.TotalMilliseconds;

                //if time is up
                if (MsLeftInRoll <= 0)
                {
                    //set the state to stationary
                    this.State = DieState.Stationary;
                }
                else
                {
                    //give the die a new value from 1 to 6
                    this.Value = _rnd.Next(6) + 1;
                    //move the offset a bit down along the Y axis
                    this.CurrentPosition += Vector2.UnitY * 2;
                }
            }
        }
        #endregion


        #region Draw

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            //pulls the correct die face by multiplying the facesize with the value
            Rectangle sourceRect = new Rectangle((this.Value - 1) * _dieFaceSize, 0, _dieFaceSize, _dieFaceSize);

            //draw a shadow, with a black onethird opacity
            SpriteBatch.Draw(DieFaces, (this.CurrentPosition) - Vector2.One * _dieFaceSize / 3, sourceRect, Color.Black * .3f);

            //and centers it on the position by subtracting half of a facesize
            SpriteBatch.Draw(DieFaces, (this.CurrentPosition) - Vector2.One * _dieFaceSize / 2, sourceRect, Color.White);
        }

        #endregion


        #region Helpermethods and ToString()

        //starts the die rolling
        public void Roll()
        {
            //if the die isn't already rolling
            if (this.State != DieState.Rolling)
            {
                //move it back to its starting position
                this.ReturnToOriginalPosition();

                //start it rolling
                this.State = DieState.Rolling;

                //reset the time to roll
                MsLeftInRoll = _msForRoll;
            }
        }


        //moves the die back to it's original position
        public void ReturnToOriginalPosition()
        {
            this.CurrentPosition = this.OriginalPosition;
        }


        //Makes this Die's value human readable when the object is used as a string
        public override string ToString()
        {
            return this.Value.ToString();
        }

        #endregion

    }
}
