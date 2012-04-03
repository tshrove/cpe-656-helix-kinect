using System;
using Microsoft.Kinect;

namespace Iava.Input.Camera {

    /// <summary>
    /// The IavaColorImageFrameReadyEventArgs class
    /// </summary>
    public sealed class IavaColorImageFrameReadyEventArgs : EventArgs {

        #region Public Properties

        /// <summary>
        /// Gets the IavaColorImageFrame
        /// </summary>
        public IavaColorImageFrame ImageFrame { get; private set; }

        /// <summary>
        /// Represents an empty IavaColorImageFrameReadyEventArgs
        /// </summary>
        public static readonly IavaColorImageFrameReadyEventArgs Empty = null;

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Creates an IavaColorImageFrameReadyEventArgs containing the supplied IavaColorImageFrame
        /// </summary>
        /// <param name="imageFrame">IavaColorImageFrame to be contained in the IavaColorImageFrameReadyEventArgs</param>
        public IavaColorImageFrameReadyEventArgs(IavaColorImageFrame imageFrame) {
            ImageFrame = imageFrame;
        }

        #endregion Constructors

        #region Operator Overloads

        /// <summary>
        /// Determines whether two IavaColorImageFrameReadyEventArgs instances are equal.
        /// </summary>
        /// <param name="eventArgs1">A IavaColorImageFrameReadyEventArgs to compare for equality.</param>
        /// <param name="eventArgs2">A IavaColorImageFrameReadyEventArgs to compare for equality.</param>
        /// <returns>TRUE if the two IavaColorImageFrameReadyEventArgs instances are equal, else FALSE</returns>
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

        /// <summary>
        /// Determines whether two IavaColorImageFrameReadyEventArgs instances are not equal.
        /// </summary>
        /// <param name="eventArgs1">A IavaColorImageFrameReadyEventArgs to compare for inequality.</param>
        /// <param name="eventArgs2">A IavaColorImageFrameReadyEventArgs to compare for inequality.</param>
        /// <returns>TRUE if the two IavaColorImageFrameReadyEventArgs instances are not equal, else FALSE</returns>
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

        /// <summary>
        /// Determines whether the specified object is equal to the current IavaColorImageFrameReadyEventArgs. 
        /// </summary>
        /// <param name="obj">Object to compare with the current IavaColorImageFrameReadyEventArgs.</param>
        /// <returns>TRUE if the specified object is equal to the current IavaColorImageFrameReadyEventArgs, else FALSE. </returns>
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

        /// <summary>
        /// Casts the specified ColorImageFrameReadyEventArgs to an IavaColorImageFrameReadyEventArgs
        /// </summary>
        /// <param name="value">ColorImageFrameReadyEventArgs to cast to an IavaColorImageFrameReadyEventArgs</param>
        /// <returns>IavaColorImageFrameReadyEventArgs representation of the ColorImageFrameReadyEventArgs</returns>
        public static explicit operator IavaColorImageFrameReadyEventArgs(ColorImageFrameReadyEventArgs value) {
            if (value == null) { return null; }

            return new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)value.OpenColorImageFrame());
        }

        #endregion Operator Overloads
    }
}
