using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace Iava.Core.Logging
{    
    /// <summary>
    /// Method signature for a message logged event.
    /// </summary>
    /// <param name="message">message object</param>
    public delegate void MessageLoggedDelegate(Message message);

    /// <summary>
    /// Class that handles logging status messages to a file. 
    /// </summary>
    public class StatusLogger
    {
        #region Private Attributes

        private static readonly ConcurrentQueue<Message> messageQueue = new ConcurrentQueue<Message>();

        private static readonly CancellationTokenSource tokenSource = new CancellationTokenSource();

        private static StreamWriter writer;

        private static string logFilePath;

        private static Task messageConsumeTask;

        private const int ConsumeTaskWaitTime = 50;

        #endregion

        /// <summary>
        /// Message logged event.  Invoked when a message is logged.
        /// </summary>
        public static event MessageLoggedDelegate MessageLogged;

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        internal static void LogMessage(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message", "Message parameter cannot be null.");
            }

            // Store message in list
            messageQueue.Enqueue(message);

            if (MessageLogged != null)
            {
                try
                {
                    MessageLogged.Invoke(message);
                }
                catch
                { 
                    // Ignore
                }
            }
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static StatusLogger()
        {
            Initialize();         
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~StatusLogger()
        {
            Shutdown();
        }

        /// <summary>
        /// Initializes the status logger.  No messages will be logged unless this method is called first.
        /// </summary>
        private static void Initialize()
        {               
            // TODO: Add lock mechanism so that only one task is started
            // Start task
            messageConsumeTask = Task.Factory.StartNew(_ => ConsumeMessageTask(tokenSource.Token), tokenSource.Token, TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// Shuts down the status logger.
        /// </summary>
        private static void Shutdown()
        {
            tokenSource.Cancel();
            messageConsumeTask = null;
        }

        /// <summary>
        /// Creates a log file.
        /// </summary>
        private static void CreateLogFile()
        {
            string logFileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IAVA\";
            if (!Directory.Exists(logFileDirectory))
            {
                Directory.CreateDirectory(logFileDirectory);
            }

            logFilePath = logFileDirectory + "IAVA_Log_" + DateTime.Now.ToString("ddMMyy_hhmmss") + ".txt";
            writer = new StreamWriter(logFilePath);
        }

        /// <summary>
        /// Task that writes messages to a log file.
        /// </summary>
        /// <param name="token">cancellation token</param>
        private static void ConsumeMessageTask(CancellationToken token)
        {
            try
            {               
                CreateLogFile();

                // Write data to a file
                while (!token.IsCancellationRequested)
                {
                    WaitHandle.WaitAny(new[] { token.WaitHandle }, ConsumeTaskWaitTime);

                    Message message;
                    while (messageQueue.TryDequeue(out message))
                    {
                        WriteMessageToLog(message);
                    }

                    writer.Flush();
                }
            }
            catch
            {
                // Ignore
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer = null;
                }                
            }
        }

        /// <summary>
        /// Formats a log message and writes it to the log file.
        /// </summary>
        /// <param name="message">message to write to the log file</param>
        private static void WriteMessageToLog(Message message)
        {
            // Time Stamp    Message Level   Source   Text
            //      Exception (if not null)
            string timeStamp = DateTime.Now.ToString("dd-MM-yy hh:mm:ss");
            string messageToLog = timeStamp + "\t" + message.Level.ToString().PadRight(12) + "\t" + message.Source + "\t" + message.Text;
            if (message.Exception != null)
            {
                messageToLog += "\n\t Exception: " + message.Exception;
                if (message.Exception.InnerException != null)
                {
                    messageToLog += "\n\t Inner Exception: " + message.Exception.InnerException;
                }

                messageToLog += "\n\t Stack Trace: " + message.Exception.StackTrace;                               
            }

            writer.WriteLine(messageToLog);
        }
    }
}
