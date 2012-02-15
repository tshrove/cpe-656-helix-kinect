using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.Generic;
using GestureRecorder.Data;
using Iava.Audio;
using Iava.Input.Camera;


namespace GestureRecorder {
    /// <summary>
    /// Interaction logic for CreateGestureWindow.xaml
    /// </summary>
    public partial class CreateGestureWindow : Window {

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CreateGestureWindow() {
            InitializeComponent();
            this.Gesture = new tempuri.org.GestureDefinition.xsd.Gesture();
            // Subscribe to the Camera events we are interested in...
            Camera.ImageFrameReady += OnCameraImageFrameReady;
            Camera.SkeletonFrameReady += OnCameraSkeletonFrameReady;

            // Set up the Audio Recognizer...
            AudioRecognizer = new AudioRecognizer();
            AudioRecognizer.SyncCommand = "IAVA";
            AudioRecognizer.Subscribe("Capture", CaptureCallback);
            AudioRecognizer.Subscribe("Snapshot", CaptureCallback);
            AudioRecognizer.Start();
        }

        #endregion Constructors

        #region Private Properties

        /// <summary>
        /// Captures audio commands to run the current window
        /// </summary>
        private AudioRecognizer AudioRecognizer { get; set; }

        /// <summary>
        /// Number of segments in the current gesture
        /// </summary>
        private int GestureSegmentCount { get; set; }

        /// <summary>
        /// Gets the gesture that is about to be saved. 
        /// </summary>
        private tempuri.org.GestureDefinition.xsd.Gesture Gesture
        {
            get;
            set;
        }
        #endregion Private Properties

        #region Private Methods

        #region Audio Callbacks

        /// <summary>
        /// Occurs when the 'Capture' command was received.
        /// </summary>
        /// <param name="e">Audio event args</param>
        private void CaptureCallback(AudioEventArgs e) {
            OnSnapshotClick(null, null);
        }

        #endregion Audio Callbacks

        #region Event Handlers

        /// <summary>
        /// Displays the raw camera image from the Kinect sensor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCameraImageFrameReady(object sender, ImageFrameReadyEventArgs e) {
            PlanarImage Image = e.ImageFrame.Image;

            VideoFeed.Source = BitmapSource.Create(
                Image.Width, Image.Height, 96, 96, PixelFormats.Bgr32, null,
                Image.Bits, Image.Width * Image.BytesPerPixel);
        }

        /// <summary>
        /// Displays the full skeleton image from the Kinect sensor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCameraSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e) {
            // If we don't have a canvas to draw on, there's nothing for us to do...
            if (_activeSegmentCanvas == null) { return; }

            SkeletonFrame skeletonFrame = e.SkeletonFrame;

            int iSkeleton = 0;
            Brush[] brushes = new Brush[6];
            brushes[0] = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            brushes[1] = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            brushes[2] = new SolidColorBrush(Color.FromRgb(64, 255, 255));
            brushes[3] = new SolidColorBrush(Color.FromRgb(255, 255, 64));
            brushes[4] = new SolidColorBrush(Color.FromRgb(255, 64, 255));
            brushes[5] = new SolidColorBrush(Color.FromRgb(128, 128, 255));

