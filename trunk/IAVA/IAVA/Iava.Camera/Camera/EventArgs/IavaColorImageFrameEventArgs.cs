using System;
using Microsoft.Kinect;

namespace Iava.Input.Camera {

    /// <summary>
    /// The IavaColorImageFrameReadyEventArgs class
    /// </summary>
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
}
