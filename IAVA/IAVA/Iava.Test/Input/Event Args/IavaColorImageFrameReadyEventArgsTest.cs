using System;
using System.Reflection;
using Iava.Input.Camera;
using Microsoft.Kinect;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iava.Test {

    /// <summary>
    ///This is a test class for IavaColorImageFrameReadyEventArgsTest and is intended
    ///to contain all IavaColorImageFrameReadyEventArgsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IavaColorImageFrameReadyEventArgsTest {

        #region Helper Methods

        /// <summary>
        /// Handles the job of initializing a IavaColorImageFrame that can be used by the Unit Tests
        /// </summary>
        /// <returns></returns>
        IavaColorImageFrame CreateImageFrame() {
            IavaColorImageFrame_Accessor accessor = new IavaColorImageFrame_Accessor()
            {
                BytesPerPixel = 31,
                Format = IavaColorImageFormat.YuvResolution640x480Fps15,
                FrameNumber = 14,
                Height = 480,
                PixelData = new byte[10],
                Timestamp = 123456789,
                Width = 640,
            };

            return (IavaColorImageFrame)accessor.Target;
        }

        /// <summary>
        /// Handles the job of initializing a ColorImageFrameReadyEventArgs that can be used by the Unit Tests
        /// </summary>
        /// <returns></returns>
        ColorImageFrameReadyEventArgs CreateColorImageFrameReadyEventArgs() {
            ConstructorInfo constuctor = typeof(ColorImageFrameReadyEventArgs).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new Type[] { typeof(ColorImageStream), typeof(int), typeof(long), typeof(bool)},
                null);

            ColorImageFrameReadyEventArgs eventArgs = (ColorImageFrameReadyEventArgs)constuctor.Invoke(new object[] { null, 31, 123456789, true });

            return eventArgs;
        }

        #endregion Helper Methods

        /// <summary>
        ///A test for Explicit Cast
        ///</summary>
        [TestMethod()]
        public void ExplicitTest() {
            // Create the Kinect object
            ColorImageFrameReadyEventArgs kinectEventArgs = CreateColorImageFrameReadyEventArgs();

            // Create the Iava Equivalent
            IavaColorImageFrameReadyEventArgs iavaEventArgs = new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)kinectEventArgs.OpenColorImageFrame());

            // Test the object as a whole
            Assert.AreEqual(iavaEventArgs, (IavaColorImageFrameReadyEventArgs)kinectEventArgs);

            // Set the Kinect Object to null
            kinectEventArgs = null;

            // Make sure we don't attempt to cast nulls
            Assert.IsNull((IavaColorImageFrameReadyEventArgs)kinectEventArgs);
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

                IavaColorImageFrameReadyEventArgs eventArgs1 = new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)accessor1.Target);
                IavaColorImageFrameReadyEventArgs eventArgs2 = new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)accessor2.Target);
                IavaColorImageFrameReadyEventArgs eventArgs3 = new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)accessor3.Target);
                IavaColorImageFrameReadyEventArgs eventArgs4 = new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)null);
                IavaColorImageFrameReadyEventArgs eventArgs5 = new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)null);

                // Make sure eventArgs1 does not equal null
                Assert.IsFalse(eventArgs1.Equals(null));

                // Make sure eventArgs1 does not equal a completly different object
                Assert.IsFalse(eventArgs1.Equals("Not a eventArgs."));

                // Make sure eventArgs1 and eventArgs3 are not equal
                Assert.IsFalse(eventArgs1.Equals(eventArgs3));

                // Make sure eventArgs1 and eventArgs2 are equal
                Assert.IsTrue(eventArgs1.Equals(eventArgs2));

                // Make sure eventArgs4 and eventArgs5 are equal
                Assert.IsTrue(eventArgs4.Equals(eventArgs5));

                // Make sure eventArgs1 equals itself
                Assert.IsTrue(eventArgs1.Equals(eventArgs1));
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

                IavaColorImageFrameReadyEventArgs eventArgs1 = new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)accessor1.Target);
                IavaColorImageFrameReadyEventArgs eventArgs2 = new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)accessor2.Target);
                IavaColorImageFrameReadyEventArgs eventArgs3 = new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)accessor3.Target);
                IavaColorImageFrameReadyEventArgs eventArgs4 = new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)null);
                IavaColorImageFrameReadyEventArgs eventArgs5 = new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)null);

                // Make sure eventArgs1 does not equal null
                Assert.IsFalse(eventArgs1 == null);

                // Make sure null does not equal eventArgs1
                Assert.IsFalse(null == eventArgs1);

                // Make sure eventArgs1 and eventArgs3 are not equal
                Assert.IsFalse(eventArgs1 == eventArgs3);

                // Make sure eventArgs1 and eventArgs2 are equal
                Assert.IsTrue(eventArgs1 == eventArgs2);

                // Make sure eventArgs4 and eventArgs5 are equal
                Assert.IsTrue(eventArgs4 == eventArgs5);

                // Make sure eventArgs1 equals itself
                Assert.IsTrue(eventArgs1 == eventArgs1);
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

                IavaColorImageFrameReadyEventArgs eventArgs1 = new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)accessor1.Target);
                IavaColorImageFrameReadyEventArgs eventArgs2 = new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)accessor2.Target);
                IavaColorImageFrameReadyEventArgs eventArgs3 = new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)accessor3.Target);
                IavaColorImageFrameReadyEventArgs eventArgs4 = new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)null);
                IavaColorImageFrameReadyEventArgs eventArgs5 = new IavaColorImageFrameReadyEventArgs((IavaColorImageFrame)null);

                // Make sure eventArgs1 does not equal null
                Assert.IsTrue(eventArgs1 != null);

                // Make sure null does not equal eventArgs1
                Assert.IsTrue(null != eventArgs1);

                // Make sure eventArgs1 and eventArgs3 are not equal
                Assert.IsTrue(eventArgs1 != eventArgs3);

                // Make sure eventArgs1 and eventArgs2 are equal
                Assert.IsFalse(eventArgs1 != eventArgs2);

                // Make sure eventArgs4 and eventArgs5 are equal
                Assert.IsFalse(eventArgs4 != eventArgs5);

                // Make sure eventArgs1 equals itself
                Assert.IsFalse(eventArgs1 != eventArgs1);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for IavaColorImageFrameReadyEventArgs Constructor
        ///</summary>
        [TestMethod()]
        public void ConstructorTest() {
            try {
                IavaColorImageFrame imageFrame = CreateImageFrame();

                IavaColorImageFrameReadyEventArgs iavaEventArgs = new IavaColorImageFrameReadyEventArgs(imageFrame);

                // Make sure the property set correctly
                Assert.AreEqual(imageFrame, iavaEventArgs.ImageFrame);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for ImageFrame
        ///</summary>
        [TestMethod()]
        public void ImageFrameTest() {
            try {
                IavaColorImageFrameReadyEventArgs_Accessor iavaEventArgs = new IavaColorImageFrameReadyEventArgs_Accessor(new IavaColorImageFrame());

                IavaColorImageFrame imageFrame = CreateImageFrame();

                // Set the ImageFrame
                iavaEventArgs.ImageFrame = imageFrame;

                // Make sure the property set correctly
                Assert.AreEqual(imageFrame, iavaEventArgs.ImageFrame);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }
    }
}
