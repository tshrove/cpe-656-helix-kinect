using System;

namespace Iava.Gesture {

    /// <summary>
    /// EventArgs specifying the name of the Gesture it came from
    /// </summary>
    public class GestureEventArgs : EventArgs {

        #region Public Properties

        /// <summary>
        /// Gets name of the gesture
        /// </summary>
        public string Name { get; private set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="name">the name of the gesture recognized</param>
        public GestureEventArgs(string name) {
            Name = name;
        }

        #endregion Constructors
    }
}