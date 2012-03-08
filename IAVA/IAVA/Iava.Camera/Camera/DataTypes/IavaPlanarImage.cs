using Microsoft.Research.Kinect.Nui;
using System;

namespace Iava.Input.Camera {

    public struct IavaPlanarImage {

        #region Public Properties

        public byte[] Bits { get; set; }

        public int BytesPerPixel { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        #endregion Public Properties

        #region Operator Overloads

        public static bool operator ==(IavaPlanarImage image1, IavaPlanarImage image2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(image1, image2)) { return true; }

            // If just one is null, return false.
            if (((object)image1 == null) || ((object)image2 == null)) { return false; }

            return (Array.Equals(image1.Bits, image2.Bits) &&
                    image1.BytesPerPixel == image2.BytesPerPixel &&
                    image1.Height == image2.Height &&
                    image1.Width == image2.Width);
        }

        public static bool operator !=(IavaPlanarImage image1, IavaPlanarImage image2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(image1, image2)) { return false; }

            // If just one is null, return true.
            if (((object)image1 == null) || ((object)image2 == null)) { return true; }

            return (!Array.Equals(image1.Bits, image2.Bits) ||
                    image1.BytesPerPixel != image2.BytesPerPixel ||
                    image1.Height != image2.Height ||
                    image1.Width != image2.Width);
        }

        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            // If parameter cannot be cast, return false.
            IavaPlanarImage image = (IavaPlanarImage)obj;
            if ((Object)image == null) { return false; }

            // Do a field by field comparison
            return (Array.Equals(image.Bits, this.Bits) &&
                    image.BytesPerPixel == this.BytesPerPixel &&
                    image.Height == this.Height &&
                    image.Width == this.Width);
        }



        public static explicit operator IavaPlanarImage(PlanarImage value) {
            return new IavaPlanarImage()
            {
                Bits = value.Bits,
                BytesPerPixel = value.BytesPerPixel,
                Height = value.Height,
                Width = value.Width
            };
        }

        #endregion Operator Overloads
    }
}