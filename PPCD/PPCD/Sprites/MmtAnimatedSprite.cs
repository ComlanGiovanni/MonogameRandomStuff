using FF.Managers;
using FF.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FF.Sprites
{
    public class MmtAnimatedSprite
    {
        public AnimationManager _animationManger;
        protected List<Animation> _animations;

        public Vector2 _position;
        private Vector2? _startPosition = null;
        public Vector2 Velocity;

        public float FrameSpeed;
        public float Speed;
        private float _timer = 0f;
        private float? _startSpeed;

        public static Random Random;

        private bool _isPlaying;
        public int SpeedInIncrementSpan = 10;

        public Color Colour = Settings.DONT_AFFECT_COLOR_SPRITE;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (_animationManger != null)
                    _animationManger.Position = _position;
                }
        }
        
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _animations.First().FrameWidth, _animations.First().FrameHeight);
            }
        }

        public MmtAnimatedSprite(List<Animation> animations)
        {
            _animations = animations;
            _animationManger = new AnimationManager(_animations.First());
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            _animationManger.Draw(spriteBatch);
        }

        public virtual void Update(GameTime gameTime, List<MmtAnimatedSprite> sprites)
        {
            SetAnimations();
            _animationManger.Update(gameTime);

            foreach (var sprite in sprites)
            {
                if (Velocity.X > 0 && IsTouchingLeft(sprite) && PerPixelCollision(sprite))
                    Velocity.X = -Velocity.X;
                if (Velocity.X < 0 && IsTouchingRight(sprite) && PerPixelCollision(sprite))
                    Velocity.X = -Velocity.X;
                if (Velocity.Y > 0 && IsTouchingTop(sprite) && PerPixelCollision(sprite))
                    Velocity.Y = -Velocity.Y;
                if (Velocity.Y < 0 && IsTouchingBottom(sprite) && PerPixelCollision(sprite))
                    Velocity.Y = -Velocity.Y;
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

            if (_timer > SpeedInIncrementSpan)
            {
                Speed++;
                _timer = 0;
            }

            if (Position.Y <= 0 || Position.Y + _animations.First().FrameHeight >= Settings.SCREEN_HEIGHT)
            {
                Velocity.Y = -Velocity.Y;
            }

            if (Position.X <= 0)
            {
                Velocity.X = -Velocity.X;
            }

            if (Position.X + _animations.First().FrameWidth >= Settings.SCREEN_WIDTH)
            {
                Velocity.X = -Velocity.X;
            }

            Position += Velocity * Speed;
        }

        public void Restart()
        {
            Random = new Random();
            var direction = Random.Next(0, 4);

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

        protected virtual void SetAnimations()
        {
            _animationManger.Play(_animations.First());
        }

        protected bool IsTouchingLeft(MmtAnimatedSprite sprite)
        {
            return Rectangle.Right + Velocity.X > sprite.Rectangle.Left &&
                   Rectangle.Left < sprite.Rectangle.Left &&
                   Rectangle.Bottom > sprite.Rectangle.Top &&
                   Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(MmtAnimatedSprite sprite)
        {
            return Rectangle.Left + Velocity.X < sprite.Rectangle.Right &&
                   Rectangle.Right > sprite.Rectangle.Right &&
                   Rectangle.Bottom > sprite.Rectangle.Top &&
                   Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(MmtAnimatedSprite sprite)
        {
            return Rectangle.Bottom + Velocity.Y > sprite.Rectangle.Top &&
                   Rectangle.Top < sprite.Rectangle.Top &&
                   Rectangle.Right > sprite.Rectangle.Left &&
                   Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(MmtAnimatedSprite sprite)
        {
            return Rectangle.Top + Velocity.Y < sprite.Rectangle.Bottom &&
                   Rectangle.Bottom > sprite.Rectangle.Bottom &&
                   Rectangle.Right > sprite.Rectangle.Left &&
                   Rectangle.Left < sprite.Rectangle.Right;
        }

        private bool PerPixelCollision(MmtAnimatedSprite target)
        {
            var sourceColors = new Color[_animations.First().FrameWidth * _animations.First().FrameHeight];
            _animations.First().Texture.GetData(sourceColors);

            var targetColors = new Color[target._animations.First().FrameWidth * target._animations.First().FrameHeight];
            target._animations.First().Texture.GetData(sourceColors);

            var left = Math.Max(Rectangle.Left, target.Rectangle.Left);
            var top = Math.Max(Rectangle.Top, target.Rectangle.Top);
            var width = Math.Min(Rectangle.Right, target.Rectangle.Right) - left;
            var height = Math.Min(Rectangle.Bottom, target.Rectangle.Bottom) - top;

            var intersectingRectangle = new Rectangle(left, top, width, height);

            for(var x = intersectingRectangle.Left; x < intersectingRectangle.Right; x++)
            {
                for(var y = intersectingRectangle.Top; y < intersectingRectangle.Bottom; y++)
                {
                    var sourceColor = sourceColors[(x - Rectangle.Left) + (y - Rectangle.Top) * _animations.First().FrameWidth];
                    var targetColor = targetColors[(x - target.Rectangle.Left) + (y - target.Rectangle.Top) * target._animations.First().FrameWidth];

                    if (sourceColor.A > 0 && targetColor.A > 0)
                        return true;
                }
            }
            return false;
        }
    }
}
