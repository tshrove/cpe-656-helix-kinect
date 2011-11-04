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
            // Nothing to do.
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
        #endregion
    }
}
