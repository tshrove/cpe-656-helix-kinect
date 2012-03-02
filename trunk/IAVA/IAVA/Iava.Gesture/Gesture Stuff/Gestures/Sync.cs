using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
//using Microsoft.Research.Kinect.Nui;

using Iava.Input.Camera;

namespace Iava.Gesture.GestureStuff{ 
    class SyncSegment : IGestureSegment {
        public GestureResult CheckGesture(IavaSkeletonData skeleton) {
            // Hands below head and above gut
            if (skeleton.Joints[IavaJointID.HandRight].Position.Y < skeleton.Joints[IavaJointID.Head].Position.Y  &&
                skeleton.Joints[IavaJointID.HandRight].Position.Y > skeleton.Joints[IavaJointID.Spine].Position.Y &&
                skeleton.Joints[IavaJointID.HandLeft].Position.Y  < skeleton.Joints[IavaJointID.Head].Position.Y  &&
                skeleton.Joints[IavaJointID.HandLeft].Position.Y  > skeleton.Joints[IavaJointID.Spine].Position.Y) {

                // Hands outside of shoulders
                if (skeleton.Joints[IavaJointID.HandRight].Position.X > skeleton.Joints[IavaJointID.ShoulderRight].Position.X &&
                    skeleton.Joints[IavaJointID.HandLeft].Position.X  < skeleton.Joints[IavaJointID.ShoulderLeft].Position.X) {
                    Console.WriteLine("Sync Gesture - Segment 1 received.");
                    return GestureResult.Succeed;
                }

                // Console.WriteLine("Sync Gesture Segment 1 - Hands outside of shoulders - UNDETERMINED");
                return GestureResult.Pause;
            }

            // Console.WriteLine("Sync Gesture Segment 1 - Hands below head and above gut - FAIL");
            return GestureResult.Fail;
        }
    }
}
