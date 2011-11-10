using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iava.Gesture.GestureStuff;
using Microsoft.Research.Kinect.Nui;

namespace Iava.Gesture
{
    // We can change this class after the prototype
    public class GestureEventArgs : EventArgs
    {
        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public GestureEventArgs(GestureType type, int trackingID, int userID)
        {
            // Take a look at:
            // System.Windows.Forms.MouseEventArgs;
            // System.Windows.Input.MouseEventArgs;
            // System.Windows.Input.MouseGesture;
            // System.Windows.Input.MouseWheelEventArgs;
            // System.Windows.Input.TouchEventArgs;

            this.GestureType = type;
            this.TrackingID  = trackingID;
            this.UserID      = userID;
        }
        #endregion

        #region Public Properties

        public GestureType GestureType { get; set; }

        public int TrackingID { get; set; }

        public int UserID { get; set; }

        #endregion Public Properties
    }

    /// <summary>
    /// The skeleton frame event args
    /// </summary>
    public class SkeletonFrameEventArgs : EventArgs {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SkeletonFrameEventArgs"/> class.
        /// </summary>
        /// <param name="skeletonIDValues">The skeleton ID values.</param>
        /// <param name="timeStamp">The time stamp.</param>
        public SkeletonFrameEventArgs(List<int> skeletonIDValues, long timeStamp) {
            this.SkeletonIDValues = skeletonIDValues;
            this.TimeStamp = timeStamp;
        }

        #endregion Constructor

        #region Public Properties

        /// <summary>
        /// Gets or sets the skeleton ID values.
        /// </summary>
        /// <value>
        /// The skeleton ID values.
        /// </value>
        public List<int> SkeletonIDValues { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        public long TimeStamp { get; set; }

        #endregion Public Properties
    }

    /// <summary>
    /// The skeleton event args
    /// </summary>
    public class SkeletonEventArgs : EventArgs {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SkeletonEventArgs"/> class.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        public SkeletonEventArgs(SkeletonData skeleton) {
            this.Skeleton = skeleton;
        }

        #endregion Constructor

        #region Public Properties

        /// <summary>
        /// Gets or sets the skeleton.
        /// </summary>
        /// <value>
        /// The skeleton.
        /// </value>
        public SkeletonData Skeleton { get; set; }

        #endregion Public Properties
    }
}
