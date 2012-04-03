using System;
using System.Linq;
using Microsoft.Kinect;

namespace Iava.Input.Camera {

    /// <summary>
    /// 
    /// </summary>
    public class IavaColorImageFrame : IDisposable {

        #region Public Properties

        /// <summary>
        /// Gets the number of bytes per pixel
        /// </summary>
        public int BytesPerPixel { get; private set; }
        
        /// <summary>
        /// Gets the format for the color data
        /// </summary>
        public IavaColorImageFormat Format { get; private set; }

        /// <summary>
        /// Gets the frame number
        /// </summary>
        public int FrameNumber { get; private set; }

        /// <summary>
        /// Gets the height of the IavaColorImage in the current frame
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the collection of bytes making up the current frame
        /// </summary>
        public byte[] PixelData { get; private set; }

        /// <summary>
        /// Gets the frame's timestamp
        /// </summary>
        public long Timestamp { get; private set; }

        /// <summary>
        /// Gets the width of the IavaColorImage in the current frame
        /// </summary>
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

        /// <summary>
        /// Determines whether two IavaColorImageFrame instances are equal.
        /// </summary>
        /// <param name="imageFrame1">A IavaColorImageFrame to compare for equality.</param>
        /// <param name="imageFrame2">A IavaColorImageFrame to compare for equality.</param>
        /// <returns>TRUE if the two IavaColorImageFrame instances are equal, else FALSE</returns>
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

        /// <summary>
        /// Determines whether two IavaColorImageFrame instances are not equal.
        /// </summary>
        /// <param name="imageFrame1">A IavaColorImageFrame to compare for inequality.</param>
        /// <param name="imageFrame2">A IavaColorImageFrame to compare for inequality.</param>
        /// <returns>TRUE if the two IavaColorImageFrame instances are not equal, else FALSE</returns>
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

        /// <summary>
        /// Determines whether the specified object is equal to the current IavaColorImageFrame. 
        /// </summary>
        /// <param name="obj">Object to compare with the current IavaColorImageFrame.</param>
        /// <returns>TRUE if the specified object is equal to the current IavaColorImageFrame, else FALSE. </returns>
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

        /// <summary>
        /// Casts the specified ColorImageFrame to an IavaColorImageFrame
        /// </summary>
        /// <param name="value">ColorImageFrame to cast to an IavaColorImageFrame</param>
        /// <returns>IavaColorImageFrame representation of the ColorImageFrame</returns>
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

        #endregion IDisposable Members
    }
}