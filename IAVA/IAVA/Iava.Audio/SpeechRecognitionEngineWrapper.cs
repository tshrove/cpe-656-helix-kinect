using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;

namespace Iava.Audio
{
    /// <summary>
    /// Wraps Microsoft's SpeechRecognitionEngine class so it conforms to the
    /// ISpeechRecognitionEngine interface.
    /// </summary>
    internal class SpeechRecognitionEngineWrapper : ISpeechRecognitionEngine
    {
        /// <summary>
        /// Event handler for when speech is recognized.
        /// </summary>
        private event EventHandler<IavaSpeechRecognizedEventArgs> SpeechRecognizedHandler;

        /// <summary>
        /// Recognizer ID for the Kinect.
        /// </summary>
        private const string RecognizerId = "SR_MS_en-US_Kinect_10.0";

        /// <summary>
        /// Reference to the speech recognition engine.
        /// </summary>
        private readonly SpeechRecognitionEngine engine;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SpeechRecognitionEngineWrapper()
        {
            //RecognizerInfo ri = SpeechRecognitionEngine.InstalledRecognizers().Where(
            //                r => r.Id == RecognizerId).FirstOrDefault();

            RecognizerInfo ri = GetKinectRecognizer();

            if (ri == null)
            {
                throw new Exception("Failed to find any installed audio recognizers.");
            }

            engine = new SpeechRecognitionEngine(ri.Id);

            engine.SpeechRecognized += OnSpeechRecognized;
        }

        private static RecognizerInfo GetKinectRecognizer()
        {
            //Func<RecognizerInfo, bool> matchingFunc = r =>
            //{
            //    string value;
            //    r.AdditionalInfo.TryGetValue("Kinect", out value);
            //    return "True".Equals(value, StringComparison.InvariantCultureIgnoreCase) && "en-US".Equals(r.Culture.Name, StringComparison.InvariantCultureIgnoreCase);
            //};

            //return SpeechRecognitionEngine.InstalledRecognizers().Where(matchingFunc).FirstOrDefault();

            RecognizerInfo rv = null;

            var recognizers = SpeechRecognitionEngine.InstalledRecognizers();
            foreach (var recognizer in recognizers)
            {
                if (recognizer.Culture.Name == "en-US")
                {
                    rv = recognizer;
                    break;
                }
            }
            //if (recognizers.Count > 0)
            //{
            //    rv = recognizers[0];
            //}            

            return rv;
        }
    
        #region ISpeechRecognition Members

        void  ISpeechRecognitionEngine.LoadGrammar(Grammar grammar)
        {
            engine.LoadGrammar(grammar);
        }

        void  ISpeechRecognitionEngine.SetInputToAudioStream(System.IO.Stream stream, System.Speech.AudioFormat.SpeechAudioFormatInfo audioFormat)
        {
            engine.SetInputToAudioStream(stream, audioFormat);
        }

        void  ISpeechRecognitionEngine.RecognizeAsync(RecognizeMode mode)
        {
            engine.RecognizeAsync(mode);
        }

        void ISpeechRecognitionEngine.RecognizeAsyncStop()
        {
            engine.RecognizeAsyncStop();
        }

        event EventHandler<IavaSpeechRecognizedEventArgs> ISpeechRecognitionEngine.SpeechRecognized
        {
            add 
            {
                SpeechRecognizedHandler += value;
            }
            remove 
            { 
                SpeechRecognizedHandler -= value;
            }
        }

        event EventHandler<SpeechHypothesizedEventArgs> ISpeechRecognitionEngine.SpeechHypothesized
        {
            add { engine.SpeechHypothesized += value; }
            remove { engine.SpeechHypothesized -= value; }
        }

        event EventHandler<SpeechRecognitionRejectedEventArgs> ISpeechRecognitionEngine.SpeechRecognitionRejected
        {
            add { engine.SpeechRecognitionRejected += value; }
            remove { engine.SpeechRecognitionRejected -= value; }
        }

        #endregion

        /// <summary>
        /// Occurs when speech is recognized.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">event args</param>
        private void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (SpeechRecognizedHandler != null)
            {
                SpeechRecognizedHandler.Invoke(engine, new IavaSpeechRecognizedEventArgs(e));
            }
        }
    }
}
