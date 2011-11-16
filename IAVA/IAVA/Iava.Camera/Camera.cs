﻿using System;
using System.Collections.Generic;
using Microsoft.Research.Kinect.Nui;

namespace Iava.Input {
    /// <summary>
    /// Wraps the Kinect Camera Sensor
    /// </summary>
    sealed class Camera {

        #region Public Events

        /// <summary>
        /// Occurs when [skeleton ready].
        /// </summary>
        public event EventHandler<SkeletonEventArgs> SkeletonReady;

        /// <summary>
        /// Occurs when [image frame ready].
        /// </summary>
        public event EventHandler<ImageFrameReadyEventArgs> ImageFrameReady;

        /// <summary>
        /// Occurs when [skeleton frame complete].
        /// </summary>
        public event EventHandler<SkeletonFrameEventArgs> SkeletonFrameComplete;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// Gets the tilt angle of the camera
        /// </summary>
        public int TiltAngle {
            get { return this.m_kinectRuntime.NuiCamera.ElevationAngle; }
            private set { this.m_kinectRuntime.NuiCamera.ElevationAngle = value; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Tilts the camera up 1 degree
        /// </summary>
        public void TiltDown() {
            // Valid Camera angle range is -25 to 25 degrees
            if (TiltAngle != -25) {
                TiltAngle--;
            }
        }

        /// <summary>
        /// Tilts the camera down 1 degree
        /// </summary>
        public void TiltUp() {
            // Valid Camera angle range is -25 to 25 degrees
            if (TiltAngle != 25) {
                TiltAngle++;
            }
        }

        #endregion Public Methods

        #region Constructors

        public Camera() {
            if (InitializeNui()) {
                // Register with the Camera events
                this.m_kinectRuntime.SkeletonFrameReady += OnSkeletonFrameReady;
                this.m_kinectRuntime.VideoFrameReady += OnVideoFrameReady;
            }

            else { throw new Exception("Error initializing camera"); }
        }

        #endregion Constructors

        #region Private Methods

        /// <summary>
        /// Responsible for initializing the Kinect Camera
        /// </summary>
        /// <returns>bool value indicating whether the Camera initialized correctly</returns>
        private bool InitializeNui() {
            // Can't do anything without the Runtime object...
            if (this.m_kinectRuntime == null) { return false; }

            try {
                // Initialize the Depth, Skeleton, and RGB cameras
                this.m_kinectRuntime.Initialize(RuntimeOptions.UseDepth | RuntimeOptions.UseSkeletalTracking | RuntimeOptions.UseColor);
            }

            // NEM: Probably want to throw this at some point instead of returning booleans
            catch (Exception exception) {
                Console.WriteLine(exception.ToString());
                return false;
            }

            // Open the RGB camera
            this.m_kinectRuntime.VideoStream.Open(ImageStreamType.Video, 2, ImageResolution.Resolution640x480, ImageType.Color);

            // Create our smoothing parameters
            var smoothingParams = new TransformSmoothParameters
            {
                Smoothing          = 0.75f,
                Correction         = 0.00f,
                Prediction         = 0.00f,
                JitterRadius       = 0.05f,
                MaxDeviationRadius = 0.04f
            };

            this.m_kinectRuntime.SkeletonEngine.SmoothParameters = smoothingParams;
            this.m_kinectRuntime.SkeletonEngine.TransformSmooth = true;

            // If we've made it here we're good.
            return true;
        }

        /// <summary>
        /// Handles the SkeletonFrameReady event of the Kinect's RunTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Research.Kinect.Nui.SkeletonFrameReadyEventArgs"/> instance containing the event data.</param>
        private void OnSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e) {
            List<int> idValues = new List<int>();

            if (e.SkeletonFrame.Skeletons.Length > 0) {
                // Process each skeleton in the event...
                foreach (SkeletonData skeleton in e.SkeletonFrame.Skeletons) {
                    // If the Kinect camera is tracking this skeleton...
                    if (skeleton.TrackingState == SkeletonTrackingState.Tracked) {
                        // Create and throw the SkeletonReady event...
                        if (this.SkeletonReady != null) {
                            this.SkeletonReady(this, new SkeletonEventArgs(skeleton));
                        }
                        // Add the Tracking ID to our watch list...
                        idValues.Add(skeleton.TrackingID);

                        // NEM: Not sure why we don't wait until we're out of the loop on this...
                        if (this.SkeletonFrameComplete != null) {
                            this.SkeletonFrameComplete(this, new SkeletonFrameEventArgs(idValues, e.SkeletonFrame.TimeStamp));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the VideoFrameReady event of the Kinect's RunTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Research.Kinect.Nui.ImageFrameReadyEventArgs"/> instance containing the event data.</param>
        private void OnVideoFrameReady(object sender, ImageFrameReadyEventArgs e) {
            if (this.ImageFrameReady != null) { this.ImageFrameReady(this, e); }
        }

        #endregion Private Methods

        #region Private Fields

        private Runtime m_kinectRuntime;

        #endregion Private Fields
    }
}