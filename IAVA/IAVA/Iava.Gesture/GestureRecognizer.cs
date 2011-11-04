using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Iava.Core;

namespace Iava.Gesture 
{
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
            OnStarted(null, new EventArgs());
            Status = RecognizerStatus.Running;
        }
        /// <summary>
        ///  Stops the recognizer.
        /// </summary>
        public override void Stop()
        {
            OnStopped(null, new EventArgs());
            Status = RecognizerStatus.Ready;
        }
        #endregion
    }
}
