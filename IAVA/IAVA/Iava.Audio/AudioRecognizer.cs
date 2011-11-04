using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iava.Core;

namespace Iava.Audio 
{
    /// <summary>
    /// Audio Callback for when a audio command is detected.
    /// </summary>
    /// <param name="e"></param>
    public delegate void AudioCallback(AudioEventArgs e);
    /// <summary>
    /// AudioRecognizer Class
    /// </summary>
    public class AudioRecognizer : Recognizer
    {
        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filePath"></param>
        public AudioRecognizer(string filePath)
            :base(filePath)
        {
            this.AudioCallbacks = new Dictionary<string, AudioCallback>();
            // TODO Add the function to load the gestures to the dictionary.
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Holds the callbacks for each gesture.
        /// </summary>
        private Dictionary<string, AudioCallback> AudioCallbacks
        {
            get;
            set;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Starts the recognizer.
        /// </summary>
        public override void Start()
        {
            Status = RecognizerStatus.Running;
            OnStarted(this, new EventArgs());  
        }
        /// <summary>
        ///  Stops the recognizer.
        /// </summary>
        public override void Stop()
        {
            Status = RecognizerStatus.Ready;
            OnStopped(this, new EventArgs());     
        }
        /// <summary>
        /// Used to connect a given delegate to a specified gesture
        /// given by the name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="d"></param>
        public void Subscribe(string name, AudioCallback callBack)
        {
            if (this.AudioCallbacks.ContainsKey(name))
            {
                this.AudioCallbacks[name] = callBack;
            }
        }
        /// <summary>
        /// Unsubscribe the given delegate from the given delegate
        /// by the name.
        /// </summary>
        /// <param name="name"></param>
        public void Unsubscribe(string name)
        {
            if (this.AudioCallbacks.ContainsKey(name))
            {
                this.AudioCallbacks[name] = null;
            }
        }
        #endregion
    }
}
