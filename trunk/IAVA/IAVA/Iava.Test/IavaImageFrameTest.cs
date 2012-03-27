//using Iava.Input.Camera;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using Microsoft.Kinect;

//namespace Iava.Test {

//    /// <summary>
//    ///This is a test class for IavaImageFrameTest and is intended
//    ///to contain all IavaImageFrameTest Unit Tests
//    ///</summary>
//    [TestClass()]
//    public class IavaImageFrameTest {

//        /// <summary>
//        ///A test for IavaColorImageFrame Constructor
//        ///</summary>
//        [TestMethod()]
//        public void IavaImageFrameConstructorTest() {
//            // The constructor doesn't do anything, there's nothing to test...
//        }

//        /// <summary>
//        ///A test for op_Explicit
//        ///</summary>
//        [TestMethod()]
//        public void ExplicitCastTest() {
//            try {
//                // Create and initialize the Iava object
//                IavaColorImageFrame accessor = new IavaColorImageFrame();
//                accessor.FrameNumber = 32;
//                accessor.Resolution = IavaImageResolution.Resolution640x480;
//                accessor.Timestamp = 1987530;
//                accessor.Type = IavaImageType.ColorYuv;
//                accessor.Image = new IavaPlanarImage()
//                {
//                    Bits = new byte[10],
//                    BytesPerPixel = 4,
//                    Height = 120,
//                    Width = 45
//                };
//                accessor.ViewArea = new IavaImageViewArea()
//                {
//                    CenterX = 45,
//                    CenterY = 19,
//                    Zoom = IavaImageDigitalZoom.Zoom2x
//                };

//                // Create and initialize the Kinect object
//                ImageFrame kinectFrame = new ImageFrame();
//                kinectFrame.FrameNumber = 32;
//                kinectFrame.Resolution = ImageResolution.Resolution640x480;
//                kinectFrame.Timestamp = 1987530;
//                kinectFrame.Type = ImageType.ColorYuv;
//                kinectFrame.Image = new PlanarImage()
//                {
//                    Bits = new byte[10],
//                    BytesPerPixel = 4,
//                    Height = 120,
//                    Width = 45
//                };
//                kinectFrame.ViewArea = new ImageViewArea()
//                {
//                    CenterX = 45,
//                    CenterY = 19,
//                    Zoom = ImageDigitalZoom.Zoom2x
//                };

//                // Make sure the ImageFrames are the same
//                Assert.AreEqual(accessor, (IavaColorImageFrame)kinectFrame);
//            }
//            catch (Exception ex) {
//                Assert.Fail(ex.Message);
//            }
//        }

//        /// <summary>
//        ///A test for FrameNumber
//        ///</summary>
//        [TestMethod()]
//        public void FrameNumberTest() {
//            try {
//                IavaColorImageFrame skeletonFrame = new IavaColorImageFrame();

//                int frameNumber = 12345;

//                // Set the Frame Number
//                skeletonFrame.FrameNumber = frameNumber;

//                // Make sure the Frame Number was updated
//                Assert.AreEqual(frameNumber, skeletonFrame.FrameNumber);
//            }
//            catch (Exception ex) {
//                Assert.Fail(ex.Message);
//            }
//        }

//        /// <summary>
//        ///A test for Image
//        ///</summary>
//        [TestMethod()]
//        public void ImageTest() {
//            try {
//                IavaColorImageFrame skeletonFrame = new IavaColorImageFrame();

//                IavaPlanarImage planarImage = new IavaPlanarImage()
//                {
//                    Bits = new byte[10],
//                    BytesPerPixel = 4,
//                    Height = 120,
//                    Width = 45
//                };

//                // Set the Image
//                skeletonFrame.Image = planarImage;

//                // Make sure the Image was updated
//                Assert.AreEqual(planarImage, skeletonFrame.Image);
//            }
//            catch (Exception ex) {
//                Assert.Fail(ex.Message);
//            }
//        }

//        /// <summary>
//        ///A test for Resolution
//        ///</summary>
//        [TestMethod()]
//        public void ResolutionTest() {
//            try {
//                IavaColorImageFrame skeletonFrame = new IavaColorImageFrame();

//                IavaImageResolution resolution = IavaImageResolution.Resolution640x480;

//                // Set the Resolution
//                skeletonFrame.Resolution = resolution;

//                // Make sure the Resolution was updated
//                Assert.AreEqual(resolution, skeletonFrame.Resolution);
//            }
//            catch (Exception ex) {
//                Assert.Fail(ex.Message);
//            }
//        }

//        /// <summary>
//        ///A test for Timestamp
//        ///</summary>
//        [TestMethod()]
//        public void TimestampTest() {
//            try {
//                IavaColorImageFrame skeletonFrame = new IavaColorImageFrame();

//                int timestamp = 12345;

//                // Set the Timestamp
//                skeletonFrame.Timestamp = timestamp;

//                // Make sure the Timestamp was updated
//                Assert.AreEqual(timestamp, skeletonFrame.Timestamp);
//            }
//            catch (Exception ex) {
//                Assert.Fail(ex.Message);
//            }
//        }

//        /// <summary>
//        ///A test for Type
//        ///</summary>
//        [TestMethod()]
//        public void TypeTest() {
//            try {
//                IavaColorImageFrame skeletonFrame = new IavaColorImageFrame();

//                IavaImageType type = IavaImageType.Depth;

//                // Set the Type
//                skeletonFrame.Type = type;

//                // Make sure the Type was updated
//                Assert.AreEqual(type, skeletonFrame.Type);
//            }
//            catch (Exception ex) {
//                Assert.Fail(ex.Message);
//            }
//        }

//        /// <summary>
//        ///A test for ViewArea
//        ///</summary>
//        [TestMethod()]
//        public void ViewAreaTest() {
//            try {
//                IavaColorImageFrame skeletonFrame = new IavaColorImageFrame();

//                IavaImageViewArea viewArea = new IavaImageViewArea()
//                {
//                    CenterX = 45,
//                    CenterY = 19,
//                    Zoom = IavaImageDigitalZoom.Zoom2x
//                };

//                // Set the View Area
//                skeletonFrame.ViewArea = viewArea;

//                // Make sure the View Area was updated
//                Assert.AreEqual(viewArea, skeletonFrame.ViewArea);
//            }
//            catch (Exception ex) {
//                Assert.Fail(ex.Message);
//            }
//        }
//    }
//}
