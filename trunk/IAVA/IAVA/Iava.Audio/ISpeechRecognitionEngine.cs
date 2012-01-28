using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Speech.Recognition;
using System.Collections.ObjectModel;
using Microsoft.Speech.AudioFormat;
using System.IO;

namespace Iava.Audio
{
    public interface ISpeechRecognitionEngine
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
