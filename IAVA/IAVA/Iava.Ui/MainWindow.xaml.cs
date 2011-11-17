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
            // Gesture Callbacks
            m_pGestureRecognizer.Subscribe("Zoom", GestureZoomCallback);
            m_pGestureRecognizer.Subscribe("Left Swipe", GestureLeftSwipeCallback);
            m_pGestureRecognizer.Subscribe("Right Swipe", GestureLeftSwipeCallback);
            m_pGestureRecognizer.Subscribe("Up Swipe", GestureUpSwipeCallback);
            m_pGestureRecognizer.Subscribe("Down Swipe", GestureDownSwipeCallback);
            // Audio Callbacks
            m_pAudioRecognizer.Subscribe("Zoom In", ZoomInCallback);
            m_pAudioRecognizer.Subscribe("Zoom Out", ZoomOutCallback);           
        }

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
            screen.X = screen.X + 300;
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
            Window.Dispatcher.Invoke(new Action(() => DisplayStatus(String.Format("Gesture: {0} Detected", e.Name))));
        }
        /// <summary>
        /// The callback for when the Up Swipe gesture is detected.
        /// </summary>
        /// <param name="e"></param>
        private void GestureUpSwipeCallback(GestureEventArgs e)
        {
            Window.Dispatcher.Invoke(new Action(() => DisplayStatus(String.Format("Gesture: {0} Detected", e.Name))));
        }
        /// <summary>
        /// The callback for when the Down Swipe gesture is detected.
        /// </summary>
        /// <param name="e"></param>
        private void GestureDownSwipeCallback(GestureEventArgs e)
        {
            Window.Dispatcher.Invoke(new Action(() => DisplayStatus(String.Format("Gesture: {0} Detected", e.Name))));
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
            /* myGlowEffect = new OuterGlowBitmapEffect();

            // Set the size of the glow to 30 pixels.
            myGlowEffect.GlowSize = 30;

            // Set the color of the glow to blue.
            Color myGlowColor = new Color();
            myGlowColor.ScA = 1;
            myGlowColor.ScB = 1;
            myGlowColor.ScG = 0;
            myGlowColor.ScR = 0;
            myGlowEffect.GlowColor = myGlowColor;

            // Set the noise of the effect to the maximum possible (range 0-1).
            myGlowEffect.Noise = 1;

            // Set the Opacity of the effect to 40%. Note that the same effect
            // could be done by setting the ScA property of the Color to 0.4.
            myGlowEffect.Opacity = 0.4;

            // Apply the bitmap effect to the TextBox.

            btnAudioSynced.BitmapEffect = myGlowEffect;*/
        }
        /// <summary>
        /// Raises when the gesture recognizer is synced.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_pGestureRecognizer_Synced(object sender, EventArgs e)
        {
            /*OuterGlowBitmapEffect myGlowEffect = new OuterGlowBitmapEffect();

            // Set the size of the glow to 30 pixels.
            myGlowEffect.GlowSize = 30;

            // Set the color of the glow to blue.
            Color myGlowColor = new Color();
            myGlowColor.ScA = 1;
            myGlowColor.ScB = 1;
            myGlowColor.ScG = 0;
            myGlowColor.ScR = 0;
            myGlowEffect.GlowColor = myGlowColor;

            // Set the noise of the effect to the maximum possible (range 0-1).
            myGlowEffect.Noise = 1;

            // Set the Opacity of the effect to 40%. Note that the same effect
            // could be done by setting the ScA property of the Color to 0.4.
            myGlowEffect.Opacity = 0.4;

            // Apply the bitmap effect to the TextBox.

            btnGestureSynced.BitmapEffect = myGlowEffect;*/
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
