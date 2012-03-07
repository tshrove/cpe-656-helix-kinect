using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iava.Core.Logging
{
    public enum MessageType
    {
        Information,
        Warning,
        Error
    }

    public class Message
    {
        public string Text { get; private set; }

        public string Source { get; private set; }

        public MessageType Level { get; private set; }      

        public Exception Exception { get; private set; }

        internal Message(string text, string source, MessageType level, Exception exception = null)
        {
            Text = text;
            Source = source;
            Level = level;          
            Exception = exception;
        }
    }
}
