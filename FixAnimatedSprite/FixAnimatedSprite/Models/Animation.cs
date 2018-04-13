using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace FixAnimatedSprite.Managers
{
    public class Animation
    {
        //Properties
        public Texture2D Texture { get; set; }

        public int CurrentFrame { get; set; }

        public int FrameCount { get; private set; }//how many frame we have

        public int FrameHeight { get { return Texture.Height; } }

        public float FrameSpeed { get; set; }//how fast the frame goes

        public int FrameWidth { get { return Texture.Width / FrameCount; } }

        public bool IsLoopping { get; set; }

        //Construtor
        public Animation(Texture2D texture, int frameCount)
        {
            Texture = texture;
            FrameCount = frameCount;
            IsLoopping = true;
            FrameSpeed = 0.2f;
        }
    }
}
