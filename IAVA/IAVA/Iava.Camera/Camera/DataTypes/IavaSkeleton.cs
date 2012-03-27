using System;
using Iava.Core.Math;
using Microsoft.Kinect;

namespace Iava.Input.Camera {

    public sealed class IavaSkeleton {

        #region Public Properties

        public IavaJointCollection Joints { get; set; }

        public IavaSkeletonPoint Position { get; set; }

        public IavaFrameEdges ClippedEdges { get; set; }

        public int TrackingId { get; set; }

        public IavaSkeletonTrackingState TrackingState { get; set; }

        #endregion Public Properties

        #region Constructors

        public IavaSkeleton() {
            Joints = new IavaJointCollection();

            IavaJoint iavaJoint = new IavaJoint();

            // Initialize the Joint Collection
            for (IavaJointType type = 0; type < IavaJointType.Count; type++) {
                iavaJoint.JointType = type;
                Joints[type] = iavaJoint;
            }
        }

        #endregion Constructors

        #region Operator Overloads

        public static bool operator ==(IavaSkeleton skeleton1, IavaSkeleton skeleton2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(skeleton1, skeleton2)) { return true; }

            // If just one is null, return false.
            if (((object)skeleton1 == null) || ((object)skeleton2 == null)) { return false; }

            return (skeleton1.ClippedEdges.Equals(skeleton2.ClippedEdges) &&
                    skeleton1.Joints.Equals(skeleton2.Joints) &&
                    skeleton1.Position.Equals(skeleton2.Position) &&
                    skeleton1.TrackingId.Equals(skeleton2.TrackingId) &&
                    skeleton1.TrackingState.Equals(skeleton2.TrackingState));
        }

        public static bool operator !=(IavaSkeleton skeleton1, IavaSkeleton skeleton2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(skeleton1, skeleton2)) { return false; }

            // If just one is null, return true.
            if (((object)skeleton1 == null) || ((object)skeleton2 == null)) { return true; }

            return (!skeleton1.ClippedEdges.Equals(skeleton2.ClippedEdges) ||
                    !skeleton1.Joints.Equals(skeleton2.Joints) ||
                    !skeleton1.Position.Equals(skeleton2.Position) ||
                    !skeleton1.TrackingId.Equals(skeleton2.TrackingId) ||
                    !skeleton1.TrackingState.Equals(skeleton2.TrackingState));
        }

        public static explicit operator IavaSkeleton(Skeleton value) {
            if (value == null) { return null; }

            return new IavaSkeleton()
            {
                ClippedEdges = (IavaFrameEdges)value.ClippedEdges,
                Joints = (IavaJointCollection)value.Joints,
                Position = (IavaSkeletonPoint)value.Position,
                TrackingId = value.TrackingId,
                TrackingState = (IavaSkeletonTrackingState)value.TrackingState
            };
        }

        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            try {
                IavaSkeleton skeleton = (IavaSkeleton)obj;

                // Do a field by field comparison
                return (skeleton.ClippedEdges.Equals(this.ClippedEdges) &&
                        skeleton.Joints.Equals(this.Joints) &&
                        skeleton.Position.Equals(this.Position) &&
                        skeleton.TrackingId.Equals(this.TrackingId) &&
                        skeleton.TrackingState.Equals(this.TrackingState));
            }
            // If parameter cannot be cast, return false.
            catch (InvalidCastException) {
                return false;
            }
        }

        #endregion Operator Overloads
    }
}