using System;
using System.Collections.Generic;
using System.Threading;
using Iava.Gesture;
using Iava.Gesture.GestureStuff;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Iava.Test.Gesture {
    /// <summary>
    /// Tests the GestureRecognizer class.
    /// </summary>
    [TestClass]
    public class GestureRecognizerTest {
        public GestureRecognizerTest() {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        /// <summary>
        /// Used to signal callbacks were invoked.
        /// </summary>
        private ManualResetEvent resetEvent = new ManualResetEvent(false);

        /// <summary>
        /// Time (in milliseconds) to wait for a reset event to be set.
        /// </summary>
        private const int TimeoutValue = 100;

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 

        /// <summary>
        /// Called before each test is run.
        /// </summary>
        [TestInitialize()]
        public void MyTestInitialize() {

        }

        /// <summary>
        /// Called after each test is run.
        /// </summary>
        [TestCleanup()]
        public void MyTestCleanup() {
            recognizerCallbackInvoked = false;

            if (recognizer != null) {
                recognizer.Stop();
            }

            recognizer = null;

            resetEvent.Reset();
        }

        #endregion

//        /// <summary>
//        /// Tests the constructor of the gesturerecognizer.
//        /// </summary>
//        [TestMethod]
//        [DeploymentItem("Iava.Gesture.dll")]
//        public void GestureRecognizerConstructorTest() {
//            GestureRecognizer_Accessor recognizer = new GestureRecognizer_Accessor(string.Empty);
//            Assert.IsNotNull(recognizer.GestureCallbacks);
//            Dictionary<string, GestureCallback> expected = new Dictionary<string, GestureCallback>();
//            Assert.AreEqual(recognizer.GestureCallbacks.Count, expected.Count);
//            Assert.IsInstanceOfType(recognizer.GestureCallbacks, typeof(Dictionary<string, GestureCallback>));
//        }

//        /// <summary>
//        /// Tests the Start method.
//        /// </summary>
//        [TestMethod]
//        public void GestureStartTest() {
//            // The recognizerCallbackInvoked variable is set to false automatically.
//            GestureRecognizer recognizer = new GestureRecognizer(string.Empty);
//            Assert.AreEqual<RecognizerStatus>(RecognizerStatus.NotReady, recognizer.Status);
//            recognizer.Started += new EventHandler<EventArgs>(recognizerCallback);
//            try {
//                recognizer.Start();
//                Thread.Sleep(100);
//                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Running, recognizer.Status);

//                // Ensure the OnStarted callback was invoked
//                Assert.IsTrue(recognizerCallbackInvoked, "OnStarted callback was not invoked.");
//            }
//            catch (Exception ex) {
//                Assert.Fail(ex.Message);
//            }
//            finally {
//                recognizer.Stop();
//                Thread.Sleep(100);
//            }
//        }

//        #region Additional Handlers
//        void Camera_SkeletonFrameReady(object sender, Input.Camera.IavaSkeletonFrameReadyEventArgs e) {
//            // To Do
//        }

//        void Camera_ImageFrameReady(object sender, Input.Camera.IavaImageFrameReadyEventArgs e) {
//            // To Do
//        }

//        void recognizerCallback(object sender, EventArgs e) {
//            recognizerCallbackInvoked = true;
//        }
//        #endregion

//        /// <summary>
//        /// Tests the Stop method.
//        /// </summary>
//        [TestMethod]
//        public void GestureStopTest() {
//            // The recognizerCallbackInvoked variable is set to false automatically.
//            GestureRecognizer recognizer = new GestureRecognizer(string.Empty);
//            Assert.AreEqual<RecognizerStatus>(RecognizerStatus.NotReady, recognizer.Status);
//            recognizer.Stopped += new EventHandler<EventArgs>(recognizerCallback);
//            try {
//                recognizer.Start();
//                Thread.Sleep(100);
//                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Running, recognizer.Status);

//                recognizer.Stop();
//                Thread.Sleep(100);
//                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Ready, recognizer.Status);
//                Assert.IsTrue(recognizerCallbackInvoked, "OnStopped callback was not invoked.");

//                // Start and stop immediately after one another and ensure it can be started again
//                recognizer.Start();

//                recognizer.Stop();
//                Thread.Sleep(100);
//                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Ready, recognizer.Status);
//                recognizer.Start();
//                Thread.Sleep(100);
//                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Running, recognizer.Status);
//            }
//            catch (Exception ex) {
//                Assert.Fail(ex.Message);
//            }
//            finally {
//                recognizer.Stop();
//            }
//        }

//        /// <summary>
//        /// Tests the Subscribe method.
//        /// </summary>
//        [TestMethod]
//        [DeploymentItem("Iava.Gesture.dll")]
//        public void GestureSubscribeTest() {
//            GestureRecognizer_Accessor recognizer = new GestureRecognizer_Accessor(string.Empty);

//            // Create our expected value of the gesture callbacks.
//            Dictionary<string, GestureCallback> expected = new Dictionary<string, GestureCallback>();
//            expected.Add("Sync", GestureRecognizedCallback);

//            // Now get the actual gesture callbacks after the subscribe function test.
//            Dictionary<string, GestureCallback> actual;

//            // Try to subscribe doing everything correctly
//            try {
//                // Call the subscribe function.
//                recognizer.Subscribe("Sync", GestureRecognizedCallback);
//                actual = recognizer.GestureCallbacks;

//                // Test the actual vs expected.
//                Assert.AreEqual(expected.Count, actual.Count);

//                // Test all stored gesture names
//                foreach (var pair in actual) {
//                    string key = pair.Key;
//                    bool contains = expected.ContainsKey(key);
//                    Assert.IsTrue(contains);
//                    Assert.AreEqual(expected[key], actual[key]);
//                }
//            }
//            catch (Exception ex) {
//                Assert.Fail(ex.Message);
//            }

//            // Try to subscribe while passing in an empty gesture name
//            try {
//                recognizer.Subscribe(string.Empty, GestureRecognizedCallback);
//            }
//            catch (Exception ex) {
//                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
//            }

//            // Try to subscribe without passing in a gesture name
//            try {
//                recognizer.Subscribe(null, GestureRecognizedCallback);
//            }
//            catch (Exception ex) {
//                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
//            }

//            // Try to subscribe without passing in a valid callback
//            try {
//                recognizer.Subscribe("Sync", null);
//            }
//            catch (Exception ex) {
//                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
//            }
//        }

//        /// <summary>
//        /// Tests the Unsubscribe method.
//        /// </summary>
//        [TestMethod]
//        [DeploymentItem("Iava.Gesture.dll")]
//        public void GestureUnsubscribeTest() {
//            GestureRecognizer_Accessor recognizer = new GestureRecognizer_Accessor(string.Empty);
            
//            // Create our expected value of the gesture callbacks.
//            Dictionary<string, GestureCallback> expected = new Dictionary<string, GestureCallback>();
//            expected.Add("Sync", GestureRecognizedCallback);

//            // Now get the actual gesture callbacks after the subscribe function test.
//            Dictionary<string, GestureCallback> actual;

//            // Subscribe to a valid gesture and callback
//            try {
//                // Call the subscribe function.
//                recognizer.Subscribe("Sync", GestureRecognizedCallback);
//                actual = recognizer.GestureCallbacks;

//                // Test the actual vs expected.
//                Assert.AreEqual(expected.Count, actual.Count);
//                foreach (var pair in actual) {
//                    string key = pair.Key;
//                    bool contains = expected.ContainsKey(key);
//                    Assert.IsTrue(contains);
//                    Assert.AreEqual(expected[key], actual[key]);
//                }

//                // Reset the expected.
//                expected = new Dictionary<string, GestureCallback>();

//                // Call the unsubscribe function to test.
//                recognizer.Unsubscribe("Sync");
//                actual = recognizer.GestureCallbacks;

//                // Make sure there are the same number of gestures left in the recognizer
//                // that we are expecting
//                Assert.AreEqual(expected.Count, actual.Count);

//                // Make sure all remaining gestures are ones we are expecting
//                foreach (var pair in actual) {
//                    string key = pair.Key;
//                    bool contains = expected.ContainsKey(key);
//                    Assert.IsTrue(contains);
//                    Assert.AreEqual(expected[key], actual[key]);
//                }
//            }

//            catch (Exception ex) {
//                Assert.Fail(ex.Message);
//            }

//            // Try to unsubscribe while passing in an empty gesture name
//            try {
//                recognizer.Unsubscribe(string.Empty);
//            }
//            catch (Exception ex) {
//                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
//            }

//            // Try to unsubscribe without passing in a gesture name
//            try {
//                recognizer.Unsubscribe(null);
//            }
//            catch (Exception ex) {
//                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
//            }

//            // Try to unsubscribe from a gesture that doesn't exist
//            try {
//                recognizer.Unsubscribe("Nonexistant Gesture");
//            }
//            catch (Exception ex) {
//                Assert.Fail(ex.Message);
//            }
//        }
        
        /// <summary>
        ///A test for OnGestureRecognized
        ///</summary>
        [TestMethod()]
        //[DeploymentItem("Iava.Gesture.dll")]
        public void OnGestureRecognizedTest() {/*
            resetEvent.Reset();
            var mockRuntime = SetupMockRuntime();
            try {
                recognizer = new GestureRecognizer(mockRuntime.Object);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            PrivateObject privateObject = new PrivateObject(recognizer);

            // The name of the sync gesture
            string syncGesture = "Sync Gesture";
            string anotherGesture = "Not Sync Gesture";

            // Set the sync gesture to an empty gesture
            try {
                privateObject.SetProperty("SyncGesture", null);
                //recognizer.SyncGesture = null;
            }
            catch (Exception ex) {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            try {
                privateObject.SetProperty("SyncGesture", new Iava.Gesture.GestureStuff.Gesture(syncGesture, new List<IGestureSegment>()));
                //recognizer.SyncGesture = new Iava.Gesture.GestureStuff.Gesture(syncGesture, new List<IGestureSegment>());
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            // Subscribe to the sync gesture
            string eventName = string.Empty;
            recognizer.Subscribe(anotherGesture, (eventArgs) => { eventName = eventArgs.Name; resetEvent.Set(); });

            recognizer.Start();

            mockRuntime.Raise(m => m.SkeletonFrameReady += null, new GestureEventArgs(syncGesture));
            Thread.Sleep(50);
            mockRuntime.Raise(m => m.SkeletonFrameReady += null, new GestureEventArgs(anotherGesture));
            Thread.Sleep(50);

            //Assert.IsTrue(resetEvent.WaitOne(TimeoutValue));
            Assert.AreEqual(anotherGesture, eventName, "Gesture name did not match expected value.");
            resetEvent.Reset();
            
            syncGesture = "Another Gesture";

            // Change the sync gesture, re-sync and recognize the gesture again
            try {
                privateObject.SetProperty("SyncGesture", new Iava.Gesture.GestureStuff.Gesture(syncGesture, new List<IGestureSegment>()));
                //recognizer.SyncGesture = new Iava.Gesture.GestureStuff.Gesture(syncGesture, new List<IGestureSegment>());
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            mockRuntime.Raise(m => m.SkeletonFrameReady += null, new GestureEventArgs(syncGesture));
            Thread.Sleep(50);
            mockRuntime.Raise(m => m.SkeletonFrameReady += null, new GestureEventArgs(anotherGesture));
            Assert.IsTrue(resetEvent.WaitOne(TimeoutValue));*/
        }

//        /// <summary>
//        ///A test for OnSkeletonReady
//        ///</summary>
//        [TestMethod()]
//        [DeploymentItem("Iava.Gesture.dll")]
//        public void OnSkeletonReadyTest() {/*
//            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
//            GestureRecognizer_Accessor target = new GestureRecognizer_Accessor(param0); // TODO: Initialize to an appropriate value
//            object sender = null; // TODO: Initialize to an appropriate value
//            IavaSkeletonEventArgs e = null; // TODO: Initialize to an appropriate value
//            target.OnSkeletonReady(sender, e);
//            Assert.Inconclusive("A method that does not return a value cannot be verified.");*/
//        }

        #region Private Methods And Attributes

        /// <summary>
        /// Recognizer object under test.
        /// </summary>
        private GestureRecognizer recognizer;

        /// <summary>
        /// Used to determine if the recognizer status ballback was invoked.
        /// </summary>
        private bool recognizerCallbackInvoked;

        /// <summary>
        /// Called when the recognizer's status changes.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void RecognizerStatusCallback(object sender, EventArgs e) {
            recognizerCallbackInvoked = true;
        }
        /*
        /// <summary>
        /// Creates and sets up a mock speech recongition engine.
        /// </summary>
        /// <returns>Mock engine</returns>
        private Mock<IRuntime> SetupMockRuntime() {
            var mockEngine = new Mock<IRuntime>(MockBehavior.Strict);

            return mockEngine;
        }*/

        #endregion
    }
}
