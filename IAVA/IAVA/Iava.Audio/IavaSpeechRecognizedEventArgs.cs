using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Speech.Recognition;

namespace Iava.Audio
{
    public class IavaSpeechRecognizedEventArgs : EventArgs
    {
        public string Text { get; private set; }

        public float Confidence { get; private set; }

        public IavaSpeechRecognizedEventArgs(SpeechRecognizedEventArgs eventArg)
        {
            if (eventArg == null)
            {
                throw new ArgumentException("eventArg parameter was null.", "eventArg");
            }

            Text = eventArg.Result.Text;
            Confidence = eventArg.Result.Confidence;
        }

        /// <summary>
        /// Argument constructor.  Used for unit testing.
        /// </summary>
        /// <param name="confidence">confidence</param>
        /// <param name="text">text</param>
        public IavaSpeechRecognizedEventArgs(string text, float confidence)
        {
            Text = text;
            Confidence = confidence;
        }
    }
}
