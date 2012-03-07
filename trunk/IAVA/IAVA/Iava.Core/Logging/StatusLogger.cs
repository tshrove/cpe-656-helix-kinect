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
    public delegate void MessageLoggedDelegate(Message message);

    public class StatusLogger
    {
        private static readonly ConcurrentQueue<Message> messageQueue = new ConcurrentQueue<Message>();

        private static readonly CancellationTokenSource tokenSource = new CancellationTokenSource();

        private static StreamWriter writer;

        private static string logFileDirectory;

        public static event MessageLoggedDelegate MessageLogged;

        internal static void LogMessage(Message message)
        {
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

        static StatusLogger()
        {
            logFileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\IAVA\";
            if (!Directory.Exists(logFileDirectory))
            {
                Directory.CreateDirectory(logFileDirectory);
            }

            CreateLogFile();           
        }

        ~StatusLogger()
        {
            tokenSource.Cancel();           
        }

        private static void CreateLogFile()
        {
            string logFilePath = logFileDirectory + "IAVA_Log_" + DateTime.Now.ToString("ddMMyy_hhmmss") + ".txt";           

            writer = new StreamWriter(logFilePath);

            // Start task
            Task.Factory.StartNew(_ => ConsumeMessageTask(tokenSource.Token), tokenSource.Token, TaskCreationOptions.LongRunning);
        }

        private static void ConsumeMessageTask(CancellationToken token)
        {
            // Write data to a file
            while (!token.IsCancellationRequested)
            {
                Message message;
                while (messageQueue.TryDequeue(out message))
                {
                    WriteMessageToLog(message);
                }

                writer.Flush();
                Thread.Sleep(50);
            }

            writer.Close();
        }

        private static void WriteMessageToLog(Message message)
        {
            // Time Stamp    Message Level   Source   Text
            //      Exception (if not null)
            string timeStamp = DateTime.Now.ToString("dd-MM-yy hh:mm:ss");
            string messageToLog = timeStamp + "\t" + message.Level.ToString().PadRight(12) + "\t" + message.Source + "\t" + message.Text;
            if (message.Exception != null)
            {
                messageToLog += "\n\t EXCEPTION: " + message.Exception;
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
