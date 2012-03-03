using Microsoft.Research.Kinect.Nui;

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