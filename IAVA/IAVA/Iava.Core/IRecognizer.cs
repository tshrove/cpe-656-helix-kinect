using System;

namespace Iava.Core {

    /// <summary>
    /// Recognizer interface.
    /// </summary>
    public interface IRecognizer {

        #region Public Events

        /// <summary>
        /// Raises when the recognizer fails.
        /// </summary>
        event EventHandler<EventArgs> Failed;
        
        /// <summary>
        /// Raises before the recognizer starts.
        /// </summary>
        event EventHandler<EventArgs> Started;

        /// <summary>
        /// Raises when the status of the recognizer is changed.
        /// </summary>
        event EventHandler<EventArgs> StatusChanged;

        /// <summary>
        /// Raises before the recognizer stops.
        /// </summary>
        event EventHandler<EventArgs> Stopped;

        /// <summary>
        /// Raises when the recognizer is sycned.
        /// </summary>
        event EventHandler<EventArgs> Synced;

        /// <summary>
        /// Raises when the recognizer is unsynced.
        /// </summary>
        event EventHandler<EventArgs> Unsynced;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// Gets the status of the recognizer.
        /// </summary>
        RecognizerStatus Status { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Starts the recognizer.
        /// </summary>
        void Start();

        /// <summary>
        ///  Stops the recognizer.
        /// </summary>
        void Stop();

        #endregion Public Methods
    }
}
