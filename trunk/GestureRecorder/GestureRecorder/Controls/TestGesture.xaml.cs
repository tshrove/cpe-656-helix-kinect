using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Iava.Gesture;

namespace GestureRecorder.Controls
{
    /// <summary>
    /// Interaction logic for TestGestureWindow.xaml
    /// </summary>
    public partial class TestGesture : UserControl
    {

        #region Members
        ObservableCollection<IavaGesture> m_pGestures = new ObservableCollection<IavaGesture>();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public TestGesture()
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
                    m_pGestures = new ObservableCollection<IavaGesture>(GestureFolderReader.Read(dialog.SelectedPath));
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
            this.Visibility = Visibility.Hidden;
        }
        #endregion
    }
}
