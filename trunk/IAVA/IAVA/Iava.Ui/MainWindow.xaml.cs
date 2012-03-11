using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Iava.Audio;
using Iava.Gesture;
using Iava.Input.Camera;

using Envelope = ESRI.ArcGIS.Client.Geometry.Envelope;
using Geometry = ESRI.ArcGIS.Client.Geometry.Geometry;
using MapPoint = ESRI.ArcGIS.Client.Geometry.MapPoint;

namespace Iava.Ui {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow() {
            InitializeComponent();
            //string temp = Directory.GetCurrentDirectory();
            m_pGestureRecognizer = new Gesture.GestureRecognizer(@"..\..\..\Gestures\");
            m_pAudioRecognizer = new AudioRecognizer();

            m_pAudioRecognizer.AudioConfidenceThreshold = 0.5f;
            CityLocations.GetCityLocation("test");
            // Events
            m_pAudioRecognizer.StatusChanged    += OnAudioRecognizerStatusChanged;
            m_pGestureRecognizer.StatusChanged  += OnGestureRecognizerStatusChanged;
            m_pAudioRecognizer.Synced           += OnAudioRecognizerSynced;
            m_pGestureRecognizer.Synced         += OnGestureRecognizerSynced;
            m_pAudioRecognizer.Unsynced         += OnAudioRecognizerUnsynced;
            m_pGestureRecognizer.Unsynced       += OnGestureRecognizerUnsynced;

            // Gesture Callbacks
            m_pGestureRecognizer.Subscribe("Zoom In", GestureZoomInCallback);
            m_pGestureRecognizer.Subscribe("Zoom Out", GestureZoomOutCallback);
            m_pGestureRecognizer.Subscribe("Swipe Left", GestureSwipeLeftCallback);
            m_pGestureRecognizer.Subscribe("Swipe Right", GestureSwipeRightCallback);
            m_pGestureRecognizer.Subscribe("Swipe Up", GestureSwipeUpCallback);
            m_pGestureRecognizer.Subscribe("Swipe Down", GestureSwipeDownCallback);

            // Audio Callbacks
            m_pAudioRecognizer.Subscribe("Zoom In", ZoomInCallback);
            m_pAudioRecognizer.Subscribe("Zoom Out", ZoomOutCallback);
            m_pAudioRecognizer.Subscribe("Move North", MoveNorthCallback);
            m_pAudioRecognizer.Subscribe("Move South", MoveSouthCallback);
            m_pAudioRecognizer.Subscribe("Move East", MoveEastCallback);
            m_pAudioRecognizer.Subscribe("Move West", MoveWestCallback);
            m_pAudioRecognizer.Subscribe("Exit Application", BlowUp);
            m_pAudioRecognizer.Subscribe("Locate *", LocateCityCallback);
            m_pAudioRecognizer.Subscribe("Get Recognizer Statuses", GetRecognizerStatusesCallback);
            //m_pAudioRecognizer.Subscribe("Change sync command to *", ChangeAudioSyncCommandCallback);  //TODO: Not working correctly

            IavaCamera.ImageFrameReady += OnCameraImageFrameReady;
            IavaCamera.SkeletonFrameReady += OnCameraSkeletonFrameReady;

            m_pAudioSyncTimer = new System.Timers.Timer(1000);
            m_pAudioSyncTimer.Elapsed += OnAudioSyncTimerElapsed;

            m_pGestureSyncTimer = new System.Timers.Timer(1000);
            m_pGestureSyncTimer.Elapsed += OnGestureSyncTimerElapsed;

            m_pConsoleTxtBox = new TextBoxStreamWriter(this.txtConsole);
            Console.SetOut(m_pConsoleTxtBox);            

            // UI theme stuff...
            lblAudioStatus.Background = lblGestureStatus.Background = unsyncedBrush;
            lblAudioSyncTime.Background = lblGestureSyncTime.Background = backgroundBrush;
            lblAudioTTL.Background = lblGestureTTL.Background = backgroundBrush;
        }

        #endregion Constructors

        #region Private Methods

        #region Audio Callbacks

        /// <summary>
        /// Occurs when a blow up command was received.
        /// </summary>
        /// <param name="e"></param>
        private void BlowUp(AudioEventArgs e) {
            this.Window.Close();
        }

