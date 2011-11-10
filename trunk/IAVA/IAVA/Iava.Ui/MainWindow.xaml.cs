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

        public MainWindow()
        {
            InitializeComponent();
            m_pGestureRecognizer = new Gesture.GestureRecognizer(string.Empty);
            m_pAudioRecognizer = new Audio.AudioRecognizer(string.Empty);

            m_pGestureRecognizer.Subscribe("Zoom", GestureZoomCallback);
            m_pAudioRecognizer.Subscribe("Zoom In", ZoomInCallback);
            m_pAudioRecognizer.Subscribe("Zoom Out", ZoomOutCallback);           
        }
        /// <summary>
        /// The callback for when the zoom gesture is detected.
        /// </summary>
        /// <param name="e"></param>
        private void GestureZoomCallback(GestureEventArgs e)
        {
            
        }

        /// <summary>
        /// Occurs when a zoom out command was received.
        /// </summary>
        /// <param name="e">Audio event args</param>
        private void ZoomOutCallback(AudioEventArgs e)
        {
            map1.Dispatcher.Invoke(new Action(() => map1.Zoom(0.5)));
        }

        /// <summary>
        /// Occurs when a zoom in command was received.
        /// </summary>
        /// <param name="e">Audio event args</param>
        private void ZoomInCallback(AudioEventArgs e)
        {
            map1.Dispatcher.Invoke(new Action(() => map1.Zoom(2.0)));
        }

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
            //m_pGestureRecognizer.Start();
        }
    }
}
