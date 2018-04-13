using Microsoft.Xna.Framework.Graphics;

namespace FF.Models
{
    public class Animation
    {

        public Texture2D Texture { get; set; }

        public int CurrentFrame { get; set; }

        public int FrameCount { get; private set; }//how many frame we have

        public int FrameHeight { get { return Texture.Height; } }

        public int FrameWidth { get { return Texture.Width / FrameCount; } }

        public float FrameSpeed { get; set; }//how fast the frame goes
        
        public bool IsLoopping { get; set; }
        
        public Animation(Texture2D texture, int frameCount, float frameSpeed)
        {
            Texture = texture;
            FrameCount = frameCount;
            FrameSpeed = frameSpeed;
            IsLoopping = true;
        }
    }
}