        /// <summary>
        /// Occurs when a move east command was received.
        /// </summary>
        /// <param name="e"></param>
        private void MoveEastCallback(AudioEventArgs e) {
            MoveEast();

            DisplayStatus(String.Format("Audio: {0} Detected", e.Command));

            ResetAudioSyncTime();
        }

        /// <summary>
        /// Occurs when a move north command was received.
        /// </summary>
        /// <param name="e"></param>
        private void MoveNorthCallback(AudioEventArgs e) {
            MoveNorth();

            DisplayStatus(String.Format("Audio: {0} Detected", e.Command));

            ResetAudioSyncTime();
        }

        /// <summary>
        /// Occurs when a move south command was received.
        /// </summary>
        /// <param name="e"></param>
        private void MoveSouthCallback(AudioEventArgs e) {
            MoveSouth();

            DisplayStatus(String.Format("Audio: {0} Detected", e.Command));

            ResetAudioSyncTime();
        }

        /// <summary>
        /// Occurs when a move west command was received.
        /// </summary>
        /// <param name="e"></param>
        private void MoveWestCallback(AudioEventArgs e) {
            MoveWest();

            DisplayStatus(String.Format("Audio: {0} Detected", e.Command));

            ResetAudioSyncTime();
        }

        /// <summary>
        /// Occurs when a zoom in command was received.
        /// </summary>
        /// <param name="e">Audio event args</param>
        private void ZoomInCallback(AudioEventArgs e) {
            ZoomIn();

            DisplayStatus(String.Format("Audio: {0} Detected", e.Command));

            ResetAudioSyncTime();
        }

        /// <summary>
        /// Occurs when a zoom out command was received.
        /// </summary>
        /// <param name="e">Audio event args</param>
        private void ZoomOutCallback(AudioEventArgs e) {
            ZoomOut();

            DisplayStatus(String.Format("Audio: {0} Detected", e.Command));

            ResetAudioSyncTime();
        }

        /// <summary>
        /// Occurs when a locate wildcard command was received.
        /// </summary>
        /// <param name="e">Audio event args</param>
        private void LocateCityCallback(AudioEventArgs e)
        {
            if (e.CommandWildcards != null && e.CommandWildcards.Count > 0)
            {
                string wildCardString = string.Empty;
                foreach (string wildcard in e.CommandWildcards)
                {
                    wildCardString += wildcard + " ";
                }
                wildCardString = wildCardString.Trim();

                MapPoint point = CityLocations.GetCityLocation(wildCardString);                
                if (point != null)
                {
                    map.PanTo(point);

                    DisplayStatus(String.Format("Audio: {0} {1}", e.Command.TrimEnd('*'), wildCardString));

                    ResetAudioSyncTime();
                }                
            }                        
        }

        /// <summary>
        /// Occurs when a get recognizer statuses command was received.
        /// </summary>
        /// <param name="e">Audio event args</param>
        private void GetRecognizerStatusesCallback(AudioEventArgs e)
        {
            DisplayStatus(String.Format("Audio Recognizer Status: {0}   Gesture Recognizer Status: {1}", m_pAudioRecognizer.Status, m_pGestureRecognizer.Status));

            ResetAudioSyncTime();
        }

        /// <summary>
        /// Occurs when change audio sync command was recieved.
        /// </summary>
        /// <param name="e">Audio event args</param>
        private void ChangeAudioSyncCommandCallback(AudioEventArgs e)
        {
            if (e.CommandWildcards != null && e.CommandWildcards.Count > 0)
            {
                string commandWildcards = string.Empty;
                foreach (string wildcard in e.CommandWildcards)
                {
                    commandWildcards += wildcard + " ";
                }
                commandWildcards = commandWildcards.Trim();

                m_pAudioRecognizer.SyncCommand = commandWildcards.Trim();

                DisplayStatus(String.Format("Sync Command Changed To: {0}", m_pAudioRecognizer.SyncCommand));

                ResetAudioSyncTime();
            }
        }

        #endregion Audio Callbacks

        #region Gesture Callbacks

        /// <summary>
        /// The callback for when the zoom gesture is detected.
        /// </summary>
        /// <param name="e"></param>
        private void GestureZoomInCallback(GestureEventArgs e) {
            ZoomIn();

            DisplayStatus(String.Format("Gesture: {0} Detected", e.Name));

            ResetGestureSyncTime();
        }

