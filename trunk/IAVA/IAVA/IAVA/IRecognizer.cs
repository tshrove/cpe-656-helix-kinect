using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Iava
{
    interface IRecognizer
    {
        #region Properties
        /// <summary>
        /// Gets the file used for configuration of the recognizer.
        /// </summary>
        FileStream Configuration
        {
            get;
        }
        /// <summary>
        /// Gets the status of the cognizer.
        /// </summary>
        RecognizerStatus Status
        {
            get;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Used to initialize the recognizer object using the configuration
        /// file in the parameter of the function.
        /// </summary>
        /// <param name="configurationFile"></param>
        void Create(FileStream configurationFile);
        /// <summary>
        /// Starts the recognizer.
        /// </summary>
        void Start();
        /// <summary>
        /// Stops the recognizer.
        /// </summary>
        void Stop();
        /// <summary>
        /// Used to subscribe the callback delegate with the specific delegate
        /// speficed by the eventName parameter.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="callback"></param>
        void Subscribe(string eventName, Delegate callback);
        /// <summary>
        /// Used to unsubscribe the callback deledate.
        /// </summary>
        /// <param name="eventName"></param>
        void Unsubscribe(string eventName);
        #endregion
    }
}
