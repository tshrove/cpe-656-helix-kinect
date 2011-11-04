using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Iava.Core
{
    public interface IRecognizer
    {
        #region Properties
        /// <summary>
        /// Gets the status of the recognizer.
        /// </summary>
        RecognizerStatus Status
        {
            get;
        }
        #endregion

        #region Static Events
        /// <summary>
        /// Raises before the recognizer starts.
        /// </summary>
        event EventHandler<EventArgs> Started;
        /// <summary>
        /// Raises before the recognizer stops.
        /// </summary>
        event EventHandler<EventArgs> Stopped;
        /// <summary>
        /// Raises when the recognizer fails.
        /// </summary>
        event EventHandler<EventArgs> Failed;
        #endregion

        #region Public Methods
        /// <summary>
        /// Starts the recognizer.
        /// </summary>
        void Start();
        /// <summary>
        ///  Stops the recognizer.
        /// </summary>
        void Stop();
        #endregion
    }
}