        /// <summary>
        /// The callback for when the zoom gesture is detected.
        /// </summary>
        /// <param name="e"></param>
        private void GestureZoomOutCallback(GestureEventArgs e) {
            ZoomOut();

            DisplayStatus(String.Format("Gesture: {0} Detected", e.Name));

            ResetGestureSyncTime();
        }

        /// <summary>
        /// The callback for when the Left Swipe gesture is detected.
        /// </summary>
        /// <param name="e"></param>
        private void GestureSwipeLeftCallback(GestureEventArgs e) {
            MoveEast();

            DisplayStatus(String.Format("Gesture: {0} Detected", e.Name));

            ResetGestureSyncTime();
        }

        /// <summary>
        /// The callback for when the Right Swipe gesture is detected.
        /// </summary>
        /// <param name="e"></param>
        private void GestureSwipeRightCallback(GestureEventArgs e) {
            MoveWest();

            DisplayStatus(String.Format("Gesture: {0} Detected", e.Name));

            ResetGestureSyncTime();
        }

        /// <summary>
        /// The callback for when the Up Swipe gesture is detected.
        /// </summary>
        /// <param name="e"></param>
        private void GestureSwipeUpCallback(GestureEventArgs e) {
            MoveSouth();

            DisplayStatus(String.Format("Gesture: {0} Detected", e.Name));

            ResetGestureSyncTime();
        }

        /// <summary>
        /// The callback for when the Down Swipe gesture is detected.
        /// </summary>
        /// <param name="e"></param>
        private void GestureSwipeDownCallback(GestureEventArgs e) {
            MoveNorth();

            DisplayStatus(String.Format("Gesture: {0} Detected", e.Name));

            ResetGestureSyncTime();
        }

        #endregion Gesture Callbacks

        #region Event Handlers

        /// <summary>
        /// Raises when the status of the audio recognizer is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAudioRecognizerStatusChanged(object sender, EventArgs e) {
            DisplayStatus("Audio Status: " + m_pAudioRecognizer.Status.ToString());
        }

        /// <summary>
        /// Raises when the audio recognizer is synced.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAudioRecognizerSynced(object sender, EventArgs e) {
            this.lblAudioStatus.Content = "Audio Status: Synced";
            this.lblAudioStatus.Background = syncedBrush;

