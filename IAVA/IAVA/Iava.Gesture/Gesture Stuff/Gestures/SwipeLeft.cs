﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
//using Microsoft.Research.Kinect.Nui;

using Iava.Input.Camera;

namespace Iava.Gesture.GestureStuff {
    public class SwipeLeftSegment1 : IGestureSegment {
        public GestureResult CheckGesture(SkeletonData skeleton)
        {
            // Right hand in front of Right shoulder
            if (skeleton.Joints[JointID.HandRight].Position.Z < skeleton.Joints[JointID.ElbowRight].Position.Z && skeleton.Joints[JointID.HandLeft].Position.Y < skeleton.Joints[JointID.HipCenter].Position.Y)
            {
                // Console.WriteLine("GesturePart 0 - Right hand in front of Right shoudler - PASS");
                // Right hand below shoulder height but above hip height
                if (skeleton.Joints[JointID.HandRight].Position.Y < skeleton.Joints[JointID.Head].Position.Y && skeleton.Joints[JointID.HandRight].Position.Y > skeleton.Joints[JointID.HipCenter].Position.Y)
                {
                    // Console.WriteLine("GesturePart 0 - Right hand below shoulder height but above hip height - PASS");
                    // Right hand Right of Right shoulder
                    if (skeleton.Joints[JointID.HandRight].Position.X > skeleton.Joints[JointID.ShoulderRight].Position.X)
                    {
                        Console.WriteLine("Swipe Left Gesture - Segment 1 received.");
                        return GestureResult.Succeed;
                    }

                    // Console.WriteLine("Swipe Left Gesture Segment 1 - Right hand Right of Right shoulder - UNDETERMINED");
                    return GestureResult.Pause;
                }

                // Console.WriteLine("Swipe Left Gesture Segment 1 - Right hand below shoulder height but above hip height - FAIL");
                return GestureResult.Fail;
            }

            // Console.WriteLine("Swipe Left Gesture Segment 1 - Right hand in front of Right shoulder - FAIL");
            return GestureResult.Fail;
        }
    }

    public class SwipeLeftSegment2 : IGestureSegment {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GestureResult CheckGesture(SkeletonData skeleton) {
            // //Right hand in front of Right shoulder
            if (skeleton.Joints[JointID.HandRight].Position.Z < skeleton.Joints[JointID.ElbowRight].Position.Z && skeleton.Joints[JointID.HandLeft].Position.Y < skeleton.Joints[JointID.HipCenter].Position.Y) {
                // Console.WriteLine("GesturePart 1 - Right hand in front of Right shoulder - PASS");
                // //Right hand below shoulder height but above hip height
                if (skeleton.Joints[JointID.HandRight].Position.Y < skeleton.Joints[JointID.Head].Position.Y && skeleton.Joints[JointID.HandRight].Position.Y > skeleton.Joints[JointID.HipCenter].Position.Y) {
                    // Console.WriteLine("GesturePart 1 - Right hand below shoulder height but above hip height - PASS");
                    // //Right hand left of Right shoulder & Right of left shoulder
                    if (skeleton.Joints[JointID.HandRight].Position.X < skeleton.Joints[JointID.ShoulderRight].Position.X && skeleton.Joints[JointID.HandRight].Position.X > skeleton.Joints[JointID.ShoulderLeft].Position.X) {
                        Console.WriteLine("Swipe Left Gesture - Segment 2 received.");
                        return GestureResult.Succeed;
                    }

                    // Console.WriteLine("Swipe Left Gesture Segment 2 - Right hand left of Right shoulder & Right of left shoulder - UNDETERMINED");
                    return GestureResult.Pause;
                }

                // Console.WriteLine("Swipe Left Gesture Segment 2 - Right hand below shoulder height but above hip height - FAIL");
                return GestureResult.Fail;
            }

            // Console.WriteLine("Swipe Left Gesture Segment 2 - Right hand in front of Right shoulder - FAIL");
            return GestureResult.Fail;
        }
    }

    public class SwipeLeftSegment3 : IGestureSegment {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GestureResult CheckGesture(SkeletonData skeleton) {
            // //Right hand in front of Right Shoulder
            if (skeleton.Joints[JointID.HandRight].Position.Z < skeleton.Joints[JointID.ElbowRight].Position.Z && skeleton.Joints[JointID.HandLeft].Position.Y < skeleton.Joints[JointID.HipCenter].Position.Y) {
                // Console.WriteLine("GesturePart 2 - Right hand in front of Right shoulder - PASS");
                // //Right hand below shoulder height but above hip height
                if (skeleton.Joints[JointID.HandRight].Position.Y < skeleton.Joints[JointID.Head].Position.Y && skeleton.Joints[JointID.HandRight].Position.Y > skeleton.Joints[JointID.HipCenter].Position.Y) {
                    // Console.WriteLine("GesturePart 2 - Right hand below shoulder height but above hip height - PASS");
                    // //Right hand left of left Shoulder
                    if (skeleton.Joints[JointID.HandRight].Position.X < skeleton.Joints[JointID.ShoulderLeft].Position.X) {
                        Console.WriteLine("Swipe Left Gesture - Segment 3 received.");
                        return GestureResult.Succeed;
                    }

                    // Console.WriteLine("Swipe Left Gesture Segment 3 - Right hand left of Right Shoulder - UNDETERMINED");
                    return GestureResult.Pause;
                }

                // Console.WriteLine("Swipe Left Gesture Segment 3 - Right hand below shoulder height but above hip height - FAIL");
                return GestureResult.Fail;
            }

            // Console.WriteLine("Swipe Left Gesture Segment 3 - Right hand in front of Right Shoulder - FAIL");
            return GestureResult.Fail;
        }
    }
}
