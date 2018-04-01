using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appTestAndOther
{
    public class Settings
    {
        public static int SCREEN_WIDTH = 500;
        public static int SCREEN_HEIGHT = 500;
        public static bool IS_FULLSCREEN = false;//False by default

        public static bool IS_MOUSE_VISIBLE = false;//False by default
        public static bool ALLOW_USER_RESIZING = false;//False by default
        public static bool IS_BORDERLESS = true;//False by default
        public static bool ALLOW_RAGE_QUIT = true;//True by default
        public static bool IS_FIXED_TIME_STEP = true; //true by default
        public static bool V_SYNC = false; //true by default

        public static Color DONT_AFFECT_COLOR_SPRITE = Color.White;
        public static Color LOADING_TEXTURE_COLOR_SPRITE = Color.Black;

        public static Color BACKGROUND_COLOR = Color.WhiteSmoke;
    }
}
