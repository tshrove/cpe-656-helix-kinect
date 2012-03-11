using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iava.Input.Camera;

namespace Iava.Gesture {
    internal class GestureEngine {

        #region Public Events

        public event EventHandler<GestureEventArgs> GestureRecognized;

        #endregion Public Events

        #region Public Properties

        public List<IavaGesture> SupportedGestures { get; set; }

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

        public void Initialize() {
            //Subscribe to the Sync Gesture's Recognized event
            SyncGesture.GestureRecognized += OnGestureRecognized;

            // Subscribe to all the GestureRecognized Events
            SupportedGestures.ForEach(x => x.GestureRecognized += OnGestureRecognized);
        }

        public void CheckForGesture(IavaSkeleton skeleton) {
            // Check to see if this skeleton completes one of our supported gestures
            foreach (IavaGesture gesture in SupportedGestures) {
                gesture.CheckGesture(skeleton);
            }
        }

        public void CheckForSyncGesture(IavaSkeleton skeleton) {
            // Check to see if the skeleton completes our sync gesture
            SyncGesture.CheckGesture(skeleton);
        }

        public void OnGestureRecognized(object sender, GestureEventArgs e) {
            // If anyone is interested in the event, throw it
            if (GestureRecognized != null) { GestureRecognized(sender, e); }

            // Reset the current state of all the Gestures
            SupportedGestures.ForEach(x => x.Reset());
        }

        #endregion Public Methods
    }
}
