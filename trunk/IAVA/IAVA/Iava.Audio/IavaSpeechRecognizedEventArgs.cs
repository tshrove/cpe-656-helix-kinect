using System;
using System.Speech.Recognition;

namespace Iava.Audio {

    /// <summary>
    /// Speed recognition event arguments.
    /// </summary>
    internal class IavaSpeechRecognizedEventArgs : EventArgs {

        #region Public Properties

        /// <summary>
        /// The confidence level of the recognized speech.
        /// </summary>
        public float Confidence { get; private set; }

        /// <summary>
        /// The spoken text the engine recognized.
        /// </summary>
        public string Text { get; private set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="eventArg">Speech recognized event args</param>
        public IavaSpeechRecognizedEventArgs(SpeechRecognizedEventArgs eventArg) {
            if (eventArg == null) {
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
        public IavaSpeechRecognizedEventArgs(string text, float confidence) {
            Text = text;
            Confidence = confidence;
        }

        #endregion Constructors
    }
}
