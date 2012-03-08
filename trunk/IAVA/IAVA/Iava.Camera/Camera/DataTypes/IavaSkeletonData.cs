using Iava.Core.Math;
using Microsoft.Research.Kinect.Nui;
using Iava.Core.Math;
using System;

namespace Iava.Input.Camera {

    public sealed class IavaSkeletonData {

        #region Public Properties

        public IavaJointsCollection Joints { get; set; }

        public IavaVector Position { get; set; }

        public IavaSkeletonQuality Quality { get; set; }

        public int TrackingID { get; set; }

        public IavaSkeletonTrackingState TrackingState { get; set; }

        public int UserIndex { get; set; }

        #endregion Public Properties

        #region Operator Overloads

        public static bool operator ==(IavaSkeletonData skeleton1, IavaSkeletonData skeleton2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(skeleton1, skeleton2)) { return true; }

            // If just one is null, return false.
            if (((object)skeleton1 == null) || ((object)skeleton2 == null)) { return false; }

            return (skeleton1.Joints == skeleton2.Joints &&
                    skeleton1.Position == skeleton2.Position &&
                    skeleton1.Quality == skeleton2.Quality &&
                    skeleton1.TrackingID == skeleton2.TrackingID &&
                    skeleton1.TrackingState == skeleton2.TrackingState &&
                    skeleton1.UserIndex == skeleton2.UserIndex);
        }

        public static bool operator !=(IavaSkeletonData skeleton1, IavaSkeletonData skeleton2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(skeleton1, skeleton2)) { return false; }

            // If just one is null, return true.
            if (((object)skeleton1 == null) || ((object)skeleton2 == null)) { return true; }

            return (skeleton1.Joints != skeleton2.Joints ||
                    skeleton1.Position != skeleton2.Position ||
                    skeleton1.Quality != skeleton2.Quality ||
                    skeleton1.TrackingID != skeleton2.TrackingID ||
                    skeleton1.TrackingState != skeleton2.TrackingState ||
                    skeleton1.UserIndex != skeleton2.UserIndex);
        }

        public static explicit operator IavaSkeletonData(SkeletonData value) {
            return new IavaSkeletonData()
            {
                Joints = (IavaJointsCollection)value.Joints,
                Position = (IavaVector)value.Position,
                Quality = (IavaSkeletonQuality)value.Quality,
                TrackingID = value.TrackingID,
                TrackingState = (IavaSkeletonTrackingState)value.TrackingState,
                UserIndex = value.UserIndex
            };
        }

        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            // If parameter cannot be cast, return false.
            IavaSkeletonData skeleton = (IavaSkeletonData)obj;
            if ((Object)skeleton == null) { return false; }

            // Do a field by field comparison
            return (skeleton.Joints == this.Joints &&
                    skeleton.Position == this.Position &&
                    skeleton.Quality == this.Quality &&
                    skeleton.TrackingID == this.TrackingID &&
                    skeleton.TrackingState == this.TrackingState &&
                    skeleton.UserIndex == this.UserIndex);
        }

        #endregion Operator Overloads
    }
}
