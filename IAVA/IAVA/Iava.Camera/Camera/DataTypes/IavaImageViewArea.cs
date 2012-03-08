using Microsoft.Research.Kinect.Nui;
using System;

namespace Iava.Input.Camera {

    public struct IavaImageViewArea {

        #region Public Properties

        public int CenterX { get; set; }

        public int CenterY { get; set; }

        public IavaImageDigitalZoom Zoom { get; set; }

        #endregion Public Properties

        #region Operator Overloads

        public static bool operator ==(IavaImageViewArea viewArea1, IavaImageViewArea viewArea2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(viewArea1, viewArea2)) { return true; }

            // If just one is null, return false.
            if (((object)viewArea1 == null) || ((object)viewArea2 == null)) { return false; }

            return (viewArea1.CenterX == viewArea2.CenterX &&
                    viewArea1.CenterY == viewArea2.CenterY &&
                    viewArea1.Zoom == viewArea2.Zoom);
        }

        public static bool operator !=(IavaImageViewArea viewArea1, IavaImageViewArea viewArea2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(viewArea1, viewArea2)) { return false; }

            // If just one is null, return true.
            if (((object)viewArea1 == null) || ((object)viewArea2 == null)) { return true; }

            return (viewArea1.CenterX != viewArea2.CenterX ||
                    viewArea1.CenterY != viewArea2.CenterY ||
                    viewArea1.Zoom != viewArea2.Zoom);
        }

        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            // If parameter cannot be cast, return false.
            IavaImageViewArea viewArea = (IavaImageViewArea)obj;
            if ((Object)viewArea == null) { return false; }

            // Do a field by field comparison
            return (viewArea.CenterX == this.CenterX &&
                    viewArea.CenterY == this.CenterY &&
                    viewArea.Zoom == this.Zoom);
        }

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