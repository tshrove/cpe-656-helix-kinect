using Microsoft.Research.Kinect.Nui;
using System;

namespace Iava.Input.Camera {

    public class IavaImageFrame {

        #region Public Properties

        public int FrameNumber { get; set; }

        public IavaPlanarImage Image { get; set; }

        public IavaImageResolution Resolution { get; set; }

        public long Timestamp { get; set; }

        public IavaImageType Type { get; set; }

        public IavaImageViewArea ViewArea { get; set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public IavaImageFrame() {
        }

        #endregion Constructors

        #region Operator Overloads

        public static bool operator ==(IavaImageFrame imageFrame1, IavaImageFrame imageFrame2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(imageFrame1, imageFrame2)) { return true; }

            // If just one is null, return false.
            if (((object)imageFrame1 == null) || ((object)imageFrame2 == null)) { return false; }

            // Do a field by field comparison
            return (imageFrame1.FrameNumber == imageFrame2.FrameNumber &&
                    imageFrame1.Image == imageFrame2.Image &&
                    imageFrame1.Resolution == imageFrame2.Resolution &&
                    imageFrame1.Timestamp == imageFrame2.Timestamp &&
                    imageFrame1.Type == imageFrame2.Type &&
                    imageFrame1.ViewArea == imageFrame2.ViewArea);
        }

        public static bool operator !=(IavaImageFrame imageFrame1, IavaImageFrame imageFrame2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(imageFrame1, imageFrame2)) { return false; }

            // If just one is null, return true.
            if (((object)imageFrame1 == null) || ((object)imageFrame2 == null)) { return true; }

            // Do a field by field comparison
            return (imageFrame1.FrameNumber != imageFrame2.FrameNumber ||
                    imageFrame1.Image != imageFrame2.Image ||
                    imageFrame1.Resolution != imageFrame2.Resolution ||
                    imageFrame1.Timestamp != imageFrame2.Timestamp ||
                    imageFrame1.Type != imageFrame2.Type ||
                    imageFrame1.ViewArea != imageFrame2.ViewArea);
        }

        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            // If parameter cannot be cast, return false.
            IavaImageFrame imageFrame = (IavaImageFrame)obj;
            if ((Object)imageFrame == null) { return false; }

            // Do a field by field comparison
            return (imageFrame.FrameNumber == this.FrameNumber &&
                    imageFrame.Image == this.Image &&
                    imageFrame.Resolution == this.Resolution &&
                    imageFrame.Timestamp == this.Timestamp &&
                    imageFrame.Type == this.Type &&
                    imageFrame.ViewArea == this.ViewArea);
        }

        public static explicit operator IavaImageFrame(ImageFrame value) {
            return new IavaImageFrame()
            {
                FrameNumber = value.FrameNumber,
                Image = (IavaPlanarImage)value.Image,
                Resolution = (IavaImageResolution)value.Resolution,
                Timestamp = value.Timestamp,
                Type = (IavaImageType)value.Type,
                ViewArea = (IavaImageViewArea)value.ViewArea
            };
        }

        #endregion Operator Overloads
    }
}