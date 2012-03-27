using System;
using Iava.Core.Math;
using Iava.Input.Camera;
using Microsoft.Kinect;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iava.Test {

    /// <summary>
    ///This is a test class for IavaJointTest and is intended
    ///to contain all IavaJointTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IavaJointTest {

        /// <summary>
        ///A test for Explicit Cast
        ///</summary>
        [TestMethod()]
        public void ExplicitCastTest() {
            try {
                // Create the Kinect object
                Joint kinectJoint = new Joint();
                PrivateObject po = new PrivateObject(kinectJoint);

                // Set the read only fields 
                po.SetFieldOrProperty("JointType", JointType.ElbowLeft);
                po.SetFieldOrProperty("TrackingState", JointTrackingState.Inferred);

                // Cast the PrivateObject back to its Kinect equivalent
                kinectJoint = (Joint)po.Target;

                // Create the Iava Equivalent
                IavaJoint iavaJoint = new IavaJoint();
                iavaJoint.JointType = IavaJointType.ElbowLeft;
                iavaJoint.TrackingState = IavaJointTrackingState.Inferred;

                // Test object as a whole
                Assert.AreEqual(iavaJoint, (IavaJoint)kinectJoint);
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
                IavaJoint joint1 = new IavaJoint() { JointType = IavaJointType.HandLeft, TrackingState = IavaJointTrackingState.NotTracked };
                IavaJoint joint2 = new IavaJoint() { JointType = IavaJointType.HandLeft, TrackingState = IavaJointTrackingState.NotTracked };
                IavaJoint joint3 = new IavaJoint() { JointType = IavaJointType.KneeLeft, TrackingState = IavaJointTrackingState.Inferred };

                // Make sure joint1 does not equal null
                Assert.IsFalse(joint1.Equals(null));

                // Make sure joint1 does not equal a completly different object
                Assert.IsFalse(joint1.Equals("Not a joint."));

                // Make sure joint1 and joint3 are not equal
                Assert.IsFalse(joint1.Equals(joint3));

                // Make sure joint1 and joint2 are equal
                Assert.IsTrue(joint1.Equals(joint2));

                // Make sure joint1 equals itself
                Assert.IsTrue(joint1.Equals(joint1));
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
                IavaJoint joint1 = new IavaJoint() { JointType = IavaJointType.HandLeft, TrackingState = IavaJointTrackingState.NotTracked };
                IavaJoint joint2 = new IavaJoint() { JointType = IavaJointType.HandLeft, TrackingState = IavaJointTrackingState.NotTracked };
                IavaJoint joint3 = new IavaJoint() { JointType = IavaJointType.KneeLeft, TrackingState = IavaJointTrackingState.Inferred };

                // Make sure joint1 does not equal null
                Assert.IsFalse(joint1 == null);

                // Make sure joint1 and joint3 are not equal
                Assert.IsFalse(joint1 == joint3);

                // Make sure joint1 and joint2 are equal
                Assert.IsTrue(joint1 == joint2);

                // Make sure joint1 equals itself
                Assert.IsTrue(joint1 == joint1);
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
                IavaJoint joint1 = new IavaJoint() { JointType = IavaJointType.HandLeft, TrackingState = IavaJointTrackingState.NotTracked };
                IavaJoint joint2 = new IavaJoint() { JointType = IavaJointType.HandLeft, TrackingState = IavaJointTrackingState.NotTracked };
                IavaJoint joint3 = new IavaJoint() { JointType = IavaJointType.KneeLeft, TrackingState = IavaJointTrackingState.Inferred };

                // Make sure joint1 does not equal null
                Assert.IsTrue(joint1 != null);

                // Make sure joint1 and joint3 are not equal
                Assert.IsTrue(joint1 != joint3);

                // Make sure joint1 and joint2 are equal
                Assert.IsFalse(joint1 != joint2);

                // Make sure joint1 equals itself
                Assert.IsFalse(joint1 != joint1);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for JointType
        ///</summary>
        [TestMethod()]
        public void JointTypeTest() {
            try {
                IavaJoint joint = new IavaJoint();

                // Set the JointType
                joint.JointType = IavaJointType.ShoulderLeft;

                // Make sure the property set correctly
                Assert.AreEqual(IavaJointType.ShoulderLeft, joint.JointType);
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
                IavaJoint joint = new IavaJoint();

                IavaSkeletonPoint position = new IavaSkeletonPoint() { X = 0, Y = -1.3, Z = 7.8 };

                // Set the Position
                joint.Position = position;

                // Make sure the property set correctly
                Assert.AreEqual(position, joint.Position);
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
                IavaJoint joint = new IavaJoint();

                // Set the TrackingState
                joint.TrackingState = IavaJointTrackingState.NotTracked;

                // Make sure the property set correctly
                Assert.AreEqual(IavaJointTrackingState.NotTracked, joint.TrackingState);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }
    }
}
