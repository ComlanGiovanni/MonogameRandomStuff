using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DieRoller
{
    /// <summary>
    /// Tells whether the die is rolling or stationary
    /// </summary>

    public enum DieState
    { 
        /// <summary>
        /// The die is still rolling
        /// </summary>
        Rolling,
    
        /// <summary>
        /// The die has stopped rolling
        /// </summary>
        Stationary 
    }
}
