using System;
using System.Runtime.Serialization;
using Iava.Input.Camera;
using Microsoft.Kinect;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iava.Test {

    /// <summary>
    ///This is a test class for IavaJointCollectionTest and is intended
    ///to contain all IavaJointCollectionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IavaJointCollectionTest {

        /// <summary>
        ///A test for Explicit Cast
        ///</summary>
        [TestMethod()]
        public void ExplicitCastTest() {
            try {
                // Create the Kinect Object
                JointCollection kinectCollection = (JointCollection)FormatterServices.GetSafeUninitializedObject(typeof(JointCollection));
                PrivateObject po = new PrivateObject(kinectCollection);
                po.SetFieldOrProperty("_skeletonData", new Joint[20]);

                // Set some Joints
                // JointType is readonly hence the FUBAR way of doing this...
                po = new PrivateObject(new Joint());
                po.SetFieldOrProperty("JointType", JointType.AnkleLeft);
                po.SetFieldOrProperty("TrackingState", JointTrackingState.Inferred);
                kinectCollection[JointType.AnkleLeft] = (Joint)po.Target;
                po = new PrivateObject(new Joint());
                po.SetFieldOrProperty("JointType", JointType.AnkleRight);
                po.SetFieldOrProperty("TrackingState", JointTrackingState.NotTracked);
                kinectCollection[JointType.AnkleRight] = (Joint)po.Target;
                po = new PrivateObject(new Joint());
                po.SetFieldOrProperty("JointType", JointType.ElbowLeft);
                po.SetFieldOrProperty("TrackingState", JointTrackingState.Tracked);
                kinectCollection[JointType.ElbowLeft] = (Joint)po.Target;
                po = new PrivateObject(new Joint());
                po.SetFieldOrProperty("JointType", JointType.ElbowRight);
                po.SetFieldOrProperty("TrackingState", JointTrackingState.NotTracked);
                kinectCollection[JointType.ElbowRight] = (Joint)po.Target;

                // Create the Iava equivalent
                IavaJointCollection iavaCollection = new IavaJointCollection();
                iavaCollection[IavaJointType.AnkleLeft] = new IavaJoint() { JointType = IavaJointType.AnkleLeft, TrackingState = IavaJointTrackingState.Inferred };
                iavaCollection[IavaJointType.AnkleRight] = new IavaJoint() { JointType = IavaJointType.AnkleRight, TrackingState = IavaJointTrackingState.NotTracked };
                iavaCollection[IavaJointType.ElbowLeft] = new IavaJoint() { JointType = IavaJointType.ElbowLeft, TrackingState = IavaJointTrackingState.Tracked };
                iavaCollection[IavaJointType.ElbowRight] = new IavaJoint() { JointType = IavaJointType.ElbowRight, TrackingState = IavaJointTrackingState.NotTracked };

                // Test object as a whole
                Assert.AreEqual(iavaCollection, (IavaJointCollection)kinectCollection);

                // Set the Kinect Object to null
                kinectCollection = null;

                // Make sure we don't attempt to cast nulls
                Assert.IsNull((IavaJointCollection)kinectCollection);
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
                IavaJointCollection collection1 = new IavaJointCollection();
                collection1[IavaJointType.AnkleLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.Inferred };
                collection1[IavaJointType.AnkleRight] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };
                collection1[IavaJointType.ElbowLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.Tracked };
                collection1[IavaJointType.ElbowRight] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };

                IavaJointCollection collection2 = new IavaJointCollection();
                collection2[IavaJointType.AnkleLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.Inferred };
                collection2[IavaJointType.AnkleRight] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };
                collection2[IavaJointType.ElbowLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.Tracked };
                collection2[IavaJointType.ElbowRight] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };

                IavaJointCollection collection3 = new IavaJointCollection();
                collection3[IavaJointType.HipCenter] = new IavaJoint() { TrackingState = IavaJointTrackingState.Inferred };
                collection3[IavaJointType.AnkleRight] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };
                collection3[IavaJointType.ElbowLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.Tracked };
                collection3[IavaJointType.Spine] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };

                // Make sure collection1 does not equal null
                Assert.IsFalse(collection1.Equals(null));

                // Make sure eventArgs1 does not equal a completly different object
                Assert.IsFalse(collection1.Equals("Not a collection."));

                // Make sure collection1 and collection3 are not equal
                Assert.IsFalse(collection1.Equals(collection3));

                // Make sure collection1 and collection2 are equal
                Assert.IsTrue(collection1.Equals(collection2));

                // Make sure collection1 equals itself
                Assert.IsTrue(collection1.Equals(collection1));
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
                IavaJointCollection collection1 = new IavaJointCollection();
                collection1[IavaJointType.AnkleLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.Inferred };
                collection1[IavaJointType.AnkleRight] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };
                collection1[IavaJointType.ElbowLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.Tracked };
                collection1[IavaJointType.ElbowRight] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };

                IavaJointCollection collection2 = new IavaJointCollection();
                collection2[IavaJointType.AnkleLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.Inferred };
                collection2[IavaJointType.AnkleRight] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };
                collection2[IavaJointType.ElbowLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.Tracked };
                collection2[IavaJointType.ElbowRight] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };

                IavaJointCollection collection3 = new IavaJointCollection();
                collection3[IavaJointType.HipCenter] = new IavaJoint() { TrackingState = IavaJointTrackingState.Inferred };
                collection3[IavaJointType.AnkleRight] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };
                collection3[IavaJointType.ElbowLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.Tracked };
                collection3[IavaJointType.Spine] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };

                // Make sure collection1 does not equal null
                Assert.IsFalse(collection1 == null);

                // Make sure null does not equal collection1
                Assert.IsFalse(null == collection1);

                // Make sure collection1 and collection3 are not equal
                Assert.IsFalse(collection1 == collection3);

                // Make sure collection1 and collection2 are equal
                Assert.IsTrue(collection1 == collection2);

                // Make sure collection1 equals itself
                Assert.IsTrue(collection1 == collection1);
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
                IavaJointCollection collection1 = new IavaJointCollection();
                collection1[IavaJointType.AnkleLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.Inferred };
                collection1[IavaJointType.AnkleRight] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };
                collection1[IavaJointType.ElbowLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.Tracked };
                collection1[IavaJointType.ElbowRight] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };

                IavaJointCollection collection2 = new IavaJointCollection();
                collection2[IavaJointType.AnkleLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.Inferred };
                collection2[IavaJointType.AnkleRight] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };
                collection2[IavaJointType.ElbowLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.Tracked };
                collection2[IavaJointType.ElbowRight] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };

                IavaJointCollection collection3 = new IavaJointCollection();
                collection3[IavaJointType.HipCenter] = new IavaJoint() { TrackingState = IavaJointTrackingState.Inferred };
                collection3[IavaJointType.AnkleRight] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };
                collection3[IavaJointType.ElbowLeft] = new IavaJoint() { TrackingState = IavaJointTrackingState.Tracked };
                collection3[IavaJointType.Spine] = new IavaJoint() { TrackingState = IavaJointTrackingState.NotTracked };

                // Make sure collection1 does not equal null
                Assert.IsTrue(collection1 != null);

                // Make sure null does not equal collection1
                Assert.IsTrue(null != collection1);

                // Make sure collection1 and collection3 are not equal
                Assert.IsTrue(collection1 != collection3);

                // Make sure collection1 and collection2 are equal
                Assert.IsFalse(collection1 != collection2);

                // Make sure collection1 equals itself
                Assert.IsFalse(collection1 != collection1);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for IavaJointCollection Constructor
        ///</summary>
        [TestMethod()]
        public void ConstructorTest() {
            try {
                IavaJointCollection jointCollection = new IavaJointCollection();

                // Make sure there is an element for every joint type
                Assert.AreEqual((int)IavaJointType.Count, jointCollection.Count);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for GetEnumerator
        ///</summary>
        [TestMethod()]
        public void GetEnumeratorTest() {
            try {
                IavaJointCollection collection = new IavaJointCollection();

                // Make sure we are getting an Enumerator
                Assert.IsNotNull(collection.GetEnumerator());
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for Count
        ///</summary>
        [TestMethod()]
        public void CountTest() {
            try {
                IavaJointCollection_Accessor jointCollection = new IavaJointCollection_Accessor();

                // Count is a getter for this private field
                jointCollection._joints = new IavaJoint[50];

                // Make sure the property set correctly
                Assert.AreEqual(50, jointCollection.Count);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest() {
            try {
                IavaJointCollection jointCollection = new IavaJointCollection();

                IavaJoint joint = new IavaJoint() { TrackingState = IavaJointTrackingState.Inferred };

                jointCollection[IavaJointType.AnkleLeft] = joint;

                Assert.AreEqual(joint, jointCollection[IavaJointType.AnkleLeft]);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }
    }
}
