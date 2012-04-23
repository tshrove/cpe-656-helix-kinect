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
using Iava.Audio;
using GestureRecorder.Controls;

namespace GestureRecorder {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public AudioRecognizer AudioRecognizer { get; set; }

        public MainWindow() {
            InitializeComponent();

            AudioRecognizer = new Iava.Audio.AudioRecognizer();

            AudioRecognizer.Subscribe("Create Gesture", CreateAudioCallback);
            AudioRecognizer.Subscribe("Test Gesture", TestAudioCallback);
            AudioRecognizer.Subscribe("Exit", ExitAudioCallback);

            AudioRecognizer.Start();
        }

        private void CreateAudioCallback(AudioEventArgs e) {
            btnCreate.IsChecked = true;
        }

        private void TestAudioCallback(AudioEventArgs e) {
            btnTest.IsChecked = true;
        }

        private void ExitAudioCallback(AudioEventArgs e) {
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void OnCreateGestureChecked(object sender, RoutedEventArgs e) {
            
        }

        private void OnTestGestureChecked(object sender, RoutedEventArgs e) {
        }

        private void OnExitChecked(object sender, RoutedEventArgs e) {
            Close();
        }
        /*
        private CreateGesture createGestureControl { get; set; }

        private TestGesture testGestureControl { get; set; }*/
    }
}
