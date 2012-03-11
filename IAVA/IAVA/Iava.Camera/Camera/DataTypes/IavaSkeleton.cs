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

        #region Operator Overloads

        public static bool operator ==(IavaSkeleton skeleton1, IavaSkeleton skeleton2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(skeleton1, skeleton2)) { return true; }

            // If just one is null, return false.
            if (((object)skeleton1 == null) || ((object)skeleton2 == null)) { return false; }

            return (skeleton1.ClippedEdges == skeleton2.ClippedEdges &&
                    skeleton1.Joints == skeleton2.Joints &&
                    skeleton1.Position == skeleton2.Position &&
                    skeleton1.TrackingId == skeleton2.TrackingId &&
                    skeleton1.TrackingState == skeleton2.TrackingState);
        }

        public static bool operator !=(IavaSkeleton skeleton1, IavaSkeleton skeleton2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(skeleton1, skeleton2)) { return false; }

            // If just one is null, return true.
            if (((object)skeleton1 == null) || ((object)skeleton2 == null)) { return true; }

            return (skeleton1.ClippedEdges != skeleton2.ClippedEdges ||
                    skeleton1.Joints != skeleton2.Joints ||
                    skeleton1.Position != skeleton2.Position ||
                    skeleton1.TrackingId != skeleton2.TrackingId ||
                    skeleton1.TrackingState != skeleton2.TrackingState);
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

            // If parameter cannot be cast, return false.
            IavaSkeleton skeleton = (IavaSkeleton)obj;
            if ((Object)skeleton == null) { return false; }

            // Do a field by field comparison
            return (skeleton.ClippedEdges == this.ClippedEdges && 
                    skeleton.Joints == this.Joints &&
                    skeleton.Position == this.Position &&
                    skeleton.TrackingId == this.TrackingId &&
                    skeleton.TrackingState == this.TrackingState);
        }

        #endregion Operator Overloads
    }
}
