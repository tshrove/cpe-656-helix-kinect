using System;
using System.Collections.Generic;
using Microsoft.Research.Kinect.Nui;

namespace Iava.Input.Camera {

    /// <summary>
    /// The SkeletonEventArgs class
    /// </summary>
    public class IavaSkeletonEventArgs : EventArgs {

        #region Public Properties

        /// <summary>
        /// Gets the Skeleton data.
        /// </summary>
        public IavaSkeletonData Skeleton { get; private set; }

        public static readonly IavaSkeletonEventArgs Empty = null;

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IavaSkeletonEventArgs"/> class.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        public IavaSkeletonEventArgs(IavaSkeletonData skeleton) {
            this.Skeleton = skeleton;
        }

        public IavaSkeletonEventArgs(SkeletonData skeleton) {
            this.Skeleton = (IavaSkeletonData)skeleton;
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
        /// <param name="skeletonIDValues">The skeleton ID values.</param>
        /// <param name="timeStamp">The time stamp.</param>
        public IavaSkeletonFrameEventArgs(List<int> skeletonIDs, long timestamp) {
            this.SkeletonIDs = skeletonIDs;
            this.Timestamp = timestamp;
        }

        #endregion Constructors
    }

    public sealed class IavaImageFrameReadyEventArgs : EventArgs {

        #region Public Properties

        public IavaImageFrame ImageFrame { get; private set; }

        public static readonly IavaImageFrameReadyEventArgs Empty = null;

        #endregion Pulbic Properties
        
        #region Constructors

        public IavaImageFrameReadyEventArgs(IavaImageFrame imageFrame) {
            ImageFrame = imageFrame;
        }

        #endregion Constructors

        #region Operator Overloads

        public static implicit operator IavaImageFrameReadyEventArgs(ImageFrameReadyEventArgs value) {
            return new IavaImageFrameReadyEventArgs((IavaImageFrame)value.ImageFrame);
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
            return new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)value.SkeletonFrame);
        }

        #endregion Operator Overloads
    }
}
