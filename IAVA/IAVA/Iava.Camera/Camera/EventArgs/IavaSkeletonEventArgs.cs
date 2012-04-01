using System;
using Microsoft.Kinect;

namespace Iava.Input.Camera {

    /// <summary>
    /// The IavaSkeletonEventArgs class
    /// </summary>
    public class IavaSkeletonEventArgs : EventArgs {

        #region Public Properties

        /// <summary>
        /// Gets the Skeleton data.
        /// </summary>
        public IavaSkeleton Skeleton { get; private set; }

        public static readonly IavaSkeletonEventArgs Empty = null;

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IavaSkeletonEventArgs"/> class.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        public IavaSkeletonEventArgs(IavaSkeleton skeleton) {
            this.Skeleton = skeleton;
        }

        public IavaSkeletonEventArgs(Skeleton skeleton) {
            this.Skeleton = (IavaSkeleton)skeleton;
        }

        #endregion Constructors

        #region Operator Overloads

        public static bool operator ==(IavaSkeletonEventArgs eventArgs1, IavaSkeletonEventArgs eventArgs2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(eventArgs1, eventArgs2)) { return true; }

            // If just one is null, return false.
            if (((object)eventArgs1 == null) || ((object)eventArgs2 == null)) { return false; }

            // Skeleton can be null need to check that first...
            if ((eventArgs1.Skeleton == null) && (eventArgs2.Skeleton == null)) { return true; }

            // Do a field by field comparison
            return (eventArgs1.Skeleton.Equals(eventArgs2.Skeleton));
        }

        public static bool operator !=(IavaSkeletonEventArgs eventArgs1, IavaSkeletonEventArgs eventArgs2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(eventArgs1, eventArgs2)) { return false; }

            // If just one is null, return true.
            if ((eventArgs1 == null) || (eventArgs2 == null)) { return true; }

            // Skeleton can be null need to check that first...
            if ((eventArgs1.Skeleton == null) && (eventArgs2.Skeleton == null)) { return false; }

            // Do a field by field comparison
            return (!eventArgs1.Skeleton.Equals(eventArgs2.Skeleton));
        }

        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            try {
                IavaSkeletonEventArgs eventArgs = (IavaSkeletonEventArgs)obj;

                // Skeleton can be null need to check that first...
                if ((eventArgs.Skeleton == null) && (this.Skeleton == null)) { return true; }

                // Do a field by field comparison
                return (eventArgs.Skeleton.Equals(this.Skeleton));
            }
            // If parameter cannot be cast, return false.
            catch (InvalidCastException) {
                return false;
            }
        }

        #endregion Operator Overloads
    }
}
