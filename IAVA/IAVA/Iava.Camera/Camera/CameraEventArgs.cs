using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.Kinect.Nui;

namespace Iava.Input.Camera {

    /// <summary>
    /// The SkeletonEventArgs class
    /// </summary>
    public class SkeletonEventArgs : EventArgs {

        #region Public Properties

        /// <summary>
        /// Gets the Skeleton data.
        /// </summary>
        public SkeletonData Skeleton { get; private set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SkeletonEventArgs"/> class.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        public SkeletonEventArgs(SkeletonData skeleton) {
            this.Skeleton = skeleton;
        }

        #endregion Constructors
    }

    /// <summary>
    /// The SkeletonFrameEventArgs class
    /// </summary>
    public class SkeletonFrameEventArgs : EventArgs {

        #region Public Properties

        /// <summary>
        /// Gets the skeleton IDs.
        /// </summary>
        public List<int> SkeletonIDs { get; private set; }

        /// <summary>
        /// Gets the event's timestamp
        /// </summary>
        public long Timestamp { get; private set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SkeletonFrameEventArgs"/> class.
        /// </summary>
        /// <param name="skeletonIDValues">The skeleton ID values.</param>
        /// <param name="timeStamp">The time stamp.</param>
        public SkeletonFrameEventArgs(List<int> skeletonIDs, long timestamp) {
            this.SkeletonIDs = skeletonIDs;
            this.Timestamp = timestamp;
        }

        #endregion Constructors
    }

    public sealed class ImageFrameReadyEventArgs : EventArgs {

        #region Public Properties

        public ImageFrame ImageFrame { get; private set; }

        #endregion Pulbic Properties
        
        #region Constructors

        public ImageFrameReadyEventArgs(ImageFrame imageFrame) {
            ImageFrame = imageFrame;
        }

        #endregion Constructors

        #region Operator Overloads

        public static implicit operator ImageFrameReadyEventArgs(Microsoft.Research.Kinect.Nui.ImageFrameReadyEventArgs value) {
            return new ImageFrameReadyEventArgs(value.ImageFrame);
        }

        #endregion Operator Overloads
    }
}
