using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Effects;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Iava.Gesture;
using Iava.Audio;
using Iava.Input.Camera;

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
            m_pGestureRecognizer = new Gesture.GestureRecognizer(string.Empty);
            m_pAudioRecognizer = new Audio.AudioRecognizer(string.Empty);
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
            m_pAudioRecognizer.Subscribe("Blow Up", BlowUp);

            m_pGestureRecognizer.Camera.ImageFrameReady += new EventHandler<ImageFrameReadyEventArgs>(OnCameraImageFrameReady);

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
        /// Raises when the camera frame is ready to be viewed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCameraImageFrameReady(object sender, ImageFrameReadyEventArgs e) {
            PlanarImage Image = e.ImageFrame.Image;

            kinectVideoFeed.Source = BitmapSource.Create(
                Image.Width, Image.Height, 96, 96, PixelFormats.Bgr32, null,
                Image.Bits, Image.Width * Image.BytesPerPixel);
        }

        /// <summary>
        /// Raises when the status of the gesture recognizer is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGestureRecognizerStatusChanged(object sender, EventArgs e) {
            DisplayStatus("Audio Status: " + m_pAudioRecognizer.Status.ToString());
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
        /// Occurs when the map is unloaded.
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event args</param>
        private void OnMapUnloaded(object sender, RoutedEventArgs e) {
            m_pAudioRecognizer.Stop();
            m_pGestureRecognizer.Stop();
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

        /// <summary>
        /// Resets the audio sync time.
        /// </summary>
        private void ResetAudioSyncTime()
        {
            this.m_sAudioSyncTime = new TimeSpan(0, 0, 0, 0, Iava.Core.Recognizer.SyncTimeoutValue);                     
        }

        /// <summary>
        /// Resets the gesture sync time.
        /// </summary>
        private void ResetGestureSyncTime()
        {
            this.m_sGestureSyncTime = new TimeSpan(0, 0, 0, 0, Iava.Core.Recognizer.SyncTimeoutValue);
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
        }

        /// <summary>
        /// Helper function for Move East Callback.
        /// </summary>
        /// <param name="e"></param>
        private void MoveEast() {
            ESRI.ArcGIS.Client.Geometry.MapPoint center = null;
            center = map1.Extent.GetCenter();

            Point screen = map1.MapToScreen(center);
            screen.X += 300;

            ESRI.ArcGIS.Client.Geometry.MapPoint newCenter = map1.ScreenToMap(screen);

            map1.PanTo(newCenter);
        }

        /// <summary>
        /// Helper function for Move North Callback.
        /// </summary>
        /// <param name="e"></param>
        private void MoveNorth() {
            ESRI.ArcGIS.Client.Geometry.MapPoint center = null;
            center = map1.Extent.GetCenter();

            Point screen = map1.MapToScreen(center);
            screen.Y -= 300;

            ESRI.ArcGIS.Client.Geometry.MapPoint newCenter = map1.ScreenToMap(screen);
            
            map1.PanTo(newCenter);
        }

        /// <summary>
        /// Helper function for Move South Callback.
        /// </summary>
        /// <param name="e"></param>
        private void MoveSouth() {
            ESRI.ArcGIS.Client.Geometry.MapPoint center = null;
            center = map1.Extent.GetCenter();

            Point screen = map1.MapToScreen(center);
            screen.Y += 300;

            ESRI.ArcGIS.Client.Geometry.MapPoint newCenter = map1.ScreenToMap(screen);

            map1.PanTo(newCenter);
        }

        /// <summary>
        /// Helper function for Move West Callback.
        /// </summary>
        /// <param name="e"></param>
        private void MoveWest() {
            ESRI.ArcGIS.Client.Geometry.MapPoint center = null;
            center = map1.Extent.GetCenter();

            Point screen = map1.MapToScreen(center);
            screen.X -= 300;

            ESRI.ArcGIS.Client.Geometry.MapPoint newCenter = map1.ScreenToMap(screen);

            map1.PanTo(newCenter);
        }

        private void ZoomIn() {
            map1.Zoom(2.0);
        }

        private void ZoomOut() {
            map1.Zoom(0.5);
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

        // UI theme specific.  It'd be great if we had time to databind the the labels to a boolean stating whether
        // or not they're synced and then apply the appropriate theme instead of doing this all in code.
        private LinearGradientBrush unsyncedBrush = new LinearGradientBrush(Color.FromRgb(226, 113, 0), Color.FromRgb(251, 155, 11), 90.0);
        private LinearGradientBrush syncedBrush = new LinearGradientBrush(Color.FromRgb(78, 154, 0), Color.FromRgb(115, 210, 23), 90.0);
        private LinearGradientBrush backgroundBrush = new LinearGradientBrush(Color.FromRgb(46, 52, 54), Color.FromRgb(65, 67, 63), 90.0);

        #endregion Private Fields
    }
}
