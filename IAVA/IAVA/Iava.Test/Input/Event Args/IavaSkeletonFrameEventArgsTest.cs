using System;
using System.Collections.Generic;
using Iava.Input.Camera;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iava.Test {

    /// <summary>
    ///This is a test class for IavaSkeletonFrameEventArgsTest and is intended
    ///to contain all IavaSkeletonFrameEventArgsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IavaSkeletonFrameEventArgsTest {

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest() {
            try {
                IavaSkeletonFrameEventArgs eventArgs1 = new IavaSkeletonFrameEventArgs(new List<int>() { 1, 2, 3 }, 123456789);
                IavaSkeletonFrameEventArgs eventArgs2 = new IavaSkeletonFrameEventArgs(new List<int>() { 1, 2, 3 }, 123456789);
                IavaSkeletonFrameEventArgs eventArgs3 = new IavaSkeletonFrameEventArgs(new List<int>() { 3, 2, 1 }, 987654321);

                // Make sure eventArgs1 does not equal null
                Assert.IsFalse(eventArgs1.Equals(null));

                // Make sure eventArgs1 does not equal a completly different object
                Assert.IsFalse(eventArgs1.Equals("Not a eventArgs."));

                // Make sure eventArgs1 and eventArgs3 are not equal
                Assert.IsFalse(eventArgs1.Equals(eventArgs3));

                // Make sure eventArgs1 and eventArgs2 are equal
                Assert.IsTrue(eventArgs1.Equals(eventArgs2));

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
                IavaSkeletonFrameEventArgs eventArgs1 = new IavaSkeletonFrameEventArgs(new List<int>() { 1, 2, 3 }, 123456789);
                IavaSkeletonFrameEventArgs eventArgs2 = new IavaSkeletonFrameEventArgs(new List<int>() { 1, 2, 3 }, 123456789);
                IavaSkeletonFrameEventArgs eventArgs3 = new IavaSkeletonFrameEventArgs(new List<int>() { 3, 2, 1 }, 987654321);

                // Make sure eventArgs1 does not equal null
                Assert.IsFalse(eventArgs1 == null);

                // Make sure null does not equal eventArgs1
                Assert.IsFalse(null == eventArgs1);

                // Make sure eventArgs1 and eventArgs3 are not equal
                Assert.IsFalse(eventArgs1 == eventArgs3);

                // Make sure eventArgs1 and eventArgs2 are equal
                Assert.IsTrue(eventArgs1 == eventArgs2);

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
                IavaSkeletonFrameEventArgs eventArgs1 = new IavaSkeletonFrameEventArgs(new List<int>() { 1, 2, 3 }, 123456789);
                IavaSkeletonFrameEventArgs eventArgs2 = new IavaSkeletonFrameEventArgs(new List<int>() { 1, 2, 3 }, 123456789);
                IavaSkeletonFrameEventArgs eventArgs3 = new IavaSkeletonFrameEventArgs(new List<int>() { 3, 2, 1 }, 987654321);

                // Make sure eventArgs1 does not equal null
                Assert.IsTrue(eventArgs1 != null);

                // Make sure null does not equal eventArgs1
                Assert.IsTrue(null != eventArgs1);

                // Make sure eventArgs1 and eventArgs3 are not equal
                Assert.IsTrue(eventArgs1 != eventArgs3);

                // Make sure eventArgs1 and eventArgs2 are equal
                Assert.IsFalse(eventArgs1 != eventArgs2);

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
                List<int> skeletonIDs = new List<int>() { 1, 2, 3 };
                long timestamp = 123456789;

                IavaSkeletonFrameEventArgs eventArgs = new IavaSkeletonFrameEventArgs(skeletonIDs, timestamp);

                // Make sure the properties got set correctly
                Assert.AreEqual(skeletonIDs, eventArgs.SkeletonIDs);
                Assert.AreEqual(timestamp, eventArgs.Timestamp);
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
                long timestamp = 123456789;

                IavaSkeletonFrameEventArgs_Accessor eventArgs = new IavaSkeletonFrameEventArgs_Accessor(new List<int>(), 0);

                // Set the Timestamp
                eventArgs.Timestamp = timestamp;

                // Make sure the property set correctly
                Assert.AreEqual(timestamp, eventArgs.Timestamp);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for SkeletonIDs
        ///</summary>
        [TestMethod()]
        public void SkeletonIDsTest() {
            try {
                List<int> skeletonIDs = new List<int>() { 1, 2, 3 };

                IavaSkeletonFrameEventArgs_Accessor eventArgs = new IavaSkeletonFrameEventArgs_Accessor(new List<int>(), 0);

                // Set the SkeletonIDs
                eventArgs.SkeletonIDs = skeletonIDs;

                // Make sure the property set correctly
                Assert.AreEqual(skeletonIDs, eventArgs.SkeletonIDs);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }
    }
}
