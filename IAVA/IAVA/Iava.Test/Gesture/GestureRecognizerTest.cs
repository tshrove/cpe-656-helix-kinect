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
                Thread.Sleep(2000);
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Running, recognizer.Status);

                // Ensure the OnStarted callback was invoked
                Assert.IsTrue(recognizerCallbackInvoked, "OnStarted callback was not invoked.");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        void recognizerCallback(object sender, EventArgs e)
        {
            recognizerCallbackInvoked = true;
        }

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
                Thread.Sleep(2000);
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Running, recognizer.Status);

                recognizer.Stop();
                Thread.Sleep(2000);
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Ready, recognizer.Status);
                Assert.IsTrue(recognizerCallbackInvoked, "OnStopped callback was not invoked.");

                // Start and stop immediately after one another and ensure it can be started again
                recognizer.Start();
                recognizer.Stop();
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Ready, recognizer.Status);
                recognizer.Start();
                Thread.Sleep(2000);
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
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            GestureRecognizer_Accessor target = new GestureRecognizer_Accessor(param0); // TODO: Initialize to an appropriate value
            Dictionary<string, GestureCallback> expected = null; // TODO: Initialize to an appropriate value
            Dictionary<string, GestureCallback> actual;
            target.GestureCallbacks = expected;
            actual = target.GestureCallbacks;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Unsubscribe method.
        /// </summary>
        [TestMethod]
        public void UnsubscribeTest()
        {
            
        }
    }
}
