using Microsoft.Research.Kinect.Nui;
using Iava.Core.Math;

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

        #endregion Operator Overloads
    }
}
