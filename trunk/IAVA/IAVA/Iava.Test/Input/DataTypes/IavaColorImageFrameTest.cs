using System;
using System.Linq;
using System.Runtime.Serialization;
using Iava.Input.Camera;
using Microsoft.Kinect;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iava.Test {

    /// <summary>
    ///This is a test class for IavaColorImageFrameTest and is intended
    ///to contain all IavaColorImageFrameTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IavaColorImageFrameTest {

        #region Helper Methods

        ColorImageFrame CreateImageFrame() {
            // This is a bitch to setup since it inherits from an abstract class,
            // has no constructor, and is sealed.  I'm just getting an unintialized
            // version of the object and working off that.
            ColorImageFrame kinectFrame = (ColorImageFrame)FormatterServices.GetSafeUninitializedObject(typeof(ColorImageFrame));

            return kinectFrame;
        }

        #endregion Helper Methods

        /// <summary>
        ///A test for Explicit Cast
        ///</summary>
        [TestMethod()]
        public void ExplicitCastTest() {
            try {
                ColorImageFrame kinectFrame = CreateImageFrame();

                // Create the Iava Equivalent
                IavaColorImageFrame_Accessor accessor = new IavaColorImageFrame_Accessor();
                accessor.BytesPerPixel = kinectFrame.BytesPerPixel;
                accessor.Format = (IavaColorImageFormat)kinectFrame.Format;
                accessor.FrameNumber = kinectFrame.FrameNumber;
                accessor.Height = kinectFrame.Height;
                accessor.PixelData = new byte[kinectFrame.PixelDataLength];
                accessor.Timestamp = kinectFrame.Timestamp;
                accessor.Width = kinectFrame.Width;

                // Get the Iava object
                IavaColorImageFrame iavaFrame = (IavaColorImageFrame)accessor.Target;

                // Test object as a whole
                Assert.AreEqual(iavaFrame, (IavaColorImageFrame)kinectFrame);

                // Set the Kinect Object to null
                kinectFrame = null;

                // Make sure we don't attempt to cast nulls
                Assert.IsNull((IavaColorImageFrame)kinectFrame);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest() {
            try {
                IavaColorImageFrame_Accessor accessor1 = new IavaColorImageFrame_Accessor()
                {
                    BytesPerPixel = 18,
                    Format = IavaColorImageFormat.RgbResolution640x480Fps30,
                    FrameNumber = 32,
                    Height = 480,
                    PixelData = new byte[16],
                    Timestamp = 123456489,
                    Width = 640
                };

                IavaColorImageFrame_Accessor accessor2 = new IavaColorImageFrame_Accessor()
                {
                    BytesPerPixel = 18,
                    Format = IavaColorImageFormat.RgbResolution640x480Fps30,
                    FrameNumber = 32,
                    Height = 480,
                    PixelData = new byte[16],
                    Timestamp = 123456489,
                    Width = 640
                };

                IavaColorImageFrame_Accessor accessor3 = new IavaColorImageFrame_Accessor()
                {
                    BytesPerPixel = 36,
                    Format = IavaColorImageFormat.Undefined,
                    FrameNumber = 32,
                    Height = 480,
                    PixelData = new byte[16],
                    Timestamp = 1234564890,
                    Width = 640
                };

                IavaColorImageFrame frame1 = (IavaColorImageFrame)accessor1.Target;
                IavaColorImageFrame frame2 = (IavaColorImageFrame)accessor2.Target;
                IavaColorImageFrame frame3 = (IavaColorImageFrame)accessor3.Target;

                // Make sure eventArgs1 does not equal null
                Assert.IsFalse(frame1.Equals(null));

                // Make sure eventArgs1 does not equal a completly different object
                Assert.IsFalse(frame1.Equals("Not a frame."));

                // Make sure eventArgs1 and eventArgs3 are not equal
                Assert.IsFalse(frame1.Equals(frame3));

                // Make sure eventArgs1 and eventArgs2 are equal
                Assert.IsTrue(frame1.Equals(frame2));

                // Make sure eventArgs1 equals itself
                Assert.IsTrue(frame1.Equals(frame1));
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for Equality Operation
        ///</summary>
        [TestMethod()]
        public void EqualityTest() {
            try {
                IavaColorImageFrame_Accessor accessor1 = new IavaColorImageFrame_Accessor()
                {
                    BytesPerPixel = 18,
                    Format = IavaColorImageFormat.RgbResolution640x480Fps30,
                    FrameNumber = 32,
                    Height = 480,
                    PixelData = new byte[16],
                    Timestamp = 123456489,
                    Width = 640
                };

                IavaColorImageFrame_Accessor accessor2 = new IavaColorImageFrame_Accessor()
                {
                    BytesPerPixel = 18,
                    Format = IavaColorImageFormat.RgbResolution640x480Fps30,
                    FrameNumber = 32,
                    Height = 480,
                    PixelData = new byte[16],
                    Timestamp = 123456489,
                    Width = 640
                };

                IavaColorImageFrame_Accessor accessor3 = new IavaColorImageFrame_Accessor()
                {
                    BytesPerPixel = 36,
                    Format = IavaColorImageFormat.Undefined,
                    FrameNumber = 32,
                    Height = 480,
                    PixelData = new byte[16],
                    Timestamp = 1234564890,
                    Width = 640
                };

                IavaColorImageFrame frame1 = (IavaColorImageFrame)accessor1.Target;
                IavaColorImageFrame frame2 = (IavaColorImageFrame)accessor2.Target;
                IavaColorImageFrame frame3 = (IavaColorImageFrame)accessor3.Target;

                // Make sure eventArgs1 does not equal null
                Assert.IsFalse(frame1 == null);

                // Make sure null does not equal eventArgs1
                Assert.IsFalse(null == frame1);

                // Make sure eventArgs1 and eventArgs3 are not equal
                Assert.IsFalse(frame1 == frame3);

                // Make sure eventArgs1 and eventArgs2 are equal
                Assert.IsTrue(frame1 == frame2);

                // Make sure eventArgs1 equals itself
                Assert.IsTrue(frame1 == frame1);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for Inequality Operation
        ///</summary>
        [TestMethod()]
        public void InequalityTest() {
            try {
                IavaColorImageFrame_Accessor accessor1 = new IavaColorImageFrame_Accessor()
                {
                    BytesPerPixel = 18,
                    Format = IavaColorImageFormat.RgbResolution640x480Fps30,
                    FrameNumber = 32,
                    Height = 480,
                    PixelData = new byte[16],
                    Timestamp = 123456489,
                    Width = 640
                };

                IavaColorImageFrame_Accessor accessor2 = new IavaColorImageFrame_Accessor()
                {
                    BytesPerPixel = 18,
                    Format = IavaColorImageFormat.RgbResolution640x480Fps30,
                    FrameNumber = 32,
                    Height = 480,
                    PixelData = new byte[16],
                    Timestamp = 123456489,
                    Width = 640
                };

                IavaColorImageFrame_Accessor accessor3 = new IavaColorImageFrame_Accessor()
                {
                    BytesPerPixel = 36,
                    Format = IavaColorImageFormat.Undefined,
                    FrameNumber = 32,
                    Height = 480,
                    PixelData = new byte[16],
                    Timestamp = 1234564890,
                    Width = 640
                };

                IavaColorImageFrame frame1 = (IavaColorImageFrame)accessor1.Target;
                IavaColorImageFrame frame2 = (IavaColorImageFrame)accessor2.Target;
                IavaColorImageFrame frame3 = (IavaColorImageFrame)accessor3.Target;

                // Make sure eventArgs1 does not equal null
                Assert.IsTrue(frame1 != null);

                // Make sure null does not equal eventArgs1
                Assert.IsTrue(null != frame1);

                // Make sure eventArgs1 and eventArgs3 are not equal
                Assert.IsTrue(frame1 != frame3);

                // Make sure eventArgs1 and eventArgs2 are equal
                Assert.IsFalse(frame1 != frame2);

                // Make sure eventArgs1 equals itself
                Assert.IsFalse(frame1 != frame1);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for IavaColorImageFrame Constructor
        ///</summary>
        [TestMethod()]
        public void ConstructorTest() {
            // Nothing to do.
            //IavaColorImageFrame skeletonFrame = new IavaColorImageFrame();
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        public void DisposeTest() {
            // Nothing to do.
            //IavaColorImageFrame skeletonFrame = new IavaColorImageFrame();
            //skeletonFrame.Dispose();
        }

        /// <summary>
        ///A test for BytesPerPixel
        ///</summary>
        [TestMethod()]
        public void BytesPerPixelTest() {
            try {
                IavaColorImageFrame_Accessor iavaFrame = new IavaColorImageFrame_Accessor();

                // Set the BytesPerPixel
                iavaFrame.BytesPerPixel = 80;

                // Make sure the property set correctly
                Assert.AreEqual(80, iavaFrame.BytesPerPixel);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for Format
        ///</summary>
        [TestMethod()]
        public void FormatTest() {
            try {
                IavaColorImageFrame_Accessor iavaFrame = new IavaColorImageFrame_Accessor();

                // Set the Format
                iavaFrame.Format = IavaColorImageFormat.YuvResolution640x480Fps15;

                // Make sure the property set correctly
                Assert.AreEqual(IavaColorImageFormat.YuvResolution640x480Fps15, iavaFrame.Format);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for FrameNumber
        ///</summary>
        [TestMethod()]
        public void FrameNumberTest() {
            try {
                IavaColorImageFrame_Accessor iavaFrame = new IavaColorImageFrame_Accessor();

                // Set the FrameNumber
                iavaFrame.FrameNumber = 1984;

                // Make sure the property set correctly
                Assert.AreEqual(1984, iavaFrame.FrameNumber);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for Height
        ///</summary>
        [TestMethod()]
        public void HeightTest() {
            try {
                IavaColorImageFrame_Accessor iavaFrame = new IavaColorImageFrame_Accessor();

                // Set the Height
                iavaFrame.Height = 480;

                // Make sure the property set correctly
                Assert.AreEqual(480, iavaFrame.Height);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for PixelData
        ///</summary>
        [TestMethod()]
        public void PixelDataTest() {
            try {
                IavaColorImageFrame_Accessor iavaFrame = new IavaColorImageFrame_Accessor();

                // Init the array
                byte[] pixelData = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };

                // Set the PixelData
                iavaFrame.PixelData = pixelData;

                // Make sure the property set correctly
                Assert.IsTrue(pixelData.SequenceEqual(iavaFrame.PixelData));
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for Timestamp
        ///</summary>
        [TestMethod()]
        public void TimestampTest() {
            try {
                IavaColorImageFrame_Accessor iavaFrame = new IavaColorImageFrame_Accessor();

                // Set the Timestamp
                iavaFrame.Timestamp = 123456789;

                // Make sure the property set correctly
                Assert.AreEqual(123456789, iavaFrame.Timestamp);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for Width
        ///</summary>
        [TestMethod()]
        public void WidthTest() {
            try {
                IavaColorImageFrame_Accessor iavaFrame = new IavaColorImageFrame_Accessor();

                // Set the Width
                iavaFrame.Width = 640;

                // Make sure the property set correctly
                Assert.AreEqual(640, iavaFrame.Width);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }
    }
}
