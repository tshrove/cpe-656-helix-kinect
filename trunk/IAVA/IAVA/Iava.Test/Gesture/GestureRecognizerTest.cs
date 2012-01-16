using Iava.Gesture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Iava.Input.Camera;
using System.Collections.Generic;
using Iava.Gesture.GestureStuff;

namespace Iava.Gesture.Test
{      
    /// <summary>
    ///This is a test class for GestureRecognizerTest and is intended
    ///to contain all GestureRecognizerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GestureRecognizerTest
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
        ///A test for GestureRecognizer Constructor
        ///</summary>
        [TestMethod()]
        public void GestureRecognizerConstructorTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            GestureRecognizer target = new GestureRecognizer(filePath);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for LoadGestures
        ///</summary>
        [TestMethod()]
        //[DeploymentItem("Iava.Gesture.dll")]
        public void LoadGesturesTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            GestureRecognizer_Accessor target = new GestureRecognizer_Accessor(param0); // TODO: Initialize to an appropriate value
            target.LoadGestures();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for OnGestureRecognized
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Iava.Gesture.dll")]
        public void OnGestureRecognizedTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            GestureRecognizer_Accessor target = new GestureRecognizer_Accessor(param0); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            GestureEventArgs e = null; // TODO: Initialize to an appropriate value
            target.OnGestureRecognized(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for OnSkeletonReady
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Iava.Gesture.dll")]
        public void OnSkeletonReadyTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            GestureRecognizer_Accessor target = new GestureRecognizer_Accessor(param0); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            SkeletonEventArgs e = null; // TODO: Initialize to an appropriate value
            target.OnSkeletonReady(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SetupGestureDevice
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Iava.Gesture.dll")]
        public void SetupGestureDeviceTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            GestureRecognizer_Accessor target = new GestureRecognizer_Accessor(param0); // TODO: Initialize to an appropriate value
            target.SetupGestureDevice();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Start
        ///</summary>
        [TestMethod()]
        public void StartTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            GestureRecognizer target = new GestureRecognizer(filePath); // TODO: Initialize to an appropriate value
            target.Start();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Stop
        ///</summary>
        [TestMethod()]
        public void StopTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            GestureRecognizer target = new GestureRecognizer(filePath); // TODO: Initialize to an appropriate value
            target.Stop();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Subscribe
        ///</summary>
        [TestMethod()]
        public void SubscribeTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            GestureRecognizer target = new GestureRecognizer(filePath); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            GestureCallback callBack = null; // TODO: Initialize to an appropriate value
            target.Subscribe(name, callBack);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Unsubscribe
        ///</summary>
        [TestMethod()]
        public void UnsubscribeTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            GestureRecognizer target = new GestureRecognizer(filePath); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            target.Unsubscribe(name);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Camera
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Iava.Gesture.dll")]
        public void CameraTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            GestureRecognizer_Accessor target = new GestureRecognizer_Accessor(param0); // TODO: Initialize to an appropriate value
            Camera expected = null; // TODO: Initialize to an appropriate value
            Camera actual;
            target.Camera = expected;
            actual = target.Camera;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GestureCallbacks
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Iava.Gesture.dll")]
        public void GestureCallbacksTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            GestureRecognizer_Accessor target = new GestureRecognizer_Accessor(param0); // TODO: Initialize to an appropriate value
            Dictionary<string, GestureCallback> expected = null; // TODO: Initialize to an appropriate value
            Dictionary<string, GestureCallback> actual;
            target.GestureCallbacks = expected;
            actual = target.GestureCallbacks;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SupportedGestures
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Iava.Gesture.dll")]
        public void SupportedGesturesTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            GestureRecognizer_Accessor target = new GestureRecognizer_Accessor(param0); // TODO: Initialize to an appropriate value
            List<Gesture.GestureStuff.Gesture> expected = null; // TODO: Initialize to an appropriate value
            List<Gesture.GestureStuff.Gesture> actual;
            target.SupportedGestures = expected;
            actual = target.SupportedGestures;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SyncGesture
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Iava.Gesture.dll")]
        public void SyncGestureTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            GestureRecognizer_Accessor target = new GestureRecognizer_Accessor(param0); // TODO: Initialize to an appropriate value
            Gesture.GestureStuff.Gesture expected = null; // TODO: Initialize to an appropriate value
            Gesture.GestureStuff.Gesture actual;
            target.SyncGesture = expected;
            actual = target.SyncGesture;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
