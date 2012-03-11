using System;
using System.Collections.Generic;
using Microsoft.Kinect;

namespace Iava.Input.Camera {

    /// <summary>
    /// The SkeletonEventArgs class
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
    }

    /// <summary>
    /// The SkeletonFrameEventArgs class
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
    }

    public sealed class IavaColorImageFrameReadyEventArgs : EventArgs {

        #region Public Properties

        public IavaColorImageFrame ImageFrame { get; private set; }

        public static readonly IavaColorImageFrameReadyEventArgs Empty = null;

        #endregion Pulbic Properties
        
        #region Constructors

        public IavaColorImageFrameReadyEventArgs(IavaColorImageFrame imageFrame) {
            ImageFrame = imageFrame;
        }

        #endregion Constructors

        #region Operator Overloads

        public static implicit operator IavaColorImageFrameReadyEventArgs(ColorImageFrameReadyEventArgs value) {
            return new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)value.OpenColorImageFrame());
        }

        #endregion Operator Overloads
    }

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

        public static implicit operator IavaSkeletonFrameReadyEventArgs(SkeletonFrameReadyEventArgs value) {
            return new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)value.OpenSkeletonFrame());
        }

        #endregion Operator Overloads
    }
}
