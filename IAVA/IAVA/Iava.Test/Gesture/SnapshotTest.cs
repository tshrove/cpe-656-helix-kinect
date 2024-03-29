﻿using Iava.Gesture;
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
        ///A test for IavaSnapshot Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Iava.Gesture.dll")]
        public void SnapshotConstructorTest()
        {
            //Snapshot_Accessor collection1 = new Snapshot_Accessor();
            //Assert.Inconclusive("TODO: Implement code to verify collection1");
        }

        /// <summary>
        ///A test for IavaSnapshot Constructor
        ///</summary>
        [TestMethod()]
        public void SnapshotConstructorTest1()
        {
            //IavaSkeleton skeleton = new IavaSkeleton();
            //IavaSnapshot collection1 = new IavaSnapshot(skeleton);
            //Assert.Inconclusive("TODO: Implement code to verify collection1");
        }

        /// <summary>
        ///A test for CheckSnapshot
        ///</summary>
        [TestMethod()]
        public void CheckSnapshotTest()
        {
            //IavaSnapshot collection1 = new IavaSnapshot(); // TODO: Initialize to an appropriate kinectFrame
            //IavaSkeleton skeleton = null; // TODO: Initialize to an appropriate kinectFrame
            //double fudgeFactor = 0F; // TODO: Initialize to an appropriate kinectFrame
            //bool accessor = false; // TODO: Initialize to an appropriate kinectFrame
            //bool actual;
            //actual = collection1.CheckSnapshot(skeleton, fudgeFactor);
            //Assert.AreEqual(accessor, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ClearTrackingJoints
        ///</summary>
        [TestMethod()]
        public void ClearTrackingJointsTest()
        {
            //IavaSnapshot collection1 = new IavaSnapshot(); // TODO: Initialize to an appropriate kinectFrame
            //collection1.ClearTrackingJoints();
            //Assert.Inconclusive("A method that does not return a kinectFrame cannot be verified.");
        }

        /// <summary>
        ///A test for SetTrackingJoints
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Iava.Gesture.dll")]
        public void SetTrackingJointsTest()
        {
            //Snapshot_Accessor collection1 = new Snapshot_Accessor(); // TODO: Initialize to an appropriate kinectFrame
            //IavaJointType[] joints = new IavaJointType[] { IavaJointType.Head, IavaJointType.AnkleLeft };
            //collection1.SetTrackingJoints(joints);
            //Assert.Inconclusive("A method that does not return a kinectFrame cannot be verified.");
        }

        /// <summary>
        ///A test for BodyParts
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Iava.Gesture.dll")]
        public void BodyPartsTest()
        {
            IavaSnapshot_Accessor target = new IavaSnapshot_Accessor();
            List<IavaBodyPart> expected = new List<IavaBodyPart>();
            List<IavaBodyPart> actual;
            actual = target.BodyParts;
            Assert.AreNotEqual(actual, null);
            Assert.AreEqual(expected.Count, actual.Count);
        }
    }
}
