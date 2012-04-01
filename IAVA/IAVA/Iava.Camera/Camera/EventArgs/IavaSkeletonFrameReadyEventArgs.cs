using System;
using Microsoft.Kinect;

namespace Iava.Input.Camera {

    /// <summary>
    /// The IavaSkeletonFrameReadyEventArgs class
    /// </summary>
    public sealed class IavaSkeletonFrameReadyEventArgs : EventArgs {

        #region Public Properties

        public IavaSkeletonFrame SkeletonFrame { get; private set; }

        public static readonly IavaSkeletonFrameReadyEventArgs Empty = null;

        #endregion Public Properties

        #region Constructors

        public IavaSkeletonFrameReadyEventArgs(IavaSkeletonFrame skeletonFrame) {
            SkeletonFrame = skeletonFrame;
        }

        #endregion Constructors

        #region Operator Overloads

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

        public static explicit operator IavaSkeletonFrameReadyEventArgs(SkeletonFrameReadyEventArgs value) {
            if (value == null) { return null; }

            return new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)value.OpenSkeletonFrame());
        }

        #endregion Operator Overloads
    }
}
