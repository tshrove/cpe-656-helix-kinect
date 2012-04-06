using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Iava.Core
{
    /// <summary>
    /// Recognizer interface.
    /// </summary>
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

        #region Events
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
        /// <summary>
        /// Raises when the recognizer is sycned.
        /// </summary>
        event EventHandler<EventArgs> Synced;
        /// <summary>
        /// Raises when the recognizer is unsynced.
        /// </summary>
        event EventHandler<EventArgs> Unsynced;
        /// <summary>
        /// Raises when the status of the recognizer is changed.
        /// </summary>
        event EventHandler<EventArgs> StatusChanged;
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
