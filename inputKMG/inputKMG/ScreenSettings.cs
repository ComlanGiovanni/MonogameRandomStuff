using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace inputKMG
{
    public class ScreenSettings
    {
        public static int SCREEN_WIDTH = 1000;
        public static int SCREEN_HEIGHT = 1000;

        public static bool IS_MOUSE_VISIBLE = false;//False by default
        public static bool IS_BORDERLESS = true;//False by default

        public static Color DONT_AFFECT_COLOR_SPRITE = Color.White;

        public static Color BACKGROUND_COLOR = Color.WhiteSmoke;
    }
}