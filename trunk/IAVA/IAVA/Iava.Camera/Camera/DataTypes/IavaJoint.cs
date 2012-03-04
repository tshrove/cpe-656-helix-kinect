using Microsoft.Research.Kinect.Nui;
using Iava.Core.Math;

namespace Iava.Input.Camera {

    public struct IavaJoint {

        #region Public Properties

        public IavaJointID ID { get; set; }

        public IavaVector Position { get; set; }

        public IavaJointTrackingState TrackingState { get; set; }

        #endregion Public Properties

        #region Operator Overloads

        public static explicit operator IavaJoint(Joint value) {
            return new IavaJoint()
            {
                ID = (IavaJointID)value.ID,
                Position = (IavaVector)value.Position,
                TrackingState = (IavaJointTrackingState)value.TrackingState
            };
        }

        #endregion Operator Overloads
    }
}