using System;
using System.Threading;
using Iava.Core;
using Iava.Gesture;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iava.Test {

    /// <summary>
    ///This is a test class for RecognizerTest and is intended
    ///to contain all RecognizerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RecognizerTest {

        /// <summary>
        ///A test for OnFailed
        ///</summary>
        [TestMethod()]
        public void RecognizerOnFailedTest() {
            // Since we're treating this as the generic Recognizer this is ok
            Recognizer recognizer = new GestureRecognizer(string.Empty);

            bool eventFired = false;

            // Register for the Recognizer Failed Event
            recognizer.Failed += (param1, param2) => eventFired = true;

            try {
                // Simulate the Failed Event
                PrivateObject privateObject = new PrivateObject(recognizer);
                privateObject.Invoke("OnFailed", null, EventArgs.Empty);
                Thread.Sleep(50);

                // Make sure the Recognizer Failed Event was fired
                Assert.IsTrue(eventFired);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for OnStarted
        ///</summary>
        [TestMethod()]
        public void RecognizerOnStartedTest() {
            // Since we're treating this as the generic Recognizer this is ok
            Recognizer recognizer = new GestureRecognizer(string.Empty);

            bool eventFired = false;

            // Register for the Recognizer Started Event
            recognizer.Started += (param1, param2) => eventFired = true;

            try {
                // Simulate the Started Event
                PrivateObject privateObject = new PrivateObject(recognizer);
                privateObject.Invoke("OnStarted", null, EventArgs.Empty);
                Thread.Sleep(50);

                // Make sure the Recognizer Started Event was fired
                Assert.IsTrue(eventFired);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for OnStatusChanged
        ///</summary>
        [TestMethod()]
        public void RecognizerOnStatusChangedTest() {
            // Since we're treating this as the generic Recognizer this is ok
            Recognizer recognizer = new GestureRecognizer(string.Empty);

            bool eventFired = false;

            // Register for the Recognizer StatusChanged Event
            recognizer.StatusChanged += (param1, param2) => eventFired = true;

            try {
                // Simulate the StatusChanged Event
                PrivateObject privateObject = new PrivateObject(recognizer);
                privateObject.Invoke("OnStatusChanged", null, EventArgs.Empty);
                Thread.Sleep(50);

                // Make sure the Recognizer StatusChanged Event was fired
                Assert.IsTrue(eventFired);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for OnStopped
        ///</summary>
        [TestMethod()]
        public void RecognizerOnStoppedTest() {
            // Since we're treating this as the generic Recognizer this is ok
            Recognizer recognizer = new GestureRecognizer(string.Empty);

            bool eventFired = false;

            // Register for the Recognizer Stopped Event
            recognizer.Stopped += (param1, param2) => eventFired = true;

            try {
                // Simulate the Stopped Event
                PrivateObject privateObject = new PrivateObject(recognizer);
                privateObject.Invoke("OnStopped", null, EventArgs.Empty);
                Thread.Sleep(50);

                // Make sure the Recognizer Stopped Event was fired
                Assert.IsTrue(eventFired);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for OnSynced
        ///</summary>
        [TestMethod()]
        public void RecognizerOnSyncedTest() {
            // Since we're treating this as the generic Recognizer this is ok
            Recognizer recognizer = new GestureRecognizer(string.Empty);

            bool eventFired = false;

            // Register for the Recognizer Synced Event
            recognizer.Synced += (param1, param2) => eventFired = true;

            try {
                // Simulate the Synced Event
                PrivateObject privateObject = new PrivateObject(recognizer);
                privateObject.Invoke("OnSynced", null, EventArgs.Empty);
                Thread.Sleep(50);

                // Make sure the Recognizer Synced Event was fired
                Assert.IsTrue(eventFired);

                // Reset the event
                eventFired = false;

                // Simulate the Synced Event again
                privateObject.Invoke("OnSynced", null, EventArgs.Empty);
                Thread.Sleep(50);

                // Make sure the Recognizer Synced Event was not fired
                Assert.IsFalse(eventFired);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for OnUnsynced
        ///</summary>
        [TestMethod()]
        public void RecognizerOnUnsyncedTest() {
            // Since we're treating this as the generic Recognizer this is ok
            Recognizer recognizer = new GestureRecognizer(string.Empty);

            bool eventFired = false;

            // Register for the Recognizer Unsynced Event
            recognizer.Unsynced += (param1, param2) => eventFired = true;

            try {
                // Try to Unsync before Syncing
                PrivateObject privateObject = new PrivateObject(recognizer);
                privateObject.Invoke("OnUnsynced", null, EventArgs.Empty);
                Thread.Sleep(50);

                // Make sure the Recognizer Unsynced Event was not fired
                Assert.IsFalse(eventFired);

                // Simulate the Synced Event
                privateObject.Invoke("OnSynced", null, EventArgs.Empty);
                Thread.Sleep(50);

                // Simulate the Unsynced Event
                privateObject.Invoke("OnUnsynced", null, EventArgs.Empty);
                Thread.Sleep(50);

                // Make sure the Recognizer Unsynced Event was fired
                Assert.IsTrue(eventFired);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for Status
        ///</summary>
        [TestMethod()]
        public void RecognizerStatusTest() {
            // Since we're treating this as the generic Recognizer this is ok
            Recognizer recognizer = new GestureRecognizer(string.Empty);

            bool eventFired = false;

            // Register for the Recognizer StatusChanged Event
            recognizer.StatusChanged += (param1, param2) => eventFired = true;

            try {
                // Simulate the StatusChanged Event with the Error status
                PrivateObject privateObject = new PrivateObject(recognizer);
                privateObject.SetProperty("Status", RecognizerStatus.Error);
                Thread.Sleep(50);

                // Make sure the Recognizer Status was updated
                Assert.AreEqual(RecognizerStatus.Error, recognizer.Status);

                // Make sure the Recognizer StatusChanged Event was fired
                Assert.IsTrue(eventFired);

                // Reset the event
                eventFired = false;

                // Simulate the StatusChanged Event with the NotReady status
                privateObject.SetProperty("Status", RecognizerStatus.NotReady);
                Thread.Sleep(50);

                // Make sure the Recognizer Status was updated
                Assert.AreEqual(RecognizerStatus.NotReady, recognizer.Status);

                // Make sure the Recognizer StatusChanged Event was fired
                Assert.IsTrue(eventFired);

                // Reset the event
                eventFired = false;

                // Simulate the StatusChanged Event with the Ready status
                privateObject.SetProperty("Status", RecognizerStatus.Ready);
                Thread.Sleep(50);

                // Make sure the Recognizer Status was updated
                Assert.AreEqual(RecognizerStatus.Ready, recognizer.Status);

                // Make sure the Recognizer StatusChanged Event was fired
                Assert.IsTrue(eventFired);

                // Reset the event
                eventFired = false;

                // Simulate the StatusChanged Event with the Running status
                privateObject.SetProperty("Status", RecognizerStatus.Running);
                Thread.Sleep(50);

                // Make sure the Recognizer Status was updated
                Assert.AreEqual(RecognizerStatus.Running, recognizer.Status);

                // Make sure the Recognizer StatusChanged Event was fired
                Assert.IsTrue(eventFired);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for SyncTimeoutValue
        ///</summary>
        [TestMethod()]
        public void RecognizerSyncTimeoutValueTest() {
            // Since we're treating this as the generic Recognizer this is ok
            Recognizer recognizer = new GestureRecognizer(string.Empty);

            int timeout = 123456;
            
            try {
                // Set the Timeout Value
                recognizer.SyncTimeoutValue = 123456;

                // Make sure the Timeout Value was updated
                Assert.AreEqual(timeout, recognizer.SyncTimeoutValue);
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }
    }
}
