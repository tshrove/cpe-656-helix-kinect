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
using System.Windows.Shapes;

namespace GestureRecorder
{
    /// <summary>
    /// Interaction logic for TestGestureWindow.xaml
    /// </summary>
    public partial class TestGestureWindow : Window
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public TestGestureWindow()
        {
            InitializeComponent();
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
            Microsoft.Win32.OpenFileDialog open = new Microsoft.Win32.OpenFileDialog();
            open.CheckFileExists = true;
            open.Filter = "IAVA Gesture file (*.iava)|*.iava";
            Nullable<bool> results = open.ShowDialog();
            if (results == true)
            {
                String filename = open.FileName;
            }
        }
        /// <summary>
        /// Raises when the close button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
