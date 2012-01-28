using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Speech.Recognition;

namespace Iava.Audio
{
    public class SpeechRecognitionEngineWrapper : ISpeechRecognitionEngine
    {

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
            RecognizerInfo ri = SpeechRecognitionEngine.InstalledRecognizers().Where(
                            r => r.Id == RecognizerId).FirstOrDefault();

            if (ri == null)
            {
                throw new Exception("Failed to find any installed audio recognizers.");
            }

            engine = new SpeechRecognitionEngine(ri.Id);

            engine.SpeechRecognized += OnSpeechRecognized;
        }
    
        #region ISpeechRecognition Members

        void  ISpeechRecognitionEngine.LoadGrammar(Grammar grammar)
        {
            engine.LoadGrammar(grammar);
        }

        void  ISpeechRecognitionEngine.SetInputToAudioStream(System.IO.Stream stream, Microsoft.Speech.AudioFormat.SpeechAudioFormatInfo audioFormat)
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
                SpeechRecognizedHandler += value;
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

        private void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (SpeechRecognizedHandler != null)
            {
                SpeechRecognizedHandler.Invoke(engine, new IavaSpeechRecognizedEventArgs(e));
            }
        }
    }
}
