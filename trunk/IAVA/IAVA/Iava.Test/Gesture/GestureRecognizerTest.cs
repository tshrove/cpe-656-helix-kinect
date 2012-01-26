using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Iava.Gesture;
using Iava.Core;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iava.Test.Gesture
{
    [TestClass]
    public class GestureRecognizerTest
    {
        #region Private Methods And Attributes

        private bool recognizerCallbackInvoked;

        void GestureRecognizedCallback(GestureEventArgs e)
        {

        }

        #endregion

        #region Additional test attributes
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
        public void MyTestInitialize()
        {

        }

        /// <summary>
        /// Called after each test is run.
        /// </summary>
        [TestCleanup()]
        public void MyTestCleanup()
        {
            recognizerCallbackInvoked = false;
        }
        #endregion

        /// <summary>
        /// Tests the Start method.
        /// </summary>
        [TestMethod]
        public void StartTest()
        {
            // The recognizerCallbackInvoked variable is set to false automatically.
            GestureRecognizer recognizer = new GestureRecognizer(string.Empty);
            Assert.AreEqual<RecognizerStatus>(RecognizerStatus.NotReady, recognizer.Status);
            recognizer.Started += new EventHandler<EventArgs>(recognizerCallback);
            try
            {
                recognizer.Start();
                Thread.Sleep(5000);
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Running, recognizer.Status);

                // Ensure the OnStarted callback was invoked
                Assert.IsTrue(recognizerCallbackInvoked, "OnStarted callback was not invoked.");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        #region Additional Handlers
        void Camera_SkeletonFrameReady(object sender, Input.Camera.SkeletonFrameReadyEventArgs e)
        {
            // To Do
        }

        void Camera_ImageFrameReady(object sender, Input.Camera.ImageFrameReadyEventArgs e)
        {
            // To Do
        }

        void recognizerCallback(object sender, EventArgs e)
        {
            recognizerCallbackInvoked = true;
        }
        #endregion

        /// <summary>
        /// Tests the Stop method.
        /// </summary>
        [TestMethod]
        public void StopTest()
        {
            // The recognizerCallbackInvoked variable is set to false automatically.
            GestureRecognizer recognizer = new GestureRecognizer(string.Empty);
            Assert.AreEqual<RecognizerStatus>(RecognizerStatus.NotReady, recognizer.Status);
            recognizer.Stopped += new EventHandler<EventArgs>(recognizerCallback);
            try
            {
                recognizer.Start();
                Thread.Sleep(5000);
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Running, recognizer.Status);

                recognizer.Stop();
                Thread.Sleep(5000);
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Ready, recognizer.Status);
                Assert.IsTrue(recognizerCallbackInvoked, "OnStopped callback was not invoked.");

                // Start and stop immediately after one another and ensure it can be started again
                recognizer.Start();

                recognizer.Stop();
                Thread.Sleep(5000);
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Ready, recognizer.Status);
                recognizer.Start();
                Thread.Sleep(5000);
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Running, recognizer.Status);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Tests the Subscribe method.
        /// </summary>
        [TestMethod]
        [DeploymentItem("Iava.Gesture.dll")]
        public void SubscribeTest()
        {
            try
            {
                GestureRecognizer_Accessor recognizer = new GestureRecognizer_Accessor(string.Empty);
                // Create our expected value of the gesture callbacks.
                Dictionary<string, GestureCallback> expected = new Dictionary<string, GestureCallback>();
                expected.Add("Sync", GestureRecognizedCallback);
                // Not get the actual gesture callbacks after the subscribe function test.
                Dictionary<string, GestureCallback> actual;
                // Call the subscribe function.
                recognizer.Subscribe("Sync", GestureRecognizedCallback);
                actual = recognizer.GestureCallbacks;
                // Test the actual vs expected.
                int actualCount = actual.Count;
                int expectedCount = expected.Count;
                Assert.AreEqual(expectedCount, actualCount);
                foreach (var pair in actual)
                {
                    string key = pair.Key;
                    bool contains = expected.ContainsKey(key);
                    Assert.IsTrue(contains);
                    Assert.AreEqual(expected[key], actual[key]);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Tests the Unsubscribe method.
        /// </summary>
        [TestMethod]
        [DeploymentItem("Iava.Gesture.dll")]
        public void UnsubscribeTest()
        {
            try
            {
                GestureRecognizer_Accessor recognizer = new GestureRecognizer_Accessor(string.Empty);
                //recognizer.Camera.ImageFrameReady += new EventHandler<Input.Camera.ImageFrameReadyEventArgs>(Camera_ImageFrameReady);
                //recognizer.Camera.SkeletonFrameReady += new EventHandler<Input.Camera.SkeletonFrameReadyEventArgs>(Camera_SkeletonFrameReady);
                // Create our expected value of the gesture callbacks.
                Dictionary<string, GestureCallback> expected = new Dictionary<string, GestureCallback>();
                expected.Add("Sync", GestureRecognizedCallback);
                // Not get the actual gesture callbacks after the subscribe function test.
                Dictionary<string, GestureCallback> actual;
                // Call the subscribe function.
                recognizer.Subscribe("Sync", GestureRecognizedCallback);
                actual = recognizer.GestureCallbacks;
                // Test the actual vs expected.
                int actualCount = actual.Count;
                int expectedCount = expected.Count;
                Assert.AreEqual(expectedCount, actualCount);
                foreach (var pair in actual)
                {
                    string key = pair.Key;
                    bool contains = expected.ContainsKey(key);
                    Assert.IsTrue(contains);
                    Assert.AreEqual(expected[key], actual[key]);
                }
                // Reset the expected.
                expected = new Dictionary<string, GestureCallback>();
                // Call the unsubscribe function to test.
                recognizer.Unsubscribe("Sync");
                actual = recognizer.GestureCallbacks;
                // Test the actual vs expected.
                actualCount = actual.Count;
                expectedCount = expected.Count;
                Assert.AreEqual(expectedCount, actualCount);
                foreach (var pair in actual)
                {
                    string key = pair.Key;
                    bool contains = expected.ContainsKey(key);
                    Assert.IsTrue(contains);
                    Assert.AreEqual(expected[key], actual[key]);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
