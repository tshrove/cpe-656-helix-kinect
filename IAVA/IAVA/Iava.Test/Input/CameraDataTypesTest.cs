//using Iava.Core.Math;
//using Iava.Input.Camera;
//using Microsoft.Kinect;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Iava.Core.Math;

//namespace Iava.Test.Camera {

//    /// <summary>
//    ///This is a test class for CameraTest and is intended
//    ///to contain all CameraTest Unit Tests
//    ///</summary>
//    [TestClass()]
//    public class CameraDataTypesTest {

//        [TestMethod()]
//        public void CameraIavaImageDigitalZoom() {
//            // Test Equality...
//            Assert.AreEqual((int)ImageDigitalZoom.Zoom1x, (int)IavaImageDigitalZoom.Zoom1x);
//            Assert.AreEqual((int)ImageDigitalZoom.Zoom2x, (int)IavaImageDigitalZoom.Zoom2x);

//            // Test object as a whole
//            ImageDigitalZoom kinectObject = ImageDigitalZoom.Zoom1x;
//            IavaImageDigitalZoom iavaObject = (IavaImageDigitalZoom)kinectObject;
//            Assert.AreEqual((IavaImageDigitalZoom)kinectObject, iavaObject);
//        }

//        [TestMethod()]
//        public void CameraIavaImageFrame() {
//            // Init the Kinect object
//            ImageFrame imageFrame = new ImageFrame();
//            imageFrame.FrameNumber = 13;
//            imageFrame.Image = new PlanarImage();
//            imageFrame.Resolution = ImageResolution.Resolution640x480;
//            imageFrame.Timestamp = 12345;
//            imageFrame.Type = ImageType.ColorYuvRaw;
//            imageFrame.ViewArea = new ImageViewArea();

//            // Explicitly cast to the Iava equivalent
//            IavaColorImageFrame iavaFrame = (IavaColorImageFrame)imageFrame;
//            Assert.AreEqual(imageFrame.FrameNumber, iavaFrame.FrameNumber);
//            Assert.AreEqual((IavaPlanarImage)imageFrame.Image, iavaFrame.Image);
//            Assert.AreEqual((IavaImageResolution)imageFrame.Resolution, iavaFrame.Resolution);
//            Assert.AreEqual(imageFrame.Timestamp, iavaFrame.Timestamp);
//            Assert.AreEqual((IavaImageType)imageFrame.Type, iavaFrame.Type);
//            Assert.AreEqual((IavaImageViewArea)imageFrame.ViewArea, iavaFrame.ViewArea);

//            // Test object as a whole
//            Assert.AreEqual((IavaColorImageFrame)imageFrame, iavaFrame);
//        }

//        [TestMethod()]
//        public void CameraIavaImageResolution() {
//            // Test Equality...
//            Assert.AreEqual((int)ImageResolution.Invalid, (int)IavaImageResolution.Invalid);
//            Assert.AreEqual((int)ImageResolution.Resolution1280x1024, (int)IavaImageResolution.Resolution1280x1024);
//            Assert.AreEqual((int)ImageResolution.Resolution320x240, (int)IavaImageResolution.Resolution320x240);
//            Assert.AreEqual((int)ImageResolution.Resolution640x480, (int)IavaImageResolution.Resolution640x480);
//            Assert.AreEqual((int)ImageResolution.Resolution80x60, (int)IavaImageResolution.Resolution80x60);

//            // Test object as a whole
//            ImageResolution kinectObject = ImageResolution.Resolution1280x1024;
//            IavaImageResolution iavaObject = (IavaImageResolution)kinectObject;
//            Assert.AreEqual((IavaImageResolution)kinectObject, iavaObject);
//        }

//        [TestMethod()]
//        public void CameraIavaImageType() {
//            // Test Equality...
//            Assert.AreEqual((int)ImageType.Color, (int)IavaImageType.Color);
//            Assert.AreEqual((int)ImageType.ColorYuv, (int)IavaImageType.ColorYuv);
//            Assert.AreEqual((int)ImageType.ColorYuvRaw, (int)IavaImageType.ColorYuvRaw);
//            Assert.AreEqual((int)ImageType.Depth, (int)IavaImageType.Depth);
//            Assert.AreEqual((int)ImageType.DepthAndPlayerIndex, (int)IavaImageType.DepthAndPlayerIndex);

