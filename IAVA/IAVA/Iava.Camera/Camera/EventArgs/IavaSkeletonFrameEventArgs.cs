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
        /// Gets the timestamp
        /// </summary>
        public long Timestamp { get; private set; }

        /// <summary>
        /// Represents an empty IavaSkeletonFrameEventArgs
        /// </summary>
        public static readonly IavaSkeletonFrameEventArgs Empty = null;

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Creates an IavaColorImageFrameReadyEventArgs containing the supplied skeleton ID values and timestamp.
        /// </summary>
        /// <param name="skeletonIDs">The skeleton JointType values.</param>
        /// <param name="timestamp">The timestamp.</param>
        public IavaSkeletonFrameEventArgs(List<int> skeletonIDs, long timestamp) {
            this.SkeletonIDs = skeletonIDs;
            this.Timestamp = timestamp;
        }

        #endregion Constructors

        #region Operator Overloads

        /// <summary>
        /// Determines whether two IavaSkeletonFrameEventArgs instances are equal.
        /// </summary>
        /// <param name="eventArgs1">A IavaSkeletonFrameEventArgs to compare for equality.</param>
        /// <param name="eventArgs2">A IavaSkeletonFrameEventArgs to compare for equality.</param>
        /// <returns>TRUE if the two IavaSkeletonFrameEventArgs instances are equal, else FALSE</returns>
        public static bool operator ==(IavaSkeletonFrameEventArgs eventArgs1, IavaSkeletonFrameEventArgs eventArgs2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(eventArgs1, eventArgs2)) { return true; }

            // If just one is null, return false.
            if (((object)eventArgs1 == null) || ((object)eventArgs2 == null)) { return false; }

            // Do a field by field comparison
            return (eventArgs1.SkeletonIDs.SequenceEqual((eventArgs2.SkeletonIDs)) &&
                    eventArgs1.Timestamp.Equals(eventArgs2.Timestamp));
        }

        /// <summary>
        /// Determines whether two IavaSkeletonFrameEventArgs instances are not equal.
        /// </summary>
        /// <param name="eventArgs1">A IavaSkeletonFrameEventArgs to compare for inequality.</param>
        /// <param name="eventArgs2">A IavaSkeletonFrameEventArgs to compare for inequality.</param>
        /// <returns>TRUE if the two IavaSkeletonFrameEventArgs instances are not equal, else FALSE</returns>
        public static bool operator !=(IavaSkeletonFrameEventArgs eventArgs1, IavaSkeletonFrameEventArgs eventArgs2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(eventArgs1, eventArgs2)) { return false; }

            // If just one is null, return true.
            if ((eventArgs1 == null) || (eventArgs2 == null)) { return true; }

            // Do a field by field comparison
            return (!eventArgs1.SkeletonIDs.SequenceEqual((eventArgs2.SkeletonIDs)) ||
                    !eventArgs1.Timestamp.Equals(eventArgs2.Timestamp));
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current IavaSkeletonFrameEventArgs. 
        /// </summary>
        /// <param name="obj">Object to compare with the current IavaSkeletonFrameEventArgs.</param>
        /// <returns>TRUE if the specified object is equal to the current IavaSkeletonFrameEventArgs, else FALSE. </returns>
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
