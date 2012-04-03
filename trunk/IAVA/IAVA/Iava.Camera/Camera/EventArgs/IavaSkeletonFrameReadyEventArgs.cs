using System;
using Microsoft.Kinect;

namespace Iava.Input.Camera {

    /// <summary>
    /// The IavaSkeletonFrameReadyEventArgs class
    /// </summary>
    public sealed class IavaSkeletonFrameReadyEventArgs : EventArgs {

        #region Public Properties

        /// <summary>
        /// Gets the IavaSkeletonFrame
        /// </summary>
        public IavaSkeletonFrame SkeletonFrame { get; private set; }

        /// <summary>
        /// Represents an empty IavaSkeletonFrameReadyEventArgs
        /// </summary>
        public static readonly IavaSkeletonFrameReadyEventArgs Empty = null;

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Creates an IavaSkeletonFrameReadyEventArgs containing the supplied IavaSkeletonFrame
        /// </summary>
        /// <param name="imageFrame">IavaSkeletonFrame to be contained in the IavaSkeletonFrameReadyEventArgs</param>
        public IavaSkeletonFrameReadyEventArgs(IavaSkeletonFrame skeletonFrame) {
            SkeletonFrame = skeletonFrame;
        }

        #endregion Constructors

        #region Operator Overloads

        /// <summary>
        /// Determines whether two IavaSkeletonFrameReadyEventArgs instances are equal.
        /// </summary>
        /// <param name="eventArgs1">A IavaSkeletonFrameReadyEventArgs to compare for equality.</param>
        /// <param name="eventArgs2">A IavaSkeletonFrameReadyEventArgs to compare for equality.</param>
        /// <returns>TRUE if the two IavaSkeletonFrameReadyEventArgs instances are equal, else FALSE</returns>
        public static bool operator ==(IavaSkeletonFrameReadyEventArgs eventArgs1, IavaSkeletonFrameReadyEventArgs eventArgs2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(eventArgs1, eventArgs2)) { return true; }

            // If just one is null, return false.
            if (((object)eventArgs1 == null) || ((object)eventArgs2 == null)) { return false; }

            // ImageFrame can be null need to check that first...
            if ((eventArgs1.SkeletonFrame == null) && (eventArgs2.SkeletonFrame == null)) { return true; }

            // Do a field by field comparison
            return (eventArgs1.SkeletonFrame.Equals(eventArgs2.SkeletonFrame));
        }

        /// <summary>
        /// Determines whether two IavaSkeletonFrameReadyEventArgs instances are not equal.
        /// </summary>
        /// <param name="eventArgs1">A IavaSkeletonFrameReadyEventArgs to compare for inequality.</param>
        /// <param name="eventArgs2">A IavaSkeletonFrameReadyEventArgs to compare for inequality.</param>
        /// <returns>TRUE if the two IavaSkeletonFrameReadyEventArgs instances are not equal, else FALSE</returns>
        public static bool operator !=(IavaSkeletonFrameReadyEventArgs eventArgs1, IavaSkeletonFrameReadyEventArgs eventArgs2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(eventArgs1, eventArgs2)) { return false; }

            // If just one is null, return true.
            if ((eventArgs1 == null) || (eventArgs2 == null)) { return true; }

            // ImageFrame can be null need to check that first...
            if ((eventArgs1.SkeletonFrame == null) && (eventArgs2.SkeletonFrame == null)) { return false; }

            // Do a field by field comparison
            return (!eventArgs1.SkeletonFrame.Equals(eventArgs2.SkeletonFrame));
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current IavaSkeletonFrameReadyEventArgs. 
        /// </summary>
        /// <param name="obj">Object to compare with the current IavaSkeletonFrameReadyEventArgs.</param>
        /// <returns>TRUE if the specified object is equal to the current IavaSkeletonFrameReadyEventArgs, else FALSE. </returns>
        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            try {
                IavaSkeletonFrameReadyEventArgs eventArgs = (IavaSkeletonFrameReadyEventArgs)obj;

                // ImageFrame can be null need to check that first...
                if ((eventArgs.SkeletonFrame == null) && (this.SkeletonFrame == null)) { return true; }

                // Do a field by field comparison
                return (eventArgs.SkeletonFrame.Equals(this.SkeletonFrame));
            }
            // If parameter cannot be cast, return false.
            catch (InvalidCastException) {
                return false;
            }
        }

        /// <summary>
        /// Casts the specified SkeletonFrameReadyEventArgs to an IavaSkeletonFrameReadyEventArgs
        /// </summary>
        /// <param name="value">SkeletonFrameReadyEventArgs to cast to an IavaSkeletonFrameReadyEventArgs</param>
        /// <returns>IavaSkeletonFrameReadyEventArgs representation of the SkeletonFrameReadyEventArgs</returns>
        public static explicit operator IavaSkeletonFrameReadyEventArgs(SkeletonFrameReadyEventArgs value) {
            if (value == null) { return null; }

            return new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)value.OpenSkeletonFrame());
        }

        #endregion Operator Overloads
    }
}
