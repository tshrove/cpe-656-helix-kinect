using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Iava.Core;
using Iava.Input.Camera;
using Iava.Gesture.GestureStuff;

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
    public class GestureRecognizer : Recognizer {

        #region Public Properties

        /// <summary>
        /// Not sure if we want to make this public or not.
        /// </summary>
        public Camera Camera { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Starts the recognizer.
        /// </summary>
        public override void Start() {
            Status = RecognizerStatus.Running;
            OnStarted(this, new EventArgs());
        }

        /// <summary>
        ///  Stops the recognizer.
        /// </summary>
        public override void Stop() {
            Status = RecognizerStatus.Ready;
            OnStopped(this, new EventArgs());
        }

        /// <summary>
        /// Used to connect a given delegate to a specified gesture
        /// given by the name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="d"></param>
        public void Subscribe(string name, GestureCallback callBack) {
            if (!GestureCallbacks.ContainsKey(name)) {
                GestureCallbacks.Add(name, callBack);
            }
        }

        /// <summary>
        /// Unsubscribe the given delegate from the given delegate
        /// by the name.
        /// </summary>
        /// <param name="name"></param>
        public void Unsubscribe(string name) {
            if (GestureCallbacks.ContainsKey(name)) {
                GestureCallbacks[name] = null;
            }
        }

        #endregion Public Methods

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filePath"></param>
        public GestureRecognizer(string filePath)
            : base(filePath) {
            // Try to connect to the camera first.  If this fails there is no point in continuing
            try {
                Camera = new Camera();
            }
            catch (Exception e) {
                throw e;
            }

            // Initialize our collections...
            GestureCallbacks = new Dictionary<string, GestureCallback>();
            SupportedGestures = new List<GestureStuff.Gesture>();

            // Register with some camera events
            Camera.SkeletonReady += OnSkeletonReady;

            // Read the gestures in from the config file
            LoadGestures();
        }

        #endregion Constructors

        #region Private Properties

        /// <summary>
        /// Holds the callbacks for each gesture.
        /// </summary>
        private Dictionary<string, GestureCallback> GestureCallbacks { get; set; }

        /// <summary>
        /// Holds all the gestures this recognizer supports in a Name, Gesture pair
        /// </summary>
        private List<Gesture.GestureStuff.Gesture> SupportedGestures { get; set; }

        #endregion Private Properties

        #region Private Methods

        private void LoadGestures() {
            // TODO: Reading in the config file needs to happen here

            // NEM: For the prototype we will manually load hard-coded gestures
            List<IGestureSegment> segments = new List<IGestureSegment>();
            segments.Add(new LeftSwipeSegment1());
            segments.Add(new LeftSwipeSegment2());
            segments.Add(new LeftSwipeSegment3());

            // Add the gesture to our supported types and register for its recognized event
            SupportedGestures.Add(new GestureStuff.Gesture("Left Swipe", segments));
            SupportedGestures.Last().GestureRecognized += OnGestureRecognized;
        }
        
        private void OnGestureRecognized(object sender, GestureEventArgs e) {
            if (GestureCallbacks.ContainsKey(e.Name)) {
                GestureCallbacks[e.Name].Invoke(e);

                // Reset all the gesture states to be ready for the next one
            }

            else { /* No one cares about this gesture */ }
        }

        private void OnSkeletonReady(object sender, SkeletonEventArgs e) {
            // Check to see if this skeleton frame completes one of our supported gestures
            foreach (Gesture.GestureStuff.Gesture gesture in SupportedGestures) {
                gesture.CheckForGesture(e.Skeleton);
            }
        }

        #endregion Private Methods
    }
}
