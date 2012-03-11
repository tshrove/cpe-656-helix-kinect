using Microsoft.Kinect;
using System;

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
        /*
        public static bool operator ==(IavaColorImageFrame imageFrame1, IavaColorImageFrame imageFrame2) {
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

        public static bool operator !=(IavaColorImageFrame imageFrame1, IavaColorImageFrame imageFrame2) {
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
            IavaColorImageFrame imageFrame = (IavaColorImageFrame)obj;
            if ((Object)imageFrame == null) { return false; }

            // Do a field by field comparison
            return (imageFrame.FrameNumber == this.FrameNumber &&
                    imageFrame.Image == this.Image &&
                    imageFrame.Resolution == this.Resolution &&
                    imageFrame.Timestamp == this.Timestamp &&
                    imageFrame.Type == this.Type &&
                    imageFrame.ViewArea == this.ViewArea);
        }
        */
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
            value.CopyPixelDataTo(colorFrame.PixelData);

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