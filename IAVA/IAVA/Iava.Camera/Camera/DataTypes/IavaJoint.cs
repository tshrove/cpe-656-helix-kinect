using Iava.Core.Math;
using Microsoft.Kinect;
using System;

namespace Iava.Input.Camera {

    /// <summary>
    /// Contains the state information of a skeletal joint.
    /// </summary>
    public struct IavaJoint {

        #region Public Properties

        public IavaJointType JointType { get; set; }

        public IavaSkeletonPoint Position { get; set; }

        public IavaJointTrackingState TrackingState { get; set; }

        #endregion Public Properties

        #region Operator Overloads

        /// <summary>
        /// Determines whether two IavaJoint instances are equal.
        /// </summary>
        /// <param name="joint1">A IavaJoint to compare for equality.</param>
        /// <param name="joint2">A IavaJoint to compare for equality.</param>
        /// <returns>TRUE if the two IavaJoint instances are equal, else FALSE</returns>
        public static bool operator ==(IavaJoint joint1, IavaJoint joint2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(joint1, joint2)) { return true; }

            // If just one is null, return false.
            if (((object)joint1 == null) || ((object)joint2 == null)) { return false; }

            return (joint1.JointType.Equals(joint2.JointType) &&
                    joint1.Position.Equals(joint2.Position) &&
                    joint1.TrackingState.Equals(joint2.TrackingState));
        }

        /// <summary>
        /// Determines whether two IavaJoint instances are not equal.
        /// </summary>
        /// <param name="joint1">A IavaJoint to compare for inequality.</param>
        /// <param name="joint2">A IavaJoint to compare for inequality.</param>
        /// <returns>TRUE if the two IavaJoint instances are not equal, else FALSE</returns>
        public static bool operator !=(IavaJoint joint1, IavaJoint joint2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(joint1, joint2)) { return false; }

            // If just one is null, return true.
            if (((object)joint1 == null) || ((object)joint2 == null)) { return true; }

            return (!joint1.JointType.Equals(joint2.JointType) ||
                    !joint1.Position.Equals(joint2.Position) ||
                    !joint1.TrackingState.Equals(joint2.TrackingState));
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current IavaJoint. 
        /// </summary>
        /// <param name="obj">Object to compare with the current IavaJoint.</param>
        /// <returns>TRUE if the specified object is equal to the current IavaJoint, else FALSE. </returns>
        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            try {
                IavaJoint joint = (IavaJoint)obj;

                // Do a field by field comparison
                return (joint.JointType.Equals(this.JointType) &&
                        joint.Position.Equals(this.Position) &&
                        joint.TrackingState.Equals(this.TrackingState));
            }
            // If parameter cannot be cast, return false.
            catch (InvalidCastException) {
                return false;
            }
        }

        /// <summary>
        /// Casts the specified Joint to an IavaJoint
        /// </summary>
        /// <param name="value">Joint to cast to an IavaJoint</param>
        /// <returns>IavaJoint representation of the Joint</returns>
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