//            // Test object as a whole
//            ImageType kinectObject = ImageType.Depth;
//            IavaImageType iavaObject = (IavaImageType)kinectObject;
//            Assert.AreEqual((IavaImageType)kinectObject, iavaObject);
//        }

//        [TestMethod()]
//        public void CameraIavaImageViewArea() {
//            // Init the Kinect object
//            ImageViewArea imageViewArea;
//            imageViewArea.CenterX = 13;
//            imageViewArea.CenterY = 27;
//            imageViewArea.Zoom = ImageDigitalZoom.Zoom1x;

//            // Explicitly cast to the Iava equivalent
//            IavaImageViewArea iavaViewArea = (IavaImageViewArea)imageViewArea;
//            Assert.AreEqual(imageViewArea.CenterX, iavaViewArea.CenterX);
//            Assert.AreEqual(imageViewArea.CenterY, iavaViewArea.CenterY);
//            Assert.AreEqual((IavaImageDigitalZoom)imageViewArea.Zoom, iavaViewArea.Zoom);

//            // Test object as a whole
//            Assert.AreEqual((IavaImageViewArea)imageViewArea, iavaViewArea);
//        }

//        [TestMethod()]
//        public void CameraIavaJoint() {
//            // Init the Kinect object
//            Joint joint = new Joint();
//            joint.ID = JointID.FootRight;
//            joint.Position = new Vector() { W = 0, X = 1, Y = 2, Z = 3 };
//            joint.TrackingState = JointTrackingState.Inferred;

//            // Explicitly cast to the Iava equivalent
//            IavaJoint iavaJoint = (IavaJoint)joint;
//            Assert.AreEqual((IavaJointType)joint.ID, iavaJoint.JointType);
//            Assert.AreEqual((IavaSkeletonPoint)joint.Position, iavaJoint.Position);
//            Assert.AreEqual((IavaJointTrackingState)joint.TrackingState, iavaJoint.TrackingState);

//            // Test object as a whole
//            Assert.AreEqual((IavaJoint)joint, iavaJoint);
//        }

//        [TestMethod()]
//        public void CameraIavaJointID() {
//            // Test Equality...
//            Assert.AreEqual((int)JointID.AnkleLeft, (int)IavaJointType.AnkleLeft);
//            Assert.AreEqual((int)JointID.AnkleRight, (int)IavaJointType.AnkleRight);
//            Assert.AreEqual((int)JointID.Count, (int)IavaJointType.Count);
//            Assert.AreEqual((int)JointID.ElbowLeft, (int)IavaJointType.ElbowLeft);
//            Assert.AreEqual((int)JointID.ElbowRight, (int)IavaJointType.ElbowRight);
//            Assert.AreEqual((int)JointID.FootLeft, (int)IavaJointType.FootLeft);
//            Assert.AreEqual((int)JointID.FootRight, (int)IavaJointType.FootRight);
//            Assert.AreEqual((int)JointID.HandLeft, (int)IavaJointType.HandLeft);
//            Assert.AreEqual((int)JointID.HandRight, (int)IavaJointType.HandRight);
//            Assert.AreEqual((int)JointID.Head, (int)IavaJointType.Head);
//            Assert.AreEqual((int)JointID.HipCenter, (int)IavaJointType.HipCenter);
//            Assert.AreEqual((int)JointID.HipLeft, (int)IavaJointType.HipLeft);
//            Assert.AreEqual((int)JointID.HipRight, (int)IavaJointType.HipRight);
//            Assert.AreEqual((int)JointID.KneeLeft, (int)IavaJointType.KneeLeft);
//            Assert.AreEqual((int)JointID.KneeRight, (int)IavaJointType.KneeRight);
//            Assert.AreEqual((int)JointID.ShoulderCenter, (int)IavaJointType.ShoulderCenter);
//            Assert.AreEqual((int)JointID.ShoulderLeft, (int)IavaJointType.ShoulderLeft);
//            Assert.AreEqual((int)JointID.ShoulderRight, (int)IavaJointType.ShoulderRight);
//            Assert.AreEqual((int)JointID.Spine, (int)IavaJointType.Spine);
//            Assert.AreEqual((int)JointID.WristLeft, (int)IavaJointType.WristLeft);
//            Assert.AreEqual((int)JointID.WristRight, (int)IavaJointType.WristRight);

