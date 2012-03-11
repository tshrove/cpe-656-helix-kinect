using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace Iava.Input.Camera {

    /// <summary>
    /// Wraps Microsoft Kinect's Runtime class so it conforms to the
    /// IRuntime interface.
    /// </summary>
    internal class KinectRuntimeWrapper : IRuntime {

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public KinectRuntimeWrapper() {
            _kinectSensor = KinectSensor.KinectSensors[0];

            if (_kinectSensor == null) { throw new Exception("Kinect camera not detected."); }

            // Register for the Skeleton and Video Frame Events
            _kinectSensor.SkeletonFrameReady += OnSkeletonFrameReady;
            _kinectSensor.ColorFrameReady += OnColorFrameReady;
        }

        #endregion Constructors

        #region Private Methods

        /// <summary>
        /// Handles the SkeletonFrameReady event of the Kinect's RunTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SkeletonFrameReadyEventArgs"/> instance containing the event data.</param>
        private void OnSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e) {
            Skeleton[] skeletons = null;

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame()) {
                bool receivedData = false;
                if (skeletonFrame != null) {

                    // Allocate only the first time
                    if (skeletons == null) {
                        skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                        skeletonFrame.CopySkeletonDataTo(skeletons);
                    }

                    // Mark that we received new data
                    receivedData = true;
                }

                if (receivedData) {

                    if (SkeletonFrameReady != null) {
                        // Rethrow this event if someone needs it...
                        SkeletonFrameReady(null, new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)skeletonFrame));
                    }

                    // Process each skeleton in the event...
                    foreach (Skeleton skeleton in skeletons) {
                        // If the Kinect camera is tracking this skeleton...
                        if (skeleton.TrackingState == SkeletonTrackingState.Tracked) {
                            // Create and throw the SkeletonReady event...
                            if (SkeletonReady != null) {
                                SkeletonReady(null, new IavaSkeletonEventArgs(skeleton));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the VideoFrameReady event of the Kinect's RunTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ImageFrameReadyEventArgs"/> instance containing the event data.</param>
        private void OnColorFrameReady(object sender, ColorImageFrameReadyEventArgs e) {
            if (ColorImageFrameReady != null) { ColorImageFrameReady(null, (ColorImageFrameReadyEventArgs)e); }
        }

        #endregion Private Methods

        #region Private Fields

        /// <summary>
        /// Reference to the speech recognition engine.
        /// </summary>
        private static KinectSensor _kinectSensor;

        #endregion Private Fields

        #region IRuntime Members

        public event EventHandler<IavaSkeletonEventArgs> SkeletonReady;

        public event EventHandler<IavaColorImageFrameReadyEventArgs> ColorImageFrameReady;

        public event EventHandler<IavaSkeletonFrameReadyEventArgs> SkeletonFrameReady;
        
        /// <summary>
        /// Responsible for initializing the Kinect Sensor
        /// </summary>
        public void Initialize() {
            // Create our smoothing parameters for the Skeleton Stream
            var smoothingParams = new TransformSmoothParameters
            {
                Smoothing = 0.75f,
                Correction = 0.00f,
                Prediction = 0.00f,
                JitterRadius = 0.05f,
                MaxDeviationRadius = 0.04f
            };

            // Initialize the Depth, Skeleton, and RGB cameras
            _kinectSensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            _kinectSensor.DepthStream.Enable(DepthImageFormat.Resolution320x240Fps30);
            _kinectSensor.SkeletonStream.Enable(smoothingParams);

            // Open the RGB camera
            _kinectSensor.Start();
        }

        /// <summary>
        /// Tears down the Kinect Sensor
        /// </summary>
        public void Uninitialize() {
            _kinectSensor.Stop();

            if (_kinectSensor.ColorStream != null) { _kinectSensor.ColorStream.Disable(); }
            if (_kinectSensor.DepthStream != null) { _kinectSensor.DepthStream.Disable(); }
            if (_kinectSensor.SkeletonStream != null) { _kinectSensor.SkeletonStream.Disable(); }
        }

        #endregion IRuntime Members
    }
}
