using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Speech.AudioFormat;
using System.Speech.Recognition;

namespace Iava.Audio
{
    /// <summary>
    /// Speech recognition interface.
    /// </summary>
    internal interface ISpeechRecognitionEngine
    {
        /// <summary>
        /// Event for when speech was recognized.
        /// </summary>
        event EventHandler<IavaSpeechRecognizedEventArgs> SpeechRecognized;

        /// <summary>
        /// Event for when speech was hypothesized.
        /// </summary>
        event EventHandler<SpeechHypothesizedEventArgs> SpeechHypothesized;

        /// <summary>
        /// Event for when speech was rejected.
        /// </summary>
        event EventHandler<SpeechRecognitionRejectedEventArgs> SpeechRecognitionRejected;

        /// <summary>
        /// Loads a grammar object into the speech recognition engine.
        /// </summary>
        /// <param name="grammar">Grammar to load</param>
        void LoadGrammar(Grammar grammar);

        /// <summary>
        /// Sets the input of the engine to an audio stream.
        /// </summary>
        /// <param name="stream">Audio stream</param>
        /// <param name="audioFormat">Format of the audio stream</param>
        void SetInputToAudioStream(Stream stream, SpeechAudioFormatInfo audioFormat);

        /// <summary>
        /// Recognizes speech asynchronously.
        /// </summary>
        /// <param name="mode">Recognizer mode</param>
        void RecognizeAsync(RecognizeMode mode);

        /// <summary>
        /// Stops recongizing speech asynchronously.
        /// </summary>
        void RecognizeAsyncStop();
    }
}
