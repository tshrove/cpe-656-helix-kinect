using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Research.Kinect.Nui;

namespace Iava.Gesture.GestureStuff{ 
    class SyncSegment : IGestureSegment {
        public GestureResult CheckGesture(Microsoft.Research.Kinect.Nui.SkeletonData skeleton) {
            // Hands below head and above gut
            if (skeleton.Joints[JointID.HandRight].Position.Y < skeleton.Joints[JointID.Head].Position.Y  &&
                skeleton.Joints[JointID.HandRight].Position.Y > skeleton.Joints[JointID.Spine].Position.Y &&
                skeleton.Joints[JointID.HandLeft].Position.Y  < skeleton.Joints[JointID.Head].Position.Y  &&
                skeleton.Joints[JointID.HandLeft].Position.Y  > skeleton.Joints[JointID.Spine].Position.Y) {

                // Hands outside of shoulders
                if (skeleton.Joints[JointID.HandRight].Position.X > skeleton.Joints[JointID.ShoulderRight].Position.X &&
                    skeleton.Joints[JointID.HandLeft].Position.X  < skeleton.Joints[JointID.ShoulderLeft].Position.X) {
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
