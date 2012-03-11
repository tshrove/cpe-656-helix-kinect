using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iava.Core;
using Iava.Core.Math;
using Iava.Input.Camera;

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
        /*
        /// <summary>
        /// Not sure if we want to make this public or not.
        /// </summary>
        public Camera Camera { get; private set; }*/

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Starts the recognizer.
        /// </summary>
        public override void Start() {
            if (Status != RecognizerStatus.Running) {
                Task.Factory.StartNew(() => SetupGestureDevice(tokenSource.Token), tokenSource.Token);

                // Wait for the SetupGestureDevice call to complete
                m_resetEvent.WaitOne();
                
                // Reset the event
                m_resetEvent.Reset();

                // TODO: Should this be called inside the task?
                OnStarted(this, new EventArgs());
            }
        }

        /// <summary>
        ///  Stops the recognizer.
        /// </summary>
        public override void Stop() {
            tokenSource.Cancel();
            tokenSource = new CancellationTokenSource();

            // Unsubscribe from the events
            IavaCamera.SkeletonReady -= OnSkeletonReady;

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
            if (string.IsNullOrEmpty(name)) {
                throw new ArgumentException("Name argument was either null or empty.", "name");
            }

            if (GestureCallbacks.ContainsKey(name)) {
                GestureCallbacks.Remove(name);
            }
        }

        #endregion Public Methods

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filePath">Path to gesture file</param>
        public GestureRecognizer(string filePath)
            : base() {

            if (string.IsNullOrEmpty(filePath)) {
                throw new ArgumentException("Filepath argument was either null or empty.", "filePath");
            }

            if (!Directory.Exists(filePath)) {
                throw new ArgumentException("Specified directory does not exist.", "filePath");
            }

            _filepath = filePath;

            // Initialize our collections...
            GestureCallbacks = new Dictionary<string, GestureCallback>();
            SupportedGestures = new List<IavaGesture>();

            _engine = new GestureEngine();
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
        private List<IavaGesture> SupportedGestures { get; set; }

        /// <summary>
        /// The sync gesture
        /// </summary>
        private IavaGesture SyncGesture {
            get {
                return _syncGesture;
            }
            set {
                if (value == null) {
                    throw new ArgumentException("value", "Sync Gesture was null.");
                }

                _syncGesture = value;

                if (Status == RecognizerStatus.Running) {
                    Stop();
                    Start();
                }
            }
        }

        #endregion Private Properties

        #region Private Methods
        
        private void SetupGestureDevice(CancellationToken token) {
            try {
                if (!token.IsCancellationRequested) {
                    // Read the gestures in from the config file
                    SupportedGestures = GestureFolderReader.Read(_filepath);
                    SyncGesture = SupportedGestures.Single(x => x.Name == "Sync");

                    // Set the Gestures for our GestureEngine
                    _engine.SyncGesture = SyncGesture;
                    _engine.SupportedGestures = SupportedGestures;
                    _engine.Initialize();

                    Status = RecognizerStatus.Running;
                }

                // Register with some camera events
                IavaCamera.SkeletonReady += OnSkeletonReady;
                _engine.GestureRecognized += OnGestureRecognized;
            }

            catch (Exception e) {
                // TODO: Log message.  Failed to detect Kinect or start Camera
                Status = RecognizerStatus.Error;
                throw e;
            }

            finally { m_resetEvent.Set(); }
        }

        #endregion Private Methods

        #region Private Fields

        private IavaGesture _syncGesture;

        private string _filepath;

        private GestureEngine _engine;

        #endregion Private Fields

        #region Protected Methods

        protected void OnGestureRecognized(object sender, GestureEventArgs e) {
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

        protected void OnSkeletonReady(object sender, IavaSkeletonEventArgs e) {
            IavaSkeletonPoint translationVector = e.Skeleton.Joints[IavaJointType.HipCenter].Position;

            IavaJoint joint = new IavaJoint();

            // Translate all the points in the skeleton to the center of the kinect view
            for (IavaJointType jointID = 0; jointID < IavaJointType.Count; jointID++) {
                // Refer to http://stackoverflow.com/questions/1003772/setting-margin-properties-in-code
                // for why things are done this way
                joint = e.Skeleton.Joints[jointID];
                joint.Position = Geometry.Translate(joint.Position, translationVector);

                // Set the joint to the joint with the updated position
                e.Skeleton.Joints[jointID] = joint;
            }

            // If we're synced up look for gestures
            if (m_isSynced) { _engine.CheckForGesture(e.Skeleton); }

            // Check for the sync gesture
            else { _engine.CheckForSyncGesture(e.Skeleton); }
        }

        #endregion Protected Methods
    }
}