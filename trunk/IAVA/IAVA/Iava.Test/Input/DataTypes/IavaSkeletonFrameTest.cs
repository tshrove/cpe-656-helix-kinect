using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Iava.Input.Camera;
using Microsoft.Kinect;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iava.Test {

    /// <summary>
    ///This is a test class for IavaSkeletonFrameTest and is intended
    ///to contain all IavaSkeletonFrameTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IavaSkeletonFrameTest {

        #region Helper Methods

        /// <summary>
        /// Handles the job of initializing a SkeletonFrame that can be used by the Unit Tests
        /// </summary>
        /// <returns></returns>
        SkeletonFrame CreateSkeletonFrame() {
            // Get an uninitialized version of the SkeletonFrame
            SkeletonFrame kinectFrame = (SkeletonFrame)FormatterServices.GetSafeUninitializedObject(typeof(SkeletonFrame));

            // Find the Assembly containing the EntryPrivate object
            Assembly kinect = Assembly.Load("Microsoft.Kinect, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
            Type entryType = kinect.GetType("Microsoft.Kinect.DataPool`4+EntryPrivate");

            // Set the generic types to the actual types needed by the SkeletonFrame
            entryType = entryType.MakeGenericType(typeof(int), typeof(Tuple<float, float, float, float>), typeof(object), typeof(Skeleton[]));

            // Create an EntryPrivate object
            object entry = entryType.GetConstructor(new Type[] { }).Invoke(new object[] { });

            // Create an Array of skeletons so we can set it as one of the Entry properties
            Skeleton[] skeletons = new Skeleton[] { new Skeleton(), new Skeleton(), new Skeleton(),
                                                    new Skeleton(), new Skeleton(), new Skeleton() };

            // Set the EntryPrivate properties
            PropertyInfo[] prop = entry.GetType().GetProperties();
            prop[3].SetValue(entry, 0, null);                                                   // Key
            prop[4].SetValue(entry, new Tuple<float, float, float, float>(0, 0, 0, 0), null);   // Value1
            prop[5].SetValue(entry, null, null);                                                // Value2
            prop[6].SetValue(entry, skeletons, null);                                           // Value3

            // Set the kinectFrame properties
            prop = kinectFrame.GetType().GetProperties();
            prop[0].SetValue(kinectFrame, 123456789, null);                                     // Timestamp
            prop[1].SetValue(kinectFrame, 31, null);                                            // FrameNumber
            prop[2].SetValue(kinectFrame, Tuple.Create(1.0f, 2.0f, 3.0f, 4.0f), null);          // FloorClipPlane
            //prop[3].SetValue(kinectFrame, 0, null);                                             // SkeletonArrayLength

            // Set the private fields on the kinectFrame
            PrivateObject po = new PrivateObject(kinectFrame);
            po.SetFieldOrProperty("_skeletonData", entry);
            po.SetFieldOrProperty("_dataAccessLock", new object());

            // Return the SkeletonFrame
            return(SkeletonFrame)po.Target;
        }

        #endregion Helper Methods

        /// <summary>
        ///A test for Explicit Cast
        ///</summary>
        [TestMethod()]
        public void ExplicitCastTest() {
            try {
                // Create the Kinect Object
                SkeletonFrame kinectFrame = CreateSkeletonFrame();

                // Create the Iava Equivalent
                IavaSkeletonFrame_Accessor accessor = new IavaSkeletonFrame_Accessor();
                accessor.FloorClipPlane = kinectFrame.FloorClipPlane;
                accessor.FrameNumber = kinectFrame.FrameNumber;
                accessor.Skeletons = new IavaSkeleton[kinectFrame.SkeletonArrayLength];
                accessor.Timestamp = kinectFrame.Timestamp;

                // Init the skeletons...
                for (int i = 0; i < accessor.Skeletons.Length; i++) { accessor.Skeletons[i] = new IavaSkeleton(); }

                // Get the Iava object
                IavaSkeletonFrame iavaFrame = (IavaSkeletonFrame)accessor.Target;

                // Test the object as a whole
                Assert.AreEqual(iavaFrame, (IavaSkeletonFrame)kinectFrame);

                // Set the Kinect Object to null
                kinectFrame = null;

                // Make sure we don't attempt to cast nulls
                Assert.IsNull((IavaSkeletonFrame)kinectFrame);
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
                // Define once to populate the various objects
                IavaSkeleton[] skeletonArray = new IavaSkeleton[] { new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton(),
                                                                    new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton()};

                IavaSkeletonFrame_Accessor accessor1 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = new Tuple<float,float,float,float>(1.0f,2.0f,2.5f, 0.3f),
                    FrameNumber = 456,
                    Skeletons = skeletonArray,
                    Timestamp = 123456789
                };

                IavaSkeletonFrame_Accessor accessor2 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = new Tuple<float, float, float, float>(1.0f, 2.0f, 2.5f, 0.3f),
                    FrameNumber = 456,
                    Skeletons = skeletonArray,
                    Timestamp = 123456789
                };

                IavaSkeletonFrame_Accessor accessor3 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = new Tuple<float, float, float, float>(7.0f, 2.1f, 2.5f, 0.3f),
                    FrameNumber = 487,
                    Skeletons = skeletonArray,
                    Timestamp = 123456789
                };

                IavaSkeletonFrame frame1 = (IavaSkeletonFrame)accessor1.Target;
                IavaSkeletonFrame frame2 = (IavaSkeletonFrame)accessor2.Target;
                IavaSkeletonFrame frame3 = (IavaSkeletonFrame)accessor3.Target;

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
                // Define once to populate the various objects
                IavaSkeleton[] skeletonArray = new IavaSkeleton[] { new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton(),
                                                                    new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton()};

                IavaSkeletonFrame_Accessor accessor1 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = new Tuple<float, float, float, float>(1.0f, 2.0f, 2.5f, 0.3f),
                    FrameNumber = 456,
                    Skeletons = skeletonArray,
                    Timestamp = 123456789
                };

                IavaSkeletonFrame_Accessor accessor2 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = new Tuple<float, float, float, float>(1.0f, 2.0f, 2.5f, 0.3f),
                    FrameNumber = 456,
                    Skeletons = skeletonArray,
                    Timestamp = 123456789
                };

                IavaSkeletonFrame_Accessor accessor3 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = new Tuple<float, float, float, float>(7.0f, 2.1f, 2.5f, 0.3f),
                    FrameNumber = 487,
                    Skeletons = skeletonArray,
                    Timestamp = 123456789
                };

                IavaSkeletonFrame frame1 = (IavaSkeletonFrame)accessor1.Target;
                IavaSkeletonFrame frame2 = (IavaSkeletonFrame)accessor2.Target;
                IavaSkeletonFrame frame3 = (IavaSkeletonFrame)accessor3.Target;

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
                // Define once to populate the various objects
                IavaSkeleton[] skeletonArray = new IavaSkeleton[] { new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton(),
                                                                    new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton()};

                IavaSkeletonFrame_Accessor accessor1 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = new Tuple<float, float, float, float>(1.0f, 2.0f, 2.5f, 0.3f),
                    FrameNumber = 456,
                    Skeletons = skeletonArray,
                    Timestamp = 123456789
                };

                IavaSkeletonFrame_Accessor accessor2 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = new Tuple<float, float, float, float>(1.0f, 2.0f, 2.5f, 0.3f),
                    FrameNumber = 456,
                    Skeletons = skeletonArray,
                    Timestamp = 123456789
                };

                IavaSkeletonFrame_Accessor accessor3 = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = new Tuple<float, float, float, float>(7.0f, 2.1f, 2.5f, 0.3f),
                    FrameNumber = 487,
                    Skeletons = skeletonArray,
                    Timestamp = 123456789
                };

                IavaSkeletonFrame frame1 = (IavaSkeletonFrame)accessor1.Target;
                IavaSkeletonFrame frame2 = (IavaSkeletonFrame)accessor2.Target;
                IavaSkeletonFrame frame3 = (IavaSkeletonFrame)accessor3.Target;

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
        ///A test for IavaSkeletonFrame Constructor
        ///</summary>
        [TestMethod()]
        public void ConstructorTest() {
            // Nothing to do...
            //IavaSkeletonFrame target = new IavaSkeletonFrame();
            //Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        public void DisposeTest() {
            // Nothing to do...
            //IavaSkeletonFrame target = new IavaSkeletonFrame(); // TODO: Initialize to an appropriate kinectFrame
            //target.Dispose();
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetActiveSkeleton
        ///</summary>
        [TestMethod()]
        public void GetActiveSkeletonTest() {
            try {
                IavaSkeleton skeleton = new IavaSkeleton() { Position = new Iava.Core.Math.IavaSkeletonPoint() { X = 1.0, Y = 1.0, Z = 1.0 } };

                // Define once to populate the various objects
                IavaSkeleton[] skeletonArray = new IavaSkeleton[] { new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton(),
                                                                    new IavaSkeleton(), new IavaSkeleton(), skeleton };

                IavaSkeletonFrame_Accessor accessor = new IavaSkeletonFrame_Accessor()
                {
                    FloorClipPlane = new Tuple<float, float, float, float>(1.0f, 2.0f, 2.5f, 0.3f),
                    FrameNumber = 456,
                    Skeletons = skeletonArray,
                    Timestamp = 123456789
                };

                // Make sure the ActiveSkeleton is our skeleton
                Assert.AreEqual(skeleton, accessor.GetActiveSkeleton());

                // Make sure the property calls the correct method
                Assert.AreEqual(skeleton, accessor.ActiveSkeleton);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for FloorClipPlane
        ///</summary>
        [TestMethod()]
        public void FloorClipPlaneTest() {
            try {
                IavaSkeletonFrame_Accessor iavaFrame = new IavaSkeletonFrame_Accessor();

                Tuple<float,float,float,float> clipPlane = new Tuple<float,float,float,float>(0.3f, 1.0f, 2.5f, 3.0f);

                // Set the FloorClipPlane
                iavaFrame.FloorClipPlane = clipPlane;

                // Make sure the property set correctly
                Assert.AreEqual(clipPlane, iavaFrame.FloorClipPlane);
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
                IavaSkeletonFrame_Accessor iavaFrame = new IavaSkeletonFrame_Accessor();

                // Set the FrameNumber
                iavaFrame.FrameNumber = 480;

                // Make sure the property set correctly
                Assert.AreEqual(480, iavaFrame.FrameNumber);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for Skeletons
        ///</summary>
        [TestMethod()]
        public void SkeletonsTest() {
            try {
                IavaSkeletonFrame_Accessor iavaFrame = new IavaSkeletonFrame_Accessor();

                IavaSkeleton[] skeletonArray = new IavaSkeleton[] { new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton(),
                                                                    new IavaSkeleton(), new IavaSkeleton(), new IavaSkeleton()};

                // Set the Skeletons
                iavaFrame.Skeletons = skeletonArray;

                // Make sure the property set correctly
                Assert.IsTrue(iavaFrame.Skeletons.SequenceEqual(skeletonArray));
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
                IavaSkeletonFrame_Accessor iavaFrame = new IavaSkeletonFrame_Accessor();

                // Set the Timestamp
                iavaFrame.Timestamp = 123456789;

                // Make sure the property set correctly
                Assert.AreEqual(123456789, iavaFrame.Timestamp);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }
    }
}