//            // Test object as a whole
//            JointID kinectObject = JointID.Head;
//            IavaJointType iavaObject = (IavaJointType)kinectObject;
//            Assert.AreEqual((IavaJointType)kinectObject, iavaObject);
//        }

//        [TestMethod()]
//        public void CameraIavaJointsCollection() {
//            // As of Beta2 JointsCollection still has no constructor, this cast will
//            // get exercised in other UnitTests, so its not the end of the world
//        }

//        [TestMethod()]
//        public void CameraIavaJointTrackingState() {
//            // Test Equality...
//            Assert.AreEqual((int)JointTrackingState.Inferred, (int)IavaJointTrackingState.Inferred);
//            Assert.AreEqual((int)JointTrackingState.NotTracked, (int)IavaJointTrackingState.NotTracked);
//            Assert.AreEqual((int)JointTrackingState.Tracked, (int)IavaJointTrackingState.Tracked);

//            // Test object as a whole
//            JointTrackingState kinectObject = JointTrackingState.NotTracked;
//            IavaJointTrackingState iavaObject = (IavaJointTrackingState)kinectObject;
//            Assert.AreEqual((IavaJointTrackingState)kinectObject, iavaObject);
//        }

//        [TestMethod()]
//        public void CameraIavaPlanarImageTest() {
//            // Init the Kinect object
//            PlanarImage planarImage;
//            planarImage.Bits = new byte[] { 0x01, 0x02, 0x03, 0x04 };
//            planarImage.BytesPerPixel = 31;
//            planarImage.Height = 17;
//            planarImage.Width = 11;

//            // Explicitly cast to the Iava equivalent
//            IavaPlanarImage iavaImage = (IavaPlanarImage)planarImage;
//            Assert.AreEqual(planarImage.Bits, iavaImage.Bits);
//            Assert.AreEqual(planarImage.BytesPerPixel, iavaImage.BytesPerPixel);
//            Assert.AreEqual(planarImage.Height, iavaImage.Height);
//            Assert.AreEqual(planarImage.Width, iavaImage.Width);

//            // Test object as a whole
//            Assert.AreEqual((IavaPlanarImage)planarImage, iavaImage);
//        }

//        [TestMethod()]
//        public void CameraIavaSkeletonData() {
//            // As of Beta2 SkeletonData still has no constructor, this cast will
//            // get exercised in other UnitTests, so its not the end of the world
//            /*
//            // Init the Kinect object
//            SkeletonData skeletonData = new SkeletonData();
//            skeletonData.Joints = new JointsCollection();
//            skeletonData.Position = new Vector() { W = 0, X = 1, Y = 2, Z = 3 };
//            skeletonData.ClippedEdges = SkeletonQuality.ClippedRight;
//            skeletonData.TrackingId = 47;
//            skeletonData.TrackingState = SkeletonTrackingState.PositionOnly;
//            skeletonData.UserIndex = 31;

//            // Explicitly cast to the Iava equivalent
//            IavaSkeleton iavaData = (IavaSkeleton)skeletonData;
//            Assert.AreEqual((IavaJointCollection)skeletonData.Joints, iavaData.Joints);
//            Assert.AreEqual((IavaSkeletonPoint)skeletonData.Position, iavaData.Position);
//            Assert.AreEqual((IavaFrameEdges)skeletonData.ClippedEdges, iavaData.ClippedEdges);
//            Assert.AreEqual(skeletonData.TrackingId, iavaData.TrackingId);
//            Assert.AreEqual((IavaSkeletonTrackingState)skeletonData.TrackingState, iavaData.TrackingState);
//            Assert.AreEqual(skeletonData.UserIndex, iavaData.UserIndex);*/
//        }

//        [TestMethod()]
//        public void CameraIavaSkeletonFrame() {
//            // As of Beta2 SkeletonFrame still has no constructor, this cast will
//            // get exercised in other UnitTests, so its not the end of the world
//            /*
//            // Init the Kinect object
//            SkeletonFrame skeletonFrame = new SkeletonFrame();
//            skeletonFrame.FloorClipPlane = new Vector() { W = 0, X = 1, Y = 2, Z = 3 };
//            skeletonFrame.FrameNumber = 13;
//            skeletonFrame.NormalToGravity = new Vector() { W = 3, X = 2, Y = 1, Z = 0 };
//            skeletonFrame.ClippedEdges = SkeletonFrameQuality.UpperBodySkeleton;
//            skeletonFrame.Skeletons = new List<SkeletonData>(3).ToArray();
//            skeletonFrame.Timestamp = 12345;

