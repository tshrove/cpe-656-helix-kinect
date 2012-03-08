using System;

namespace Iava.Gesture {
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>In the future we probably want to ship the gesture object along with the name</remarks>
    public class GestureEventArgs : EventArgs {

        #region Public Properties

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