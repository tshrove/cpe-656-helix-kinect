using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace Iava
{
    public abstract class Recognizer : IRecognizer
    {
        #region Public Properties
        /// <summary>
        /// Gets the file used for configuration of the recognizer.
        /// </summary>
        public FileStream Configuration
        {
            get;
            protected set;
        }
        /// <summary>
        /// Gets the status of the cognizer.
        /// </summary>
        public RecognizerStatus Status
        {
            get;
            protected set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Recognizer()
            :this(null)
        {
            // Nothing to do.
        }
        /// <summary>
        /// Initializes the recognizers.
        /// </summary>
        /// <param name="configurationFile"></param>
        public Recognizer(FileStream configurationFile)
        {
            this.Configuration = configurationFile;
            this.Status = RecognizerStatus.Uninitialized;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Used to initialize the recognizer object using the configuration
        /// file in the parameter of the function.
        /// </summary>
        /// <param name="configurationFile"></param>
        public abstract void Create(FileStream configurationFile);
        /// <summary>
        /// Starts the recognizer.
        /// </summary>
        public virtual void Start()
        {

        }
        /// <summary>
        ///  Stops the recognizer.
        /// </summary>
        public virtual void Stop()
        {
           
        }
        /// <summary>
        /// Used to subscribe the callback delegate with the specific delegate
        /// speficed by the eventName parameter.
        /// </summary>
        /// <param name="eventName">Name of event to subscribe to</param>
        /// <param name="callback"></param>
        public virtual void Subscribe(string eventName, Delegate callback)
        {
            
        }
        /// <summary>
        /// Used to unsubscribe the callback deledate.
        /// </summary>
        /// <param name="eventName"></param>
        public virtual void Unsubscribe(string eventName)
        {
            
        }
        #endregion
    }
}
