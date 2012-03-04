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

using Iava.Input.Camera;

namespace GestureRecorder.Controls {
    /// <summary>
    /// Interaction logic for SkeletonCanvas.xaml
    /// </summary>
    public partial class SkeletonCanvas : Canvas {

        #region Public Properties

        public IavaSkeletonData Skeleton {
            get { return _skeleton; }
            set { _skeleton = value; UpdateCanvas(); }
        }

        #endregion Public Properties

        #region Constructors

        public SkeletonCanvas() {
            InitializeComponent();
        }

        #endregion Constructors

        #region Private Methods

        private void UpdateCanvas() {
            Children.Clear();

            if (Skeleton == null) { return; }

            Brush brush = new SolidColorBrush(Color.FromRgb(255, 0, 0));

            if (Skeleton.TrackingState == IavaSkeletonTrackingState.Tracked) {
                // Draw bones
                Children.Add(GetBodySegment(Skeleton.Joints, brush, IavaJointID.HipCenter, IavaJointID.Spine, IavaJointID.ShoulderCenter, IavaJointID.Head));
                Children.Add(GetBodySegment(Skeleton.Joints, brush, IavaJointID.ShoulderCenter, IavaJointID.ShoulderLeft, IavaJointID.ElbowLeft, IavaJointID.WristLeft, IavaJointID.HandLeft));
                Children.Add(GetBodySegment(Skeleton.Joints, brush, IavaJointID.ShoulderCenter, IavaJointID.ShoulderRight, IavaJointID.ElbowRight, IavaJointID.WristRight, IavaJointID.HandRight));
                Children.Add(GetBodySegment(Skeleton.Joints, brush, IavaJointID.HipCenter, IavaJointID.HipLeft, IavaJointID.KneeLeft, IavaJointID.AnkleLeft, IavaJointID.FootLeft));
                Children.Add(GetBodySegment(Skeleton.Joints, brush, IavaJointID.HipCenter, IavaJointID.HipRight, IavaJointID.KneeRight, IavaJointID.AnkleRight, IavaJointID.FootRight));

                // Draw joints
                foreach (IavaJoint joint in Skeleton.Joints) {
                    Point jointPos = new Point(joint.Position.X, joint.Position.Y);
                    Line jointLine = new Line();
                    jointLine.X1 = jointPos.X - 3;
                    jointLine.X2 = jointLine.X1 + 6;
                    jointLine.Y1 = jointLine.Y2 = jointPos.Y;
                    jointLine.StrokeThickness = 6;
                    Children.Add(jointLine);
                }
            }
        }

        /// <summary>
        /// Draws a line between the specified joints of a skeleton
        /// </summary>
        /// <param name="joints">Joints in a skeleton</param>
        /// <param name="brush">Color of the lines to draw</param>
        /// <param name="jointIDs">Joints to be connected by the poly line</param>
        /// <returns></returns>
        private Polyline GetBodySegment(IavaJointsCollection joints, Brush brush, params IavaJointID[] jointIDs) {
            PointCollection points = new PointCollection(jointIDs.Length);
            for (int i = 0; i < jointIDs.Length; ++i) {
                points.Add(ScalePoint(joints[jointIDs[i]].Position.X, joints[jointIDs[i]].Position.Y, ActualWidth, ActualHeight));
            }

            Polyline polyline = new Polyline();
            polyline.Points = points;
            polyline.Stroke = brush;
            polyline.StrokeThickness = 5;
            return polyline;
        }

        /// <summary>
        /// Scales a point between -1 and 1 and scales it up to the Max X and Max Y
        /// </summary>
        /// <param name="xPos">the X position of the point to scale</param>
        /// <param name="yPos">the Y position of the point to scale</param>
        /// <param name="maxX">the largest allowed X value after scale</param>
        /// <param name="maxY">the largest allowed Y value after scale</param>
        /// <returns>Point with the new X,Y values</returns>
        private Point ScalePoint(double xPos, double yPos, double maxX, double maxY) {
            double newX, newY;

            double temp = ((maxX / 2.0) * xPos) + (maxX / 2.0);

            // Scale the X value
            if (temp > maxX) { newX = maxX; }
            else if (temp < 0.0) { newX = 0.0; }
            else { newX = temp; }

            temp = ((maxY / 2.0) * yPos) + (maxY / 2.0);

            // Scale the Y value
            if (temp > maxY) { newY = maxY; }
            else if (temp < 0.0) { newY = 0.0; }
            else { newY = temp; }

            // Return the new point
            return new Point(newX, newY);
        }

        #endregion Private Methods

        #region Private Fields

        private IavaSkeletonData _skeleton;

        #endregion Private Fields
    }
}
