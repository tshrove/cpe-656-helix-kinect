using System;
using System.Reflection;
using Iava.Input.Camera;
using Microsoft.Kinect;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iava.Test {

    /// <summary>
    ///This is a test class for IavaSkeletonFrameReadyEventArgsTest and is intended
    ///to contain all IavaSkeletonFrameReadyEventArgsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IavaSkeletonFrameReadyEventArgsTest {

        #region Helper Methods

        /// <summary>
        /// Handles the job of initializing a IavaSkeletonFrame that can be used by the Unit Tests
        /// </summary>
        /// <returns></returns>
        IavaSkeletonFrame CreateSkeletonFrame() {
            IavaSkeleton[] skeletons = new IavaSkeleton[] { new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton(),
                                                            new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton() };

            IavaSkeletonFrame_Accessor accessor = new IavaSkeletonFrame_Accessor()
            {
                FloorClipPlane = Tuple.Create(1.0f, 2.0f, 3.0f, 4.0f),
                FrameNumber = 14,
                Skeletons = skeletons,
                Timestamp = 123456789
            };

            return (IavaSkeletonFrame)accessor.Target;
        }

        /// <summary>
        /// Handles the job of initializing a SkeletonFrameReadyEventArgs that can be used by the Unit Tests
        /// </summary>
        /// <returns></returns>
        SkeletonFrameReadyEventArgs CreateSkeletonFrameReadyEventArgs() {
            ConstructorInfo constuctor = typeof(SkeletonFrameReadyEventArgs).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new Type[] { typeof(SkeletonStream), typeof(int), typeof(long), typeof(bool) },
                null);

            SkeletonFrameReadyEventArgs eventArgs = (SkeletonFrameReadyEventArgs)constuctor.Invoke(new object[] { null, 31, 123456789, true });

            return eventArgs;
        }

        #endregion Helper Methods

        /// <summary>
        ///A test for Explicit Cast
        ///</summary>
        [TestMethod()]
        public void ExplicitTest() {
            // Create the Kinect object
            SkeletonFrameReadyEventArgs kinectEventArgs = CreateSkeletonFrameReadyEventArgs();

            // Create the Iava Equivalent
            IavaSkeletonFrameReadyEventArgs iavaEventArgs = new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)kinectEventArgs.OpenSkeletonFrame());

            // Test the object as a whole
            Assert.AreEqual(iavaEventArgs, (IavaSkeletonFrameReadyEventArgs)kinectEventArgs);

            // Set the Kinect Object to null
            kinectEventArgs = null;

            // Make sure we don't attempt to cast nulls
            Assert.IsNull((IavaSkeletonFrameReadyEventArgs)kinectEventArgs);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest() {
            try {
                IavaSkeleton[] skeletons = new IavaSkeleton[] { new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton(),
                                                                new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton() };

                IavaSkeletonFrame_Accessor accessor1 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = Tuple.Create(1.0f, 2.0f, 3.0f, 4.0f),
                    FrameNumber = 14,
                    Skeletons = skeletons,
                    Timestamp = 123456789
                };

                IavaSkeletonFrame_Accessor accessor2 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = Tuple.Create(1.0f, 2.0f, 3.0f, 4.0f),
                    FrameNumber = 14,
                    Skeletons = skeletons,
                    Timestamp = 123456789
                };

                IavaSkeletonFrame_Accessor accessor3 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = Tuple.Create(4.0f, 3.0f, 2.0f, 1.0f),
                    FrameNumber = 18,
                    Skeletons = skeletons,
                    Timestamp = 123456789
                };

                IavaSkeletonFrameReadyEventArgs eventArgs1 = new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)accessor1.Target);
                IavaSkeletonFrameReadyEventArgs eventArgs2 = new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)accessor2.Target);
                IavaSkeletonFrameReadyEventArgs eventArgs3 = new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)accessor3.Target);
                IavaSkeletonFrameReadyEventArgs eventArgs4 = new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)null);
                IavaSkeletonFrameReadyEventArgs eventArgs5 = new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)null);

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
                IavaSkeleton[] skeletons = new IavaSkeleton[] { new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton(),
                                                                new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton() };

                IavaSkeletonFrame_Accessor accessor1 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = Tuple.Create(1.0f, 2.0f, 3.0f, 4.0f),
                    FrameNumber = 14,
                    Skeletons = skeletons,
                    Timestamp = 123456789
                };

                IavaSkeletonFrame_Accessor accessor2 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = Tuple.Create(1.0f, 2.0f, 3.0f, 4.0f),
                    FrameNumber = 14,
                    Skeletons = skeletons,
                    Timestamp = 123456789
                };

                IavaSkeletonFrame_Accessor accessor3 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = Tuple.Create(4.0f, 3.0f, 2.0f, 1.0f),
                    FrameNumber = 18,
                    Skeletons = skeletons,
                    Timestamp = 123456789
                };

                IavaSkeletonFrameReadyEventArgs eventArgs1 = new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)accessor1.Target);
                IavaSkeletonFrameReadyEventArgs eventArgs2 = new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)accessor2.Target);
                IavaSkeletonFrameReadyEventArgs eventArgs3 = new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)accessor3.Target);
                IavaSkeletonFrameReadyEventArgs eventArgs4 = new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)null);
                IavaSkeletonFrameReadyEventArgs eventArgs5 = new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)null);

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
                IavaSkeleton[] skeletons = new IavaSkeleton[] { new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton(),
                                                                new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton() };

                IavaSkeletonFrame_Accessor accessor1 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = Tuple.Create(1.0f, 2.0f, 3.0f, 4.0f),
                    FrameNumber = 14,
                    Skeletons = skeletons,
                    Timestamp = 123456789
                };

                IavaSkeletonFrame_Accessor accessor2 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = Tuple.Create(1.0f, 2.0f, 3.0f, 4.0f),
                    FrameNumber = 14,
                    Skeletons = skeletons,
                    Timestamp = 123456789
                };

                IavaSkeletonFrame_Accessor accessor3 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = Tuple.Create(4.0f, 3.0f, 2.0f, 1.0f),
                    FrameNumber = 18,
                    Skeletons = skeletons,
                    Timestamp = 123456789
                };

                IavaSkeletonFrameReadyEventArgs eventArgs1 = new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)accessor1.Target);
                IavaSkeletonFrameReadyEventArgs eventArgs2 = new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)accessor2.Target);
                IavaSkeletonFrameReadyEventArgs eventArgs3 = new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)accessor3.Target);
                IavaSkeletonFrameReadyEventArgs eventArgs4 = new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)null);
                IavaSkeletonFrameReadyEventArgs eventArgs5 = new IavaSkeletonFrameReadyEventArgs((IavaSkeletonFrame)null);

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
                IavaSkeletonFrame skeletonFrame = CreateSkeletonFrame();

                IavaSkeletonFrameReadyEventArgs iavaEventArgs = new IavaSkeletonFrameReadyEventArgs(skeletonFrame);

                // Make sure the property set correctly
                Assert.AreEqual(skeletonFrame, iavaEventArgs.SkeletonFrame);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for SkeletonFrame
        ///</summary>
        [TestMethod()]
        public void SkeletonFrameTest() {
            try {
                IavaSkeletonFrameReadyEventArgs_Accessor iavaEventArgs = new IavaSkeletonFrameReadyEventArgs_Accessor(new IavaSkeletonFrame());

                IavaSkeletonFrame skeletonFrame = CreateSkeletonFrame();

                // Set the SkeletonFrame
                iavaEventArgs.SkeletonFrame = skeletonFrame;

                // Make sure the property set correctly
                Assert.AreEqual(skeletonFrame, iavaEventArgs.SkeletonFrame);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }
    }
}
