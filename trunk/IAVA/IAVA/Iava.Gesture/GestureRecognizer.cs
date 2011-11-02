using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Iava;

namespace Iava.Gesture 
{
    public static class GestureRecognizer
    {
        #region Properties
        /// <summary>
        /// Gets the status of the cognizer.
        /// </summary>
        public static RecognizerStatus Status
        {
            get; 
            private set;
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets the file used for configuration of the recognizer.
        /// </summary>
        private static FileStream Configuration
        {
            get;
            set;
        }
        #endregion

        #region Static Events
        /// <summary>
        /// Raises before the recognizer starts.
        /// </summary>
        public static event EventHandler<EventArgs> Started;
        /// <summary>
        /// Raises before the recognizer stops.
        /// </summary>
        public static event EventHandler<EventArgs> Stopped;
        /// <summary>
        /// Raises when the recognizer fails.
        /// </summary>
        public static event EventHandler<EventArgs> Failed;
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        static GestureRecognizer()
        {
            Status = RecognizerStatus.NotReady;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Used to initialize the recognizer object.
        /// </summary>
        public static void Create()
        {
            // TODO Should this be null
            Create(null);
        }
        /// <summary>
        /// Used to initialize the recognizer object using the configuration
        /// file in the parameter of the function.
        /// </summary>
        /// <param name="configurationFile"></param>
        public static void Create(FileStream configurationFile)
        {
            Status = RecognizerStatus.Ready;
            Configuration = configurationFile;
        }
        /// <summary>
        /// Starts the recognizer.
        /// </summary>
        public static void Start()
        {
            // TODO Should be null or something else. Usually it is the object or this that is used.
            OnStarted(null, new EventArgs());
            Status = RecognizerStatus.Running;
        }
        /// <summary>
        ///  Stops the recognizer.
        /// </summary>
        public static void Stop()
        {
            // TODO Should be null or something else. Usually it is the object or this that is used.
            OnStopped(null, new EventArgs());
            Status = RecognizerStatus.Ready;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Raises before the recognizer starts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnStarted(object sender, EventArgs e)
        {
            if (Started != null)
                Started(sender, e);
        }
        /// <summary>
        /// Raises before the recognizer stops.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnStopped(object sender, EventArgs e)
        {
            if (Stopped != null)
                Stopped(sender, e);
        }
        /// <summary>
        /// Raises before the recognizer fails.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnFailed(object sender, EventArgs e)
        {
            if (Failed != null)
                Failed(sender, e);
        }
        #endregion
    }
}
