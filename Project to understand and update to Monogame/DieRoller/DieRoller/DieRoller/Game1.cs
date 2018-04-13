using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DieRoller
{
    // Created in 2012 by Jakob Krarup (www.xnafan.net).
    // Use, alter and redistribute this code freely,
    // but please leave this comment :)

    /// <summary>
    /// Sample game for showing how to roll dice
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;                        //the spritebatch for drawing
        KeyboardState _currentKeyboard, _oldKeyboard;   //stores current and previous keyboard states
        SpriteFont _font, _bigFont;                     //for instructions
        string _result;                                 //the combined roll of the dice
        List<Die> _dice = new List<Die>();              //for referencing all the dice
        bool _resultCalculated = false;                 //make sure we only calculate the result once


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        //calls LoadContent on all GameComponents in the base.Initialize() call
        protected override void Initialize()
        {
            for (int i = 0; i < 6; i++)
            {
                //creates a new die, a bit further to the left and below the previous
                Die die = new Die(this, new Vector2(100 + 120 * i, 100 + 25 * i));

                //adds it to the Game's components, to make sure it gets updated and drawn
                Components.Add(die);

                //adds it the dice list, so we can roll and perform other actions on them
                _dice.Add(die);
            }

            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //add it to the gameservices, so the Die can access it
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            _font = Content.Load<SpriteFont>("font");
            _bigFont = Content.Load<SpriteFont>("BigFont");
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            //get the keyboardstate
            _currentKeyboard = Keyboard.GetState();

            // Allows the game to exit
            if (WasJustPressed(Keys.Escape))
            {
                this.Exit();
            }

            //roll the Die when user presses space
            if (WasJustPressed(Keys.Space))
            {
                RollTheDice();
            }

            base.Update(gameTime);

            //if we don't have a result yet, and all dice have stopped rolling
            if (!_resultCalculated && _dice.TrueForAll(die => die.State == DieState.Stationary))
            {
                _result = CalculateResult();
            }

            //store current keyboardstate as the old one
            _oldKeyboard = _currentKeyboard;
        }


        //get a string describing the result
        private string CalculateResult()
        {
            //calculate and write out the result
            string finalResult = "";

            int sum = 0;    //for summing up the values

            //iterate over all dice
            foreach (Die die in _dice)
            {
                //if we've already added a number to the result
                if (finalResult != "")
                {
                    //.. then add a plus in front of the next number
                    finalResult += " + ";
                }

                //get the next dievalue as a string
                finalResult += die.ToString();

                //add this to the sum
                sum += die.Value;
            }

            //remember that the sum has been calculated
            _resultCalculated = true;

            //return the final result
            return "Result: " + finalResult + " = " + sum;
        }


        private void RollTheDice()
        {
            //update the status on screen
            _result = @"""Rolling, rolling, rolling..."" - The Blues Brothers";

            //remember that we need to calculate the result of the roll
            _resultCalculated = false;

            //roll all the dice 
            _dice.ForEach(die => die.Roll());
        }

        protected override void Draw(GameTime gameTime)
        {
            //make a felt-like background
            GraphicsDevice.Clear(Color.DarkGreen);

            //draw
            spriteBatch.Begin();
            spriteBatch.DrawString(_bigFont, "Diceroller codesample for XNA", new Vector2(370, 20), Color.LightGreen);
            spriteBatch.DrawString(_bigFont, _result, new Vector2(20, 385), Color.White);
            spriteBatch.DrawString(_font, "SPACE to roll, ESC to exit", new Vector2(20, 440), Color.Silver);
            spriteBatch.DrawString(_font, "www.xnafan.net", new Vector2(620, 440), Color.LightGreen);
            base.Draw(gameTime);
            spriteBatch.End();
        }

        // Tells if a key was just pressed
        bool WasJustPressed(Keys key)
        {
            //the key is down now, but was up in the previous Update()
            return _currentKeyboard.IsKeyDown(key) && _oldKeyboard.IsKeyDown(key);
        }


    }
}
