using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iava.Core.Logging
{
    /// <summary>
    /// Log message type.
    /// </summary>
    public enum MessageType
    {
        Information,
        Warning,
        Error
    }

    /// <summary>
    /// Log message class.
    /// </summary>
    public class Message
    {
        public string Text { get; private set; }

        public string Source { get; private set; }

        public MessageType Level { get; private set; }      

        public Exception Exception { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Message text</param>
        /// <param name="source">Source of message, usually the class name</param>
        /// <param name="level">Message level</param>
        /// <param name="exception">Exception that was thrown</param>
        internal Message(string text, string source, MessageType level, Exception exception = null)
        {
            Text = text;
            Source = source;
            Level = level;          
            Exception = exception;
        }

    }
}
