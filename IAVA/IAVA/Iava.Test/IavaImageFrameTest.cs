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
//        public void op_ExplicitTest() {
//            try {
//                // Create and initialize the Iava object
//                IavaColorImageFrame iavaFrame = new IavaColorImageFrame();
//                iavaFrame.FrameNumber = 32;
//                iavaFrame.Resolution = IavaImageResolution.Resolution640x480;
//                iavaFrame.Timestamp = 1987530;
//                iavaFrame.Type = IavaImageType.ColorYuv;
//                iavaFrame.Image = new IavaPlanarImage()
//                {
//                    Bits = new byte[10],
//                    BytesPerPixel = 4,
//                    Height = 120,
//                    Width = 45
//                };
//                iavaFrame.ViewArea = new IavaImageViewArea()
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
//                Assert.AreEqual(iavaFrame, (IavaColorImageFrame)kinectFrame);
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
//                IavaColorImageFrame imageFrame = new IavaColorImageFrame();

//                int frameNumber = 12345;

//                // Set the Frame Number
//                imageFrame.FrameNumber = frameNumber;

//                // Make sure the Frame Number was updated
//                Assert.AreEqual(frameNumber, imageFrame.FrameNumber);
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
//                IavaColorImageFrame imageFrame = new IavaColorImageFrame();

//                IavaPlanarImage planarImage = new IavaPlanarImage()
//                {
//                    Bits = new byte[10],
//                    BytesPerPixel = 4,
//                    Height = 120,
//                    Width = 45
//                };

//                // Set the Image
//                imageFrame.Image = planarImage;

//                // Make sure the Image was updated
//                Assert.AreEqual(planarImage, imageFrame.Image);
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
//                IavaColorImageFrame imageFrame = new IavaColorImageFrame();

//                IavaImageResolution resolution = IavaImageResolution.Resolution640x480;

//                // Set the Resolution
//                imageFrame.Resolution = resolution;

//                // Make sure the Resolution was updated
//                Assert.AreEqual(resolution, imageFrame.Resolution);
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
//                IavaColorImageFrame imageFrame = new IavaColorImageFrame();

//                int timestamp = 12345;

//                // Set the Timestamp
//                imageFrame.Timestamp = timestamp;

//                // Make sure the Timestamp was updated
//                Assert.AreEqual(timestamp, imageFrame.Timestamp);
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
//                IavaColorImageFrame imageFrame = new IavaColorImageFrame();

//                IavaImageType type = IavaImageType.Depth;

//                // Set the Type
//                imageFrame.Type = type;

//                // Make sure the Type was updated
//                Assert.AreEqual(type, imageFrame.Type);
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
//                IavaColorImageFrame imageFrame = new IavaColorImageFrame();

//                IavaImageViewArea viewArea = new IavaImageViewArea()
//                {
//                    CenterX = 45,
//                    CenterY = 19,
//                    Zoom = IavaImageDigitalZoom.Zoom2x
//                };

//                // Set the View Area
//                imageFrame.ViewArea = viewArea;

//                // Make sure the View Area was updated
//                Assert.AreEqual(viewArea, imageFrame.ViewArea);
//            }
//            catch (Exception ex) {
//                Assert.Fail(ex.Message);
//            }
//        }
//    }
//}
