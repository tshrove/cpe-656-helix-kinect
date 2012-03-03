using Microsoft.Research.Kinect.Nui;

namespace Iava.Input.Camera {

    public struct IavaPlanarImage {

        #region Public Properties

        public byte[] Bits { get; set; }

        public int BytesPerPixel { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        #endregion Public Properties

        #region Operator Overloads

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