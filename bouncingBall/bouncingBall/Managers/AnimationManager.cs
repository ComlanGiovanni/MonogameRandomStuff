using FF.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FF.Managers
{
    public class AnimationManager
    {
        private Animation _animation;

        private float _timer;

        public Vector2 Position { get; set; }
        
        public AnimationManager(Animation animation)
        {
            _animation = animation;
        }

        public void Play(Animation animation)
        {
            if (_animation == animation)
                return;

            _animation = animation;

            _animation.CurrentFrame = 0;

            _timer = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_animation.Texture,
                            Position,
                            new Rectangle(_animation.CurrentFrame * _animation.FrameWidth, 0, _animation.FrameWidth, _animation.FrameHeight),
                            Models.Settings.BACKGROUND_COLOR);
            //how to wiew on the sprites png
        }

        public void Stop()
        {
            _timer = 0f;

            _animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0f;
                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount)
                    _animation.CurrentFrame = 0;
            }
        }
    }
}
