using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Iava.Core;
using Iava.Audio;
using System.Threading;

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
            recognizer = new AudioRecognizer(string.Empty);

            Assert.AreEqual<RecognizerStatus>(RecognizerStatus.NotReady, recognizer.Status);
            recognizer.Started += RecognizerCallback;

            try
            {
                recognizer.Start();
                Thread.Sleep(2000);
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Running, recognizer.Status);

                // Ensure the OnStarted callback was invoked
                Assert.IsTrue(recognizerCallbackInvoked, "OnStarted callback was not invoked.");

                recognizer.Start();
            }
            finally
            {
                if (recognizer != null)
                {
                    recognizer.Started -= RecognizerCallback;
                }
            }
        }

        /// <summary>
        /// Tests the Stop method.
        /// </summary>
        [TestMethod]
        public void StopTest()
        {
            recognizer = new AudioRecognizer(string.Empty);

            Assert.AreEqual<RecognizerStatus>(RecognizerStatus.NotReady, recognizer.Status);
            recognizer.Stopped += RecognizerCallback;

            try
            {
                recognizer.Start();
                Thread.Sleep(2000);
                Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Running, recognizer.Status);

                recognizer.Stop();
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
            finally
            {
                if (recognizer != null)
                {
                    recognizer.Stopped -= RecognizerCallback;
                }
            }
        }

        /// <summary>
        /// Tests the Subscribe method.
        /// </summary>
        [TestMethod]
        public void SubscribeTest()
        {
            recognizer = new AudioRecognizer(string.Empty);
            const string commandString = "Test Callback";

            try
            {
                recognizer.Subscribe(string.Empty, AudioRecognizedCallback);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            try
            {
                recognizer.Subscribe(commandString, null);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            // Subscribe before started
            recognizer.Subscribe(commandString, AudioRecognizedCallback);

            recognizer.Start();
            Thread.Sleep(2000);
            Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Running, recognizer.Status);

            // Subscribe after started
            recognizer.Subscribe("Test Callback 2", AudioRecognizedCallback);            
        }

        /// <summary>
        /// Tests the Unsubscribe method.
        /// </summary>
        [TestMethod]
        public void UnsubscribeTest()
        {
            recognizer = new AudioRecognizer(string.Empty);
            const string commandString = "Test Callback";

            // Unsubscribe before started
            recognizer.Subscribe(commandString, AudioRecognizedCallback);
            recognizer.Unsubscribe(commandString);

            recognizer.Start();
            Thread.Sleep(2000);
            Assert.AreEqual<RecognizerStatus>(RecognizerStatus.Running, recognizer.Status);

            // Unsubscribe after started
            string commandString2 = "Test Callback 2";
            recognizer.Subscribe(commandString2, AudioRecognizedCallback);
            recognizer.Unsubscribe(null);
            recognizer.Unsubscribe(commandString2);
        }

        #region Private Methods And Attributes

        private AudioRecognizer recognizer;

        private bool recognizerCallbackInvoked;

        void RecognizerCallback(object sender, EventArgs e)
        {
            recognizerCallbackInvoked = true;
        }

        void AudioRecognizedCallback(AudioEventArgs e)
        {

        }

        #endregion
    }
}
