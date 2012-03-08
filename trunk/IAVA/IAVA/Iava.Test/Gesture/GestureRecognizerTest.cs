using System;
using System.Collections.Generic;
using System.Threading;
using Iava.Core;
using Iava.Gesture;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iava.Test.Gesture {

    /// <summary>
    /// Tests the GestureRecognizer class.
    /// </summary>
    [TestClass]
    public class GestureRecognizerTest {

        #region Test Attributes

        /// <summary>
        /// Points to a directory containing gesture files
        /// </summary>
        private string _directoryPath = @"..\..\..\Gestures\";

        #endregion Test Attributes

        /// <summary>
        /// Tests the constructor of the gesturerecognizer.
        /// </summary>
        [TestMethod]
        public void GestureRecognizerConstructorTest() {
            // Supply an empty file path
            try {
                GestureRecognizer recognizer = new GestureRecognizer(string.Empty);
            }

            catch (Exception ex) {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            // Pass in a null file path
            try {
                GestureRecognizer recognizer = new GestureRecognizer(null);
            }

            catch (Exception ex) {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            // Pass in a file path that does not exist
            try {
                GestureRecognizer recognizer = new GestureRecognizer(@"C:\Does\not\exist\");
            }

            catch (Exception ex) {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            // Pass in an existing filepath
            try {
                GestureRecognizer recognizer = new GestureRecognizer(_directoryPath);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }


            try {
                GestureRecognizer_Accessor recognizer = new GestureRecognizer_Accessor(_directoryPath);

                // Make sure GestureCallbacks exist 
                Assert.IsNotNull(recognizer.GestureCallbacks);

                // Make sure the GestureCallbacks is a dictionary
                Assert.IsInstanceOfType(recognizer.GestureCallbacks, typeof(Dictionary<string, GestureCallback>));

                // Hold our expected callbacks (in this case empty)
                Dictionary<string, GestureCallback> expectedCallbacks = new Dictionary<string, GestureCallback>();

                // Make sure we have the same number of callbacks as we are expecting
                Assert.AreEqual(recognizer.GestureCallbacks.Count, expectedCallbacks.Count);

                // Make sure SupportedGestures exist
                Assert.IsNotNull(recognizer.SupportedGestures);

                // Make sure the SupportedGestures is a list
                Assert.IsInstanceOfType(recognizer.SupportedGestures, typeof(List<IavaGesture>));

                // Hold our expected gestures (in this case empty)
                List<string> expectedGestures = new List<string>();

                // Make sure we have the same number of gestures as we are expecting
                Assert.AreEqual(recognizer.SupportedGestures.Count, expectedGestures.Count);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Tests the Start method.
        /// </summary>
        [TestMethod]
        public void GestureStartTest() {
            try {
                GestureRecognizer recognizer = new GestureRecognizer(_directoryPath);

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
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Tests the Stop method.
        /// </summary>
        [TestMethod]
        public void GestureStopTest() {
            try {
                GestureRecognizer recognizer = new GestureRecognizer(_directoryPath);

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
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Tests to see if the Recognizer can be restarted.
        /// </summary>
        [TestMethod]
        public void GestureRestartTest() {
            try {
                GestureRecognizer recognizer = new GestureRecognizer(_directoryPath);

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
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Tests the Subscribe method.
        /// </summary>
        [TestMethod]
        public void GestureSubscribeTest() {
            try {
                GestureRecognizer_Accessor recognizer = new GestureRecognizer_Accessor(_directoryPath);

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
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Tests the Unsubscribe method.
        /// </summary>
        [TestMethod]
        public void GestureUnsubscribeTest() {
            try {
                GestureRecognizer_Accessor recognizer = new GestureRecognizer_Accessor(_directoryPath);

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
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Tests the syncing and unsyncing of the recognizer.
        /// </summary>
        [TestMethod]
        public void GestureSyncUnsyncTest() {
            try {
                GestureRecognizer recognizer = new GestureRecognizer(_directoryPath);

                bool syncEventFired = false;
                bool unsyncEventFired = false;

                // Register for the Synced and Unsynced events
                recognizer.Synced += (param1, param2) => syncEventFired = true;
                recognizer.Unsynced += (param1, param2) => unsyncEventFired = true;

                // Set the Sync Timeout to 5 seconds
                recognizer.SyncTimeoutValue = 5000;

                // Set the Sync Gesture
                PrivateObject privateObject = new PrivateObject(recognizer);
                privateObject.SetProperty("SyncGesture", new IavaGesture("Sync", new List<Snapshot>()));

                // Recognize the 'Sync' Gesture
                privateObject.Invoke("OnGestureRecognized", null, new GestureEventArgs("Sync"));
                Thread.Sleep(50);

                // Make sure the Sync Event fired
                Assert.IsTrue(syncEventFired);

                // Make sure the Unsync Event did not fire
                Assert.IsFalse(unsyncEventFired);

                // Reset the sync event
                syncEventFired = false;

                // Wait for the timeout to occur
                Thread.Sleep(recognizer.SyncTimeoutValue);

                // Make sure the Unsync Event fired
                Assert.IsTrue(unsyncEventFired);

                // Make sure the Sync Event did not fire
                Assert.IsFalse(syncEventFired);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for OnGestureRecognized
        ///</summary>
        [TestMethod()]
        public void OnGestureRecognizedTest() {
            try {
                GestureRecognizer recognizer = new GestureRecognizer(_directoryPath);

                bool syncEventFired = false;
                bool waveCallbackInvoked = false;
                bool shakeCallbackInvoked = false;

                // Set the Sync Gesture
                PrivateObject privateObject = new PrivateObject(recognizer);
                privateObject.SetProperty("SyncGesture", new IavaGesture("Sync", new List<Snapshot>()));

                // Register for some Gestures
                recognizer.Synced += (parm1, param2) => { syncEventFired = true; };
                recognizer.Subscribe("Wave", (eventArgs) => { waveCallbackInvoked = true; });
                recognizer.Subscribe("Shake", (eventArgs) => { shakeCallbackInvoked = true; });

                try {
                    // Recognize the Wave gesture
                    privateObject.Invoke("OnGestureRecognized", null, new GestureEventArgs("Wave"));
                    Thread.Sleep(50);

                    // Make sure the Wave callback fired
                    Assert.IsTrue(waveCallbackInvoked);

                }
                catch (Exception ex) {
                    Assert.Fail(ex.Message);
                }

                try {
                    // Recognize the Shake gesture
                    privateObject.Invoke("OnGestureRecognized", null, new GestureEventArgs("Shake"));
                    Thread.Sleep(50);

                    // Make sure the Shake callback fired
                    Assert.IsTrue(shakeCallbackInvoked);

                }
                catch (Exception ex) {
                    Assert.Fail(ex.Message);
                }

                try {
                    // Recognize the Sync gesture
                    privateObject.Invoke("OnGestureRecognized", null, new GestureEventArgs("Sync"));
                    Thread.Sleep(50);

                    // Make sure the Sync event fired,
                    Assert.IsTrue(syncEventFired);

                }
                catch (Exception ex) {
                    Assert.Fail(ex.Message);
                }

                try {
                    // Reset the Sync, Wave, and Shake callbacks
                    syncEventFired = false;
                    waveCallbackInvoked = false;
                    shakeCallbackInvoked = false;

                    // Recognize the Wave gesture again
                    privateObject.Invoke("OnGestureRecognized", null, new GestureEventArgs("Wave"));
                    Thread.Sleep(50);

                    // Make sure the Wave callback fired
                    Assert.IsTrue(waveCallbackInvoked);

                    // Recognize the Shake gesture again
                    privateObject.Invoke("OnGestureRecognized", null, new GestureEventArgs("Shake"));
                    Thread.Sleep(50);

                    // Make sure the Shake callback fired
                    Assert.IsTrue(shakeCallbackInvoked);
                }
                catch (Exception ex) {
                    Assert.Fail(ex.Message);
                }
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        // ************************************************

        /// <summary>
        ///A test for OnSkeletonReady
        ///</summary>
        [TestMethod()]
        public void OnSkeletonReadyTest() {/*
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            GestureRecognizer_Accessor target = new GestureRecognizer_Accessor(param0); // TODO: Initialize to an appropriate value
            object sender = null; // TODO: Initialize to an appropriate value
            IavaSkeletonEventArgs e = null; // TODO: Initialize to an appropriate value
            target.OnSkeletonReady(sender, e);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");*/
        }

        // ************************************************

        /// <summary>
        ///A test for GestureCallbacks
        ///</summary>
        [TestMethod()]
        public void GestureCallbacksTest() {
            GestureRecognizer_Accessor recognizer = new GestureRecognizer_Accessor(_directoryPath);

            string eventName;

            // Hold our expected callbacks
            Dictionary<string, GestureCallback> gestureCallbacks = new Dictionary<string, GestureCallback>();
            gestureCallbacks.Add("Sync", (eventArgs) => { eventName = eventArgs.Name; });
            gestureCallbacks.Add("Wave", (eventArgs) => { eventName = eventArgs.Name; });
            gestureCallbacks.Add("Shake", (eventArgs) => { eventName = eventArgs.Name; });

            try {
                // Set the Gesture Callbacks
                recognizer.GestureCallbacks = gestureCallbacks;

                // Make sure the Gesture Callbacks were updated
                Assert.AreEqual(gestureCallbacks, recognizer.GestureCallbacks);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for SupportedGestures
        ///</summary>
        [TestMethod()]
        public void SupportedGesturesTest() {
            try {
                GestureRecognizer_Accessor recognizer = new GestureRecognizer_Accessor(_directoryPath);

                // Hold our supported gestures
                List<IavaGesture> supportedGestures = new List<IavaGesture>();
                supportedGestures.Add(new IavaGesture("Sync", new List<Snapshot>()));
                supportedGestures.Add(new IavaGesture("Wave", new List<Snapshot>()));
                supportedGestures.Add(new IavaGesture("Shake", new List<Snapshot>()));

                try {
                    // Set the Supported Gestures
                    recognizer.SupportedGestures = supportedGestures;

                    // Make sure the Supported Gestures were updated
                    Assert.AreEqual(supportedGestures, recognizer.SupportedGestures);
                }
                catch (Exception ex) {
                    Assert.Fail(ex.Message);
                }
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for SyncGesture
        ///</summary>
        [TestMethod()]
        public void SyncGestureTest() {
            try {
                GestureRecognizer_Accessor recognizer = new GestureRecognizer_Accessor(_directoryPath);

                // Hold our sync gesture
                IavaGesture syncGesture = new IavaGesture("Sync", new List<Snapshot>());

                try {
                    // Set the Sync Gesture
                    recognizer.SyncGesture = syncGesture;

                    // Make sure the Sync Gesture was updated
                    Assert.AreEqual(syncGesture, recognizer.SyncGesture);
                }
                catch (Exception ex) {
                    Assert.Fail(ex.Message);
                }
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }
    }
}
