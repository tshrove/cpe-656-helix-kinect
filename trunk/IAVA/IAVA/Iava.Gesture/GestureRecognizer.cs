using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Iava.Core;

namespace Iava.Gesture 
{
    /// <summary>
    /// Audio Callback for when a audio command is detected.
    /// </summary>
    /// <param name="e"></param>
    public delegate void GestureCallback(GestureEventArgs e);
    /// <summary>
    /// GestureRecognizer Class
    /// </summary>
    public class GestureRecognizer : Recognizer
    {
        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filePath"></param>
        public GestureRecognizer(string filePath)
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
