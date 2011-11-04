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

        #region Private Members
        /// <summary>
        /// Holds the callback for each gesture.
        /// </summary>
        private Dictionary<string, GestureCallback> m_pGestureCallbacks = new Dictionary<string, GestureCallback>();
        #endregion

        #region Private Properties
        /// <summary>
        /// Holds the callbacks for each gesture.
        /// </summary>
        private Dictionary<string, GestureCallback> GestureCallbacks
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
        public void Subscribe(string name, GestureCallback callBack)
        {
            GestureCallbacks["name"] = callBack;
        }
        /// <summary>
        /// Unsubscribe the given delegate from the given delegate
        /// by the name.
        /// </summary>
        /// <param name="name"></param>
        public void Unsubscribe(string name)
        {
            GestureCallbacks["name"] = null;
        }
        #endregion
    }
}
