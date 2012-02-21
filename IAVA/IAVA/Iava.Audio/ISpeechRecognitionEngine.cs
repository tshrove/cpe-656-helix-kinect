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
        event EventHandler<IavaSpeechRecognizedEventArgs> SpeechRecognized;

        event EventHandler<SpeechHypothesizedEventArgs> SpeechHypothesized;

        event EventHandler<SpeechRecognitionRejectedEventArgs> SpeechRecognitionRejected;

        void LoadGrammar(Grammar grammar);

        void SetInputToAudioStream(Stream stream, SpeechAudioFormatInfo audioFormat);

        void RecognizeAsync(RecognizeMode mode);

        void RecognizeAsyncStop();
    }
}
