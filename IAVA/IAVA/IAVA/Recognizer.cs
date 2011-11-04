using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Iava.Core;

namespace Iava.Core
{
    public abstract class Recognizer : IRecognizer
    {
        #region Properties
        /// <summary>
        /// Gets the status of the cognizer.
        /// </summary>
        public RecognizerStatus Status
        {
            get; 
            protected set;
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

        #region Static Events
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
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public Recognizer()
            :this(null)
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
            this.Configuration = new FileStream(filePath, FileMode.Open);
            this.Status = RecognizerStatus.NotReady;
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
        #endregion
    }
}
