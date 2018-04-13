﻿using FF.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Managers;
using System;
using System.Collections.Generic;

namespace Pong.Sprites
{
    public class Ball : Sprite
    {
        private float _timer = 0f;
        private Vector2? _startPosition = null;
        private float? _startSpeed;
        private bool _isPlaying;

        public Score Score;
        public int SpeedInIncrementSpan = 10;

        public Ball(Texture2D texture) : base(texture)
        {
            //Speed = 5f;
        }

        public override void Update(GameTime gameTime, List<Sprite> twoDirMmtStaticSprite)
        {
            foreach (var sprite in twoDirMmtStaticSprite)
            {
                if (this.Velocity.X > 0 && this.IsTouchingLeft(sprite))
                    this.Velocity.X = -this.Velocity.X;
                if (this.Velocity.X < 0 && this.IsTouchingRight(sprite))
                    this.Velocity.X = -this.Velocity.X;
                if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite))
                    this.Velocity.Y = -this.Velocity.Y;
                if (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite))
                    this.Velocity.Y = -this.Velocity.Y;
            }

            if (_startPosition == null)
            {
                _startPosition = Position;
                _startSpeed = Speed;

                Restart();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                _isPlaying = true;

            if (!_isPlaying)
                return;

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(_timer > SpeedInIncrementSpan)
            {
                Speed++;
                _timer = 0;
            }

            if (Position.Y <= 0 || Position.Y + _texture.Height >= Game1.ScreenHeight)
            {
                Velocity.Y = -Velocity.Y;
                Score.Score1++;
            }

            if(Position.X <=0)
            {
                Velocity.X = -Velocity.X;
                Score.Score1++;
            }

            if(Position.X + _texture.Width >= Game1.ScreenWidth)
            {
                Velocity.X = -Velocity.X;
                Score.Score1++;
            }

            Position += Velocity * Speed;
        }

        public void Restart()
        {
            var direction = Game1.Random.Next(0, 4);

            switch (direction)
            {
                case 0:
                    Velocity = new Vector2(1, 1);
                    break;
                case 1:
                    Velocity = new Vector2(1, -1);
                    break;
                case 2:
                    Velocity = new Vector2(-1, -1);
                    break;
                case 3:
                    Velocity = new Vector2(-1, 1);
                    break;
            }

            Position = (Vector2)_startPosition;
            Speed = (float)_startSpeed;

            _timer = 0;
            _isPlaying = false;
        }
    }
}
