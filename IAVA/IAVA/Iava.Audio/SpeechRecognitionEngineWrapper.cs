﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Recognition;
using System.Speech.AudioFormat;
using Iava.Core.Logging;

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
            RecognizerInfo ri = GetKinectRecognizer();

            if (ri == null)
            {
                const string message = "Failed to find any installed audio recognizers.";
                StatusLogger.LogMessage(new Message(message,
                                                    GetType().Name,
                                                    MessageType.Error));
                throw new Exception(message);
            }

            engine = new SpeechRecognitionEngine(ri.Id);

            engine.SpeechRecognized += OnSpeechRecognized;
        }

        /// <summary>
        /// Retrieves the Kinect speech recognizer.
        /// </summary>
        /// <returns>Recognizer object</returns>
        private static RecognizerInfo GetKinectRecognizer()
        {
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

            return rv;
        }
    
        #region ISpeechRecognition Members

        /// <summary>
        /// Loads a grammar object into the speech recognition engine.
        /// </summary>
        /// <param name="grammar">Grammar to load</param>
        void  ISpeechRecognitionEngine.LoadGrammar(Grammar grammar)
        {
            engine.LoadGrammar(grammar);
        }

        /// <summary>
        /// Sets the input of the engine to an audio stream.
        /// </summary>
        /// <param name="stream">Audio stream</param>
        /// <param name="audioFormat">Format of the audio stream</param>
        void ISpeechRecognitionEngine.SetInputToAudioStream(System.IO.Stream stream, SpeechAudioFormatInfo audioFormat)
        {
            engine.SetInputToAudioStream(stream, audioFormat);
        }

        /// <summary>
        /// Recognizes speech asynchronously.
        /// </summary>
        /// <param name="mode">Recognizer mode</param>
        void ISpeechRecognitionEngine.RecognizeAsync(RecognizeMode mode)
        {
            engine.RecognizeAsync(mode);
        }

        /// <summary>
        /// Stops recongizing speech asynchronously.
        /// </summary>
        void ISpeechRecognitionEngine.RecognizeAsyncStop()
        {
            engine.RecognizeAsyncStop();
        }

        /// <summary>
        /// Event for when speech was recognized.
        /// </summary>
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

        /// <summary>
        /// Event for when speech was hypothesized.
        /// </summary>
        event EventHandler<SpeechHypothesizedEventArgs> ISpeechRecognitionEngine.SpeechHypothesized
        {
            add { engine.SpeechHypothesized += value; }
            remove { engine.SpeechHypothesized -= value; }
        }

        /// <summary>
        /// Event for when speech was rejected.
        /// </summary>
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
