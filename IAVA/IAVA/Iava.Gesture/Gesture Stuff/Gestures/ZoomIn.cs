using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
//using Microsoft.Research.Kinect.Nui;

using Iava.Input.Camera;

namespace Iava.Gesture.GestureStuff {
    class ZoomInSegment1 : IGestureSegment {
        public GestureResult CheckGesture(IavaSkeletonData skeleton) {
            // Hands in front of body
            if (skeleton.Joints[IavaJointID.HandRight].Position.Z < skeleton.Joints[IavaJointID.ShoulderRight].Position.Z &&
                skeleton.Joints[IavaJointID.HandLeft].Position.Z  < skeleton.Joints[IavaJointID.ShoulderLeft].Position.Z) {

                // Hands below head and above gut
                if (skeleton.Joints[IavaJointID.HandRight].Position.Y < skeleton.Joints[IavaJointID.Head].Position.Y  &&
                    skeleton.Joints[IavaJointID.HandRight].Position.Y > skeleton.Joints[IavaJointID.Spine].Position.Y &&
                    skeleton.Joints[IavaJointID.HandLeft].Position.Y  < skeleton.Joints[IavaJointID.Head].Position.Y  &&
                    skeleton.Joints[IavaJointID.HandLeft].Position.Y  > skeleton.Joints[IavaJointID.Spine].Position.Y) {

                    // Hands between shoulders
                    if (skeleton.Joints[IavaJointID.HandRight].Position.X < skeleton.Joints[IavaJointID.ShoulderRight].Position.X &&
                        skeleton.Joints[IavaJointID.HandLeft].Position.X  > skeleton.Joints[IavaJointID.ShoulderLeft].Position.X) {

                        Console.WriteLine("Zoom In Gesture - Segment 1 received.");
                        return GestureResult.Succeed;
                    }

                    // Console.WriteLine("Zoom In Gesture Segment 1 - Hands between shoulders - UNDETERMINED");
                    return GestureResult.Pause;
                }

                // Console.WriteLine("Zoom In Gesture Segment 1 - Hands below head and above gut - FAIL");
                return GestureResult.Fail;
            }

            // Console.WriteLine("Zoom In Gesture Segment 1 - Hands in front of body - FAIL");
            return GestureResult.Fail;
        }
    }

    class ZoomInSegment2 : IGestureSegment {
        public GestureResult CheckGesture(IavaSkeletonData skeleton) {
            // Hands in front of body
            if (skeleton.Joints[IavaJointID.HandRight].Position.Z < skeleton.Joints[IavaJointID.ShoulderRight].Position.Z &&
                skeleton.Joints[IavaJointID.HandLeft].Position.Z  < skeleton.Joints[IavaJointID.ShoulderLeft].Position.Z) {

                // Hands below head and above gut
                if (skeleton.Joints[IavaJointID.HandRight].Position.Y < skeleton.Joints[IavaJointID.Head].Position.Y   &&
                    skeleton.Joints[IavaJointID.HandRight].Position.Y > skeleton.Joints[IavaJointID.Spine].Position.Y  &&
                    skeleton.Joints[IavaJointID.HandLeft].Position.Y  < skeleton.Joints[IavaJointID.Head].Position.Y   &&
                    skeleton.Joints[IavaJointID.HandLeft].Position.Y  > skeleton.Joints[IavaJointID.Spine].Position.Y) {

                    // Hands outside of shoulders
                    if (skeleton.Joints[IavaJointID.HandRight].Position.X > skeleton.Joints[IavaJointID.ShoulderRight].Position.X &&
                        skeleton.Joints[IavaJointID.HandLeft].Position.X  < skeleton.Joints[IavaJointID.ShoulderLeft].Position.X) {

                        Console.WriteLine("Zoom In Gesture - Segment 2 received.");
                        return GestureResult.Succeed;
                    }

                    // Console.WriteLine("Zoom In Gesture Segment 2 - Hands outside of shoulders - UNDETERMINED");
                    return GestureResult.Pause;
                }

                // Console.WriteLine("Zoom In Gesture Segment 2 - Hands below head and above gut - FAIL");
                return GestureResult.Fail;
            }

            // Console.WriteLine("Zoom In Gesture Segment 2 - Hands in front of body - FAIL");
            return GestureResult.Fail;
        }
    }
}