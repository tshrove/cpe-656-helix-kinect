using System;
using System.Collections.Generic;
using System.Linq;
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

        public static bool operator ==(IavaColorImageFrameReadyEventArgs eventArgs1, IavaColorImageFrameReadyEventArgs eventArgs2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(eventArgs1, eventArgs2)) { return true; }

            // If just one is null, return false.
            if (((object)eventArgs1 == null) || ((object)eventArgs2 == null)) { return false; }

            // ImageFrame can be null need to check that first...
            if ((eventArgs1.ImageFrame == null) && (eventArgs2.ImageFrame == null)) { return true; }

            // Do a field by field comparison
            return (eventArgs1.ImageFrame.Equals(eventArgs2.ImageFrame));
        }

        public static bool operator !=(IavaColorImageFrameReadyEventArgs eventArgs1, IavaColorImageFrameReadyEventArgs eventArgs2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(eventArgs1, eventArgs2)) { return false; }

            // If just one is null, return true.
            if ((eventArgs1 == null) || (eventArgs2 == null)) { return true; }

            // ImageFrame can be null need to check that first...
            if ((eventArgs1.ImageFrame == null) && (eventArgs2.ImageFrame == null)) { return false; }

            // Do a field by field comparison
            return (!eventArgs1.ImageFrame.Equals(eventArgs2.ImageFrame));
        }

        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            try {
                IavaColorImageFrameReadyEventArgs eventArgs = (IavaColorImageFrameReadyEventArgs)obj;

                // ImageFrame can be null need to check that first...
                if ((eventArgs.ImageFrame == null) && (this.ImageFrame == null)) { return true; }

                // Do a field by field comparison
                return (eventArgs.ImageFrame.Equals(this.ImageFrame));
            }
            // If parameter cannot be cast, return false.
            catch (InvalidCastException) {
                return false;
            }
        }

        public static explicit operator IavaColorImageFrameReadyEventArgs(ColorImageFrameReadyEventArgs value) {
            if (value == null) { return null; }

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
