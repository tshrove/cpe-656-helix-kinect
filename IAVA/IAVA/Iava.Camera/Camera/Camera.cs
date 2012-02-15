using System;
using System.Collections.Generic;
using Microsoft.Research.Kinect.Nui;

namespace Iava.Input.Camera {
    /// <summary>
    /// Wraps the Kinect Camera Sensor
    /// </summary>
    public static class Camera {

        #region Public Events

        /// <summary>
        /// Occurs when [skeleton ready].
        /// </summary>
        public static event EventHandler<SkeletonEventArgs> SkeletonReady;

        /// <summary>
        /// Occurs when [image frame ready].
        /// </summary>
        public static event EventHandler<ImageFrameReadyEventArgs> ImageFrameReady;

        /// <summary>
        /// Occurs when [skeleton frame complete].
        /// </summary>
        //public event EventHandler<SkeletonFrameEventArgs> SkeletonFrameComplete;

        public static event EventHandler<SkeletonFrameReadyEventArgs> SkeletonFrameReady;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// Gets the tilt angle of the camera
        /// </summary>
        public static int TiltAngle {
            get { return _kinectRuntime.NuiCamera.ElevationAngle; }
            set {
                if ((value > -28) && (value < 28)) {
                    _kinectRuntime.NuiCamera.ElevationAngle = value;
                }
            }
        }

        #endregion Public Properties

        #region Public Methods
        /*
        /// <summary>
        /// Tilts the camera down 1 degree
        /// </summary>
        public static void TiltDown() {
            // Valid Camera angle range is -27 to 27 degrees
            if (TiltAngle > -27) { TiltAngle -= 1; }
        }

        /// <summary>
        /// Tilts the camera up 1 degree
        /// </summary>
        public static void TiltUp() {
            // Valid Camera angle range is -27 to 27 degrees
            if (TiltAngle < 27) { TiltAngle += 1; }
        }
        */
        #endregion Public Methods

        #region Constructors

        static Camera() {
            if (InitializeNui()) {
                // Register with the Camera events
                _kinectRuntime.SkeletonFrameReady += OnSkeletonFrameReady;
                _kinectRuntime.VideoFrameReady += OnVideoFrameReady;
            }

            else { throw new Exception("Error initializing camera"); }
        }

        #endregion Constructors

        #region Private Methods

        /// <summary>
        /// Responsible for initializing the Kinect Camera
        /// </summary>
        /// <returns>bool value indicating whether the Camera initialized correctly</returns>
        private static bool InitializeNui() {
            // Create the Kinect Runtime object...
            _kinectRuntime = Runtime.Kinects[0];

            // Odds are a Kinect camera is not plugged in...
            if (_kinectRuntime == null) { Console.WriteLine("Kinect camera not detected!"); return false; }

            try {
                // Initialize the Depth, Skeleton, and RGB cameras
                _kinectRuntime.Initialize(RuntimeOptions.UseDepth | RuntimeOptions.UseSkeletalTracking | RuntimeOptions.UseColor);
            }

            // NEM: Probably want to throw this at some point instead of returning booleans
            catch (Exception exception) {
                Console.WriteLine(exception.ToString());
                return false;
            }

            // Open the RGB camera
            _kinectRuntime.VideoStream.Open((Microsoft.Research.Kinect.Nui.ImageStreamType)ImageStreamType.Video, 2,
                                                  (Microsoft.Research.Kinect.Nui.ImageResolution)ImageResolution.Resolution640x480,
                                                  (Microsoft.Research.Kinect.Nui.ImageType)ImageType.Color);

            // Create our smoothing parameters
            var smoothingParams = new TransformSmoothParameters
            {
                Smoothing = 0.75f,
                Correction = 0.00f,
                Prediction = 0.00f,
                JitterRadius = 0.05f,
                MaxDeviationRadius = 0.04f
            };

            _kinectRuntime.SkeletonEngine.TransformSmooth = true;
            _kinectRuntime.SkeletonEngine.SmoothParameters = smoothingParams;

            // If we've made it here we're good.
            return true;
        }

        /// <summary>
        /// Handles the SkeletonFrameReady event of the Kinect's RunTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Research.Kinect.Nui.SkeletonFrameReadyEventArgs"/> instance containing the event data.</param>
        private static void OnSkeletonFrameReady(object sender, Microsoft.Research.Kinect.Nui.SkeletonFrameReadyEventArgs e) {
            if (SkeletonFrameReady != null) {
                // Rethrow this event if someone needs it...
                SkeletonFrameReady(null, new SkeletonFrameReadyEventArgs((SkeletonFrame)e.SkeletonFrame));
            }

            List<int> idValues = new List<int>();

            if (e.SkeletonFrame == null) { return; }

            if (e.SkeletonFrame.Skeletons.Length > 0) {
                // Process each skeleton in the event...
                foreach (SkeletonData skeleton in e.SkeletonFrame.Skeletons) {
                    // If the Kinect camera is tracking this skeleton...
                    if (skeleton.TrackingState == SkeletonTrackingState.Tracked) {
                        // Create and throw the SkeletonReady event...
                        if (SkeletonReady != null) {
                            SkeletonReady(null, new SkeletonEventArgs(skeleton));
                        }
                        // Add the Tracking ID to our watch list...
                        idValues.Add(skeleton.TrackingID);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the VideoFrameReady event of the Kinect's RunTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Research.Kinect.Nui.ImageFrameReadyEventArgs"/> instance containing the event data.</param>
        private static void OnVideoFrameReady(object sender, Microsoft.Research.Kinect.Nui.ImageFrameReadyEventArgs e) {
            if (ImageFrameReady != null) { ImageFrameReady(null, (ImageFrameReadyEventArgs)e); }
        }

        #endregion Private Methods

        #region Private Fields

        private static Runtime _kinectRuntime;

        #endregion Private Fields
    }
}
