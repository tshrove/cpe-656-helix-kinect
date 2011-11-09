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
            m_pAudioRecognizer.Subscribe("Zoom", AudioZoomCallback);
        }
        /// <summary>
        /// The callback for when the zoom gesture is detected.
        /// </summary>
        /// <param name="e"></param>
        private void GestureZoomCallback(GestureEventArgs e)
        {
            this.ZoomCallback();
        }
        private void AudioZoomCallback(AudioEventArgs e)
        {
            this.ZoomCallback();
        }
        private void ZoomCallback()
        {
            // TODO: This needs to be changed.
            this.map1.Zoom(2.0);
        }
    }
}
