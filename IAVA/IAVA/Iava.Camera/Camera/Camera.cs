using System;

namespace Iava.Input.Camera {
    /// <summary>
    /// Wraps the Kinect Camera Sensor
    /// </summary>
    public class IavaCamera {

        #region Public Events

        /// <summary>
        /// Occurs when [image frame ready].
        /// </summary>
        public static event EventHandler<IavaImageFrameReadyEventArgs> ImageFrameReady;

        /// <summary>
        /// Occurs when [skeleton ready].
        /// </summary>
        public static event EventHandler<IavaSkeletonEventArgs> SkeletonReady;

        /// <summary>
        /// Occurs when [skeleton frame ready].
        /// </summary>
        public static event EventHandler<IavaSkeletonFrameReadyEventArgs> SkeletonFrameReady;

        #endregion Public Events

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        static IavaCamera() {
            // Set the IRuntime object
            _runtime = new KinectRuntimeWrapper();

            InitializeDevice();
        }

        /// <summary>
        /// Constructor used for unit testing
        /// </summary>
        /// <param name="runtime">IRuntime object to provide the data to the camera</param>
        internal IavaCamera(IRuntime runtime) {
            // Set the IRuntime object
            _runtime = runtime;

            InitializeDevice();
        }

        #endregion Constructors

        #region Private Methods

        private static void InitializeDevice() {
            // Initialize the runtime
            _runtime.Initialize();

            // Register with the IRuntime events
            _runtime.ImageFrameReady += OnImageFrameReady;
            _runtime.SkeletonReady += OnSkeletonReady;
            _runtime.SkeletonFrameReady += OnSkeletonFrameReady;
        }

        /// <summary>
        /// Handles the SkeletonReady event of the IRuntime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SkeletonReadyEventArgs"/> instance containing the event data.</param>
        private static void OnSkeletonReady(object sender, IavaSkeletonEventArgs e) {
            if (SkeletonReady != null) { SkeletonReady(null, e); }
        }

        /// <summary>
        /// Handles the SkeletonFrameReady event of the IRuntime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SkeletonFrameReadyEventArgs"/> instance containing the event data.</param>
        private static void OnSkeletonFrameReady(object sender, IavaSkeletonFrameReadyEventArgs e) {
            if (SkeletonFrameReady != null) { SkeletonFrameReady(null, e); }
        }

        /// <summary>
        /// Handles the ImageFrameReady event of the IRuntime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ImageFrameReadyEventArgs"/> instance containing the event data.</param>
        private static void OnImageFrameReady(object sender, IavaImageFrameReadyEventArgs e) {
            if (ImageFrameReady != null) { ImageFrameReady(null, e); }
        }

        #endregion Private Methods

        #region Private Fields

        private static IRuntime _runtime;

        #endregion Private Fields
    }
}
