using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.Kinect.Nui;
using Iava.Input.Camera;

namespace Iava.Gesture {

    /// <summary>
    /// Wraps Microsoft Kinect's Runtime class so it conforms to the
    /// IRuntime interface.
    /// </summary>
    internal class RuntimeWrapper : IRuntime {

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public RuntimeWrapper() {
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
        /// Responsible for initializing the Kinect Runtime
        /// </summary>
        /// <returns>bool value indicating whether the Runtime initialized correctly</returns>
        private bool InitializeNui() {
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
                throw exception;
            }

            // Open the RGB camera
            _kinectRuntime.VideoStream.Open((ImageStreamType)ImageStreamType.Video, 2,
                                                  (ImageResolution)ImageResolution.Resolution640x480,
                                                  (ImageType)ImageType.Color);

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
        /// <param name="e">The <see cref="SkeletonFrameReadyEventArgs"/> instance containing the event data.</param>
        private void OnSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e) {
            if (SkeletonFrameReady != null) {
                // Rethrow this event if someone needs it...
                SkeletonFrameReady(null, new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)e.SkeletonFrame));
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
                            SkeletonReady(null, new IavaSkeletonEventArgs(skeleton));
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
        /// <param name="e">The <see cref="ImageFrameReadyEventArgs"/> instance containing the event data.</param>
        private void OnVideoFrameReady(object sender, ImageFrameReadyEventArgs e) {
            if (ImageFrameReady != null) { ImageFrameReady(null, (ImageFrameReadyEventArgs)e); }
        }

        #endregion Private Methods

        #region Private Fields

        /// <summary>
        /// Reference to the speech recognition engine.
        /// </summary>
        private static Runtime _kinectRuntime;

        #endregion Private Fields

        #region IRuntime Members

        public event EventHandler<IavaSkeletonEventArgs> SkeletonReady;

        public event EventHandler<IavaImageFrameReadyEventArgs> ImageFrameReady;

        public event EventHandler<IavaSkeletonFrameReadyEventArgs> SkeletonFrameReady;

        #endregion IRuntime Members
    }
}
