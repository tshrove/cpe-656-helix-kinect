using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Research.Kinect.Nui;

namespace Iava.Gesture.GestureStuff {
    class DownSwipeSegment1 : IGestureSegment {
        public GestureResult CheckGesture(Microsoft.Research.Kinect.Nui.SkeletonData skeleton) {
            // Right hand in front of body with Left hand hanging at the side
            if (skeleton.Joints[JointID.HandRight].Position.Z < skeleton.Joints[JointID.ShoulderCenter].Position.Z && skeleton.Joints[JointID.HandLeft].Position.Y < skeleton.Joints[JointID.HipCenter].Position.Y) {
                // Debug.WriteLine("GesturePart 0 - Right hand in front of body - PASS");
                // Right hand between shoulders
                if (skeleton.Joints[JointID.HandRight].Position.X < skeleton.Joints[JointID.ShoulderRight].Position.X && skeleton.Joints[JointID.HandRight].Position.X > skeleton.Joints[JointID.ShoulderLeft].Position.X) {
                    // Debug.WriteLine("GesturePart 0 - Right hand below shoulder height but above hip height - PASS");
                    // Right hand above the shoulders
                    if (skeleton.Joints[JointID.HandRight].Position.Y > skeleton.Joints[JointID.ShoulderCenter].Position.Y) {
                        Debug.WriteLine("Down Swipe Gesture - Segment 1 received.");
                        return GestureResult.Succeed;
                    }

                    // Debug.WriteLine("Down Swipe Gesture Segment 1 - Right hand above the shoulders - UNDETERMINED");
                    return GestureResult.Pause;
                }

                // Debug.WriteLine("Down Swipe Gesture Segment 1 - Right hand between shoulders - FAIL");
                return GestureResult.Fail;
            }

            // Debug.WriteLine("Down Swipe Gesture Segment 1 - Right hand in front of body with Left hand hanging at the side - FAIL");
            return GestureResult.Fail;
        }
    }

    class DownSwipeSegment2 : IGestureSegment {
        public GestureResult CheckGesture(Microsoft.Research.Kinect.Nui.SkeletonData skeleton) {
            // Right hand in front of body with Left hand hanging at the side
            if (skeleton.Joints[JointID.HandRight].Position.Z < skeleton.Joints[JointID.ShoulderCenter].Position.Z && skeleton.Joints[JointID.HandLeft].Position.Y < skeleton.Joints[JointID.HipCenter].Position.Y) {
                // Debug.WriteLine("GesturePart 0 - Right hand in front of body - PASS");
                // Right hand between shoulders
                if (skeleton.Joints[JointID.HandRight].Position.X < skeleton.Joints[JointID.ShoulderRight].Position.X && skeleton.Joints[JointID.HandRight].Position.X > skeleton.Joints[JointID.ShoulderLeft].Position.X) {
                    // Debug.WriteLine("GesturePart 0 - Right hand below shoulder height but above hip height - PASS");
                    // Right hand between the shoulders and chest/gut
                    if (skeleton.Joints[JointID.HandRight].Position.Y < skeleton.Joints[JointID.ShoulderCenter].Position.Y && skeleton.Joints[JointID.HandRight].Position.Y > skeleton.Joints[JointID.Spine].Position.Y) {
                        Debug.WriteLine("Down Swipe Gesture - Segment 2 received.");
                        return GestureResult.Succeed;
                    }

                    // Debug.WriteLine("Down Swipe Gesture Segment 2 - Right hand between the shoulders and chest/gut - UNDETERMINED");
                    return GestureResult.Pause;
                }

                // Debug.WriteLine("Down Swipe Gesture Segment 2 - Right hand between shoulders - FAIL");
                return GestureResult.Fail;
            }

            // Debug.WriteLine("Down Swipe Gesture Segment 2 - Right hand in front of body with Left hand hanging at the side - FAIL");
            return GestureResult.Fail;
        }
    }

    class DownSwipeSegment3 : IGestureSegment {
        public GestureResult CheckGesture(Microsoft.Research.Kinect.Nui.SkeletonData skeleton) {
            // Right hand in front of body with Left hand hanging at the side
            if (skeleton.Joints[JointID.HandRight].Position.Z < skeleton.Joints[JointID.ShoulderCenter].Position.Z && skeleton.Joints[JointID.HandLeft].Position.Y < skeleton.Joints[JointID.HipCenter].Position.Y) {
                // Debug.WriteLine("GesturePart 0 - Right hand in front of body - PASS");
                // Right hand between shoulders
                if (skeleton.Joints[JointID.HandRight].Position.X < skeleton.Joints[JointID.ShoulderRight].Position.X && skeleton.Joints[JointID.HandRight].Position.X > skeleton.Joints[JointID.ShoulderLeft].Position.X) {
                    // Debug.WriteLine("GesturePart 0 - Right hand below shoulder height but above hip height - PASS");
                    // Right hand below the chest/gut
                    if (skeleton.Joints[JointID.HandRight].Position.Y < skeleton.Joints[JointID.Spine].Position.Y) {
                        Debug.WriteLine("Down Swipe Gesture - Segment 3 received.");
                        return GestureResult.Succeed;
                    }

                    // Debug.WriteLine("Down Swipe Gesture Segment 3 - Right hand below the chest/gut - UNDETERMINED");
                    return GestureResult.Pause;
                }

                // Debug.WriteLine("Down Swipe Gesture Segment 3 - Right hand between shoulders - FAIL");
                return GestureResult.Fail;
            }

            // Debug.WriteLine("Down Swipe Gesture Segment 3 - Right hand in front of body with Left hand hanging at the side - FAIL");
            return GestureResult.Fail;
        }
    }
}
