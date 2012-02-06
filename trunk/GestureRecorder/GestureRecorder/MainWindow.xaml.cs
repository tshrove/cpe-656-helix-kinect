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
using tempuri.org.GestureDefinition.xsd;

namespace GestureRecorder {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();
            if (System.IO.File.Exists(dlg.FileName))
            {
                System.IO.FileStream file = new System.IO.FileStream(dlg.FileName, System.IO.FileMode.Open);
                System.IO.StreamReader stream = new System.IO.StreamReader(file);
                Gesture gesture = Gesture.Load(stream);
            }
        }
    }
}
