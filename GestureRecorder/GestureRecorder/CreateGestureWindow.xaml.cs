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
    /// Interaction logic for CreateGestureWindow.xaml
    /// </summary>
    public partial class CreateGestureWindow : Window
    {
        #region Members
        #region DotBodyPart Members
        private bool m_bHead = false;
        private bool m_bShoulderCenter = false;
        private bool m_bShoulderLeft = false;
        private bool m_bShoulderRight = false;
        private bool m_bElbowLeft = false;
        private bool m_bElbowRight = false;
        private bool m_bWristLeft = false;
        private bool m_bWristRight = false;
        private bool m_bHandLeft = false;
        private bool m_bHandRight = false;
        private bool m_bSpine = false;
        private bool m_bHipCenter = false;
        private bool m_bHipLeft = false;
        private bool m_bHipRight = false;
        private bool m_bKneeLeft = false;
        private bool m_bKneeRight = false;
        private bool m_bAnkleLeft = false;
        private bool m_bAnkleRight = false;
        private bool m_bFootLeft = false;
        private bool m_bFootRight = false;
        #endregion
        #endregion

        public CreateGestureWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Raises when the cancel button has been clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Raises when one of the body parts have been selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dot_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            String sBodyPart = ((Image)sender).Name;
            switch (sBodyPart)
            {
                case "dotHead":
                    if (!m_bHead) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    } 
                    m_bHead = !m_bHead; 
                    break;
                case "dotShoulderCenter": 
                    if (!m_bShoulderCenter) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bShoulderCenter = !m_bShoulderCenter;
                    break;
                case "dotShoulderLeft": 
                    if (!m_bShoulderLeft) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bShoulderLeft = !m_bShoulderLeft;
                    break;
                case "dotShoulderRight": 
                    if (!m_bShoulderRight) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bShoulderRight = !m_bShoulderRight;
                    break;
                case "dotElbowLeft": 
                    if (!m_bElbowLeft) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bElbowLeft = !m_bElbowLeft;
                    break;
                case "dotElbowRight": 
                    if (!m_bElbowRight) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bElbowRight = !m_bElbowRight;
                    break;
                case "dotWristLeft":
                    if (!m_bWristLeft) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bWristLeft = !m_bWristLeft;
                    break;
                case "dotWristRight": 
                    if (!m_bWristRight) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bWristRight = !m_bWristRight;
                    break;
                case "dotHandLeft": 
                    if (!m_bHandLeft) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bHandLeft = !m_bHandLeft;
                    break;
                case "dotHandRight": 
                    if (!m_bHandRight) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bHandRight = !m_bHandRight;
                    break;
                case "dotSpine":
                    if (!m_bSpine) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bSpine = !m_bSpine;
                    break;
                case "dotHipCenter":
                    if (!m_bHipCenter) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bHipCenter = !m_bHipCenter;
                    break;
                case "dotHipLeft": 
                    if (!m_bHipLeft) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bHipLeft = !m_bHipLeft;
                    break;
                case "dotHipRight":
                    if (!m_bHipRight) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bHipRight = !m_bHipRight;
                    break;
                case "dotKneeLeft":
                    if (!m_bKneeLeft) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bKneeLeft = !m_bKneeLeft;
                    break;
                case "dotKneeRight":
                    if (!m_bKneeRight) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bKneeRight = !m_bKneeRight;
                    break;
                case "dotAnkleLeft":
                    if (!m_bAnkleLeft) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bAnkleLeft = !m_bAnkleLeft;
                    break;
                case "dotAnkleRight":
                    if (!m_bAnkleRight) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bAnkleRight = !m_bAnkleRight;
                    break;
                case "dotFootLeft":
                    if (!m_bFootLeft) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bFootLeft = !m_bFootLeft;
                    break;
                case "dotFootRight":
                    if (!m_bFootRight) 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/bluecircle.png")); 
                    } 
                    else 
                    {
                        ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/GestureRecorder;component/Resources/circle.png")); 
                    }
                    m_bFootRight = !m_bFootRight;
                    break;
                default:
                    break;
            }
        }
    }
}
