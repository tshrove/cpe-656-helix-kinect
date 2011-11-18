using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Research.Kinect.Nui;

namespace Iava.Input.Camera {
    public enum ImageResolution {
        Invalid = -1,
        Resolution80x60 = 0,
        Resolution320x240 = 1,
        Resolution640x480 = 2,
        Resolution1280x1024 = 3
    }

    public enum ImageType {
        DepthAndPlayerIndex = 0,
        Color = 1,
        ColorYuv = 2,
        ColorYuvRaw = 3,
        Depth = 4
    }

    public enum ImageDigitalZoom {
        Zoom1x = 0,
        Zoom2x = 1
    }

    public struct PlanarImage {
        public byte[] Bits;
        public int BytesPerPixel;
        public int Height;
        public int Width;

        public static implicit operator PlanarImage(Microsoft.Research.Kinect.Nui.PlanarImage value) {
            return new PlanarImage()
            {
                Bits = value.Bits,
                BytesPerPixel = value.BytesPerPixel,
                Height = value.Height,
                Width = value.Width
            };
        }
    }

    public struct ImageViewArea {
        public int CenterX;
        public int CenterY;
        public ImageDigitalZoom Zoom;

        public static implicit operator ImageViewArea(Microsoft.Research.Kinect.Nui.ImageViewArea value) {
            return new ImageViewArea()
            {
                CenterX = value.CenterX,
                CenterY = value.CenterY,
                Zoom = (ImageDigitalZoom)value.Zoom
            };
        }
    }

    public class ImageFrame {
        public int FrameNumber;
        public PlanarImage Image;
        public ImageResolution Resolution;
        public long Timestamp;
        public ImageType Type;
        public ImageViewArea ViewArea;

        public static implicit operator ImageFrame(Microsoft.Research.Kinect.Nui.ImageFrame value) {
            return new ImageFrame()
            {
                FrameNumber = value.FrameNumber,
                Image = value.Image,
                Resolution = (ImageResolution)value.Resolution,
                Timestamp = value.Timestamp,
                Type = (ImageType)value.Type,
                ViewArea = value.ViewArea
            };
        }
    }
}