//            // Explicitly cast to the Iava equivalent
//            IavaSkeletonFrame iavaFrame = (IavaSkeletonFrame)skeletonFrame;
//            Assert.AreEqual((IavaSkeletonPoint)skeletonFrame.FloorClipPlane, iavaFrame.FloorClipPlane);
//            Assert.AreEqual(skeletonFrame.FrameNumber, iavaFrame.FrameNumber);
//            Assert.AreEqual((IavaSkeletonPoint)skeletonFrame.NormalToGravity, iavaFrame.NormalToGravity);
//            Assert.AreEqual((IavaSkeletonFrameQuality)skeletonFrame.ClippedEdges, iavaFrame.ClippedEdges);
//            Assert.AreEqual(skeletonFrame.Timestamp, iavaFrame.Timestamp);

//            // Test each SkeletonData in the collection
//            int index = 0;
//            foreach (SkeletonData skeleton in skeletonFrame.Skeletons) {
//                Assert.AreEqual<IavaSkeleton>((IavaSkeleton)skeleton, iavaFrame.Skeletons[index]);
//                index++;
//            }*/
//        }

//        [TestMethod()]
//        public void CameraSkeletonFrameQuality() {
//            // Test Equality...
//            Assert.AreEqual((int)SkeletonFrameQuality.CameraMotion, (int)IavaSkeletonFrameQuality.CameraMotion);
//            Assert.AreEqual((int)SkeletonFrameQuality.ExtrapolatedFloor, (int)IavaSkeletonFrameQuality.ExtrapolatedFloor);
//            Assert.AreEqual((int)SkeletonFrameQuality.UpperBodySkeleton, (int)IavaSkeletonFrameQuality.UpperBodySkeleton);

//            // Test object as a whole
//            SkeletonFrameQuality kinectObject = SkeletonFrameQuality.CameraMotion;
//            IavaSkeletonFrameQuality iavaObject = (IavaSkeletonFrameQuality)kinectObject;
//            Assert.AreEqual((IavaSkeletonFrameQuality)kinectObject, iavaObject);
//        }

//        [TestMethod()]
//        public void CameraSkeletonQuality() {
//            // Test Equality...
//            Assert.AreEqual((int)SkeletonQuality.ClippedBottom, (int)IavaFrameEdges.ClippedBottom);
//            Assert.AreEqual((int)SkeletonQuality.ClippedLeft, (int)IavaFrameEdges.ClippedLeft);
//            Assert.AreEqual((int)SkeletonQuality.ClippedRight, (int)IavaFrameEdges.ClippedRight);
//            Assert.AreEqual((int)SkeletonQuality.ClippedTop, (int)IavaFrameEdges.ClippedTop);

//            // Test object as a whole
//            SkeletonQuality kinectObject = SkeletonQuality.ClippedTop;
//            IavaFrameEdges iavaObject = (IavaFrameEdges)kinectObject;
//            Assert.AreEqual((IavaFrameEdges)kinectObject, iavaObject);
//        }

//        [TestMethod()]
//        public void CameraSkeletonTrackingState() {
//            // Test Equality...
//            Assert.AreEqual((int)SkeletonTrackingState.NotTracked, (int)IavaSkeletonTrackingState.NotTracked);
//            Assert.AreEqual((int)SkeletonTrackingState.PositionOnly, (int)IavaSkeletonTrackingState.PositionOnly);
//            Assert.AreEqual((int)SkeletonTrackingState.Tracked, (int)IavaSkeletonTrackingState.Tracked);

//            // Test object as a whole
//            SkeletonTrackingState kinectObject = SkeletonTrackingState.PositionOnly;
//            IavaSkeletonTrackingState iavaObject = (IavaSkeletonTrackingState)kinectObject;
//            Assert.AreEqual((IavaSkeletonTrackingState)kinectObject, iavaObject);
//        }
//    }
//}
