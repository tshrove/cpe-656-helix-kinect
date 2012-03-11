//using System.Collections.Generic;
//using Iava.Input.Camera;
//using Microsoft.Kinect;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Iava.Test.Camera {

//    /// <summary>
//    ///This is a test class for CameraTest and is intended
//    ///to contain all CameraTest Unit Tests
//    ///</summary>
//    [TestClass()]
//    public class CameraEventArgsTest {

//        [TestMethod()]
//        public void CameraIavaSkeletonEventArgs() {
//            IavaSkeleton skeletonData = new IavaSkeleton();

//            // Init the Kinect object
//            IavaSkeletonEventArgs eventArgs = new IavaSkeletonEventArgs(skeletonData);

//            // Test Equality...
//            Assert.AreEqual(skeletonData, eventArgs.Skeleton);
//            Assert.AreEqual(null, IavaSkeletonEventArgs.Empty);
//        }

//        [TestMethod()]
//        public void CameraIavaSkeletonFrameEventArgs() {
//            List<int> skeletonIDs = new List<int>();
//            skeletonIDs.Add(1);
//            skeletonIDs.Add(58);
//            skeletonIDs.Add(-9);

//            long timestamp = 123456;

//            // Init the Kinect object
//            IavaSkeletonFrameEventArgs eventArgs = new IavaSkeletonFrameEventArgs(skeletonIDs, timestamp);

//            // Test Equality...
//            Assert.AreEqual(skeletonIDs, eventArgs.SkeletonIDs);
//            Assert.AreEqual(timestamp, eventArgs.Timestamp);
//            Assert.AreEqual(null, IavaSkeletonFrameEventArgs.Empty);
//        }

//        [TestMethod()]
//        public void CameraIavaImageFrameReadyEventArgs() {
//            IavaColorImageFrame imageFrame = new IavaColorImageFrame();

//            // Init the Kinect object
//            IavaColorImageFrameReadyEventArgs eventArgs = new IavaColorImageFrameReadyEventArgs(imageFrame);

//            // Test Equality...
//            Assert.AreEqual(imageFrame, eventArgs.ImageFrame);
//            Assert.AreEqual(null, IavaColorImageFrameReadyEventArgs.Empty);

//            // Init the Kinect object
//            ImageFrameReadyEventArgs kinectEventArgs = new ImageFrameReadyEventArgs() { ImageFrame = new ImageFrame() };

//            // Explicitly cast to the Iava equivalent
//            eventArgs = (IavaColorImageFrameReadyEventArgs)kinectEventArgs;
//            Assert.AreEqual<IavaColorImageFrame>((IavaColorImageFrame)kinectEventArgs.ImageFrame, eventArgs.ImageFrame);
//        }

//        [TestMethod()]
//        public void CameraIavaSkeletonFrameReadyEventArgs() {
//            IavaSkeletonFrame skeletonFrame = new IavaSkeletonFrame();

//            // Init the Kinect object
//            IavaSkeletonFrameReadyEventArgs eventArgs = new IavaSkeletonFrameReadyEventArgs(skeletonFrame);

//            // Test Equality...
//            Assert.AreEqual(skeletonFrame, eventArgs.SkeletonFrame);
//            Assert.AreEqual(null, IavaSkeletonFrameReadyEventArgs.Empty);

//            // Init the Kinect object
//            SkeletonFrameReadyEventArgs kinectEventArgs = new SkeletonFrameReadyEventArgs();

//            // Explicitly cast to the Iava equivalent
//            eventArgs = (IavaSkeletonFrameReadyEventArgs)kinectEventArgs;
//            Assert.AreEqual((IavaSkeletonFrame)kinectEventArgs.SkeletonFrame, eventArgs.SkeletonFrame);
//        }
//    }
//}
