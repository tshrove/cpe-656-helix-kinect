﻿using Iava.Core.Math;
using Microsoft.Kinect;
using System;

namespace Iava.Input.Camera {

    public struct IavaJoint {

        #region Public Properties

        public IavaJointType JointType { get; set; }

        public IavaSkeletonPoint Position { get; set; }

        public IavaJointTrackingState TrackingState { get; set; }

        #endregion Public Properties

        #region Operator Overloads

        public static bool operator ==(IavaJoint joint1, IavaJoint joint2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(joint1, joint2)) { return true; }

            // If just one is null, return false.
            if (((object)joint1 == null) || ((object)joint2 == null)) { return false; }

            return (joint1.JointType == joint2.JointType &&
                    joint1.Position == joint2.Position &&
                    joint1.TrackingState == joint2.TrackingState);
        }

        public static bool operator !=(IavaJoint joint1, IavaJoint joint2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(joint1, joint2)) { return false; }

            // If just one is null, return true.
            if (((object)joint1 == null) || ((object)joint2 == null)) { return true; }

            return (joint1.JointType != joint2.JointType ||
                    joint1.Position != joint2.Position ||
                    joint1.TrackingState != joint2.TrackingState);
        }

        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            // If parameter cannot be cast, return false.
            IavaJoint joint = (IavaJoint)obj;
            if ((Object)joint == null) { return false; }

            // Do a field by field comparison
            return (joint.JointType == this.JointType &&
                    joint.Position == this.Position &&
                    joint.TrackingState == this.TrackingState);
        }

        public static explicit operator IavaJoint(Joint value) {
            return new IavaJoint()
            {
                JointType = (IavaJointType)value.JointType,
                Position = (IavaSkeletonPoint)value.Position,
                TrackingState = (IavaJointTrackingState)value.TrackingState
            };
        }

        #endregion Operator Overloads
    }
}