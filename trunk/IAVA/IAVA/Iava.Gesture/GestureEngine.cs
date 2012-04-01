using System;
using System.Collections.Generic;
using Iava.Input.Camera;

namespace Iava.Gesture {

    /// <summary>
    /// Manages the partial recognition states off all supported IavaGestures and fires the
    /// appropriate events when one is detected
    /// </summary>
    internal class GestureEngine {

        #region Public Events

        /// <summary>
        /// Fired when an IavaGesture has been detected
        /// </summary>
        public event EventHandler<GestureEventArgs> GestureRecognized;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// Gets/Sets the list of IavaGestures to look for
        /// </summary>
        public List<IavaGesture> SupportedGestures { get; set; }

        /// <summary>
        /// Gets/Sets the 'Sync' IavaGesture
        /// </summary>
        public IavaGesture SyncGesture { get; set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public GestureEngine() {
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Initializes the GestureEngine so the appropriate events are fired when a gesture is recognized
        /// </summary>
        public void Initialize() {
            //Subscribe to the Sync Gesture's Recognized event
            SyncGesture.GestureRecognized += OnGestureRecognized;

            // Subscribe to all the GestureRecognized Events
            SupportedGestures.ForEach(x => x.GestureRecognized += OnGestureRecognized);
        }

        /// <summary>
        /// Checks the provided IavaSkeleton object to see if it completes a gesture
        /// </summary>
        /// <param name="skeleton">IavaSkeleton to check for gesture match</param>
        public void CheckForGesture(IavaSkeleton skeleton) {
            // Check to see if this skeleton completes one of our supported gestures
            foreach (IavaGesture gesture in SupportedGestures) {
                gesture.CheckGesture(skeleton);
            }
        }

        /// <summary>
        /// Checks the provided IavaSkeleton object to see if it completes the Sync gesture
        /// </summary>
        /// <param name="skeleton">IavaSkeleton to check for gesture match</param>
        public void CheckForSyncGesture(IavaSkeleton skeleton) {
            // Check to see if the skeleton completes our sync gesture
            SyncGesture.CheckGesture(skeleton);
        }

        /// <summary>
        /// Fires the GestureRecognized event when a gesture is detected and resets
        /// the partially recognized states off all the supported gestures
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnGestureRecognized(object sender, GestureEventArgs e) {
            // If anyone is interested in the event, throw it
            if (GestureRecognized != null) { GestureRecognized(sender, e); }

            // Reset the current state of all the Gestures
            SupportedGestures.ForEach(x => x.Reset());
        }

        #endregion Public Methods
    }
}
