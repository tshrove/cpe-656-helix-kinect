using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Iava.Core;

namespace Iava.Core
{
    /// <summary>
    /// Recognizer Class.
    /// </summary>
    public abstract class Recognizer : IRecognizer
    {
        #region Private Members
        private RecognizerStatus m_pStatus = RecognizerStatus.NotReady;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the status of the cognizer.
        /// </summary>
        public RecognizerStatus Status
        {
            get { return this.m_pStatus; }
            protected set { this.m_pStatus = value; OnStatusChanged(this, new EventArgs()); }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets the file used for configuration of the recognizer.
        /// </summary>
        protected FileStream Configuration
        {
            get;
            set;
        }
        #endregion

        #region Events
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
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public Recognizer()
            : this(null)
        {
            // Nothing to do.
        }
        /// <summary>
        /// Constructor that takes the file and creates the recognizer based on
        /// the file input.
        /// </summary>
        /// <param name="filePath">Path to the configuration file</param>
        public Recognizer(string filePath)
        {
            // TODO Check for correct file path and may want to do stuff with.
            //this.Configuration = new FileStream(filePath, FileMode.Open);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Starts the recognizer.
        /// </summary>
        public abstract void Start();
        /// <summary>
        ///  Stops the recognizer.
        /// </summary>
        public abstract void Stop();
        #endregion

        #region Protected Methods
        /// <summary>
        /// Raises before the recognizer starts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnStarted(object sender, EventArgs e)
        {
            if (Started != null)
                Started(sender, e);
        }
        /// <summary>
        /// Raises before the recognizer stops.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnStopped(object sender, EventArgs e)
        {
            if (Stopped != null)
                Stopped(sender, e);
        }
        /// <summary>
        /// Raises before the recognizer fails.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnFailed(object sender, EventArgs e)
        {
            if (Failed != null)
                Failed(sender, e);
        }
        /// <summary>
        /// Raises when the recognizer is synced.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSynced(object sender, EventArgs e)
        {
            if (Synced != null)
                Synced(sender, e);
        }
        /// <summary>
        /// Raises when the recognizer is unsynced.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnUnsynced(object sender, EventArgs e)
        {
            if (Unsynced != null)
                Unsynced(sender, e);
        }
        /// <summary>
        /// Raises when the status of the recognizer is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnStatusChanged(object sender, EventArgs e)
        {
            if (StatusChanged != null)
                StatusChanged(sender, e);
        }
        #endregion
    }
}
