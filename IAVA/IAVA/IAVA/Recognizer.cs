using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Iava.Core;

namespace Iava.Core {
    /// <summary>
    /// Recognizer Class.
    /// </summary>
    public abstract class Recognizer : IRecognizer {

        #region Public Events

        /// <summary>
        /// Raises before the recognizer starts.
        /// </summary>
        public event EventHandler<EventArgs> Started;

        /// <summary>
        /// Raises before the recognizer stops.
        /// </summary>
        public event EventHandler<EventArgs> Stopped;

        /// <summary>
        /// Raises when the recognizer fails.
        /// </summary>
        public event EventHandler<EventArgs> Failed;

        /// <summary>
        /// Raises when the recognizer synced.
        /// </summary>
        public event EventHandler<EventArgs> Synced;

        /// <summary>
        /// Raises when the recognizer unsynced.
        /// </summary>
        public event EventHandler<EventArgs> Unsynced;

        /// <summary>
        /// Raises when the status of the recognizer is changed.
        /// </summary>
        public event EventHandler<EventArgs> StatusChanged;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// Gets the status of the cognizer.
        /// </summary>
        public RecognizerStatus Status {
            get { return this.m_pStatus; }
            protected set { this.m_pStatus = value; OnStatusChanged(this, new EventArgs()); }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Starts the recognizer.
        /// </summary>
        public abstract void Start();

        /// <summary>
        ///  Stops the recognizer.
        /// </summary>
        public abstract void Stop();

        #endregion Public Methods

        #region Constructors

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public Recognizer()
            : this(null) {
            // Nothing to do.
        }

        /// <summary>
        /// Constructor that takes the file and creates the recognizer based on
        /// the file input.
        /// </summary>
        /// <param name="filePath">Path to the configuration file</param>
        public Recognizer(string filePath) {
            // TODO Check for correct file path and may want to do stuff with.
            //this.Configuration = new FileStream(filePath, FileMode.Open);

            m_timeoutTimer.Elapsed += OnTimerEllapsed;
        }

        #endregion Constructors

        #region Protected Methods

        /// <summary>
        /// Raises before the recognizer starts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnStarted(object sender, EventArgs e) {
            if (Started != null)
                Started(sender, e);
        }

        /// <summary>
        /// Raises before the recognizer stops.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnStopped(object sender, EventArgs e) {
            if (Stopped != null)
                Stopped(sender, e);
        }

        /// <summary>
        /// Raises before the recognizer fails.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnFailed(object sender, EventArgs e) {
            if (Failed != null)
                Failed(sender, e);
        }

        /// <summary>
        /// Raises when the recognizer is synced.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSynced(object sender, EventArgs e) {
            // Only throw the event if we were previously unsynced
            if (!m_isSynced && (Synced != null)) { Synced(sender, e); }

            m_isSynced = true;

            // Reset the timer
            ResetTimer();
        }

        /// <summary>
        /// Raises when the recognizer is unsynced.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnUnsynced(object sender, EventArgs e) {
            // Only throw the event if we were previously synced
            if (m_isSynced && (Unsynced != null)) { Unsynced(sender, e); }

            m_isSynced = false;
        }

        /// <summary>
        /// Raises when the status of the recognizer is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnStatusChanged(object sender, EventArgs e) {
            if (StatusChanged != null)
                StatusChanged(sender, e);
        }

        /// <summary>
        /// Raised when the Timeout Timer expires.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnTimerEllapsed(object sender, System.Timers.ElapsedEventArgs e) {
            OnUnsynced(this, e);
        }

        /// <summary>
        /// Resets the Timeout timer.
        /// </summary>
        protected void ResetTimer() {
            m_timeoutTimer.Stop();
            m_timeoutTimer.Start();
        }

        #endregion Protected Methods

        #region Private Properties

        /// <summary>
        /// Gets the file used for configuration of the recognizer.
        /// </summary>
        protected FileStream Configuration {
            get;
            set;
        }

        #endregion Private Properties

        #region Private Fields

        private RecognizerStatus m_pStatus = RecognizerStatus.NotReady;

        protected bool m_isSynced = false;

        protected System.Timers.Timer m_timeoutTimer = new System.Timers.Timer(30000);

        #endregion Private Fields
    }
}
