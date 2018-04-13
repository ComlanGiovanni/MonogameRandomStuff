using Microsoft.Xna.Framework;

namespace FF.Models
{
    public class Settings
    {
        public static int SCREEN_WIDTH = 1000;
        public static int SCREEN_HEIGHT = 1000;

        public static bool IS_MOUSE_VISIBLE = true;//False by default
        public static bool IS_BORDERLESS = false;//False by default

        public static Color DONT_AFFECT_COLOR_SPRITE = Color.White;

        public static Color BACKGROUND_COLOR = Color.WhiteSmoke;
        public static Vector2 THE_ORIGIN = Vector2.Zero;//0,0

        /*
         * better make a founction who get the texture height and with and return a vetor2 ????
        public static Vector2 TOP_LEFT_CORNER;
        public static Vector2 TOP_RIGHT_CORNER;

        public static Vector2 DOWN_LEFT_CORNER;//0,0
        public static Vector2 DOWN_RIGHT_CORNER;//0,0

        public static Vector2 MILDDLE_SCREEN;//0,0

        public static Vector2 MILDDLE_TOP_HEIGHT;//0,0
        public static Vector2 MILDDLE_DOWN_HEIGHT;//0,0

        public static Vector2 MILDDLE_LEFT_WITDHT;//0,0
        public static Vector2 MILDDLE_RIGHT_WITDHT;//0,0
        */
    }
}
