using System;
using System.Collections.Generic;

namespace Iava.Audio {

    /// <summary>
    /// Audio recognized event arguments.
    /// </summary>
    public class AudioEventArgs : EventArgs {

        #region Public Properties

        /// <summary>
        /// The audio command that was recognized.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// The wildcard words (if any present) recognized from the command.
        /// </summary>
        public List<string> CommandWildcards { get; set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AudioEventArgs() {
        }

        #endregion Constructors
    }
}
