using Microsoft.Research.Kinect.Nui;

namespace Iava.Input.Camera {

    public struct IavaImageViewArea {

        #region Public Properties

        public int CenterX { get; set; }

        public int CenterY { get; set; }

        public IavaImageDigitalZoom Zoom { get; set; }

        #endregion Public Properties

        #region Operator Overloads

        public static explicit operator IavaImageViewArea(ImageViewArea value) {
            return new IavaImageViewArea()
            {
                CenterX = value.CenterX,
                CenterY = value.CenterY,
                Zoom = (IavaImageDigitalZoom)value.Zoom
            };
        }

        #endregion Operator Overloads
    }
}