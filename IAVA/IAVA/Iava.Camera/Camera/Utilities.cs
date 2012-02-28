using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Research.Kinect.Nui;
using System.Collections;

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

        public static explicit operator PlanarImage(Microsoft.Research.Kinect.Nui.PlanarImage value) {
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

        public static explicit operator ImageViewArea(Microsoft.Research.Kinect.Nui.ImageViewArea value) {
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

        public static explicit operator ImageFrame(Microsoft.Research.Kinect.Nui.ImageFrame value) {
            return new ImageFrame()
            {
                FrameNumber = value.FrameNumber,
                Image = (PlanarImage)value.Image,
                Resolution = (ImageResolution)value.Resolution,
                Timestamp = value.Timestamp,
                Type = (ImageType)value.Type,
                ViewArea = (ImageViewArea)value.ViewArea
            };
        }
    }

    public sealed class SkeletonFrame {
        public Vector FloorClipPlane;
        public int FrameNumber;
        public Vector NormalToGravity;
        public SkeletonFrameQuality Quality;
        public SkeletonData[] Skeletons;
        public long TimeStamp;

        private int _skeletonIndex = 0;

        /// <summary>
        /// Gets the fist active skeleton, in any, in the frame
        /// </summary>
        public SkeletonData ActiveSkeleton { get { return GetActiveSkeleton(); } }

        private SkeletonData GetActiveSkeleton() {
            // Check the last known active skeleton first...
            if (Skeletons[_skeletonIndex] != null) { return Skeletons[_skeletonIndex]; }

            // Check all the skeleton slots
            for (int i = 0; i < Skeletons.Count(); i++) {
                if (Skeletons[i] != null) {
                    _skeletonIndex = i;
                    return Skeletons[i];
                }
            }

            // If we get here we didn't find anything
            _skeletonIndex = 0;
            return null;
        }

        public static explicit operator SkeletonFrame(Microsoft.Research.Kinect.Nui.SkeletonFrame value) {
            SkeletonFrame skeletonFrame = new SkeletonFrame()
            {
                FloorClipPlane = (Vector)value.FloorClipPlane,
                FrameNumber = value.FrameNumber,
                NormalToGravity = (Vector)value.NormalToGravity,
                Quality = (SkeletonFrameQuality)value.Quality,
                Skeletons = new SkeletonData[value.Skeletons.Length],
                TimeStamp = value.TimeStamp
            };

            // Copy and convert the array
            for (int i = 0; i < skeletonFrame.Skeletons.Length; i++) {
                skeletonFrame.Skeletons[i] = (SkeletonData)value.Skeletons[i];
            }

            return skeletonFrame;
        }
    }

    [Flags]
    public enum SkeletonFrameQuality {
        CameraMotion = 1,
        ExtrapolatedFloor = 2,
        UpperBodySkeleton = 4,
    }

    public sealed class SkeletonData {
        public JointsCollection Joints;
        public Vector Position;
        public SkeletonQuality Quality;
        public int TrackingID;
        public SkeletonTrackingState TrackingState;
        public int UserIndex;

        public static explicit operator SkeletonData(Microsoft.Research.Kinect.Nui.SkeletonData value) {
            return new SkeletonData()
            {
                Joints = (JointsCollection)value.Joints,
                Position = (Vector)value.Position,
                Quality = (SkeletonQuality)value.Quality,
                TrackingID = value.TrackingID,
                TrackingState = (SkeletonTrackingState)value.TrackingState,
                UserIndex = value.UserIndex
            };
        }
    }

    public class JointsCollection : IEnumerable {
        public int Count { get { return (int)JointID.Count; } }

        public Joint this[JointID i] {
            get { return _joints[(int)i]; }
            set { _joints[(int)i] = value; }
        }

        public IEnumerator GetEnumerator() { return _joints.GetEnumerator(); }

        public static explicit operator JointsCollection(Microsoft.Research.Kinect.Nui.JointsCollection value) {
            JointsCollection jointsCollection = new JointsCollection();

            for (int i = 0; i < (int)JointID.Count; i++) { jointsCollection[(JointID)i] = (Joint)value[(Microsoft.Research.Kinect.Nui.JointID)i]; }

            return jointsCollection;
        }

        private Joint[] _joints = new Joint[(int)JointID.Count];
    }

    public struct Joint {
        public JointID ID { get; set; }
        public Vector Position { get; set; }
        public JointTrackingState TrackingState { get; set; }

        public static explicit operator Joint(Microsoft.Research.Kinect.Nui.Joint value) {
            return new Joint() {
                ID = (JointID)value.ID,
                Position = (Vector)value.Position,
                TrackingState = (JointTrackingState)value.TrackingState
            };
        }
    }

    public enum JointTrackingState {
        NotTracked = 0,
        Inferred = 1,
        Tracked = 2,
    }

    public enum SkeletonTrackingState {
        NotTracked = 0,
        PositionOnly = 1,
        Tracked = 2,
    }

        public enum JointID {
        HipCenter = 0,
        Spine = 1,
        ShoulderCenter = 2,
        Head = 3,
        ShoulderLeft = 4,
        ElbowLeft = 5,
        WristLeft = 6,
        HandLeft = 7,
        ShoulderRight = 8,
        ElbowRight = 9,
        WristRight = 10,
        HandRight = 11,
        HipLeft = 12,
        KneeLeft = 13,
        AnkleLeft = 14,
        FootLeft = 15,
        HipRight = 16,
        KneeRight = 17,
        AnkleRight = 18,
        FootRight = 19,
        Count = 20,
    }

        public enum SkeletonQuality {
            ClippedRight = 1,
            ClippedLeft = 2,
            ClippedTop = 4,
            ClippedBottom = 8
        }

        public struct Vector {

            public float W { get; set; }
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }

            public static explicit operator Vector(Microsoft.Research.Kinect.Nui.Vector value) {
                return new Vector()
                {
                    W = value.W,
                    X = value.X,
                    Y = value.Y,
                    Z = value.Z
                };
            }
        }
}
