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
        public SkeletonCanvas() {
            InitializeComponent();
        }

        public SkeletonFrame SkeletonFrame {
            get { return _skeletonFrame; }
            set { _skeletonFrame = value; UpdateCanvas(); }
        }

        private SkeletonFrame _skeletonFrame;

        private void UpdateCanvas() {
            if (SkeletonFrame == null) { return; }

                int iSkeleton = 0;
                Brush[] brushes = new Brush[6];
                brushes[0] = new SolidColorBrush(Color.FromRgb(255,   0, 0));
                brushes[1] = new SolidColorBrush(Color.FromRgb(0,   255, 0));
                brushes[2] = new SolidColorBrush(Color.FromRgb(0,     0, 255));
                brushes[3] = new SolidColorBrush(Color.FromRgb(255, 255, 255));/*
                brushes[4] = new SolidColorBrush(Color.FromRgb(255, 64,  255));
                brushes[5] = new SolidColorBrush(Color.FromRgb(128, 128, 255));*/

                Children.Clear();
                foreach (SkeletonData data in SkeletonFrame.Skeletons) {
                    if (SkeletonTrackingState.Tracked == data.TrackingState) {
                        // Draw bones
                        Brush brush = brushes[iSkeleton % brushes.Length];
                        Children.Add(GetBodySegment(data.Joints, brush, JointID.HipCenter, JointID.Spine, JointID.ShoulderCenter, JointID.Head));
                        Children.Add(GetBodySegment(data.Joints, brush, JointID.ShoulderCenter, JointID.ShoulderLeft, JointID.ElbowLeft, JointID.WristLeft, JointID.HandLeft));
                        Children.Add(GetBodySegment(data.Joints, brush, JointID.ShoulderCenter, JointID.ShoulderRight, JointID.ElbowRight, JointID.WristRight, JointID.HandRight));
                        Children.Add(GetBodySegment(data.Joints, brush, JointID.HipCenter, JointID.HipLeft, JointID.KneeLeft, JointID.AnkleLeft, JointID.FootLeft));
                        Children.Add(GetBodySegment(data.Joints, brush, JointID.HipCenter, JointID.HipRight, JointID.KneeRight, JointID.AnkleRight, JointID.FootRight));

                        // Draw joints
                        foreach (Joint joint in data.Joints) {
                            Point jointPos = new Point(joint.Position.X, joint.Position.Y);
                            Line jointLine = new Line();
                            jointLine.X1 = jointPos.X - 3;
                            jointLine.X2 = jointLine.X1 + 6;
                            jointLine.Y1 = jointLine.Y2 = jointPos.Y;
                            jointLine.StrokeThickness = 6;
                            Children.Add(jointLine);
                        }
                    }
                    iSkeleton++;
                } // for each skeleton
        }

        /// <summary>
        /// Draws a line between the specified joints of a skeleton
        /// </summary>
        /// <param name="joints">Joints in a skeleton</param>
        /// <param name="brush">Color of the lines to draw</param>
        /// <param name="jointIDs">Joints to be connected by the poly line</param>
        /// <returns></returns>
        private Polyline GetBodySegment(JointsCollection joints, Brush brush, params JointID[] jointIDs) {
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
    }
}
