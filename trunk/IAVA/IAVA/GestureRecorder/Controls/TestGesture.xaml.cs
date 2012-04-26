using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Iava.Gesture;
using Iava.Input.Camera;
using Iava.Audio;

namespace GestureRecorder.Controls {

    /// <summary>
    /// Interaction logic for TestGestureWindow.xaml
    /// </summary>
    public partial class TestGesture : UserControl {

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public TestGesture() {
            InitializeComponent();

            SizeSkeletonCanvas(kinectSkeletonFeed);
        }

        #endregion Constructors

        #region Private Methods

        #region Event Handlers

        /// <summary>
        /// Displays the full skeleton image from the Kinect sensor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCameraSkeletonFrameReady(object sender, IavaSkeletonFrameReadyEventArgs e) {
            kinectSkeletonFeed.Skeleton = e.SkeletonFrame.ActiveSkeleton;
        }

        /// <summary>
        /// Raises when the close button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCloseClick(object sender, RoutedEventArgs e) {
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Raises when a gesture is detected from the list of gestures.
        /// </summary>
        /// <param name="e"></param>
        private void OnGestureDetected(GestureEventArgs e) {
            if (!popStatus.IsOpen) {
                m_pTimer.Interval = 2000; // 2 seconds
                m_pTimer.Elapsed += OnTimerElapsed;
                m_pTimer.Enabled = true;
                popStatus.IsOpen = true;
                lblStatus.Content = e.Name;
            }
            else {
                m_pTimer.Interval = 2000;
                lblStatus.Content = e.Name;
            }
            System.Media.SystemSounds.Beep.Play();
        }

        /// <summary>
        /// Raises when the load button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLoadGesturesClick(object sender, RoutedEventArgs e) {
            using (System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog()) {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    m_pGestureRecognizer = new GestureRecognizer(dialog.SelectedPath);
                    m_pGestures = new ObservableCollection<IavaGesture>(GestureFolderReader.Read(dialog.SelectedPath));
                    foreach (IavaGesture g in m_pGestures) {
                        m_pGestureRecognizer.Subscribe(g.Name, OnGestureDetected);
                    }
                    this.lstGestures.ItemsSource = m_pGestures;
                    btnStartTest.IsEnabled = true;
                }
            }
        }

        /// <summary>
        /// Performs the last of the setup operations once the Control has finished loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLoaded(object sender, RoutedEventArgs e) {
            Initialize();
        }

        /// <summary>
        /// Raises when the start button has been clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnStartTestClick(object sender, RoutedEventArgs e) {
            if (m_bStarted) {
                // Stop everything.
                m_pGestureRecognizer.Stop();
                m_bStarted = false;
                btnStartTest.Content = "Start Test";
            }
            else {
                // Start everything.
                m_pGestureRecognizer.Start();
                m_bStarted = true;
                btnStartTest.Content = "Stop Test";
            }
        }

        /// <summary>
        /// Event used in junction with the display status function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e) {
            popStatus.Dispatcher.Invoke(new Action(() => popStatus.IsOpen = false));
        }

        /// <summary>
        /// Resets the Control when the Visibility Changes
        /// </summary>
        private void OnVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e) {
            Reset();
        }

        #endregion Event Handlers

        /// <summary>
        /// Initializes the control
        /// </summary>
        private void Initialize() {
            // If we are in design mode, do nothing...
            if (DesignerProperties.GetIsInDesignMode(this)) { return; }

            try {
                IavaCamera.SkeletonFrameReady += OnCameraSkeletonFrameReady;

                // Subscribe to the Audio events we are interested in...
                (Window.GetWindow(this) as MainWindow).AudioRecognizer.Subscribe("Load Gestures", LoadGesturesCallback);
                (Window.GetWindow(this) as MainWindow).AudioRecognizer.Subscribe("Start Test", StartTestCallback);
                (Window.GetWindow(this) as MainWindow).AudioRecognizer.Subscribe("Stop Test", StopTestCallback);
            }

            catch (Exception) { }
        }

        /// <summary>
        /// Occurs when the 'Load Gestures' command was received.
        /// </summary>
        /// <param name="e">Audio event args</param>
        private void LoadGesturesCallback(AudioEventArgs e) {
            OnLoadGesturesClick(null, null);
        }

        /// <summary>
        /// Resets the state of the UserControl
        /// </summary>
        private void Reset() {
            // 'Bounce' the control
            TearDown();
            Initialize();
        }

        /// <summary>
        /// Resizes the SkeletonCanvas
        /// </summary>
        /// <param name="canvas"></param>
        private void SizeSkeletonCanvas(SkeletonCanvas canvas) {
            //canvas.RenderTransform = new ScaleTransform(1.0, -1.0, this.ActualWidth / 2.0, this.ActualHeight / 2.0);
            /*
                gridSnapshot.ColumnDefinitions[0].ActualWidth / 2.0,
                gridSnapshot.RowDefinitions[0].ActualHeight / 2.0);*/
        }

        /// <summary>
        /// Occurs when the 'Start Test' command was received.
        /// </summary>
        /// <param name="e">Audio event args</param>
        private void StartTestCallback(AudioEventArgs e) {
            if (!m_bStarted) { OnStartTestClick(null, null); }
        }

        /// <summary>
        /// Occurs when the 'Stop Test' command was received.
        /// </summary>
        /// <param name="e">Audio event args</param>
        private void StopTestCallback(AudioEventArgs e) {
            if (m_bStarted) { OnStartTestClick(null, null); }
        }

        /// <summary>
        /// Tears down the control
        /// </summary>
        private void TearDown() {
            try {
                IavaCamera.SkeletonFrameReady -= OnCameraSkeletonFrameReady;

                // Unsubscribe from the Audio events we are interested in...
                (Window.GetWindow(this) as MainWindow).AudioRecognizer.Unsubscribe("Load Gestures");
                (Window.GetWindow(this) as MainWindow).AudioRecognizer.Unsubscribe("Start Test");
                (Window.GetWindow(this) as MainWindow).AudioRecognizer.Unsubscribe("Stop Test");

                m_pGestureRecognizer.Stop();
            }

            catch (Exception) { }
        }

        #endregion Private Methods

        #region Private Fields

        private ObservableCollection<IavaGesture> m_pGestures = new ObservableCollection<IavaGesture>();

        private GestureRecognizer m_pGestureRecognizer = null;

        private bool m_bStarted = false;

        private System.Timers.Timer m_pTimer = new System.Timers.Timer();

        #endregion Private Fields
    }
}
