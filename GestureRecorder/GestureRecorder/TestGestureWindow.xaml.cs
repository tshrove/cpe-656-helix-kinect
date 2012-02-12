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
using GestureRecorder.DataStructures;
using System.Collections.ObjectModel;

namespace GestureRecorder
{
    /// <summary>
    /// Interaction logic for TestGestureWindow.xaml
    /// </summary>
    public partial class TestGestureWindow : Window
    {

        #region Members
        ObservableCollection<tempuri.org.GestureDefinition.xsd.Gesture> m_pGestures = new ObservableCollection<tempuri.org.GestureDefinition.xsd.Gesture>();
        #endregion

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
            using (System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_pGestures = new ObservableCollection<tempuri.org.GestureDefinition.xsd.Gesture>(GestureFolderReader.Read(dialog.SelectedPath));
                    this.lstGestures.ItemsSource = m_pGestures;
                }
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
