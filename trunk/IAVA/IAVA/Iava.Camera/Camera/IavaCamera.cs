using System;

namespace Iava.Input.Camera {

    /// <summary>
    /// Controls access to the KinectSensor
    /// </summary>
    public class IavaCamera {

        #region Public Events

        /// <summary>
        /// Occurs when [image frame ready].
        /// </summary>
        public static event EventHandler<IavaColorImageFrameReadyEventArgs> ImageFrameReady;

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

            // Since there is no such thing as a static destructor do
            // our clean up code when we recognize the ProcessExit event
            AppDomain.CurrentDomain.ProcessExit += ShutDown;
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

        /// <summary>
        /// Initializes the IRuntime device and subscribes to events
        /// </summary>
        private static void InitializeDevice() {
            // Initialize the runtime
            _runtime.Initialize();

            // Register with the IRuntime events
            _runtime.ColorImageFrameReady += OnImageFrameReady;
            _runtime.SkeletonReady += OnSkeletonReady;
            _runtime.SkeletonFrameReady += OnSkeletonFrameReady;
        }

        /// <summary>
        /// Handles the ColorImageFrameReady event of the IRuntime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="IavaColorImageFrameReadyEventArgs"/> instance containing the event data.</param>
        private static void OnImageFrameReady(object sender, IavaColorImageFrameReadyEventArgs e) {
            if (ImageFrameReady != null) { ImageFrameReady(null, e); }
        }

        /// <summary>
        /// Handles the SkeletonReady event of the IRuntime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="IavaSkeletonEventArgs"/> instance containing the event data.</param>
        private static void OnSkeletonReady(object sender, IavaSkeletonEventArgs e) {
            if (SkeletonReady != null) { SkeletonReady(null, e); }
        }

        /// <summary>
        /// Handles the SkeletonFrameReady event of the IRuntime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="IavaSkeletonFrameReadyEventArgs"/> instance containing the event data.</param>
        private static void OnSkeletonFrameReady(object sender, IavaSkeletonFrameReadyEventArgs e) {
            if (SkeletonFrameReady != null) { SkeletonFrameReady(null, e); }
        }

        /// <summary>
        /// Cleans up the IRuntime device.
        /// </summary>
        private static void ShutDown(object sender, EventArgs e) {
            // Unsubscribe from the sensor events
            _runtime.ColorImageFrameReady -= OnImageFrameReady;
            _runtime.SkeletonReady -= OnSkeletonReady;
            _runtime.SkeletonFrameReady -= OnSkeletonFrameReady;

            _runtime.Uninitialize();
        }

        #endregion Private Methods

        #region Private Fields

        private static IRuntime _runtime;

        #endregion Private Fields
    }
}