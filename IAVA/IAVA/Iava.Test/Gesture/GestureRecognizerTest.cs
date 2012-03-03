using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using Iava.Gesture;
using Iava.Gesture.GestureStuff;
using Iava.Input.Camera;
using Iava.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Iava.Test.Gesture {

    /// <summary>
    /// Tests the GestureRecognizer class.
    /// </summary>
    [TestClass]
    public class GestureRecognizerTest {

        #region Additional test attributes

        /// <summary>
        /// Used to signal callbacks were invoked.
        /// </summary>
        private ManualResetEvent resetEvent = new ManualResetEvent(false);

        /// <summary>
        /// Time (in milliseconds) to wait for a reset event to be set.
        /// </summary>
        private const int TimeoutValue = 100;

        #endregion

        /// <summary>
        /// Tests the constructor of the gesturerecognizer.
        /// </summary>
        [TestMethod]
        public void GestureRecognizerConstructorTest() {
            GestureRecognizer_Accessor recognizer = new GestureRecognizer_Accessor(string.Empty);
            Assert.IsNotNull(recognizer.GestureCallbacks);
            Dictionary<string, GestureCallback> expected = new Dictionary<string, GestureCallback>();
            Assert.AreEqual(recognizer.GestureCallbacks.Count, expected.Count);
            Assert.IsInstanceOfType(recognizer.GestureCallbacks, typeof(Dictionary<string, GestureCallback>));
        }

        /// <summary>
        /// Tests the Start method.
        /// </summary>
        [TestMethod]
        public void GestureStartTest() {
            GestureRecognizer recognizer = new GestureRecognizer(string.Empty);

            // Make sure the Recognizer isn't showing a faulty status
            Assert.AreEqual(RecognizerStatus.NotReady, recognizer.Status);

            bool eventFired = false;

            // Register for the Recognizer Started Event
            recognizer.Started += (param1, param2) => eventFired = true;

            try {
                // Start the Recognizer
                recognizer.Start();
                Thread.Sleep(50);

                // Make sure we're showing the correct status
                Assert.AreEqual(RecognizerStatus.Running, recognizer.Status);

                // Make sure the Recognizer Started Event was fired
                Assert.IsTrue(eventFired);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Tests the Stop method.
        /// </summary>
        [TestMethod]
        public void GestureStopTest() {
            GestureRecognizer recognizer = new GestureRecognizer(string.Empty);

            // Make sure the Recognizer isn't showing a faulty status
            Assert.AreEqual(RecognizerStatus.NotReady, recognizer.Status);

            bool eventFired = false;

            // Register for the Recognizer Stopped Event
            recognizer.Stopped += (param1, param2) => eventFired = true;

            try {
                // Start the Recognizer
                recognizer.Start();
                Thread.Sleep(50);

                // Make sure we're showing the correct status
                Assert.AreEqual(RecognizerStatus.Running, recognizer.Status);

                // Stop the Recognizer
                recognizer.Stop();
                Thread.Sleep(50);

                // Make sure we're showing the correct status
                Assert.AreEqual(RecognizerStatus.Ready, recognizer.Status);

                // Make sure the Recognizer Stopped Event was fired
                Assert.IsTrue(eventFired);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Tests to see if the Recognizer can be restarted.
        /// </summary>
        [TestMethod]
        public void GestureRestartTest() {
            GestureRecognizer recognizer = new GestureRecognizer(string.Empty);

            // Make sure the Recognizer isn't showing a faulty status
            Assert.AreEqual(RecognizerStatus.NotReady, recognizer.Status);

            bool eventFired = false;

            // Register for the Recognizer Started Event
            recognizer.Started += (param1, param2) => eventFired = true;

            try {
                // Start the Recognizer
                recognizer.Start();
                Thread.Sleep(50);

                // Make sure we're showing the correct status
                Assert.AreEqual(RecognizerStatus.Running, recognizer.Status);

                // Make sure the Recognizer Started Event was fired
                Assert.IsTrue(eventFired);

                // Reset the event
                eventFired = false;

                // Stop the Recognizer
                recognizer.Stop();
                Thread.Sleep(50);

                // Make sure we're showing the correct status
                Assert.AreEqual(RecognizerStatus.Ready, recognizer.Status);

                // Attempt to restart the Recognizer
                recognizer.Start();
                Thread.Sleep(50);

                // Make sure we're showing the correct status
                Assert.AreEqual(RecognizerStatus.Running, recognizer.Status);

                // Make sure the Recognizer Started Event was fired
                Assert.IsTrue(eventFired);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Tests the Subscribe method.
        /// </summary>
        [TestMethod]
        public void GestureSubscribeTest() {
            GestureRecognizer_Accessor recognizer = new GestureRecognizer_Accessor(string.Empty);

            string eventName;

            // Hold our expected callbacks and a key value pair for the subscription signature
            Dictionary<string, GestureCallback> expectedCallbacks = new Dictionary<string, GestureCallback>();
            KeyValuePair<string, GestureCallback> subscriptionSignature = new KeyValuePair<string,GestureCallback>();

            // Try to do things correctly the first time
            try {
                // Create the subscription signature for the 'Sync' Gesture
                subscriptionSignature = new KeyValuePair<string, GestureCallback>("Sync", (eventArgs) => { eventName = eventArgs.Name; });

                // Subscribe to the gesture
                recognizer.Subscribe(subscriptionSignature.Key, subscriptionSignature.Value);

                // Add it to our expected callbacks
                expectedCallbacks.Add(subscriptionSignature.Key, subscriptionSignature.Value);

                // Make sure we have the same number of callbacks
                Assert.AreEqual(expectedCallbacks.Count, recognizer.GestureCallbacks.Count);

                // Create the subscription signature for the 'Wave' Gesture
                subscriptionSignature = new KeyValuePair<string, GestureCallback>("Wave", (eventArgs) => { eventName = eventArgs.Name; });

                // Subscribe to the gesture
                recognizer.Subscribe(subscriptionSignature.Key, subscriptionSignature.Value);

                // Add it to our expected callbacks
                expectedCallbacks.Add(subscriptionSignature.Key, subscriptionSignature.Value);

                // Again, make sure we have the same number of callbacks
                Assert.AreEqual(expectedCallbacks.Count, recognizer.GestureCallbacks.Count);

                // Do a deep inspection to make sure all the elements are the same
                foreach (var pair in recognizer.GestureCallbacks) {
                    string key = pair.Key;
                    Assert.IsTrue(recognizer.GestureCallbacks.ContainsKey(key));
                    Assert.AreEqual(expectedCallbacks[key], recognizer.GestureCallbacks[key]);
                }
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            // Try to subscribe while passing in an empty gesture name
            try {
                recognizer.Subscribe(string.Empty, (eventArgs) => { eventName = eventArgs.Name; });
            }
            catch (Exception ex) {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            // Try to subscribe without passing in a gesture name
            try {
                recognizer.Subscribe(null, (eventArgs) => { eventName = eventArgs.Name; });
            }
            catch (Exception ex) {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            // Try to subscribe without passing in a valid callback
            try {
                recognizer.Subscribe("Sync", null);
            }
            catch (Exception ex) {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        /// <summary>
        /// Tests the Unsubscribe method.
        /// </summary>
        [TestMethod]
        public void GestureUnsubscribeTest() {
            GestureRecognizer_Accessor recognizer = new GestureRecognizer_Accessor(string.Empty);

            string eventName;

            // Hold our expected callbacks and a key value pair for the subscription signature
            Dictionary<string, GestureCallback> expectedCallbacks = new Dictionary<string, GestureCallback>();
            KeyValuePair<string, GestureCallback> subscriptionSignature = new KeyValuePair<string, GestureCallback>();

            // Try to do things correctly the first time
            try {
                // Create the subscription signature for the 'Sync' Gesture
                subscriptionSignature = new KeyValuePair<string, GestureCallback>("Sync", (eventArgs) => { eventName = eventArgs.Name; });

                // Subscribe to the gesture
                recognizer.Subscribe(subscriptionSignature.Key, subscriptionSignature.Value);

                // Add it to our expected callbacks
                expectedCallbacks.Add(subscriptionSignature.Key, subscriptionSignature.Value);

                // Create the subscription signature for the 'Wave' Gesture
                subscriptionSignature = new KeyValuePair<string, GestureCallback>("Wave", (eventArgs) => { eventName = eventArgs.Name; });

                // Subscribe to the gesture
                recognizer.Subscribe(subscriptionSignature.Key, subscriptionSignature.Value);

                // Add it to our expected callbacks
                expectedCallbacks.Add(subscriptionSignature.Key, subscriptionSignature.Value);

                // Unsubscribe from the 'Wave' gesture
                recognizer.Unsubscribe("Wave");

                // Remove it from our expected callbacks
                expectedCallbacks.Remove("Wave");
                
                // Make sure we have the same number of callbacks
                Assert.AreEqual(expectedCallbacks.Count, recognizer.GestureCallbacks.Count);

                // Do a deep inspection to make sure all the elements are the same
                foreach (var pair in recognizer.GestureCallbacks) {
                    string key = pair.Key;
                    Assert.IsTrue(recognizer.GestureCallbacks.ContainsKey(key));
                    Assert.AreEqual(expectedCallbacks[key], recognizer.GestureCallbacks[key]);
                }
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            // Try to unsubscribe while passing in an empty gesture name
            try {
                recognizer.Unsubscribe(string.Empty);
            }
            catch (Exception ex) {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            // Try to unsubscribe without passing in a gesture name
            try {
                recognizer.Unsubscribe(null);
            }
            catch (Exception ex) {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            // Try to unsubscribe from a gesture that doesn't exist
            try {
                recognizer.Unsubscribe("Nonexistant Gesture");
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            // Try to unsubscribe from a gesture that we already removed
            try {
                recognizer.Unsubscribe("Wave");
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }

            // After all that make sure nothing has changed
            Assert.AreEqual(expectedCallbacks.Count, recognizer.GestureCallbacks.Count);

            // Do a deep inspection to make sure all the elements are the same
            foreach (var pair in recognizer.GestureCallbacks) {
                string key = pair.Key;
                Assert.IsTrue(recognizer.GestureCallbacks.ContainsKey(key));
                Assert.AreEqual(expectedCallbacks[key], recognizer.GestureCallbacks[key]);
            }
        }
        
        /// <summary>
        ///A test for OnGestureRecognized
        ///</summary>
        [TestMethod()]
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
            // Should be able to use the eventFired trick without needing to use a reset event...
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
        /// Creates and sets up a mock IRuntime in the Camera class so we can drive Recongnizer events.
        /// </summary>
        private void SetupCamera() {
            var mockRuntime = new Mock<IRuntime>(MockBehavior.Strict);

            // This lets up replace the IRuntime even though its an instance object
            IavaCamera camera = new IavaCamera(mockRuntime.Object);
        }

        #endregion
    }
}
