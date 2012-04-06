using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iava.Audio
{
    /// <summary>
    /// Audio recognized event arguments.
    /// </summary>
    public class AudioEventArgs : EventArgs
    {
        #region Attributes

        /// <summary>
        /// The audio command that was recognized.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// The wildcard words (if any present) recognized from the command.
        /// </summary>
        public List<string> CommandWildcards { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AudioEventArgs()
        {

        }
        #endregion
    }
}
