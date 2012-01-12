using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
//using Microsoft.Research.Kinect.Nui;

using Iava.Input.Camera;

namespace Iava.Gesture.GestureStuff {
    public class SwipeRightSegment1 : IGestureSegment {
        public GestureResult CheckGesture(SkeletonData skeleton) {
            // Left hand in front of Left shoulder
            if (skeleton.Joints[JointID.HandLeft].Position.Z < skeleton.Joints[JointID.ElbowLeft].Position.Z && skeleton.Joints[JointID.HandRight].Position.Y < skeleton.Joints[JointID.HipCenter].Position.Y) {
                // Console.WriteLine("GesturePart 0 - Left hand in front of Left shoudler - PASS");
                // Left hand below shoulder height but above hip height
                if (skeleton.Joints[JointID.HandLeft].Position.Y < skeleton.Joints[JointID.Head].Position.Y && skeleton.Joints[JointID.HandLeft].Position.Y > skeleton.Joints[JointID.HipCenter].Position.Y) {
                    // Console.WriteLine("GesturePart 0 - Left hand below shoulder height but above hip height - PASS");
                    // Left hand Left of Left shoulder
                    if (skeleton.Joints[JointID.HandLeft].Position.X < skeleton.Joints[JointID.ShoulderLeft].Position.X) {
                        Console.WriteLine("Swipe Right Gesture - Segment 1 received.");
                        return GestureResult.Succeed;
                    }

                    // Console.WriteLine("Swipe Right Gesture Segment 1 - Left hand Left of Left shoulder - UNDETERMINED");
                    return GestureResult.Pause;
                }

                // Console.WriteLine("Swipe Right Gesture Segment 1 - Left hand below shoulder height but above hip height - FAIL");
                return GestureResult.Fail;
            }

            // Console.WriteLine("Swipe Right Gesture Segment 1 - Left hand in front of Left shoulder - FAIL");
            return GestureResult.Fail;
        }
    }

    public class SwipeRightSegment2 : IGestureSegment {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GestureResult CheckGesture(SkeletonData skeleton) {
            // Left hand in front of Left shoulder
            if (skeleton.Joints[JointID.HandLeft].Position.Z < skeleton.Joints[JointID.ElbowLeft].Position.Z && skeleton.Joints[JointID.HandRight].Position.Y < skeleton.Joints[JointID.HipCenter].Position.Y) {
                // Console.WriteLine("GesturePart 0 - Left hand in front of Left shoudler - PASS");
                // Left hand below shoulder height but above hip height
                if (skeleton.Joints[JointID.HandLeft].Position.Y < skeleton.Joints[JointID.Head].Position.Y && skeleton.Joints[JointID.HandLeft].Position.Y > skeleton.Joints[JointID.HipCenter].Position.Y) {
                    // Console.WriteLine("GesturePart 0 - Left hand below shoulder height but above hip height - PASS");
                    // Left hand Right of Left shoulder & Left of Right shoulder
                    if (skeleton.Joints[JointID.HandLeft].Position.X > skeleton.Joints[JointID.ShoulderLeft].Position.X && skeleton.Joints[JointID.HandLeft].Position.X < skeleton.Joints[JointID.ShoulderRight].Position.X) {
                        Console.WriteLine("Swipe Right Gesture - Segment 2 received.");
                        return GestureResult.Succeed;
                    }

                    // Console.WriteLine("Swipe Right Gesture Segment 2 - Left hand Right of Left shoulder & Left of Right shoulder - UNDETERMINED");
                    return GestureResult.Pause;
                }

                // Console.WriteLine("Swipe Right Gesture Segment 2 - Left hand below shoulder height but above hip height - FAIL");
                return GestureResult.Fail;
            }

            // Console.WriteLine("Swipe Right Gesture Segment 2 - Left hand in front of Left shoulder - FAIL");
            return GestureResult.Fail;
        }
    }

    public class SwipeRightSegment3 : IGestureSegment {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GestureResult CheckGesture(SkeletonData skeleton) {
            // Left hand in front of Left shoulder
            if (skeleton.Joints[JointID.HandLeft].Position.Z < skeleton.Joints[JointID.ElbowLeft].Position.Z && skeleton.Joints[JointID.HandRight].Position.Y < skeleton.Joints[JointID.HipCenter].Position.Y) {
                // Console.WriteLine("GesturePart 0 - Left hand in front of Left shoudler - PASS");
                // Left hand below shoulder height but above hip height
                if (skeleton.Joints[JointID.HandLeft].Position.Y < skeleton.Joints[JointID.Head].Position.Y && skeleton.Joints[JointID.HandLeft].Position.Y > skeleton.Joints[JointID.HipCenter].Position.Y) {
                    // Console.WriteLine("GesturePart 0 - Left hand below shoulder height but above hip height - PASS");
                    // Left hand Right of Right Shoulder
                    if (skeleton.Joints[JointID.HandLeft].Position.X > skeleton.Joints[JointID.ShoulderRight].Position.X) {
                        Console.WriteLine("Swipe Right Gesture - Segment 3 received.");
                        return GestureResult.Succeed;
                    }

                    // Console.WriteLine("Swipe Right Gesture Segment 3 - Left hand Right of Right Shoulder - UNDETERMINED");
                    return GestureResult.Pause;
                }

                // Console.WriteLine("Swipe Right Gesture Segment 3 - Left hand below shoulder height but above hip height - FAIL");
                return GestureResult.Fail;
            }

            // Console.WriteLine("Swipe Right Gesture Segment 3 - Left hand in front of Left Shoulder - FAIL");
            return GestureResult.Fail;
        }
    }
}
