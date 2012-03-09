using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Iava.Gesture;
using Iava.Input.Camera;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.Generic;

namespace GestureRecorder.Controls
{
    /// <summary>
    /// Interaction logic for TestGestureWindow.xaml
    /// </summary>
    public partial class TestGesture : UserControl
    {

        #region Members
        ObservableCollection<IavaGesture> m_pGestures = new ObservableCollection<IavaGesture>();
        GestureRecognizer m_pGestureRecognizer = null;
        bool m_bStarted = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public TestGesture()
        {
            InitializeComponent();
            IavaCamera.SkeletonFrameReady += OnCameraSkeletonFrameReady;
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Raises when the load button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_pGestureRecognizer = new GestureRecognizer(dialog.SelectedPath);
                    m_pGestures = new ObservableCollection<IavaGesture>(GestureFolderReader.Read(dialog.SelectedPath));
                    foreach (IavaGesture g in m_pGestures)
                    {
                        m_pGestureRecognizer.Subscribe(g.Name, GestureDetected);
                    }
                    this.lstGestures.ItemsSource = m_pGestures;
                    btnStartTest.IsEnabled = true;
                }
            }
        }
        /// <summary>
        /// Raises when the close button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Raises when a gesture is detected from the list of gestures.
        /// </summary>
        /// <param name="e"></param>
        private void GestureDetected(GestureEventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
        }
        /// <summary>
        /// Raises when the start button has been clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartTest_Click(object sender, RoutedEventArgs e)
        {
            if (m_bStarted)
            {
                // Stop everything.
                m_pGestureRecognizer.Stop();
                m_bStarted = false;
                btnStartTest.Content = "Start Test";
            }
            else
            {
                // Start everything.
                m_pGestureRecognizer.Start();
                m_bStarted = true;
                btnStartTest.Content = "Stop Test";
            }         
        }
        /// <summary>
        /// Displays the full skeleton image from the Kinect sensor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCameraSkeletonFrameReady(object sender, IavaSkeletonFrameReadyEventArgs e)
        {
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
            foreach (IavaSkeletonData data in skeletonFrame.Skeletons)
            {
                if (IavaSkeletonTrackingState.Tracked == data.TrackingState)
                {
                    // Draw bones
                    Brush brush = brushes[iSkeleton % brushes.Length];
                    kinectSkeletonFeed.Children.Add(GetBodySegment(data.Joints, brush, IavaJointID.HipCenter, IavaJointID.Spine, IavaJointID.ShoulderCenter, IavaJointID.Head));
                    kinectSkeletonFeed.Children.Add(GetBodySegment(data.Joints, brush, IavaJointID.ShoulderCenter, IavaJointID.ShoulderLeft, IavaJointID.ElbowLeft, IavaJointID.WristLeft, IavaJointID.HandLeft));
                    kinectSkeletonFeed.Children.Add(GetBodySegment(data.Joints, brush, IavaJointID.ShoulderCenter, IavaJointID.ShoulderRight, IavaJointID.ElbowRight, IavaJointID.WristRight, IavaJointID.HandRight));
                    kinectSkeletonFeed.Children.Add(GetBodySegment(data.Joints, brush, IavaJointID.HipCenter, IavaJointID.HipLeft, IavaJointID.KneeLeft, IavaJointID.AnkleLeft, IavaJointID.FootLeft));
                    kinectSkeletonFeed.Children.Add(GetBodySegment(data.Joints, brush, IavaJointID.HipCenter, IavaJointID.HipRight, IavaJointID.KneeRight, IavaJointID.AnkleRight, IavaJointID.FootRight));

                    // Draw joints
                    foreach (IavaJoint joint in data.Joints)
                    {
                        Point jointPos = new Point(joint.Position.X, joint.Position.Y);
                        Line jointLine = new Line();
                        jointLine.X1 = jointPos.X - 3;
                        jointLine.X2 = jointLine.X1 + 6;
                        jointLine.Y1 = jointLine.Y2 = jointPos.Y;
                        //jointLine.Stroke = jointColors[joint.ID];
                        jointLine.StrokeThickness = 6;
                        kinectSkeletonFeed.Children.Add(jointLine);
                    }
                }
                iSkeleton++;
            } // for each skeleton
        }
        private Polyline GetBodySegment(IavaJointsCollection joints, Brush brush, params IavaJointID[] jointIDs)
        {
            PointCollection points = new PointCollection(jointIDs.Length);
            for (int i = 0; i < jointIDs.Length; ++i)
            {
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
        private Point ScalePoint(double xPos, double yPos, double maxX, double maxY)
        {
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
        #endregion
    }
}
