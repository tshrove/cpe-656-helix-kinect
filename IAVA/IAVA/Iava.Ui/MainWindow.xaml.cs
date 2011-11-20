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

namespace Iava.Ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Iava.Gesture.GestureRecognizer m_pGestureRecognizer;
        private Iava.Audio.AudioRecognizer m_pAudioRecognizer;
        private System.Timers.Timer m_pTimer = new System.Timers.Timer();
        private TextBoxStreamWriter m_pConsoleTxtBox = null;

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            m_pGestureRecognizer = new Gesture.GestureRecognizer(string.Empty);
            m_pAudioRecognizer = new Audio.AudioRecognizer(string.Empty);
            // Events
            m_pAudioRecognizer.StatusChanged += new EventHandler<EventArgs>(m_pAudioRecognizer_StatusChanged);
            m_pGestureRecognizer.StatusChanged += new EventHandler<EventArgs>(m_pGestureRecognizer_StatusChanged);
            m_pAudioRecognizer.Synced += new EventHandler<EventArgs>(m_pAudioRecognizer_Synced);
            m_pGestureRecognizer.Synced += new EventHandler<EventArgs>(m_pGestureRecognizer_Synced);
            m_pAudioRecognizer.Unsynced += new EventHandler<EventArgs>(m_pAudioRecognizer_Unsynced);
            m_pGestureRecognizer.Unsynced += new EventHandler<EventArgs>(m_pGestureRecognizer_Unsynced);
            // Gesture Callbacks
            m_pGestureRecognizer.Subscribe("Zoom", GestureZoomCallback);
            m_pGestureRecognizer.Subscribe("Left Swipe", GestureLeftSwipeCallback);
            m_pGestureRecognizer.Subscribe("Right Swipe", GestureRightSwipeCallback);
            m_pGestureRecognizer.Subscribe("Up Swipe", GestureUpSwipeCallback);
            m_pGestureRecognizer.Subscribe("Down Swipe", GestureDownSwipeCallback);
            // Audio Callbacks
            m_pAudioRecognizer.Subscribe("Zoom In", ZoomInCallback);
            m_pAudioRecognizer.Subscribe("Zoom Out", ZoomOutCallback);

            m_pGestureRecognizer.Camera.ImageFrameReady += new EventHandler<ImageFrameReadyEventArgs>(Camera_ImageFrameReady);

            m_pConsoleTxtBox = new TextBoxStreamWriter(this.txtConsole);
            Console.SetOut(m_pConsoleTxtBox);
        }
        #endregion

        #region Gesture Callbacks
        /// <summary>
        /// The callback for when the zoom gesture is detected.
        /// </summary>
        /// <param name="e"></param>
        private void GestureZoomCallback(GestureEventArgs e)
        {
            Window.Dispatcher.Invoke(new Action(() => DisplayStatus(String.Format("Gesture: {0} Detected", e.Name))));
        }
        /// <summary>
        /// The callback for when the Left Swipe gesture is detected.
        /// </summary>
        /// <param name="e"></param>
        private void GestureLeftSwipeCallback(GestureEventArgs e) 
        {
            ESRI.ArcGIS.Client.Geometry.MapPoint center = map1.Extent.GetCenter();
            Point screen = map1.MapToScreen(center);
            screen.X += 300;
            ESRI.ArcGIS.Client.Geometry.MapPoint newCenter = map1.ScreenToMap(screen);
            Window.Dispatcher.Invoke(new Action(() => DisplayStatus(String.Format("Gesture: {0} Detected", e.Name))));
            map1.Dispatcher.Invoke(new Action(() => map1.PanTo(newCenter)));
        }
        /// <summary>
        /// The callback for when the Right Swipe gesture is detected.
        /// </summary>
        /// <param name="e"></param>
        private void GestureRightSwipeCallback(GestureEventArgs e)
        {
            ESRI.ArcGIS.Client.Geometry.MapPoint center = map1.Extent.GetCenter();
            Point screen = map1.MapToScreen(center);
            screen.X -= 300;
            ESRI.ArcGIS.Client.Geometry.MapPoint newCenter = map1.ScreenToMap(screen);
            Window.Dispatcher.Invoke(new Action(() => DisplayStatus(String.Format("Gesture: {0} Detected", e.Name))));
            map1.Dispatcher.Invoke(new Action(() => map1.PanTo(newCenter)));
        }
        /// <summary>
        /// The callback for when the Up Swipe gesture is detected.
        /// </summary>
        /// <param name="e"></param>
        private void GestureUpSwipeCallback(GestureEventArgs e)
        {
            ESRI.ArcGIS.Client.Geometry.MapPoint center = map1.Extent.GetCenter();
            Point screen = map1.MapToScreen(center);
            screen.Y += 300;
            ESRI.ArcGIS.Client.Geometry.MapPoint newCenter = map1.ScreenToMap(screen);
            Window.Dispatcher.Invoke(new Action(() => DisplayStatus(String.Format("Gesture: {0} Detected", e.Name))));
            map1.Dispatcher.Invoke(new Action(() => map1.PanTo(newCenter)));
        }
        /// <summary>
        /// The callback for when the Down Swipe gesture is detected.
        /// </summary>
        /// <param name="e"></param>
        private void GestureDownSwipeCallback(GestureEventArgs e)
        {
            ESRI.ArcGIS.Client.Geometry.MapPoint center = map1.Extent.GetCenter();
            Point screen = map1.MapToScreen(center);
            screen.Y -= 300;
            ESRI.ArcGIS.Client.Geometry.MapPoint newCenter = map1.ScreenToMap(screen);
            Window.Dispatcher.Invoke(new Action(() => DisplayStatus(String.Format("Gesture: {0} Detected", e.Name))));
            map1.Dispatcher.Invoke(new Action(() => map1.PanTo(newCenter)));
        }
        #endregion

        #region Audio Callbacks
        /// <summary>
        /// Occurs when a zoom out command was received.
        /// </summary>
        /// <param name="e">Audio event args</param>
        private void ZoomOutCallback(AudioEventArgs e)
        {
            Window.Dispatcher.Invoke(new Action(() => DisplayStatus(String.Format("Audio: {0} Detected", e.Command))));
            map1.Dispatcher.Invoke(new Action(() => map1.Zoom(0.5)));
        }

        /// <summary>
        /// Occurs when a zoom in command was received.
        /// </summary>
        /// <param name="e">Audio event args</param>
        private void ZoomInCallback(AudioEventArgs e)
        {
            Window.Dispatcher.Invoke(new Action(() => DisplayStatus(String.Format("Audio: {0} Detected", e.Command))));
            map1.Dispatcher.Invoke(new Action(() => map1.Zoom(2.0)));
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Occurs when the map is unloaded.
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event args</param>
        private void OnMapUnloaded(object sender, RoutedEventArgs e)
        {
            m_pAudioRecognizer.Stop();
            m_pGestureRecognizer.Stop();
        }
        /// <summary>
        /// Occurs when the map is loaded.
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event args</param>
        private void OnMapLoaded(object sender, RoutedEventArgs e)
        {
            m_pAudioRecognizer.Start();
            m_pGestureRecognizer.Start();
        } 
        /// <summary>
        /// Raises when the status of the audio recognizer is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_pAudioRecognizer_StatusChanged(object sender, EventArgs e)
        {
            Window.Dispatcher.Invoke(new Action(() => DisplayStatus("Audio Status: " + m_pAudioRecognizer.Status.ToString())));
        }
        /// <summary>
        /// Raises when the status of the gesture recognizer is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_pGestureRecognizer_StatusChanged(object sender, EventArgs e)
        {
            Window.Dispatcher.Invoke(new Action(() => DisplayStatus("Audio Status: " + m_pAudioRecognizer.Status.ToString())));
        }
        /// <summary>
        /// Raises when the audio recognizer is synced.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_pAudioRecognizer_Synced(object sender, EventArgs e)
        {
            this.lblAudioStatus.Dispatcher.Invoke(new Action(() => this.lblAudioStatus.Content = "Audio Status: Synced"));
            this.lblAudioStatus.Dispatcher.Invoke(new Action(() => this.lblAudioStatus.Background = Brushes.Green));
        }
        /// <summary>
        /// Raises when the gesture recognizer is synced.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_pGestureRecognizer_Synced(object sender, EventArgs e)
        {
            this.lblGestureStatus.Dispatcher.Invoke(new Action(() => this.lblGestureStatus.Content = "Gesture Status: Synced"));
            this.lblGestureStatus.Dispatcher.Invoke(new Action(() => this.lblGestureStatus.Background = Brushes.Green));
        }
        /// <summary>
        /// Raises when the camera frame is ready to be viewed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Camera_ImageFrameReady(object sender, ImageFrameReadyEventArgs e)
        {
            PlanarImage Image = e.ImageFrame.Image;

            kinectVideoFeed.Source = BitmapSource.Create(
                Image.Width, Image.Height, 96, 96, PixelFormats.Bgr32, null,
                Image.Bits, Image.Width * Image.BytesPerPixel);
        }
        /// <summary>
        /// Event used in junction with the display status function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            popStatus.Dispatcher.Invoke(new Action(() => popStatus.IsOpen = false));
        }
        /// <summary>
        /// Raises when the gesture recognizer becomes unsynced.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_pGestureRecognizer_Unsynced(object sender, EventArgs e)
        {
            this.lblGestureStatus.Dispatcher.Invoke(new Action(() => this.lblGestureStatus.Content = "Gesture Status: Unsynced"));
            this.lblGestureStatus.Dispatcher.Invoke(new Action(() => this.lblGestureStatus.Background = Brushes.Orange)); 
        }
        /// <summary>
        /// Raises when the audio recognizer becomes unsycned.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_pAudioRecognizer_Unsynced(object sender, EventArgs e)
        {
            this.lblAudioStatus.Dispatcher.Invoke(new Action(() => this.lblAudioStatus.Content = "Audio Status: Unsyned"));
            this.lblAudioStatus.Dispatcher.Invoke(new Action(() => this.lblAudioStatus.Background = Brushes.Orange));
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Displays the pop status control with the auto close.
        /// </summary>
        /// <param name="text"></param>
        private void DisplayStatus(string text)
        {
            if (!popStatus.IsOpen)
            {
                m_pTimer.Interval = 2000; // 2 seconds
                m_pTimer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
                m_pTimer.Enabled = true;
                popStatus.IsOpen = true;
                lblStatus.Content = text;
            }
            else
            {
                m_pTimer.Interval = 2000;
                lblStatus.Content = text;
            }
        }
        #endregion
    }
}
