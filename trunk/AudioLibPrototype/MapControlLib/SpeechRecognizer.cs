using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Research.Kinect.Audio;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.AudioFormat;
using System.Threading;

namespace MapControlLib
{
    /// <summary>
    /// Handles speech recognition and fires events assoiciated with recognized words.
    /// </summary>
    internal class SpeechRecognizer
    {
        /// <summary>
        /// Maps a spoken word or phrase to an action.
        /// </summary>
        private Dictionary<string, Action> wordToActionMap;

        private string initializerKeyword;

        private KinectAudioSource kinectSource;
        private SpeechRecognitionEngine speechEngine;
        private const string RecognizerId = "SR_MS_en-US_Kinect_10.0";
        private bool paused = false;
        private bool valid = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SpeechRecognizer()
        {

        }

        /// <summary>
        /// Starts the speech recognition thread.
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="commandMap">Words or phrases mapped to actions to perform when the word or phrase is spoken.</param>
        public void Start(string keyword, Dictionary<string, Action> commandMap)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                throw new ArgumentException("Keyword argument was either null or empty.", "keyword");
            }

            if (commandMap == null || commandMap.Count == 0)
            {
                throw new ArgumentException("Dictionary was null or contained no items.", "commandMap"); 
            }

            initializerKeyword = keyword;
            wordToActionMap = commandMap;

            RecognizerInfo ri = SpeechRecognitionEngine.InstalledRecognizers().Where(
                r => r.Id == RecognizerId).FirstOrDefault();
            
            if (ri == null)
            {
                return;
            }

            speechEngine = new SpeechRecognitionEngine(ri.Id);

            // Build a simple grammar of commands
            Choices choices = new Choices();
            foreach (string phrase in wordToActionMap.Keys)
            {
                choices.Add(phrase);
            }

            GrammarBuilder gb = new GrammarBuilder(choices);

            Grammar g = new Grammar(gb);
            speechEngine.LoadGrammar(g);
            speechEngine.SpeechRecognized += SpeechRecognized;

            Thread t = new Thread(StartDMO);
            t.Start();

            valid = true;
        }

        public void Stop()
        {
            if (speechEngine != null)
            {
                speechEngine.RecognizeAsyncCancel();
                speechEngine.RecognizeAsyncStop();
                kinectSource.Dispose();
            }
        }

        #region Private Methods

        /// <summary>
        /// Sets up the kinect microphone.
        /// </summary>
        private void StartDMO()
        {
            kinectSource = new KinectAudioSource();
            kinectSource.SystemMode = SystemMode.OptibeamArrayOnly;
            kinectSource.FeatureMode = true;
            kinectSource.AutomaticGainControl = false;
            kinectSource.MicArrayMode = MicArrayMode.MicArrayAdaptiveBeam;
            var kinectStream = kinectSource.Start();
            speechEngine.SetInputToAudioStream(kinectStream, new SpeechAudioFormatInfo(
                                                  EncodingFormat.Pcm, 16000, 16, 1,
                                                  32000, 2, null));
            speechEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        /// <summary>
        /// Called when a word or phrase was recognized.
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event args</param>
        void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            foreach (string word in wordToActionMap.Keys)
            {
                if (e.Result.Text.Contains(word))
                {
                    try
                    {
                        wordToActionMap[word].Invoke();
                    }
                    catch
                    {
                        
                    }

                    break;
                }
            }
        }

        #endregion
    }
}
