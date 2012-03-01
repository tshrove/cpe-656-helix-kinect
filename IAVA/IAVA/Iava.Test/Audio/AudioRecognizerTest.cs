using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

using Iava.Audio;
using Iava.Core;

using System.Speech.AudioFormat;
using System.Speech.Recognition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Iava.Test.Audio
{
    /// <summary>
    /// Tests the AudioRecognizer class.
    /// </summary>
    [TestClass]
    public class AudioRecognizerTest
    {
        public AudioRecognizerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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

            if (recognizer != null)
            {
                recognizer.Stop();
            }

            recognizer = null;
        }
        
        #endregion

        /// <summary>
        /// Tests the Start method.
        /// </summary>
        [TestMethod]
        public void StartTest()
        {
            recognizer = new AudioRecognizer();

            Assert.AreEqual<RecognizerStatus>(RecognizerStatus.NotReady, recognizer.Status);
            recognizer.Started += RecognizerStatusCallback;

            try
            {
                recognizer.Start();
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Running, recognizer.Status);

                // Ensure the OnStarted callback was invoked
                Thread.Sleep(1000);
                Assert.IsTrue(recognizerCallbackInvoked, "OnStarted callback was not invoked.");

                recognizer.Start();
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Running, recognizer.Status);
            }
            finally
            {
                if (recognizer != null)
                {
                    recognizer.Started -= RecognizerStatusCallback;
                }
            }
        }

        /// <summary>
        /// Tests the Stop method.
        /// </summary>
        [TestMethod]
        public void StopTest()
        {
            recognizer = new AudioRecognizer();

            Assert.AreEqual<RecognizerStatus>(RecognizerStatus.NotReady, recognizer.Status);
            recognizer.Stopped += RecognizerStatusCallback;

            try
            {
                recognizer.Start();
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Running, recognizer.Status);

                recognizer.Stop();
                Thread.Sleep(1000);
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Ready, recognizer.Status);
                Assert.IsTrue(recognizerCallbackInvoked, "OnStopped callback was not invoked.");

                // Start and stop immediately after one another and ensure it can be started again
                recognizer.Start();
                recognizer.Stop();
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Ready, recognizer.Status);
                recognizer.Start();
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Running, recognizer.Status);
            }
            finally
            {
                if (recognizer != null)
                {
                    recognizer.Stopped -= RecognizerStatusCallback;
                }
            }
        }

        /// <summary>
        /// Tests the Subscribe method.
        /// </summary>
        [TestMethod]
        public void SubscribeTest()
        {
            const string commandString = "Test Callback";

            // Create a mock speech engine and set it up
            var mockEngine = SetupMockSpeechRecognitionEngine();

            recognizer = new AudioRecognizer(mockEngine.Object);

            bool callback1Invoked = false;
            recognizer.Subscribe(commandString, (eventArgs) => 
                {
                    if (callback1Invoked)
                    {
                        Assert.Fail("Audio callback method was called more than once.");
                    }

                    Assert.AreEqual(commandString, eventArgs.Command, "Command string returned did not match expected value.");
                    callback1Invoked = true;                   
                });           

            // Test for exception throwing on invalid parameters entered
            try
            { recognizer.Subscribe(string.Empty, (eventArgs) => { }); }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            try
            { recognizer.Subscribe(commandString, null); }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
            
            recognizer.Start();

            const string commandString2 = "Command String 2";
            bool callback2Invoked = false;
            recognizer.Subscribe(commandString2, (eventArgs) =>
            {
                Assert.AreEqual(commandString2, eventArgs.Command, "Command string returned did not match expected value.");
                callback2Invoked = true;
            });

            Thread.Sleep(100);  // Allow time to restart

            // Sync the callback first then raise spoken event
            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs("Blah Blah blah", recognizer.AudioConfidenceThreshold + 0.01f));
            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs(recognizer.SyncCommand, recognizer.AudioConfidenceThreshold + 0.01f));
            Thread.Sleep(50);
            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs(commandString, recognizer.AudioConfidenceThreshold + 0.01f));
            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs("Blah Blah blah", recognizer.AudioConfidenceThreshold + 0.01f));
            Thread.Sleep(100);
            Assert.IsTrue(callback1Invoked);

            // Callback with the same command but with confidence below the threshold level to ensure the audio callback method is not called
            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs(commandString, recognizer.AudioConfidenceThreshold - 0.01f));
            
            // Call second command
            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs(commandString2, 0.95f));
            Thread.Sleep(100);
            Assert.IsTrue(callback2Invoked);

            // For code coverage
            try
            { recognizer.Subscribe(commandString2, (eventArgs) => { }); }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
            }
        }

        /// <summary>
        /// Tests the Unsubscribe method.
        /// </summary>
        [TestMethod]
        public void UnsubscribeTest()
        {
            const string commandString = "Test Callback";

            // Create a mock speech engine and set it up
            var mockEngine = SetupMockSpeechRecognitionEngine();

            recognizer = new AudioRecognizer(mockEngine.Object);

            // Test the audio confidence level
            try
            { recognizer.AudioConfidenceThreshold = -0.5f; }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentOutOfRangeException));
            }

            try
            { recognizer.AudioConfidenceThreshold = 1.01f; }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentOutOfRangeException));
            }

            recognizer.AudioConfidenceThreshold = 0.6f;

            // Raise the speech recognized event, unsubscribe the event, and try the command again
            bool callback1Invoked = false;
            recognizer.Subscribe(commandString, (eventArgs) =>
                {
                    Assert.AreEqual(commandString, eventArgs.Command, "Command string returned did not match expected value.");
                    callback1Invoked = true;
                });

            recognizer.Start();

            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs(recognizer.SyncCommand, recognizer.AudioConfidenceThreshold + 0.01f));
            Thread.Sleep(50);

            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs(commandString, recognizer.AudioConfidenceThreshold + 0.01f));
            Thread.Sleep(100);
            Assert.IsTrue(callback1Invoked);
            callback1Invoked = false;

            recognizer.Unsubscribe(commandString);

            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs(commandString, recognizer.AudioConfidenceThreshold + 0.01f));
            Assert.IsFalse(callback1Invoked, "Audio callback method was called when the command was unsubscribed.");

            // For code coverage
            try
            { recognizer.Unsubscribe(null); }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
            }

            try
            { recognizer.Unsubscribe("Non Existant Key"); }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
            }
        }

        /// <summary>
        /// Tests the syncing and unsyncing of the recognizer.
        /// </summary>
        [TestMethod]
        public void SyncUnsyncTest()
        {
            const string commandString = "Test Callback";

            // Create a mock speech engine and set it up
            var mockEngine = SetupMockSpeechRecognitionEngine();
            recognizer = new AudioRecognizer(mockEngine.Object);

            bool syncedCallbackInvoked = false;
            recognizer.Synced += 
                (sender, args) => 
                {
                    syncedCallbackInvoked = true;
                };

            bool unsyncedCallbackInvoked = false;
            recognizer.Unsynced +=
                (sender, args) =>
                {
                    unsyncedCallbackInvoked = true;
                };

            // Sync the recognizer, call a command, wait until un-sync and re-call the same command.
            // Ensure the callback is not called twice.

            bool callback1Invoked = false;
            recognizer.Subscribe(commandString, (eventArgs) =>
            {
                Assert.AreEqual(commandString, eventArgs.Command, "Command string returned did not match expected value.");
                callback1Invoked = true;
            });

            // Set the timeout to 5 seconds
            recognizer.SyncTimeoutValue = 5000;
            
            recognizer.Start();

            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs(recognizer.SyncCommand, recognizer.AudioConfidenceThreshold + 0.01f));
            Thread.Sleep(50);
            Assert.IsTrue(syncedCallbackInvoked);

            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs(commandString, recognizer.AudioConfidenceThreshold + 0.01f));
            Thread.Sleep(100);
            Assert.IsTrue(callback1Invoked);
            callback1Invoked = false;

            Thread.Sleep(recognizer.SyncTimeoutValue + 100);
            Assert.IsTrue(unsyncedCallbackInvoked);

            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs(commandString, recognizer.AudioConfidenceThreshold + 0.01f));
            Assert.IsFalse(callback1Invoked);
        }

        /// <summary>
        /// Tests changing the sync command
        /// </summary>
        [TestMethod]
        public void SyncCommandTest()
        {        
            // Create a mock speech engine and set it up
            var mockEngine = SetupMockSpeechRecognitionEngine();
            recognizer = new AudioRecognizer(mockEngine.Object);

            // Test changing the sync command
            try { recognizer.SyncCommand = null; }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            }

            try { recognizer.SyncCommand = string.Empty; }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            }

            try { recognizer.SyncCommand = "    "; }
            catch (Exception exception)
            {
                Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            }

            recognizer.SyncCommand = "Test Sync Command";  

            const string commandString = "Test Callback";
            bool callback1Invoked = false;
            recognizer.Subscribe(commandString, (eventArgs) =>
            {
                Assert.AreEqual(commandString, eventArgs.Command, "Command string returned did not match expected value.");
                callback1Invoked = true;
            });

            recognizer.Start();

            // Sync the callback first then raise spoken event
            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs(recognizer.SyncCommand, recognizer.AudioConfidenceThreshold + 0.01f));
            Thread.Sleep(50);
            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs(commandString, recognizer.AudioConfidenceThreshold + 0.01f));
            Thread.Sleep(100);
            Assert.IsTrue(callback1Invoked);
            callback1Invoked = false;

            // Change the sync command, re-sync and recognize the command again
            recognizer.SyncCommand = "Open the pod bay doors HAL...";
            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs(recognizer.SyncCommand, recognizer.AudioConfidenceThreshold + 0.01f));
            Thread.Sleep(50);
            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs(commandString, recognizer.AudioConfidenceThreshold + 0.01f));
            Thread.Sleep(100);
            Assert.IsTrue(callback1Invoked);
        }

        /// <summary>
        /// Tests wildcard functionality.
        /// </summary>
        [TestMethod]
        public void WildcardTest()
        {
            // Create a mock speech engine and set it up
            var mockEngine = SetupMockSpeechRecognitionEngine();
            recognizer = new AudioRecognizer(mockEngine.Object);

            const string commandString = "Test Callback *";
            
            bool callback1Invoked = false;
            recognizer.Subscribe(commandString, (eventArgs) =>
            {
                Assert.AreEqual(commandString, eventArgs.Command, "Command string returned did not match expected value.");

                Assert.IsNotNull(eventArgs.CommandWildcards);
                Assert.AreEqual(3, eventArgs.CommandWildcards.Count);
                Assert.AreEqual("one", eventArgs.CommandWildcards[0]);
                Assert.AreEqual("two", eventArgs.CommandWildcards[1]);
                Assert.AreEqual("three", eventArgs.CommandWildcards[2]);

                callback1Invoked = true;
            });

            recognizer.Start();

            // Sync the recognizer
            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs(recognizer.SyncCommand, recognizer.AudioConfidenceThreshold + 0.01f));
            Thread.Sleep(50);

            // Raise the recognized command
            const string wildcardPhrase = "one two three";
            mockEngine.Raise(m => m.SpeechRecognized += null, new IavaSpeechRecognizedEventArgs(commandString.Replace('*', ' ') + wildcardPhrase, recognizer.AudioConfidenceThreshold + 0.01f));
            Thread.Sleep(150);
            Assert.IsTrue(callback1Invoked);
        }

        #region Private Methods And Attributes

        /// <summary>
        /// Recognizer object under test.
        /// </summary>
        private AudioRecognizer recognizer;

        /// <summary>
        /// Used to determine if the recognizer status ballback was invoked.
        /// </summary>
        private bool recognizerCallbackInvoked;

        /// <summary>
        /// Called when the recognizer's status changes.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void RecognizerStatusCallback(object sender, EventArgs e)
        {
            recognizerCallbackInvoked = true;
        }

        /// <summary>
        /// Creates and sets up a mock speech recongition engine.
        /// </summary>
        /// <returns>Mock engine</returns>
        private Mock<ISpeechRecognitionEngine> SetupMockSpeechRecognitionEngine()
        {
            var mockEngine = new Mock<ISpeechRecognitionEngine>(MockBehavior.Strict);

            // Setup the methods that are going to be called
            mockEngine.Setup(m => m.LoadGrammar(It.IsAny<Grammar>()));
            mockEngine.Setup(m => m.SetInputToAudioStream(It.IsAny<Stream>(), It.IsAny<SpeechAudioFormatInfo>()));
            mockEngine.Setup(m => m.RecognizeAsync(RecognizeMode.Multiple));
            mockEngine.Setup(m => m.RecognizeAsyncStop());

            return mockEngine;
        }

        #endregion
    }
}
