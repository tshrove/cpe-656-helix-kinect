using System;

namespace Iava.Core.Logging {

    /// <summary>
    /// Log message type.
    /// </summary>
    public enum MessageType {
        Information,
        Warning,
        Error
    }

    /// <summary>
    /// Log message class.
    /// </summary>
    public class Message {

        #region Public Properties

        /// <summary>
        /// Exception logged with the message.  If no exception was logged with the message, 
        /// this value will be null.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// The level at which the message should be logged.
        /// </summary>
        public MessageType Level { get; private set; }

        /// <summary>
        /// Source of the log message (usually the class name the message originated from).
        /// </summary>
        public string Source { get; private set; }

        /// <summary>
        /// The message text.
        /// </summary>
        public string Text { get; private set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Message text</param>
        /// <param name="source">Source of message, usually the class name</param>
        /// <param name="level">Message level</param>
        /// <param name="exception">Exception that was thrown</param>
        internal Message(string text, string source, MessageType level, Exception exception = null) {
            Text = text;
            Source = source;
            Level = level;
            Exception = exception;
        }

        #endregion Constructors
    }
}
