using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FixAnimatedSprite.Managers
{
    public class Sprite
    {
        public AnimationManager _animationManger;
        public Dictionary<string, Animation> _animations;
        public Texture2D _texture;
        public Vector2 Position;

        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManger = new AnimationManager(_animations.First().Value);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            SetAnimations();
            _animationManger.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_texture, Position, Colour);
            //We can draw animation or simple texture
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Models.Settings.DONT_AFFECT_COLOR_SPRITE);
            else if (_animationManger != null)
                _animationManger.Draw(spriteBatch);
            else throw new Exception("WTF DUDE");
        }

        //Method
        protected virtual void SetAnimations()
        {
            _animationManger.Play(_animations["WalkUp"]);
        }
    }
}
