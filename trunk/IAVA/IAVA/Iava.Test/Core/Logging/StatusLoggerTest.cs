using Iava.Core.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.IO;

namespace Iava.Test
{     
    /// <summary>
    ///This is a test class for StatusLoggerTest and is intended
    ///to contain all StatusLoggerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StatusLoggerTest
    {
        /// <summary>
        /// Used to signal that callbacks were invoked.
        /// </summary>
        private ManualResetEvent resetEvent = new ManualResetEvent(false);

        /// <summary>
        /// Time (in milliseconds) to wait for a reset event to be set.
        /// </summary>
        private const int TimeoutValue = 200;

        /// <summary>
        /// The message received by the MessageLogged callback.
        /// </summary>
        private Message messageReceived = null;

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

        /// <summary>
        /// Called before each test is run.
        /// </summary>
        [TestInitialize()]
        public void MyTestInitialize()
        {
            StatusLogger_Accessor.Initialize();
        }
        
        /// <summary>
        /// Test cleanup.  Stops the status logger and deletes the log file.
        /// </summary>
        [TestCleanup()]
        public void MyTestCleanup()
        {
            messageReceived = null;
            StatusLogger_Accessor.Shutdown();

            // Allow time for the thread to stop
            Thread.Sleep(500);

            string logFile = StatusLogger_Accessor.logFilePath;
            if (File.Exists(logFile))
            {
                File.Delete(logFile);
            }
        }
        
        #endregion

        /// <summary>
        /// A test for the LogMessage method.
        ///</summary>
        [TestMethod()]
        public void LogMessageTest()
        {            
            try { StatusLogger.LogMessage(null); }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentNullException));
            }

            const string messageText = "Test Message";
            const string messageSource = "MessageSource";
            const MessageType messageType = MessageType.Information;
            Message message = new Message(messageText, messageSource, messageType);

            StatusLogger.LogMessage(message);

            const string innerExceptionText = "Inner Exception TEST";
            const string exceptionText = "This is an exception.";
            Message message2 = new Message(messageText, messageSource, messageType, new ArgumentNullException(exceptionText, new ArgumentException(innerExceptionText)));
            StatusLogger.LogMessage(message2);

            Thread.Sleep(100);
            StatusLogger_Accessor.Shutdown();
            Thread.Sleep(100);

            Assert.IsTrue(File.Exists(StatusLogger_Accessor.logFilePath));

            using (FileStream stream = new FileStream(StatusLogger_Accessor.logFilePath, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(stream))
            {
                string line = reader.ReadLine();
                Assert.IsFalse(string.IsNullOrWhiteSpace(line));
                
                string[] values = line.Split(new [] {'\t'}, StringSplitOptions.RemoveEmptyEntries);
                Assert.AreEqual(4, values.Length);
                Assert.AreEqual(messageType.ToString(), values[1].Trim());
                Assert.AreEqual(messageSource, values[2].Trim());
                Assert.AreEqual(messageText, values[3].Trim());
              
                string line2 = reader.ReadLine();
                Assert.IsFalse(string.IsNullOrWhiteSpace(line2));

                string[] values2 = line2.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                Assert.AreEqual(4, values2.Length);
                Assert.AreEqual(messageType.ToString(), values2[1].Trim());
                Assert.AreEqual(messageSource, values2[2].Trim());
                Assert.AreEqual(messageText, values2[3].Trim());

                string remainder = reader.ReadToEnd();
                Assert.IsTrue(remainder.Contains("Exception: "));
                Assert.IsTrue(remainder.Contains(" Inner Exception: "));
                Assert.IsTrue(remainder.Contains(" Stack Trace: "));
                Assert.IsTrue(remainder.Contains(innerExceptionText));
                Assert.IsTrue(remainder.Contains(exceptionText));    
            }           
        }

        /// <summary>
        /// Tests that a callback method is invoked when a message is logged.
        /// </summary>
        [TestMethod]
        public void LogMessageCallbackTest()
        {
            StatusLogger.MessageLogged += MessageLoggedCallback;

            const string messageText = "Test Message";
            const string messageSource = "MessageSource";
            const MessageType messageType = MessageType.Error;
            const string innerExceptionText = "Inner Exception TEST";
            const string exceptionText = "This is an exception.";
            ArgumentNullException exception = new ArgumentNullException(exceptionText, new ArgumentException(innerExceptionText));
            Message messageToLog = new Message(messageText, messageSource, messageType, exception);

            StatusLogger.LogMessage(messageToLog);

            Assert.IsTrue(resetEvent.WaitOne(TimeoutValue));
            Assert.IsNotNull(messageReceived);
            Assert.AreEqual(messageText, messageReceived.Text);
            Assert.AreEqual(messageSource, messageReceived.Source);
            Assert.AreEqual(messageType, messageReceived.Level);
            Assert.AreEqual(exceptionText, messageReceived.Exception.Message);
            Assert.AreEqual(innerExceptionText, messageReceived.Exception.InnerException.Message);

            // Test that another message is not logged if unsubscribed
            resetEvent.Reset();
            StatusLogger.MessageLogged -= MessageLoggedCallback;
            StatusLogger.LogMessage(messageToLog);
            Assert.IsFalse(resetEvent.WaitOne(TimeoutValue));
        }

        /// <summary>
        /// Called when a message is logged.
        /// </summary>
        /// <param name="message">Logged message</param>
        private void MessageLoggedCallback(Message message)
        {
            messageReceived = message;
            resetEvent.Set();
        }
    }
}
