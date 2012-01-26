using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Iava.Core;
using Iava.Input.Camera;
using Iava.Gesture.GestureStuff;
using System.Threading;

namespace Iava.Gesture 
{
    /// <summary>
    /// Audio Callback for when a gesture command is detected.
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
            if (m_thread.ThreadState == ThreadState.Stopped)
            {
                m_thread = new Thread(SetupGestureDevice);
                m_thread.Name = "GestureRecognizerThread";
            }
            m_thread.Start();
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
            if (string.IsNullOrEmpty(name)) {
                throw new ArgumentException("Name argument was either null or empty.", "name");
            }

            if (callBack == null) {
                throw new ArgumentException("Callback argument was null.", "callback");
            }

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
            if (!string.IsNullOrEmpty(name) && GestureCallbacks.ContainsKey(name)) {
                GestureCallbacks.Remove(name);
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

            // Don't really like this here, but the UI
            // depends on this property being set in
            // the constructor...
            Camera = new Camera();

            // Initialize our collections...
            GestureCallbacks = new Dictionary<string, GestureCallback>();
            SupportedGestures = new List<GestureStuff.Gesture>();

            // Set up the thread
            m_thread = new Thread(SetupGestureDevice);
            m_thread.Name = "GestureRecognizerThread";
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

        /// <summary>
        /// The sync gesture
        /// </summary>
        private Gesture.GestureStuff.Gesture SyncGesture { get; set; }

        #endregion Private Properties

        #region Private Methods

        private void LoadGestures() {
            // TODO: Reading in the config file needs to happen here

            // NEM: For the prototype we will manually load hard-coded gestures

            // Sync Gesture
            List<IGestureSegment> syncSegments = new List<IGestureSegment>();
            for (int i = 0; i < 20; i++) { syncSegments.Add(new SyncSegment()); }

            // Add the gesture as our Sync Gestyre
            SyncGesture = new GestureStuff.Gesture("Sync", syncSegments);
            SyncGesture.GestureRecognized += OnGestureRecognized;
            
            // Left Swipe
            List<IGestureSegment> swipeLeftSegments = new List<IGestureSegment>();
            swipeLeftSegments.Add(new SwipeLeftSegment1());
            swipeLeftSegments.Add(new SwipeLeftSegment2());
            swipeLeftSegments.Add(new SwipeLeftSegment3());

            // Add the gesture to our supported types and register for its recognized event
            SupportedGestures.Add(new GestureStuff.Gesture("Swipe Left", swipeLeftSegments));
            SupportedGestures.Last().GestureRecognized += OnGestureRecognized;

            // Right Swipe
            List<IGestureSegment> swipeRightSegments = new List<IGestureSegment>();
            swipeRightSegments.Add(new SwipeRightSegment1());
            swipeRightSegments.Add(new SwipeRightSegment2());
            swipeRightSegments.Add(new SwipeRightSegment3());

            // Add the gesture to our supported types and register for its recognized event
            SupportedGestures.Add(new GestureStuff.Gesture("Swipe Right", swipeRightSegments));
            SupportedGestures.Last().GestureRecognized += OnGestureRecognized;

            // Up Swipe
            List<IGestureSegment> swipeUpSegments = new List<IGestureSegment>();
            swipeUpSegments.Add(new SwipeUpSegment1());
            swipeUpSegments.Add(new SwipeUpSegment2());
            swipeUpSegments.Add(new SwipeUpSegment3());

            // Add the gesture to our supported types and register for its recognized event
            SupportedGestures.Add(new GestureStuff.Gesture("Swipe Up", swipeUpSegments));
            SupportedGestures.Last().GestureRecognized += OnGestureRecognized;

            // Down Swipe
            List<IGestureSegment> swipeDownSegments = new List<IGestureSegment>();
            swipeDownSegments.Add(new SwipeDownSegment1());
            swipeDownSegments.Add(new SwipeDownSegment2());
            swipeDownSegments.Add(new SwipeDownSegment3());

            // Add the gesture to our supported types and register for its recognized event
            SupportedGestures.Add(new GestureStuff.Gesture("Swipe Down", swipeDownSegments));
            SupportedGestures.Last().GestureRecognized += OnGestureRecognized;

            // Zoom In
            List<IGestureSegment> zoomInSwipeSegments = new List<IGestureSegment>();
            zoomInSwipeSegments.Add(new ZoomInSegment1());
            zoomInSwipeSegments.Add(new ZoomInSegment2());

            // Add the gesture to our supported types and register for its recognized event
            SupportedGestures.Add(new GestureStuff.Gesture("Zoom In", zoomInSwipeSegments));
            SupportedGestures.Last().GestureRecognized += OnGestureRecognized;

            // Zoom Out
            List<IGestureSegment> zoomOutSwipeSegments = new List<IGestureSegment>();
            zoomOutSwipeSegments.Add(new ZoomOutSegment1());
            zoomOutSwipeSegments.Add(new ZoomOutSegment2());

            // Add the gesture to our supported types and register for its recognized event
            SupportedGestures.Add(new GestureStuff.Gesture("Zoom Out", zoomOutSwipeSegments));
            SupportedGestures.Last().GestureRecognized += OnGestureRecognized;
        }
        
        private void OnGestureRecognized(object sender, GestureEventArgs e) {
            // If we just synced, set the flag and return
            if (e.Name == SyncGesture.Name) {
                m_syncContext.Post(new SendOrPostCallback(delegate(object state) { OnSynced(this, e); }), null);
                return;
            }

            if (GestureCallbacks.ContainsKey(e.Name)) {
                // We found a command, reset the timer
                ResetTimer();

                // Throw the gesture event
                m_syncContext.Post(new SendOrPostCallback(delegate(object state) { GestureCallbacks[e.Name].Invoke(e); }), null);

                // Reset all the gesture states
                SupportedGestures.ForEach(x => x.Reset());
            }

            else { /* No one cares about this gesture =( */ }
        }

        private void OnSkeletonReady(object sender, SkeletonEventArgs e) {

            // If we're synced up look for gestures
            if (m_isSynced) {
                // Check to see if this skeleton frame completes one of our supported gestures
                foreach (Gesture.GestureStuff.Gesture gesture in SupportedGestures) {
                    gesture.CheckForGesture(e.Skeleton);
                }
            }

            // Check for the sync gesture
            else { SyncGesture.CheckForGesture(e.Skeleton); }
        }

        private void SetupGestureDevice() {
            // Try to connect to the camera first.  If this fails there is no point in continuing
            try {
                //Camera = new Camera();

                // Register with some camera events
                Camera.SkeletonReady += OnSkeletonReady;

                // Read the gestures in from the config file
                LoadGestures();

                Status = RecognizerStatus.Running;
            }
            catch (Exception e) {
                // TODO: Log message.  Failed to detect Kinect or start Camera
                Status = RecognizerStatus.Error;
            }
        }

        #endregion Private Methods
    }
}