            _activeSegmentCanvas.Children.Clear();
            foreach (SkeletonData data in skeletonFrame.Skeletons) {
                if (SkeletonTrackingState.Tracked == data.TrackingState) {
                    // Draw bones
                    Brush brush = brushes[iSkeleton % brushes.Length];
                    _activeSegmentCanvas.Children.Add(GetBodySegment(data.Joints, brush, JointID.HipCenter, JointID.Spine, JointID.ShoulderCenter, JointID.Head));
                    _activeSegmentCanvas.Children.Add(GetBodySegment(data.Joints, brush, JointID.ShoulderCenter, JointID.ShoulderLeft, JointID.ElbowLeft, JointID.WristLeft, JointID.HandLeft));
                    _activeSegmentCanvas.Children.Add(GetBodySegment(data.Joints, brush, JointID.ShoulderCenter, JointID.ShoulderRight, JointID.ElbowRight, JointID.WristRight, JointID.HandRight));
                    _activeSegmentCanvas.Children.Add(GetBodySegment(data.Joints, brush, JointID.HipCenter, JointID.HipLeft, JointID.KneeLeft, JointID.AnkleLeft, JointID.FootLeft));
                    _activeSegmentCanvas.Children.Add(GetBodySegment(data.Joints, brush, JointID.HipCenter, JointID.HipRight, JointID.KneeRight, JointID.AnkleRight, JointID.FootRight));

                    // Draw joints
                    foreach (Joint joint in data.Joints) {
                        Point jointPos = new Point(joint.Position.X, joint.Position.Y);
                        Line jointLine = new Line();
                        jointLine.X1 = jointPos.X - 3;
                        jointLine.X2 = jointLine.X1 + 6;
                        jointLine.Y1 = jointLine.Y2 = jointPos.Y;
                        jointLine.StrokeThickness = 6;
                        _activeSegmentCanvas.Children.Add(jointLine);
                    }
                }
                iSkeleton++;
            } // for each skeleton
        }

        /// <summary>
        /// Raises when the cancel button has been clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCancelClick(object sender, RoutedEventArgs e) {
            this.Close();
        }

        /// <summary>
        /// Tells the selected joint to whether it should be tracked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnJointClick(object sender, MouseButtonEventArgs e) {
            String sBodyPart = ((Image)sender).Name;
            switch (sBodyPart) {
                case "dotHead":
                    if (!m_bHead) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the heads in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.Head.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the heads in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.Head.Tracking = false;
                        }
                    }
                    m_bHead = !m_bHead;
                    break;
                case "dotShoulderCenter":
                    if (!m_bShoulderCenter) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.ShoulderCenter.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.ShoulderCenter.Tracking = false;
                        }
                    }
                    m_bShoulderCenter = !m_bShoulderCenter;
                    break;
                case "dotShoulderLeft":
                    if (!m_bShoulderLeft) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.ShoulderLeft.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.ShoulderLeft.Tracking = false;
                        }
                    }
                    m_bShoulderLeft = !m_bShoulderLeft;
                    break;
                case "dotShoulderRight":
                    if (!m_bShoulderRight) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.ShoulderRight.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.ShoulderRight.Tracking = false;
                        }
                    }
                    m_bShoulderRight = !m_bShoulderRight;
                    break;
                case "dotElbowLeft":
                    if (!m_bElbowLeft) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.ElbowLeft.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.ElbowLeft.Tracking = false;
                        }
                    }
                    m_bElbowLeft = !m_bElbowLeft;
                    break;
                case "dotElbowRight":
                    if (!m_bElbowRight) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.ElbowRight.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.ElbowRight.Tracking = false;
                        }
                    }
                    m_bElbowRight = !m_bElbowRight;
                    break;
                case "dotWristLeft":
                    if (!m_bWristLeft) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.WristLeft.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.WristLeft.Tracking = false;
                        }
                    }
                    m_bWristLeft = !m_bWristLeft;
                    break;
                case "dotWristRight":
                    if (!m_bWristRight) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.WristRight.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.WristRight.Tracking = false;
                        }
                    }
                    m_bWristRight = !m_bWristRight;
                    break;
                case "dotHandLeft":
                    if (!m_bHandLeft) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.HandLeft.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.HandLeft.Tracking = false;
                        }
                    }
                    m_bHandLeft = !m_bHandLeft;
                    break;
                case "dotHandRight":
                    if (!m_bHandRight) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.HandRight.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.HandRight.Tracking = false;
                        }
                    }
                    m_bHandRight = !m_bHandRight;
                    break;
                case "dotSpine":
                    if (!m_bSpine) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.Spine.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.Spine.Tracking = false;
                        }
                    }
                    m_bSpine = !m_bSpine;
                    break;
                case "dotHipCenter":
                    if (!m_bHipCenter) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.HipCenter.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.HipCenter.Tracking = false;
                        }
                    }
                    m_bHipCenter = !m_bHipCenter;
                    break;
                case "dotHipLeft":
                    if (!m_bHipLeft) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.HipLeft.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.HipLeft.Tracking = false;
                        }
                    }
                    m_bHipLeft = !m_bHipLeft;
                    break;
                case "dotHipRight":
                    if (!m_bHipRight) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.HipRight.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.HipRight.Tracking = false;
                        }
                    }
                    m_bHipRight = !m_bHipRight;
                    break;
                case "dotKneeLeft":
                    if (!m_bKneeLeft) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.KneeLeft.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.KneeLeft.Tracking = false;
                        }
                    }
                    m_bKneeLeft = !m_bKneeLeft;
                    break;
                case "dotKneeRight":
                    if (!m_bKneeRight) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.KneeRight.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.KneeRight.Tracking = false;
                        }
                    }
                    m_bKneeRight = !m_bKneeRight;
                    break;
                case "dotAnkleLeft":
                    if (!m_bAnkleLeft) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.AnkleLeft.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.AnkleLeft.Tracking = false;
                        }
                    }
                    m_bAnkleLeft = !m_bAnkleLeft;
                    break;
                case "dotAnkleRight":
                    if (!m_bAnkleRight) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.AnkleRight.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.AnkleRight.Tracking = false;
                        }
                    }
                    m_bAnkleRight = !m_bAnkleRight;
                    break;
                case "dotFootLeft":
                    if (!m_bFootLeft) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.FootLeft.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.FootLeft.Tracking = false;
                        }
                    }
                    m_bFootLeft = !m_bFootLeft;
                    break;
                case "dotFootRight":
                    if (!m_bFootRight) {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.FootRight.Tracking = true;
                        }
                    }
                    else {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png"));
                        // Add the tracking capability to all the shoudlers in the gesture segments
                        foreach (var segment in this.Gesture.Segment)
                        {
                            segment.FootRight.Tracking = false;
                        }
                    }
                    m_bFootRight = !m_bFootRight;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Saves the defined gesture to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNextClicked(object sender, RoutedEventArgs e) {
            // TODO: Add save gesture code...
        }

        /// <summary>
        /// Freezes the current gesture segment and loads the next segment canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSnapshotClick(object sender, RoutedEventArgs e) {
            //ToDo: Get the joints and add them to a new segment to add to the current gesture.
            tempuri.org.GestureDefinition.xsd.Gesture.SegmentLocalType gestureSegment = new tempuri.org.GestureDefinition.xsd.Gesture.SegmentLocalType();

            GestureSegment segment = new GestureSegment();
            segment.SetTrackingJoints(JointID.AnkleLeft, JointID.AnkleRight);         
            AddSegmentCanvas();

            // Add the new set of segments of the body to the gesture that will be saved to a file.
            this.Gesture.Segment.Add(gestureSegment);
        }

        /// <summary>
        /// Performs the last of the setup operations once the Window has finished loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowLoaded(object sender, RoutedEventArgs e) {
            AddSegmentCanvas();
        }

        #endregion Event Handlers

        /// <summary>
        /// Adds a new Gesture Segment Canvas to the DataGrid
        /// </summary>
        private void AddSegmentCanvas() {
            // We only support a maximum of 10 segments per gesture
            if (GestureSegmentCount > 9) { _activeSegmentCanvas = null; return; }

            // Create a new Canvas to add to the grid
            Canvas temp = new Canvas() { Background = new SolidColorBrush(Color.FromRgb(0, 0, 0)) };

            // Put the canvas in the correct grid position
            Grid.SetColumn(temp, (GestureSegmentCount < 5) ? GestureSegmentCount : GestureSegmentCount - 5);
            Grid.SetRow(temp, GestureSegmentCount / 5);

            // Do the transformation necessary to draw the skeleton
            temp.RenderTransform = new ScaleTransform(1.0, -1.0,
                gridSnapshot.ColumnDefinitions[0].ActualWidth / 2.0,
                gridSnapshot.RowDefinitions[0].ActualHeight / 2.0);

            // Add the canvas to the grid
            gridSnapshot.Children.Add(temp);

            // Set the active canvas segment
            _activeSegmentCanvas = temp;

            // Increase the number of segments in the gesture
            GestureSegmentCount++;
        }

        /// <summary>
        /// Draws a line between the specified joints of a skeleton
        /// </summary>
        /// <param name="joints">Joints in a skeleton</param>
        /// <param name="brush">Color of the lines to draw</param>
        /// <param name="jointIDs">Joints to be connected by the poly line</param>
        /// <returns></returns>
        private Polyline GetBodySegment(JointsCollection joints, Brush brush, params JointID[] jointIDs) {
            PointCollection points = new PointCollection(jointIDs.Length);
            for (int i = 0; i < jointIDs.Length; ++i) {
                points.Add(ScalePoint(joints[jointIDs[i]].Position.X, joints[jointIDs[i]].Position.Y, _activeSegmentCanvas.ActualWidth, _activeSegmentCanvas.ActualHeight));
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
            if (temp > maxX) { newX = maxX; }
            else if (temp < 0.0) { newX = 0.0; }
            else { newX = temp; }

            temp = ((maxY / 2.0) * yPos) + (maxY / 2.0);

            // Scale the Y value
            if (temp > maxY) { newY = maxY; }
            else if (temp < 0.0) { newY = 0.0; }
            else { newY = temp; }

            // Return the new point
            return new Point(newX, newY);
        }

        #endregion Private Methods

        #region Private Fields

        #region Joints
        private bool m_bHead = false;
        private bool m_bShoulderCenter = false;
        private bool m_bShoulderLeft = false;
        private bool m_bShoulderRight = false;
        private bool m_bElbowLeft = false;
        private bool m_bElbowRight = false;
        private bool m_bWristLeft = false;
        private bool m_bWristRight = false;
        private bool m_bHandLeft = false;
        private bool m_bHandRight = false;
        private bool m_bSpine = false;
        private bool m_bHipCenter = false;
        private bool m_bHipLeft = false;
        private bool m_bHipRight = false;
        private bool m_bKneeLeft = false;
        private bool m_bKneeRight = false;
        private bool m_bAnkleLeft = false;
        private bool m_bAnkleRight = false;
        private bool m_bFootLeft = false;
        private bool m_bFootRight = false;
        #endregion Joints

        /// <summary>
        /// Pointer to the current gesture segment canvas
        /// </summary>
        private Canvas _activeSegmentCanvas = null;

        #endregion Private Fields
    }
}
