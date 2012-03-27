using Iava.Input.Camera;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Kinect;
using Iava.Core.Math;

namespace Iava.Test {

    /// <summary>
    ///This is a test class for IavaSkeletonTest and is intended
    ///to contain all IavaSkeletonTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IavaSkeletonTest {

        /// <summary>
        ///A test for op_Explicit
        ///</summary>
        [TestMethod()]
        public void ExplicitCastTest() {
            try {
                Skeleton kinectSkeleton = new Skeleton()
                {
                    ClippedEdges = FrameEdges.Left,
                    Position = new SkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 51,
                    TrackingState = SkeletonTrackingState.PositionOnly
                };

                IavaSkeleton iavaSkeleton = new IavaSkeleton()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 51,
                    TrackingState = IavaSkeletonTrackingState.PositionOnly
                };

                // Test object as a whole
                Assert.AreEqual(iavaSkeleton, (IavaSkeleton)kinectSkeleton);
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
                IavaSkeleton skeleton1 = new IavaSkeleton()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 51,
                    TrackingState = IavaSkeletonTrackingState.PositionOnly
                };

                IavaSkeleton skeleton2 = new IavaSkeleton()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 51,
                    TrackingState = IavaSkeletonTrackingState.PositionOnly
                };

                IavaSkeleton skeleton3 = new IavaSkeleton()
                {
                    ClippedEdges = IavaFrameEdges.Right,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 48,
                    TrackingState = IavaSkeletonTrackingState.Tracked
                };

                // Make sure skeleton1 does not equal null
                Assert.IsFalse(skeleton1.Equals(null));

                // Make sure skeleton1 does not equal a completly different object
                Assert.IsFalse(skeleton1.Equals("Not a skeleton."));

                // Make sure skeleton1 and skeleton3 are not equal
                Assert.IsFalse(skeleton1.Equals(skeleton3));

                // Make sure skeleton1 and skeleton2 are equal
                Assert.IsTrue(skeleton1.Equals(skeleton2));

                // Make sure skeleton1 equals itself
                Assert.IsTrue(skeleton1.Equals(skeleton1));
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
                IavaSkeleton skeleton1 = new IavaSkeleton()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 51,
                    TrackingState = IavaSkeletonTrackingState.PositionOnly
                };

                IavaSkeleton skeleton2 = new IavaSkeleton()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 51,
                    TrackingState = IavaSkeletonTrackingState.PositionOnly
                };

                IavaSkeleton skeleton3 = new IavaSkeleton()
                {
                    ClippedEdges = IavaFrameEdges.Right,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 48,
                    TrackingState = IavaSkeletonTrackingState.Tracked
                };

                // Make sure skeleton1 does not equal null
                Assert.IsFalse(skeleton1 == null);

                // Make sure skeleton1 and skeleton3 are not equal
                Assert.IsFalse(skeleton1 == skeleton3);

                // Make sure skeleton1 and skeleton2 are equal
                Assert.IsTrue(skeleton1 == skeleton2);

                // Make sure skeleton1 equals itself
                Assert.IsTrue(skeleton1 == skeleton1);
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
                IavaSkeleton skeleton1 = new IavaSkeleton()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 51,
                    TrackingState = IavaSkeletonTrackingState.PositionOnly
                };

                IavaSkeleton skeleton2 = new IavaSkeleton()
                {
                    ClippedEdges = IavaFrameEdges.Left,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 51,
                    TrackingState = IavaSkeletonTrackingState.PositionOnly
                };

                IavaSkeleton skeleton3 = new IavaSkeleton()
                {
                    ClippedEdges = IavaFrameEdges.Right,
                    Position = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f },
                    TrackingId = 48,
                    TrackingState = IavaSkeletonTrackingState.Tracked
                };

                // Make sure skeleton1 does not equal null
                Assert.IsTrue(skeleton1 != null);

                // Make sure skeleton1 and skeleton3 are not equal
                Assert.IsTrue(skeleton1 != skeleton3);

                // Make sure skeleton1 and skeleton2 are equal
                Assert.IsFalse(skeleton1 != skeleton2);

                // Make sure skeleton1 equals itself
                Assert.IsFalse(skeleton1 != skeleton1);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for IavaSkeleton Constructor
        ///</summary>
        [TestMethod()]
        public void IavaSkeletonConstructorTest() {
            try {
                IavaSkeleton skeleton = new IavaSkeleton();

                // Verify the joints are initialized properly
                for (IavaJointType joint = 0; joint < IavaJointType.Count; joint++) {
                    Assert.AreEqual(joint, skeleton.Joints[joint].JointType);
                }
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for ClippedEdges
        ///</summary>
        [TestMethod()]
        public void ClippedEdgesTest() {
            try {
                IavaSkeleton skeleton = new IavaSkeleton();

                // Set the ClippedEdges
                skeleton.ClippedEdges = IavaFrameEdges.Bottom;

                // Make sure the property set correctly
                Assert.AreEqual(IavaFrameEdges.Bottom, skeleton.ClippedEdges);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for Joints
        ///</summary>
        [TestMethod()]
        public void JointsTest() {
            try {
                IavaSkeleton skeleton = new IavaSkeleton();

                IavaJointCollection jointCollection = new IavaJointCollection();
                jointCollection[IavaJointType.ElbowLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };
                jointCollection[IavaJointType.FootRight] = new IavaJoint() { TrackingState = IavaJointTrackingState.Tracked };
                jointCollection[IavaJointType.Head] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };
                jointCollection[IavaJointType.KneeLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.Inferred };

                // Set the Joints
                skeleton.Joints = jointCollection;

                // Make sure the property set correctly
                Assert.AreEqual(jointCollection, skeleton.Joints);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for Position
        ///</summary>
        [TestMethod()]
        public void PositionTest() {
            try {
                IavaSkeleton skeleton = new IavaSkeleton();

                IavaSkeletonPoint point = new IavaSkeletonPoint() { X = 1.0f, Y = 2.0f, Z = 3.0f };

                // Set the Position
                skeleton.Position = point;

                // Make sure the property set correctly
                Assert.AreEqual(point, skeleton.Position);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for TrackingId
        ///</summary>
        [TestMethod()]
        public void TrackingIdTest() {
            try {
                IavaSkeleton skeleton = new IavaSkeleton();

                // Set the TrackingId
                skeleton.TrackingId = 405;

                // Make sure the property set correctly
                Assert.AreEqual(405, skeleton.TrackingId);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for TrackingState
        ///</summary>
        [TestMethod()]
        public void TrackingStateTest() {
            try {
                IavaSkeleton skeleton = new IavaSkeleton();

                // Set the TrackingState
                skeleton.TrackingState = IavaSkeletonTrackingState.NotTracked;

                // Make sure the property set correctly
                Assert.AreEqual(IavaSkeletonTrackingState.NotTracked, skeleton.TrackingState);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }
    }
}
