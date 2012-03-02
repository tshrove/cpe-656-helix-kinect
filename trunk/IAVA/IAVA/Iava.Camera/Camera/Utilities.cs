using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Research.Kinect.Nui;
using System.Collections;

namespace Iava.Input.Camera {
    public enum IavaImageResolution {
        Invalid = -1,
        Resolution80x60 = 0,
        Resolution320x240 = 1,
        Resolution640x480 = 2,
        Resolution1280x1024 = 3
    }

    public enum IavaImageType {
        DepthAndPlayerIndex = 0,
        Color = 1,
        ColorYuv = 2,
        ColorYuvRaw = 3,
        Depth = 4
    }

    public enum IavaImageDigitalZoom {
        Zoom1x = 0,
        Zoom2x = 1
    }

    public struct IavaPlanarImage {
        public byte[] Bits;
        public int BytesPerPixel;
        public int Height;
        public int Width;

        public static explicit operator IavaPlanarImage(PlanarImage value) {
            return new IavaPlanarImage()
            {
                Bits = value.Bits,
                BytesPerPixel = value.BytesPerPixel,
                Height = value.Height,
                Width = value.Width
            };
        }
    }

    public struct IavaImageViewArea {
        public int CenterX;
        public int CenterY;
        public IavaImageDigitalZoom Zoom;

        public static explicit operator IavaImageViewArea(ImageViewArea value) {
            return new IavaImageViewArea()
            {
                CenterX = value.CenterX,
                CenterY = value.CenterY,
                Zoom = (IavaImageDigitalZoom)value.Zoom
            };
        }
    }

    public class IavaImageFrame {
        public int FrameNumber;
        public IavaPlanarImage Image;
        public IavaImageResolution Resolution;
        public long Timestamp;
        public IavaImageType Type;
        public IavaImageViewArea ViewArea;

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
    }

    public sealed class IavaSkeletonFrame {
        public IavaVector FloorClipPlane;
        public int FrameNumber;
        public IavaVector NormalToGravity;
        public IavaSkeletonFrameQuality Quality;
        public IavaSkeletonData[] Skeletons;
        public long TimeStamp;

        private int _skeletonIndex = 0;

        /// <summary>
        /// Gets the fist active skeleton, in any, in the frame
        /// </summary>
        public IavaSkeletonData ActiveSkeleton { get { return GetActiveSkeleton(); } }

        private IavaSkeletonData GetActiveSkeleton() {
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

        public static explicit operator IavaSkeletonFrame(SkeletonFrame value) {
            if (value == null) { return null; }

            IavaSkeletonFrame skeletonFrame = new IavaSkeletonFrame()
            {
                FloorClipPlane = (IavaVector)value.FloorClipPlane,
                FrameNumber = value.FrameNumber,
                NormalToGravity = (IavaVector)value.NormalToGravity,
                Quality = (IavaSkeletonFrameQuality)value.Quality,
                Skeletons = new IavaSkeletonData[value.Skeletons.Length],
                TimeStamp = value.TimeStamp
            };

            // Copy and convert the array
            for (int i = 0; i < skeletonFrame.Skeletons.Length; i++) {
                skeletonFrame.Skeletons[i] = (IavaSkeletonData)value.Skeletons[i];
            }

            return skeletonFrame;
        }
    }

    [Flags]
    public enum IavaSkeletonFrameQuality {
        CameraMotion = 1,
        ExtrapolatedFloor = 2,
        UpperBodySkeleton = 4,
    }

    public sealed class IavaSkeletonData {
        public IavaJointsCollection Joints;
        public IavaVector Position;
        public IavaSkeletonQuality Quality;
        public int TrackingID;
        public IavaSkeletonTrackingState TrackingState;
        public int UserIndex;

        public static explicit operator IavaSkeletonData(SkeletonData value) {
            return new IavaSkeletonData()
            {
                Joints = (IavaJointsCollection)value.Joints,
                Position = (IavaVector)value.Position,
                Quality = (IavaSkeletonQuality)value.Quality,
                TrackingID = value.TrackingID,
                TrackingState = (IavaSkeletonTrackingState)value.TrackingState,
                UserIndex = value.UserIndex
            };
        }
    }

    public class IavaJointsCollection : IEnumerable {
        public int Count { get { return (int)IavaJointID.Count; } }

        public IavaJoint this[IavaJointID i] {
            get { return _joints[(int)i]; }
            set { _joints[(int)i] = value; }
        }

        public IEnumerator GetEnumerator() { return _joints.GetEnumerator(); }

        public static explicit operator IavaJointsCollection(JointsCollection value) {
            IavaJointsCollection jointsCollection = new IavaJointsCollection();

            for (int i = 0; i < (int)IavaJointID.Count; i++) { jointsCollection[(IavaJointID)i] = (IavaJoint)value[(JointID)i]; }

            return jointsCollection;
        }

        private IavaJoint[] _joints = new IavaJoint[(int)IavaJointID.Count];
    }

    public struct IavaJoint {
        public IavaJointID ID { get; set; }
        public IavaVector Position { get; set; }
        public IavaJointTrackingState TrackingState { get; set; }

        public static explicit operator IavaJoint(Joint value) {
            return new IavaJoint() {
                ID = (IavaJointID)value.ID,
                Position = (IavaVector)value.Position,
                TrackingState = (IavaJointTrackingState)value.TrackingState
            };
        }
    }

    public enum IavaJointTrackingState {
        NotTracked = 0,
        Inferred = 1,
        Tracked = 2,
    }

    public enum IavaSkeletonTrackingState {
        NotTracked = 0,
        PositionOnly = 1,
        Tracked = 2,
    }

        public enum IavaJointID {
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

        public enum IavaSkeletonQuality {
            ClippedRight = 1,
            ClippedLeft = 2,
            ClippedTop = 4,
            ClippedBottom = 8
        }

        public struct IavaVector {

            public float W { get; set; }
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }

            public static explicit operator IavaVector(Vector value) {
                return new IavaVector()
                {
                    W = value.W,
                    X = value.X,
                    Y = value.Y,
                    Z = value.Z
                };
            }
        }
}
