using System;
using Iava.Core.Math;
using Microsoft.Kinect;

namespace Iava.Input.Camera {

    /// <summary>
    /// Contains the state information of a skeletal object.
    /// </summary>
    public sealed class IavaSkeleton {

        #region Public Properties

        /// <summary>
        /// Gets/Sets a description of which parts of the skeleton are clipped by the edges of the frame.
        /// </summary>
        public IavaFrameEdges ClippedEdges { get; set; }

        /// <summary>
        /// Gets/Sets a collection of joints.
        /// </summary>
        public IavaJointCollection Joints { get; set; }

        /// <summary>
        /// Gets/Sets the skeleton position.
        /// </summary>
        public IavaSkeletonPoint Position { get; set; }

        /// <summary>
        /// Gets/Sets an ID for tracking a skeleton.
        /// </summary>
        public int TrackingId { get; set; }

        /// <summary>
        /// Gets/Sets the way a skeleton is tracked.
        /// </summary>
        public IavaSkeletonTrackingState TrackingState { get; set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
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

        /// <summary>
        /// Determines whether two IavaSkeleton instances are equal.
        /// </summary>
        /// <param name="skeleton1">A IavaSkeleton to compare for equality.</param>
        /// <param name="skeleton2">A IavaSkeleton to compare for equality.</param>
        /// <returns>TRUE if the two IavaSkeleton instances are equal, else FALSE</returns>
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

        /// <summary>
        /// Determines whether two IavaSkeleton instances are not equal.
        /// </summary>
        /// <param name="skeleton1">A IavaSkeleton to compare for inequality.</param>
        /// <param name="skeleton2">A IavaSkeleton to compare for inequality.</param>
        /// <returns>TRUE if the two IavaSkeleton instances are not equal, else FALSE</returns>
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

        /// <summary>
        /// Determines whether the specified object is equal to the current IavaSkeleton. 
        /// </summary>
        /// <param name="obj">Object to compare with the current IavaSkeleton.</param>
        /// <returns>TRUE if the specified object is equal to the current IavaSkeleton, else FALSE. </returns>
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

        /// <summary>
        /// Casts the specified Skeleton to an IavaSkeleton
        /// </summary>
        /// <param name="value">Skeleton to cast to an IavaSkeleton</param>
        /// <returns>IavaSkeleton representation of the Skeleton</returns>
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

        #endregion Operator Overloads
    }
}