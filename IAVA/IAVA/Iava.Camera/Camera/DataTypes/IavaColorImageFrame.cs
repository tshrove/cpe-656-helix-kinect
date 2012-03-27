using Microsoft.Kinect;
using System;
using System.Linq;

namespace Iava.Input.Camera {

    public class IavaColorImageFrame : IDisposable {

        #region Public Properties

        public int BytesPerPixel { get; private set; }
        
        public IavaColorImageFormat Format { get; private set; }

        public int FrameNumber { get; private set; }

        public int Height { get; private set; }

        public byte[] PixelData { get; private set; }

        public long Timestamp { get; private set; }

        public int Width { get; private set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public IavaColorImageFrame() {
        }

        #endregion Constructors

        #region Operator Overloads
        
        public static bool operator ==(IavaColorImageFrame imageFrame1, IavaColorImageFrame imageFrame2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(imageFrame1, imageFrame2)) { return true; }

            // If just one is null, return false.
            if (((object)imageFrame1 == null) || ((object)imageFrame2 == null)) { return false; }

            // Do a field by field comparison
            return (imageFrame1.BytesPerPixel.Equals(imageFrame2.BytesPerPixel) &&
                    imageFrame1.Format.Equals(imageFrame2.Format) &&
                    imageFrame1.FrameNumber.Equals(imageFrame2.FrameNumber) &&
                    imageFrame1.Height.Equals(imageFrame2.Height) &&
                    imageFrame1.PixelData.SequenceEqual(imageFrame2.PixelData) &&
                    imageFrame1.Timestamp.Equals(imageFrame2.Timestamp) &&
                    imageFrame1.Width.Equals(imageFrame2.Width));
        }

        public static bool operator !=(IavaColorImageFrame imageFrame1, IavaColorImageFrame imageFrame2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(imageFrame1, imageFrame2)) { return false; }

            // If just one is null, return true.
            if (((object)imageFrame1 == null) || ((object)imageFrame2 == null)) { return true; }
        
            // Do a field by field comparison
            return (!imageFrame1.BytesPerPixel.Equals(imageFrame2.BytesPerPixel) ||
                    !imageFrame1.Format.Equals(imageFrame2.Format) ||
                    !imageFrame1.FrameNumber.Equals(imageFrame2.FrameNumber) ||
                    !imageFrame1.Height.Equals(imageFrame2.Height) ||
                    !imageFrame1.PixelData.SequenceEqual(imageFrame2.PixelData) ||
                    !imageFrame1.Timestamp.Equals(imageFrame2.Timestamp) ||
                    !imageFrame1.Width.Equals(imageFrame2.Width));
        }

        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            try {
                IavaColorImageFrame imageFrame = (IavaColorImageFrame)obj;

                // Do a field by field comparison
                return (imageFrame.BytesPerPixel.Equals(this.BytesPerPixel) &&
                        imageFrame.Format.Equals(this.Format) &&
                        imageFrame.FrameNumber.Equals(this.FrameNumber) &&
                        imageFrame.Height.Equals(this.Height) &&
                        imageFrame.PixelData.SequenceEqual(this.PixelData) &&
                        imageFrame.Timestamp.Equals(this.Timestamp) &&
                        imageFrame.Width.Equals(this.Width));
            }
            // If parameter cannot be cast, return false.
            catch (InvalidCastException) {
                return false;
            }
        }
        
        public static explicit operator IavaColorImageFrame(ColorImageFrame value) {
            if (value == null) { return null; }

            IavaColorImageFrame colorFrame = new IavaColorImageFrame()
            {
                BytesPerPixel = value.BytesPerPixel,
                Format = (IavaColorImageFormat)value.Format,
                FrameNumber = value.FrameNumber,
                Height = value.Height,
                PixelData = new byte[value.PixelDataLength],
                Timestamp = value.Timestamp,
                Width = value.Width
            };

            // Copy the PixelData
            if (value.PixelDataLength > 0) {
                value.CopyPixelDataTo(colorFrame.PixelData);
            }

            return colorFrame;
        }
        
        #endregion Operator Overloads

        #region IDisposable Members

        public void Dispose() {
            throw new NotImplementedException();
        }

        #endregion
    }
}