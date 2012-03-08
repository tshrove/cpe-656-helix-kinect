using Iava.Gesture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Iava.Input.Camera;
using System.Collections.Generic;

namespace Iava.Test
{
    
    
    /// <summary>
    ///This is a test class for SnapshotTest and is intended
    ///to contain all SnapshotTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SnapshotTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for Snapshot Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Iava.Gesture.dll")]
        public void SnapshotConstructorTest()
        {
            //Snapshot_Accessor target = new Snapshot_Accessor();
            //Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Snapshot Constructor
        ///</summary>
        [TestMethod()]
        public void SnapshotConstructorTest1()
        {
            //IavaSkeletonData skeleton = new IavaSkeletonData();
            //Snapshot target = new Snapshot(skeleton);
            //Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CheckSnapshot
        ///</summary>
        [TestMethod()]
        public void CheckSnapshotTest()
        {
            //Snapshot target = new Snapshot(); // TODO: Initialize to an appropriate value
            //IavaSkeletonData skeleton = null; // TODO: Initialize to an appropriate value
            //double fudgeFactor = 0F; // TODO: Initialize to an appropriate value
            //bool expected = false; // TODO: Initialize to an appropriate value
            //bool actual;
            //actual = target.CheckSnapshot(skeleton, fudgeFactor);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ClearTrackingJoints
        ///</summary>
        [TestMethod()]
        public void ClearTrackingJointsTest()
        {
            //Snapshot target = new Snapshot(); // TODO: Initialize to an appropriate value
            //target.ClearTrackingJoints();
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SetTrackingJoints
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Iava.Gesture.dll")]
        public void SetTrackingJointsTest()
        {
            //Snapshot_Accessor target = new Snapshot_Accessor(); // TODO: Initialize to an appropriate value
            //IavaJointID[] joints = new IavaJointID[] { IavaJointID.Head, IavaJointID.AnkleLeft };
            //target.SetTrackingJoints(joints);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for BodyParts
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Iava.Gesture.dll")]
        public void BodyPartsTest()
        {
            Snapshot_Accessor target = new Snapshot_Accessor();
            List<BodyPart> expected = new List<BodyPart>();
            List<BodyPart> actual;
            actual = target.BodyParts;
            Assert.AreNotEqual(actual, null);
            Assert.AreEqual(expected.Count, actual.Count);
        }
    }
}
