using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace inputManagerClass
{
    //The input class gonna be a Proprities in a sprite who can be not assign == NULL
    public class Input //Can be access by anywhere
    {
        /*
         *Properties
         * Keys -> using Microsoft.Xna.Framework.Input;
         * get set in not that necessaire we can just
         * -------------------------------------
         *  public Keys Up;
         *  public Keys Down;
         *  public Keys Left;
         *  public Keys Right;
         * -------------------------------------
         * 
         * */
        public Keys Down { get; set; }

        public Keys Left { get; set; }

        public Keys Right { get; set; }

        public Keys Up { get; set; }
    }
}
