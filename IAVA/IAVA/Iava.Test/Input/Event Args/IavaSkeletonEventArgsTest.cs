using System;
using Iava.Core.Math;
using Iava.Input.Camera;
using Microsoft.Kinect;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iava.Test {

    /// <summary>
    ///This is a test class for IavaSkeletonEventArgsTest and is intended
    ///to contain all IavaSkeletonEventArgsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IavaSkeletonEventArgsTest {

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest() {
            try {
                IavaSkeleton_Accessor accessor1 = new IavaSkeleton_Accessor()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 531,
                    TrackingState = IavaSkeletonTrackingState.Tracked
                };

                IavaSkeleton_Accessor accessor2 = new IavaSkeleton_Accessor()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 531,
                    TrackingState = IavaSkeletonTrackingState.Tracked
                };

                IavaSkeleton_Accessor accessor3 = new IavaSkeleton_Accessor()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 3.0f, Y = 2.0f, Z = 1.0f },
                    TrackingId = 450,
                    TrackingState = IavaSkeletonTrackingState.Tracked
                };

                IavaSkeletonEventArgs eventArgs1 = new IavaSkeletonEventArgs((IavaSkeleton)accessor1.Target);
                IavaSkeletonEventArgs eventArgs2 = new IavaSkeletonEventArgs((IavaSkeleton)accessor2.Target);
                IavaSkeletonEventArgs eventArgs3 = new IavaSkeletonEventArgs((IavaSkeleton)accessor3.Target);
                IavaSkeletonEventArgs eventArgs4 = new IavaSkeletonEventArgs((IavaSkeleton)null);
                IavaSkeletonEventArgs eventArgs5 = new IavaSkeletonEventArgs((IavaSkeleton)null);

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
                IavaSkeleton_Accessor accessor1 = new IavaSkeleton_Accessor()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 531,
                    TrackingState = IavaSkeletonTrackingState.Tracked
                };

                IavaSkeleton_Accessor accessor2 = new IavaSkeleton_Accessor()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 531,
                    TrackingState = IavaSkeletonTrackingState.Tracked
                };

                IavaSkeleton_Accessor accessor3 = new IavaSkeleton_Accessor()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 3.0f, Y = 2.0f, Z = 1.0f },
                    TrackingId = 450,
                    TrackingState = IavaSkeletonTrackingState.Tracked
                };

                IavaSkeletonEventArgs eventArgs1 = new IavaSkeletonEventArgs((IavaSkeleton)accessor1.Target);
                IavaSkeletonEventArgs eventArgs2 = new IavaSkeletonEventArgs((IavaSkeleton)accessor2.Target);
                IavaSkeletonEventArgs eventArgs3 = new IavaSkeletonEventArgs((IavaSkeleton)accessor3.Target);
                IavaSkeletonEventArgs eventArgs4 = new IavaSkeletonEventArgs((IavaSkeleton)null);
                IavaSkeletonEventArgs eventArgs5 = new IavaSkeletonEventArgs((IavaSkeleton)null);

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
                IavaSkeleton_Accessor accessor1 = new IavaSkeleton_Accessor()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 531,
                    TrackingState = IavaSkeletonTrackingState.Tracked
                };

                IavaSkeleton_Accessor accessor2 = new IavaSkeleton_Accessor()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 531,
                    TrackingState = IavaSkeletonTrackingState.Tracked
                };

                IavaSkeleton_Accessor accessor3 = new IavaSkeleton_Accessor()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 3.0f, Y = 2.0f, Z = 1.0f },
                    TrackingId = 450,
                    TrackingState = IavaSkeletonTrackingState.Tracked
                };

                IavaSkeletonEventArgs eventArgs1 = new IavaSkeletonEventArgs((IavaSkeleton)accessor1.Target);
                IavaSkeletonEventArgs eventArgs2 = new IavaSkeletonEventArgs((IavaSkeleton)accessor2.Target);
                IavaSkeletonEventArgs eventArgs3 = new IavaSkeletonEventArgs((IavaSkeleton)accessor3.Target);
                IavaSkeletonEventArgs eventArgs4 = new IavaSkeletonEventArgs((IavaSkeleton)null);
                IavaSkeletonEventArgs eventArgs5 = new IavaSkeletonEventArgs((IavaSkeleton)null);

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
        ///A test for IavaSkeletonEventArgs Constructor
        ///</summary>
        [TestMethod()]
        public void ConstructorTest1() {
            try {
                IavaSkeleton skeleton = new IavaSkeleton()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 531,
                    TrackingState = IavaSkeletonTrackingState.Tracked
                };

                // Create the object
                IavaSkeletonEventArgs iavaEventArgs = new IavaSkeletonEventArgs(skeleton);

                // Make sure the property set correctly
                Assert.AreEqual(skeleton, iavaEventArgs.Skeleton);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for IavaSkeletonEventArgs Constructor
        ///</summary>
        [TestMethod()]
        public void ConstructorTest2() {
            try {
                Skeleton kinectSkeleton = new Skeleton()
                {
                    ClippedEdges = FrameEdges.Left,
                    Position = new SkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 531,
                    TrackingState = SkeletonTrackingState.Tracked
                };

                // Create the object
                IavaSkeletonEventArgs iavaEventArgs = new IavaSkeletonEventArgs(kinectSkeleton);

                // Constructor will cast this to a IavaSkeleton
                IavaSkeleton iavaSkeleton = (IavaSkeleton)kinectSkeleton;

                // Make sure the property set correctly
                Assert.AreEqual(iavaSkeleton, iavaEventArgs.Skeleton);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for Skeleton
        ///</summary>
        [TestMethod()]
        public void SkeletonTest() {
            try {
                IavaSkeletonEventArgs_Accessor iavaEventArgs = new IavaSkeletonEventArgs_Accessor(new IavaSkeleton());

                IavaSkeleton skeleton = new IavaSkeleton()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 531,
                    TrackingState = IavaSkeletonTrackingState.Tracked
                };

                // Set the ImageFrame
                iavaEventArgs.Skeleton = skeleton;

                // Make sure the property set correctly
                Assert.AreEqual(skeleton, iavaEventArgs.Skeleton);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }
    }
}
