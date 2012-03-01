using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
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
        /// Gets the status of the recognizer.
        /// </summary>
        public RecognizerStatus Status {
            get { return this.m_pStatus; }
            protected set { this.m_pStatus = value; OnStatusChanged(this, new EventArgs()); }
        }

        /// <summary>
        /// The timeout value, in milliseconds, to wait until the recognizer unsyncs.
        /// </summary>
        public int SyncTimeoutValue {
            get { return m_syncTimeout; }
            set { m_syncTimeout = value; m_timeoutTimer.Interval = value; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Starts the recognizer.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Stops the recognizer.
        /// </summary>
        public abstract void Stop();

        #endregion Public Methods

        #region Constructors

        /// <summary>
        /// Deafult constructor.
        /// </summary>
        public Recognizer() {
            // Default to 30 seconds
            SyncTimeoutValue = 30000;

            m_timeoutTimer.Elapsed += OnTimerElapsed;

            // Set the SynchronizationContext
            m_syncContext = SynchronizationContext.Current;

            if (m_syncContext == null) {
                // This situation occurs during testing so create a new context
                m_syncContext = new SynchronizationContext();
            }
        }

        #endregion Constructors

        #region Protected Methods

        /// <summary>
        /// Raises before the recognizer starts.
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event args</param>
        protected void OnStarted(object sender, EventArgs e) {
            if (Started != null)
                m_syncContext.Post(new SendOrPostCallback(delegate(object state) { Started(this, e); }), null);
        }

        /// <summary>
        /// Raises before the recognizer stops.
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event args</param>
        protected void OnStopped(object sender, EventArgs e) {
            if (Stopped != null)
                m_syncContext.Post(new SendOrPostCallback(delegate(object state) { Stopped(this, e); }), null);
        }

        /// <summary>
        /// Raises before the recognizer fails.
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event args</param>
        protected void OnFailed(object sender, EventArgs e) {
            if (Failed != null)
                m_syncContext.Post(new SendOrPostCallback(delegate(object state) { Failed(this, e); }), null);
        }

        /// <summary>
        /// Raises when the recognizer is synced.
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event args</param>
        protected void OnSynced(object sender, EventArgs e) {
            // Only throw the event if we were previously unsynced
            if (!m_isSynced && (Synced != null)) {
                m_syncContext.Post(new SendOrPostCallback(delegate(object state) { Synced(this, e); }), null);
            }

            m_isSynced = true;

            // Reset the timer
            ResetTimer();
        }

        /// <summary>
        /// Raises when the recognizer is unsynced.
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event args</param>
        protected void OnUnsynced(object sender, EventArgs e) {
            // Only throw the event if we were previously synced
            if (m_isSynced && (Unsynced != null)) {
                m_syncContext.Post(new SendOrPostCallback(delegate(object state) { Unsynced(this, e); }), null);
            }

            m_isSynced = false;
        }

        /// <summary>
        /// Raises when the status of the recognizer is changed.
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event args</param>
        protected void OnStatusChanged(object sender, EventArgs e) {
            if (StatusChanged != null) {
                m_syncContext.Post(new SendOrPostCallback(delegate(object state) { StatusChanged(this, e); }), null);
            }
        }

        /// <summary>
        /// Raised when the Timeout Timer expires.
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event args</param>
        protected void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e) {
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

        private int m_syncTimeout;

        /// <summary>
        /// Recognizer status.
        /// </summary>
        private RecognizerStatus m_pStatus = RecognizerStatus.NotReady;

        /// <summary>
        /// Specifies if the recongizer is synced.
        /// </summary>
        protected bool m_isSynced = false;

        /// <summary>
        /// Syncronization timeout timer.
        /// </summary>
        protected System.Timers.Timer m_timeoutTimer = new System.Timers.Timer();

        /// <summary>
        /// Synchronization context.
        /// </summary>
        protected SynchronizationContext m_syncContext;

        /// <summary>
        /// Token source used to stop any background tasks.
        /// </summary>
        protected CancellationTokenSource tokenSource = new CancellationTokenSource();

        /// <summary>
        /// Event used to signal the setup thread has finished.
        /// </summary>
        protected readonly AutoResetEvent m_resetEvent = new AutoResetEvent(false);

        #endregion Private Fields
    }
}
