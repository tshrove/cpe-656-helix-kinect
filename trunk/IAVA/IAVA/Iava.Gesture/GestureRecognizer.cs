using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Iava.Core;
using Iava.Gesture.GestureStuff;
using Microsoft.Research.Kinect.Nui;

namespace Iava.Gesture 
{
    /// <summary>
    /// Audio Callback for when a audio command is detected.
    /// </summary>
    /// <param name="e"></param>
    public delegate void GestureCallback(GestureEventArgs e);
    /// <summary>
    /// GestureRecognizer Class
    /// </summary>
    public class GestureRecognizer : Recognizer
    {
        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filePath"></param>
        public GestureRecognizer(string filePath)
            :base(filePath)
        {
            this.GestureCallbacks = new Dictionary<string, GestureCallback>();
            // TODO Add the function to load the gestures to the dictionary.

            // Initialize the Kinect Camera
            if (this.InitializeNui()) {
                this.kinectRunTime.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(this.KinectRunTime_SkeletonFrameReady);
                this.kinectRunTime.VideoFrameReady += new EventHandler<ImageFrameReadyEventArgs>(this.KinectRunTime_VideoFrameReady);

                if (this.kinectRunTime.NuiCamera.ElevationAngle != cameraAngles[13]) {
                    this.kinectRunTime.NuiCamera.ElevationAngle = cameraAngles[13];
                    this.currentAngle = 12;
                }
            }
            else {
                throw new Exception("Error initialising Kinect sensor");
            }

        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Holds the callbacks for each gesture.
        /// </summary>
        private Dictionary<string, GestureCallback> GestureCallbacks
        {
            get;
            set;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Starts the recognizer.
        /// </summary>
        public override void Start()
        {
            Status = RecognizerStatus.Running;
            OnStarted(this, new EventArgs());   
        }
        /// <summary>
        ///  Stops the recognizer.
        /// </summary>
        public override void Stop()
        {
            Status = RecognizerStatus.Ready;
            OnStopped(this, new EventArgs()); 
        }
        /// <summary>
        /// Used to connect a given delegate to a specified gesture
        /// given by the name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="d"></param>
        public void Subscribe(string name, GestureCallback callBack)
        {
            if (this.GestureCallbacks.ContainsKey(name))
            {
                this.GestureCallbacks[name] = callBack;
            }
        }
        /// <summary>
        /// Unsubscribe the given delegate from the given delegate
        /// by the name.
        /// </summary>
        /// <param name="name"></param>
        public void Unsubscribe(string name)
        {
            if (this.GestureCallbacks.ContainsKey(name))
            {
                this.GestureCallbacks[name] = null;
            }
        }
        #endregion

        // The following code can be vastly reworked after the prototype.

        #region Gesture Recognition

        /// <summary>
        /// The list of all gestures we are currently looking for
        /// </summary>
        private List<Iava.Gesture.GestureStuff.Gesture> gestures = new List<Iava.Gesture.GestureStuff.Gesture>();

        /// <summary>
        /// Occurs when [gesture recognised].
        /// </summary>
        public event EventHandler<GestureEventArgs> GestureRecognised;

        /// <summary>
        /// Updates all gestures.
        /// </summary>
        /// <param name="data">The skeleton data.</param>
        public void UpdateAllGestures(SkeletonData data) {
            foreach (Iava.Gesture.GestureStuff.Gesture gesture in this.gestures) {
                gesture.UpdateGesture(data);
            }
        }

        /// <summary>
        /// Adds the gesture.
        /// </summary>
        /// <param name="type">The gesture type.</param>
        /// <param name="gestureDefinition">The gesture definition.</param>
        public void AddGesture(GestureType type, IGestureSegment[] gestureDefinition) {
            Iava.Gesture.GestureStuff.Gesture gesture = new Iava.Gesture.GestureStuff.Gesture(type, gestureDefinition);
            gesture.GestureRecognised += new EventHandler<GestureEventArgs>(this.Gesture_GestureRecognised);
            this.gestures.Add(gesture);
        }

        /// <summary>
        /// Handles the GestureRecognised event of the g control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KinectSkeltonTracker.GestureEventArgs"/> instance containing the event data.</param>
        private void Gesture_GestureRecognised(object sender, GestureEventArgs e) {
            if (this.GestureRecognised != null) {
                this.GestureRecognised(this, e);
            }

            foreach (Iava.Gesture.GestureStuff.Gesture g in this.gestures) {
                g.Reset();
            }
        }

        #endregion Gesture Recognition

        #region Kinect Initialization

        /// <summary>
        /// The list of approved camera angles
        /// </summary>
        private static readonly int[] cameraAngles = { -25, -23, -21, -19, -17, -15, -13, -11, -9, -7, -5, -3, -1, 0, 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25 };

        /// <summary>
        /// The Kinect run time
        /// </summary>
        private Runtime kinectRunTime = new Runtime();

        /// <summary>
        /// The current index of the angle that the sensor is at
        /// </summary>
        private int currentAngle;
       

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

        /// <summary>
        /// Tilts the camera up.
        /// </summary>
        /// <returns> bool value true if the sensore moved</returns>
        public bool TiltUp()
        {
            if (this.currentAngle >= cameraAngles.Length)
            {
                return false;
            }

            try
            {
                this.currentAngle += 1;
                this.kinectRunTime.NuiCamera.ElevationAngle = cameraAngles[this.currentAngle];
                return true;
            }
            catch (InvalidOperationException)
            {
                this.currentAngle -= 1;
                return false;
            }
        }

        /// <summary>
        /// Tilts the camera down.
        /// </summary>
        /// <returns>bool value true if the sensore moved</returns>
        public bool TiltDown()
        {
            if (this.currentAngle <= 0)
            {
                return false;
            }

            try
            {
                this.currentAngle -= 1;
                this.kinectRunTime.NuiCamera.ElevationAngle = cameraAngles[this.currentAngle];
                return true;
            }
            catch (InvalidOperationException)
            {
                this.currentAngle += 1;
                return false;
            }
        }

        /// <summary>
        /// Handles the VideoFrameReady event of the kinectRunTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Research.Kinect.Nui.ImageFrameReadyEventArgs"/> instance containing the event data.</param>
        private void KinectRunTime_VideoFrameReady(object sender, ImageFrameReadyEventArgs e)
        {
            if (this.ImageFrameReady != null)
            {
                this.ImageFrameReady(this, e);
            }
        }

        /// <summary>
        /// Handles the SkeletonFrameReady event of the kinectRunTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Research.Kinect.Nui.SkeletonFrameReadyEventArgs"/> instance containing the event data.</param>
        private void KinectRunTime_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            List<int> idValues = new List<int>();

            if (e.SkeletonFrame.Skeletons.Length >= 1)
            {
                int trackingCount = 0;
              
                while (trackingCount < e.SkeletonFrame.Skeletons.Length)
                {
                    if (e.SkeletonFrame.Skeletons[trackingCount].TrackingState == SkeletonTrackingState.Tracked)
                    {
                        if (this.SkeletonReady != null)
                        {
                            this.SkeletonReady(this, new SkeletonEventArgs(e.SkeletonFrame.Skeletons[trackingCount]));
                        }
                        
                        idValues.Add(e.SkeletonFrame.Skeletons[trackingCount].TrackingID);
                    }

                    trackingCount++;
                    
                    if (this.SkeletonFrameComplete != null)
                    {
                        this.SkeletonFrameComplete(this, new SkeletonFrameEventArgs(idValues, e.SkeletonFrame.TimeStamp));
                    }
                }
            }
        }

        /// <summary>
        /// Initializes the Kinect sensor.
        /// </summary>
        /// <returns>bool value true if the sensor initialised correctly</returns>
        private bool InitializeNui()
        {
            if (this.kinectRunTime == null)
            {
                return false;
            }

            try
            {
                this.kinectRunTime.Initialize(RuntimeOptions.UseDepth | RuntimeOptions.UseSkeletalTracking | RuntimeOptions.UseColor);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                return false;
            }

            this.kinectRunTime.VideoStream.Open(ImageStreamType.Video, 2, ImageResolution.Resolution640x480, ImageType.Color);

            this.kinectRunTime.SkeletonEngine.TransformSmooth = true;

            var parameters = new TransformSmoothParameters
            {
                Smoothing = 0.75f,
                Correction = 0.0f,
                Prediction = 0.0f,
                JitterRadius = 0.05f,
                MaxDeviationRadius = 0.04f
            };

            this.kinectRunTime.SkeletonEngine.SmoothParameters = parameters;

            return true;
        }

        #endregion Kinect Initiialization
    }
}
