﻿using System;
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
        public static event EventHandler<IavaSkeletonEventArgs> SkeletonReady;

        /// <summary>
        /// Occurs when [image frame ready].
        /// </summary>
        public static event EventHandler<IavaImageFrameReadyEventArgs> ImageFrameReady;

        /// <summary>
        /// Occurs when [skeleton frame complete].
        /// </summary>
        //public event EventHandler<SkeletonFrameEventArgs> SkeletonFrameComplete;

        public static event EventHandler<IavaSkeletonFrameReadyEventArgs> SkeletonFrameReady;

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
                throw exception;
            }

            // Open the RGB camera
            _kinectRuntime.VideoStream.Open((ImageStreamType)ImageStreamType.Video, 2,
                                                  (ImageResolution)IavaImageResolution.Resolution640x480,
                                                  (ImageType)IavaImageType.Color);

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
        private static void OnSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e) {
            if (SkeletonFrameReady == null) { return; }

            if (e == null) { return; }

            if (e.SkeletonFrame == null) { return; }

            // Rethrow this event if someone needs it...
            SkeletonFrameReady(null, new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)e.SkeletonFrame));

            List<int> idValues = new List<int>();

            if (e.SkeletonFrame == null) { return; }

            if (e.SkeletonFrame.Skeletons.Length > 0) {
                // Process each skeleton in the event...
                foreach (IavaSkeletonData skeleton in e.SkeletonFrame.Skeletons) {
                    // If the Kinect camera is tracking this skeleton...
                    if (skeleton.TrackingState == IavaSkeletonTrackingState.Tracked) {
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
        private static void OnVideoFrameReady(object sender, ImageFrameReadyEventArgs e) {
            if (ImageFrameReady != null) { ImageFrameReady(null, (IavaImageFrameReadyEventArgs)e); }
        }

        #endregion Private Methods

        #region Private Fields

        private static Runtime _kinectRuntime;

        #endregion Private Fields
    }
}
