﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
//using Microsoft.Research.Kinect.Nui;

using Iava.Input.Camera;

namespace Iava.Gesture.GestureStuff {
    class SwipeDownSegment1 : IGestureSegment {
        public GestureResult CheckGesture(IavaSkeletonData skeleton) {
            // Right hand in front of body with Left hand hanging at the side
            if (skeleton.Joints[IavaJointID.HandRight].Position.Z < skeleton.Joints[IavaJointID.ShoulderCenter].Position.Z && skeleton.Joints[IavaJointID.HandLeft].Position.Y < skeleton.Joints[IavaJointID.HipCenter].Position.Y) {
                // Console.WriteLine("GesturePart 0 - Right hand in front of body - PASS");
                // Right hand between shoulders
                if (skeleton.Joints[IavaJointID.HandRight].Position.X < skeleton.Joints[IavaJointID.ShoulderRight].Position.X && skeleton.Joints[IavaJointID.HandRight].Position.X > skeleton.Joints[IavaJointID.ShoulderLeft].Position.X) {
                    // Console.WriteLine("GesturePart 0 - Right hand below shoulder height but above hip height - PASS");
                    // Right hand above the shoulders
                    if (skeleton.Joints[IavaJointID.HandRight].Position.Y > skeleton.Joints[IavaJointID.ShoulderCenter].Position.Y) {
                        Console.WriteLine("Swipe Down Gesture - Segment 1 received.");
                        return GestureResult.Succeed;
                    }

                    // Console.WriteLine("Swipe Down Gesture Segment 1 - Right hand above the shoulders - UNDETERMINED");
                    return GestureResult.Pause;
                }

                // Console.WriteLine("Swipe Down Gesture Segment 1 - Right hand between shoulders - FAIL");
                return GestureResult.Fail;
            }

            // Console.WriteLine("Swipe Down Gesture Segment 1 - Right hand in front of body with Left hand hanging at the side - FAIL");
            return GestureResult.Fail;
        }
    }

    class SwipeDownSegment2 : IGestureSegment {
        public GestureResult CheckGesture(IavaSkeletonData skeleton) {
            // Right hand in front of body with Left hand hanging at the side
            if (skeleton.Joints[IavaJointID.HandRight].Position.Z < skeleton.Joints[IavaJointID.ShoulderCenter].Position.Z && skeleton.Joints[IavaJointID.HandLeft].Position.Y < skeleton.Joints[IavaJointID.HipCenter].Position.Y) {
                // Console.WriteLine("GesturePart 0 - Right hand in front of body - PASS");
                // Right hand between shoulders
                if (skeleton.Joints[IavaJointID.HandRight].Position.X < skeleton.Joints[IavaJointID.ShoulderRight].Position.X && skeleton.Joints[IavaJointID.HandRight].Position.X > skeleton.Joints[IavaJointID.ShoulderLeft].Position.X) {
                    // Console.WriteLine("GesturePart 0 - Right hand below shoulder height but above hip height - PASS");
                    // Right hand between the shoulders and chest/gut
                    if (skeleton.Joints[IavaJointID.HandRight].Position.Y < skeleton.Joints[IavaJointID.ShoulderCenter].Position.Y && skeleton.Joints[IavaJointID.HandRight].Position.Y > skeleton.Joints[IavaJointID.Spine].Position.Y) {
                        Console.WriteLine("Swipe Down Gesture - Segment 2 received.");
                        return GestureResult.Succeed;
                    }

                    // Console.WriteLine("Swipe Down Gesture Segment 2 - Right hand between the shoulders and chest/gut - UNDETERMINED");
                    return GestureResult.Pause;
                }

                // Console.WriteLine("Swipe Down Gesture Segment 2 - Right hand between shoulders - FAIL");
                return GestureResult.Fail;
            }

            // Console.WriteLine("Swipe Down Gesture Segment 2 - Right hand in front of body with Left hand hanging at the side - FAIL");
            return GestureResult.Fail;
        }
    }

    class SwipeDownSegment3 : IGestureSegment {
        public GestureResult CheckGesture(IavaSkeletonData skeleton) {
            // Right hand in front of body with Left hand hanging at the side
            if (skeleton.Joints[IavaJointID.HandRight].Position.Z < skeleton.Joints[IavaJointID.ShoulderCenter].Position.Z && skeleton.Joints[IavaJointID.HandLeft].Position.Y < skeleton.Joints[IavaJointID.HipCenter].Position.Y) {
                // Console.WriteLine("GesturePart 0 - Right hand in front of body - PASS");
                // Right hand between shoulders
                if (skeleton.Joints[IavaJointID.HandRight].Position.X < skeleton.Joints[IavaJointID.ShoulderRight].Position.X && skeleton.Joints[IavaJointID.HandRight].Position.X > skeleton.Joints[IavaJointID.ShoulderLeft].Position.X) {
                    // Console.WriteLine("GesturePart 0 - Right hand below shoulder height but above hip height - PASS");
                    // Right hand below the chest/gut
                    if (skeleton.Joints[IavaJointID.HandRight].Position.Y < skeleton.Joints[IavaJointID.Spine].Position.Y) {
                        Console.WriteLine("Swipe Down Gesture - Segment 3 received.");
                        return GestureResult.Succeed;
                    }

                    // Console.WriteLine("Swipe Down Gesture Segment 3 - Right hand below the chest/gut - UNDETERMINED");
                    return GestureResult.Pause;
                }

                // Console.WriteLine("Swipe Down Gesture Segment 3 - Right hand between shoulders - FAIL");
                return GestureResult.Fail;
            }

            // Console.WriteLine("Swipe Down Gesture Segment 3 - Right hand in front of body with Left hand hanging at the side - FAIL");
            return GestureResult.Fail;
        }
    }
}
