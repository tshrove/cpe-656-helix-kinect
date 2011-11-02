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
        /// Gets the file used for configuration of the recognizer.
        /// </summary>
        public static FileStream Configuration
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the status of the cognizer.
        /// </summary>
        public static RecognizerStatus Status
        {
            get; 
            private set;
        }
        #endregion

        #region Static Events
        /// <summary>
        /// Raises before the recognizer starts.
        /// </summary>
        public static event EventHandler<EventArgs> RecognizerStarted;
        /// <summary>
        /// Raises before the recognizer stops.
        /// </summary>
        public static event EventHandler<EventArgs> RecognizerStopped;
        #endregion

        #region Public Methods
        /// <summary>
        /// Used to initialize the recognizer object using the configuration
        /// file in the parameter of the function.
        /// </summary>
        /// <param name="configurationFile"></param>
        public static void Create(FileStream configurationFile)
        {
        }
        /// <summary>
        /// Starts the recognizer.
        /// </summary>
        public static void Start()
        {
            // TODO Should be null or something else. Usually it is the object or this that is used.
            OnRecognizerStarted(null, new EventArgs());
        }
        /// <summary>
        ///  Stops the recognizer.
        /// </summary>
        public static void Stop()
        {
            // TODO Should be null or something else. Usually it is the object or this that is used.
            OnRecognizerStopped(null, new EventArgs());
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Raises before the recognizer starts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnRecognizerStarted(object sender, EventArgs e)
        {
            if (RecognizerStarted != null)
                RecognizerStarted(sender, e);
        }
        /// <summary>
        /// Raises before the recognizer stops.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnRecognizerStopped(object sender, EventArgs e)
        {
            if (RecognizerStopped != null)
                RecognizerStopped(sender, e);
        }
        #endregion
    }
}
