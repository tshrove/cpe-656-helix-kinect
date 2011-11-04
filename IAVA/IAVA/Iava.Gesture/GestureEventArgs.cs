using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iava.Gesture
{
    public class GestureEventArgs : EventArgs
    {
        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public GestureEventArgs()
        {
            // Take a look at:
            // System.Windows.Forms.MouseEventArgs;
            // System.Windows.Input.MouseEventArgs;
            // System.Windows.Input.MouseGesture;
            // System.Windows.Input.MouseWheelEventArgs;
            // System.Windows.Input.TouchEventArgs;
        }
        #endregion
    }
}
