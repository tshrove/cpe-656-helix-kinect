using System;
using System.Collections.Generic;
using System.Linq;

namespace Iava.Input.Camera {

    /// <summary>
    /// The IavaSkeletonFrameEventArgs class
    /// </summary>
    public class IavaSkeletonFrameEventArgs : EventArgs {

        #region Public Properties

        /// <summary>
        /// Gets the skeleton IDs.
        /// </summary>
        public List<int> SkeletonIDs { get; private set; }

        /// <summary>
        /// Gets the event's timestamp
        /// </summary>
        public long Timestamp { get; private set; }

        public static readonly IavaSkeletonFrameEventArgs Empty = null;

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IavaSkeletonFrameEventArgs"/> class.
        /// </summary>
        /// <param name="skeletonIDValues">The skeleton JointType values.</param>
        /// <param name="timeStamp">The time stamp.</param>
        public IavaSkeletonFrameEventArgs(List<int> skeletonIDs, long timestamp) {
            this.SkeletonIDs = skeletonIDs;
            this.Timestamp = timestamp;
        }

        #endregion Constructors

        #region Operator Overloads

        public static bool operator ==(IavaSkeletonFrameEventArgs eventArgs1, IavaSkeletonFrameEventArgs eventArgs2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(eventArgs1, eventArgs2)) { return true; }

            // If just one is null, return false.
            if (((object)eventArgs1 == null) || ((object)eventArgs2 == null)) { return false; }

            // Do a field by field comparison
            return (eventArgs1.SkeletonIDs.SequenceEqual((eventArgs2.SkeletonIDs)) &&
                    eventArgs1.Timestamp.Equals(eventArgs2.Timestamp));
        }

        public static bool operator !=(IavaSkeletonFrameEventArgs eventArgs1, IavaSkeletonFrameEventArgs eventArgs2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(eventArgs1, eventArgs2)) { return false; }

            // If just one is null, return true.
            if ((eventArgs1 == null) || (eventArgs2 == null)) { return true; }

            // Do a field by field comparison
            return (!eventArgs1.SkeletonIDs.SequenceEqual((eventArgs2.SkeletonIDs)) ||
                    !eventArgs1.Timestamp.Equals(eventArgs2.Timestamp));
        }

        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            try {
                IavaSkeletonFrameEventArgs eventArgs = (IavaSkeletonFrameEventArgs)obj;

                // Do a field by field comparison
                return (eventArgs.SkeletonIDs.SequenceEqual((this.SkeletonIDs)) &&
                        eventArgs.Timestamp.Equals(this.Timestamp));
            }
            // If parameter cannot be cast, return false.
            catch (InvalidCastException) {
                return false;
            }
        }

        #endregion Operator Overloads
    }
}