            ResetAudioSyncTime();
            this.m_pAudioSyncTimer.Enabled = true;
        }

        /// <summary>
        /// Raises when the audio recognizer becomes unsycned.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAudioRecognizerUnsynced(object sender, EventArgs e) {
            this.lblAudioStatus.Content = "Audio Status: Unsynced";
            this.lblAudioStatus.Background = unsyncedBrush;
            this.m_pAudioSyncTimer.Enabled = false;
            this.lblAudioSyncTime.Content = "00:00:00";
        }

        /// <summary>
        /// Occurs when the audio sync timer elapses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAudioSyncTimerElapsed(object sender, System.Timers.ElapsedEventArgs e) {
            // Update the audio sync time label
            this.lblAudioSyncTime.Dispatcher.Invoke(new Action(() =>
            {
                this.m_sAudioSyncTime -= new TimeSpan(0, 0, 1);
                this.lblAudioSyncTime.Content = this.m_sAudioSyncTime.ToString(@"hh\:mm\:ss");
            }));
        }

        /// <summary>
        /// Displays the raw camera image from the Kinect sensor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCameraImageFrameReady(object sender, IavaColorImageFrameReadyEventArgs e) {
            if (e.ImageFrame == null) { return; }

            // Set up the writable bitmap...
            if (_firstRun) {
                // This is more efficient than creating a new bitmap every frame
                _colorBitmap = new WriteableBitmap(
                    e.ImageFrame.Width,
                    e.ImageFrame.Height,
                    96.0, // DpiX
                    96.0, // DpiY
                    PixelFormats.Bgr32,
                    null);

                // Set the image source
                kinectVideoFeed.Source = _colorBitmap;

                // Indicate this is not the first run
                _firstRun = false;
            }

            // Draw the image
            _colorBitmap.WritePixels(
                new Int32Rect(0, 0, e.ImageFrame.Width, e.ImageFrame.Height),
                e.ImageFrame.PixelData,
                e.ImageFrame.Width * ((PixelFormats.Bgr32.BitsPerPixel + 7) / 8), // Got this from sample code
                0);
        }

        /// <summary>
        /// Displays the full skeleton image from the Kinect sensor
        /// </summary>
        private void OnCameraSkeletonFrameReady(object sender, IavaSkeletonFrameReadyEventArgs e) {
            Iava.Input.Camera.IavaSkeletonFrame skeletonFrame = e.SkeletonFrame;

            int iSkeleton = 0;
            Brush[] brushes = new Brush[6];
            brushes[0] = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            brushes[1] = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            brushes[2] = new SolidColorBrush(Color.FromRgb(64, 255, 255));
            brushes[3] = new SolidColorBrush(Color.FromRgb(255, 255, 64));
            brushes[4] = new SolidColorBrush(Color.FromRgb(255, 64, 255));
            brushes[5] = new SolidColorBrush(Color.FromRgb(128, 128, 255));

            kinectSkeletonFeed.Children.Clear();
            foreach (IavaSkeleton data in skeletonFrame.Skeletons) {
                if (IavaSkeletonTrackingState.Tracked == data.TrackingState) {
                    // Draw bones
                    Brush brush = brushes[iSkeleton % brushes.Length];
                    kinectSkeletonFeed.Children.Add(GetBodySegment(data.Joints, brush, IavaJointType.HipCenter, IavaJointType.Spine, IavaJointType.ShoulderCenter, IavaJointType.Head));
                    kinectSkeletonFeed.Children.Add(GetBodySegment(data.Joints, brush, IavaJointType.ShoulderCenter, IavaJointType.ShoulderLeft, IavaJointType.ElbowLeft, IavaJointType.WristLeft, IavaJointType.HandLeft));
                    kinectSkeletonFeed.Children.Add(GetBodySegment(data.Joints, brush, IavaJointType.ShoulderCenter, IavaJointType.ShoulderRight, IavaJointType.ElbowRight, IavaJointType.WristRight, IavaJointType.HandRight));
                    kinectSkeletonFeed.Children.Add(GetBodySegment(data.Joints, brush, IavaJointType.HipCenter, IavaJointType.HipLeft, IavaJointType.KneeLeft, IavaJointType.AnkleLeft, IavaJointType.FootLeft));
                    kinectSkeletonFeed.Children.Add(GetBodySegment(data.Joints, brush, IavaJointType.HipCenter, IavaJointType.HipRight, IavaJointType.KneeRight, IavaJointType.AnkleRight, IavaJointType.FootRight));

                    // Draw joints
                    foreach (IavaJoint joint in data.Joints) {
                        Point jointPos = new Point(joint.Position.X, joint.Position.Y);
                        Line jointLine = new Line();
                        jointLine.X1 = jointPos.X - 3;
                        jointLine.X2 = jointLine.X1 + 6;
                        jointLine.Y1 = jointLine.Y2 = jointPos.Y;
                        //jointLine.Stroke = jointColors[joint.JointType];
                        jointLine.StrokeThickness = 6;
                        kinectSkeletonFeed.Children.Add(jointLine);
                    }
                }
                iSkeleton++;
            } // for each skeleton
        }

        /// <summary>
        /// Unsubscribes from everything when the windows closes
        /// </summary>
        private void OnClosed(object sender, EventArgs e) {
            // Unsubscribe from the Camera Events
            IavaCamera.ImageFrameReady -= OnCameraImageFrameReady;
            IavaCamera.SkeletonFrameReady -= OnCameraSkeletonFrameReady;

            // Stop the Recognizers
            m_pAudioRecognizer.Stop();
            m_pGestureRecognizer.Stop();
        }

        /// <summary>
        /// Raises when the status of the gesture recognizer is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGestureRecognizerStatusChanged(object sender, EventArgs e) {
            DisplayStatus("Gesture Status: " + m_pGestureRecognizer.Status.ToString());

            if (m_pGestureRecognizer.Status == Core.RecognizerStatus.Running)
            {
                //m_pGestureRecognizer.Camera.ColorImageFrameReady += OnCameraImageFrameReady;
                //m_pGestureRecognizer.Camera.SkeletonFrameReady += OnCameraSkeletonFrameReady;
            }
            else
            {
                //m_pGestureRecognizer.Camera.ColorImageFrameReady -= OnCameraImageFrameReady;
                //m_pGestureRecognizer.Camera.SkeletonFrameReady -= OnCameraSkeletonFrameReady;
            }
        }

        /// <summary>
        /// Raises when the gesture recognizer is synced.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGestureRecognizerSynced(object sender, EventArgs e) {
            this.lblGestureStatus.Content = "Gesture Status: Synced";
            this.lblGestureStatus.Background = syncedBrush;

            ResetGestureSyncTime();
            this.m_pGestureSyncTimer.Enabled = true;
        }

        /// <summary>
        /// Raises when the gesture recognizer becomes unsynced.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGestureRecognizerUnsynced(object sender, EventArgs e) {
            this.lblGestureStatus.Content = "Gesture Status: Unsynced";
            this.lblGestureStatus.Background = unsyncedBrush;
            this.m_pGestureSyncTimer.Enabled = false;
            this.lblGestureSyncTime.Content = "00:00:00";
        }

        /// <summary>
        /// Occurs when the gesture sync timer elapses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGestureSyncTimerElapsed(object sender, System.Timers.ElapsedEventArgs e) {
            // Update the audio sync time label
            this.lblGestureSyncTime.Dispatcher.Invoke(new Action(() =>
            {
                this.m_sGestureSyncTime -= new TimeSpan(0, 0, 1);
                this.lblGestureSyncTime.Content = this.m_sGestureSyncTime.ToString(@"hh\:mm\:ss");
            }));
        }

        /// <summary>
        /// Occurs when the map is loaded.
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event args</param>
        private void OnMapLoaded(object sender, RoutedEventArgs e) {
            m_pAudioRecognizer.Start();
            m_pGestureRecognizer.Start();
        }

        /// <summary>
        /// Event used in junction with the display status function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e) {
            popStatus.Dispatcher.Invoke(new Action(() => popStatus.IsOpen = false));
        }

        #endregion Event Handlers

        private Polyline GetBodySegment(IavaJointCollection joints, Brush brush, params IavaJointType[] jointIDs) {
            PointCollection points = new PointCollection(jointIDs.Length);
            for (int i = 0; i < jointIDs.Length; ++i) {
                points.Add(ScalePoint(joints[jointIDs[i]].Position.X, joints[jointIDs[i]].Position.Y, kinectSkeletonFeed.Width, kinectSkeletonFeed.Height));
            }

            Polyline polyline = new Polyline();
            polyline.Points = points;
            polyline.Stroke = brush;
            polyline.StrokeThickness = 5;
            return polyline;
        }

        /// <summary>
        /// Scales a point between -1 and 1 and scales it up to the Max X and Max Y
        /// </summary>
        /// <param name="xPos">the X position of the point to scale</param>
        /// <param name="yPos">the Y position of the point to scale</param>
        /// <param name="maxX">the largest allowed X value after scale</param>
        /// <param name="maxY">the largest allowed Y value after scale</param>
        /// <returns>Point with the new X,Y values</returns>
        private Point ScalePoint(double xPos, double yPos, double maxX, double maxY) {
            double newX, newY;

            double temp = ((maxX / 2.0) * xPos) + (maxX / 2.0);

            // Scale the X value
            if (temp > maxX)     { newX = maxX; }
            else if (temp < 0.0) { newX = 0.0;  }
            else                 { newX = temp; }

            temp = ((maxY / 2.0) * yPos) + (maxY / 2.0);

            // Scale the Y value
            if (temp > maxY)     { newY = maxY; }
            else if (temp < 0.0) { newY = 0.0; }
            else                 { newY = temp; }
            
            // Return the new point
            return new Point(newX, newY);
        }

        /// <summary>
        /// Resets the audio sync time.
        /// </summary>
        private void ResetAudioSyncTime()
        {         
            Dispatcher.Invoke(new Action(() => this.m_sAudioSyncTime = new TimeSpan(0, 0, 0, 0, m_pAudioRecognizer.SyncTimeoutValue)));
        }

        /// <summary>
        /// Resets the gesture sync time.
        /// </summary>
        private void ResetGestureSyncTime()
        {
            Dispatcher.Invoke(new Action(() => this.m_sGestureSyncTime = new TimeSpan(0, 0, 0, 0, m_pGestureRecognizer.SyncTimeoutValue)));
        }

        /// <summary>
        /// Displays the pop status control with the auto close.
        /// </summary>
        /// <param name="text"></param>
        private void DisplayStatus(string text) {
            if (!popStatus.IsOpen) {
                m_pTimer.Interval = 2000; // 2 seconds
                m_pTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerElapsed);
                m_pTimer.Enabled = true;
                popStatus.IsOpen = true;
                lblStatus.Content = text;
            }
            else {
                m_pTimer.Interval = 2000;
                lblStatus.Content = text;
            }

            Console.WriteLine(text);
        }

        /// <summary>
        /// Helper function for Move East Callback.
        /// </summary>
        /// <param name="e"></param>
        private void MoveEast() {
            ESRI.ArcGIS.Client.Geometry.MapPoint center = null;
            center = map.Extent.GetCenter();

            Point screen = map.MapToScreen(center);
            screen.X += 300;

            ESRI.ArcGIS.Client.Geometry.MapPoint newCenter = map.ScreenToMap(screen);
          
            map.PanTo(newCenter);
        }

        /// <summary>
        /// Helper function for Move North Callback.
        /// </summary>
        /// <param name="e"></param>
        private void MoveNorth() {
            ESRI.ArcGIS.Client.Geometry.MapPoint center = null;
            center = map.Extent.GetCenter();

            Point screen = map.MapToScreen(center);
            screen.Y -= 300;

            ESRI.ArcGIS.Client.Geometry.MapPoint newCenter = map.ScreenToMap(screen);
            
            map.PanTo(newCenter);
        }

        /// <summary>
        /// Helper function for Move South Callback.
        /// </summary>
        /// <param name="e"></param>
        private void MoveSouth() {
            ESRI.ArcGIS.Client.Geometry.MapPoint center = null;
            center = map.Extent.GetCenter();

            Point screen = map.MapToScreen(center);
            screen.Y += 300;

            ESRI.ArcGIS.Client.Geometry.MapPoint newCenter = map.ScreenToMap(screen);

            map.PanTo(newCenter);
        }

        /// <summary>
        /// Helper function for Move West Callback.
        /// </summary>
        /// <param name="e"></param>
        private void MoveWest() {
            ESRI.ArcGIS.Client.Geometry.MapPoint center = null;
            center = map.Extent.GetCenter();

            Point screen = map.MapToScreen(center);
            screen.X -= 300;

            ESRI.ArcGIS.Client.Geometry.MapPoint newCenter = map.ScreenToMap(screen);

            map.PanTo(newCenter);
        }

        private void ZoomIn() {
            map.Zoom(2.0);
        }

        private void ZoomOut() {
            map.Zoom(0.5);
        }

        #endregion Private Methods

        #region Private Fields

        private Iava.Gesture.GestureRecognizer m_pGestureRecognizer;
        private Iava.Audio.AudioRecognizer m_pAudioRecognizer;
        private System.Timers.Timer m_pTimer = new System.Timers.Timer();
        private System.Timers.Timer m_pAudioSyncTimer;
        private TimeSpan m_sAudioSyncTime;
        private System.Timers.Timer m_pGestureSyncTimer;
        private TimeSpan m_sGestureSyncTime;
        private TextBoxStreamWriter m_pConsoleTxtBox = null;
        private WriteableBitmap _colorBitmap;
        private bool _firstRun = true;

        // UI theme specific.  It'd be great if we had time to databind the the labels to a boolean stating whether
        // or not they're synced and then apply the appropriate theme instead of doing this all in code.
        private LinearGradientBrush unsyncedBrush = new LinearGradientBrush(Color.FromRgb(226, 113, 0), Color.FromRgb(251, 155, 11), 90.0);
        private LinearGradientBrush syncedBrush = new LinearGradientBrush(Color.FromRgb(78, 154, 0), Color.FromRgb(115, 210, 23), 90.0);
        private LinearGradientBrush backgroundBrush = new LinearGradientBrush(Color.FromRgb(46, 52, 54), Color.FromRgb(65, 67, 63), 90.0);

        #endregion Private Fields

        
    }
}
