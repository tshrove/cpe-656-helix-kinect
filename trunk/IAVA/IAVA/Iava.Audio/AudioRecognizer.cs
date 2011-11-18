using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iava.Core;
using Microsoft.Research.Kinect.Audio;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.AudioFormat;
using System.Threading.Tasks;
using System.Threading;

namespace Iava.Audio 
{
    /// <summary>
    /// Audio Callback for when a audio command is detected.
    /// </summary>
    /// <param name="e"></param>
    public delegate void AudioCallback(AudioEventArgs e);
    
    /// <summary>
    /// AudioRecognizer Class
    /// </summary>
    public class AudioRecognizer : Recognizer
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filePath">Path to configuration file</param>
        public AudioRecognizer(string filePath)
            :base(filePath)
        {
            this.AudioCallbacks = new Dictionary<string, AudioCallback>();
            // TODO This is temporary for prototype.
            this.AudioCallbacks.Add("Helix", null);
            // TODO Add the function to load the gestures to the dictionary.
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Holds the callbacks for each gesture.
        /// </summary>
        private Dictionary<string, AudioCallback> AudioCallbacks
        {
            get;
            set;
        }

        #endregion

        #region Private Variables

        /// <summary>
        /// Reference to the Kinect audio source.
        /// </summary>
        private KinectAudioSource audioSource;

        /// <summary>
        /// Speech recognition engine instance.
        /// </summary>
        private SpeechRecognitionEngine speechEngine;

        /// <summary>
        /// Recognizer ID for the Kinect.
        /// </summary>
        private const string RecognizerId = "SR_MS_en-US_Kinect_10.0";

        /// <summary>
        /// Token source used to stop any background tasks.
        /// </summary>
        private CancellationTokenSource tokenSource = new CancellationTokenSource();

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts the recognizer.
        /// </summary>
        public override void Start()
        {
            Thread t = new Thread(SetupAudioDevice);
            t.SetApartmentState(ApartmentState.MTA);
            t.Start();

            OnStarted(this, new EventArgs());
        }

        /// <summary>
        ///  Stops the recognizer.
        /// </summary>
        public override void Stop()
        {           
            tokenSource.Cancel();
            tokenSource = new CancellationTokenSource();
            speechEngine.RecognizeAsyncStop();

            Status = RecognizerStatus.Ready;

            OnStopped(this, new EventArgs());     
        }

        /// <summary>
        /// Used to connect a given delegate to a specified gesture
        /// given by the name.
        /// </summary>
        /// <param name="name">The audio command to listen for.</param>
        /// <param name="callBack">The method to invoke when the spoken command is recognized.</param>
        public void Subscribe(string name, AudioCallback callBack)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name argument was either null or empty.", "name");
            }

            if (!this.AudioCallbacks.ContainsKey(name))
            {
                AudioCallbacks.Add(name, callBack);
                
                // To update the grammar the recognizer needs to be restarted
                if (Status == RecognizerStatus.Running)
                {
                    Stop();
                    Start();
                }
            }           
        }

        /// <summary>
        /// Unsubscribe the given delegate from the given delegate
        /// by the name.
        /// </summary>
        /// <param name="name"></param>
        public void Unsubscribe(string name)
        {
            if (this.AudioCallbacks.ContainsKey(name))
            {
                AudioCallbacks.Remove(name);

                // To update the grammar the recognizer needs to be restarted
                if (Status == RecognizerStatus.Running)
                {
                    Stop();
                    Start();
                }
            }            
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets up the Kinect Audio Source.
        /// </summary>
        private void SetupAudioDevice()
        {
            try
            {
                RecognizerInfo ri = SpeechRecognitionEngine.InstalledRecognizers().Where(
                            r => r.Id == RecognizerId).FirstOrDefault();

                if (ri == null)
                {
                    throw new Exception("Failed to find any installed audio recognizers.");
                }

                speechEngine = new SpeechRecognitionEngine(ri.Id);

                speechEngine.LoadGrammar(CreateGrammar());
                speechEngine.SpeechRecognized += SpeechRecognized;
                speechEngine.SpeechHypothesized += SpeechHypothesized;
                speechEngine.SpeechRecognitionRejected += SpeechRejected;

                audioSource = new KinectAudioSource();
                audioSource.SystemMode = SystemMode.OptibeamArrayOnly;
                audioSource.FeatureMode = true;
                audioSource.AutomaticGainControl = false;
                audioSource.MicArrayMode = MicArrayMode.MicArrayAdaptiveBeam;

                var kinectStream = audioSource.Start();
                speechEngine.SetInputToAudioStream(kinectStream, new SpeechAudioFormatInfo(
                                                      EncodingFormat.Pcm, 16000, 16, 1,
                                                      32000, 2, null));
                speechEngine.RecognizeAsync(RecognizeMode.Multiple);

                Status = RecognizerStatus.Running;
            }
            catch (OperationCanceledException)
            {
                Status = RecognizerStatus.Error;
            }
            catch (Exception exception)
            {
                // TODO: Log message.  Failed to detect Kinect or start speech engine
                Status = RecognizerStatus.Error;
            }           
        }


        /// <summary>
        /// Creates a Grammar object based on the spoken commands to listen for.
        /// </summary>
        /// <returns></returns>
        private Grammar CreateGrammar()
        {
            Grammar rv = null;

            //// Build a simple grammar of commands

            //// TODO: The speech recognition is picking up false positives.  Look into how
            //// to better refine speech input.
            Choices choices = new Choices();
            foreach (string phrase in AudioCallbacks.Keys)
            {
                choices.Add(phrase);
            }

            GrammarBuilder builder = new GrammarBuilder();
            builder.Append(choices);

            rv = new Grammar(builder);
            rv.Enabled = true;
            rv.Name = "IAVA";

            return rv;
        }

        /// <summary>
        /// Called when a word or phrase was recognized.
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event args</param>
        void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.Write("\rSpeech Recognized: \t{0}\tConfidence:\t{1}", e.Result.Text, e.Result.Confidence);
            foreach (string command in AudioCallbacks.Keys)
            {
                if (e.Result.Text.Contains("Helix"))
                {
                    // Found the sync command
                    OnSynced(this, new EventArgs());
                }
                else
                {
                    if (e.Result.Text.Contains(command))
                    {
                        try
                        {
                            AudioEventArgs args = new AudioEventArgs
                                {
                                    Command = command,
                                    CommandWilcards = null
                                };

                            if (e.Result.Confidence > 0.8f)
                            {
                                AudioCallbacks[command].Invoke(args);
                            }
                        }
                        catch (Exception exception)
                        {
                            //TODO: Log message if this fails
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Occurs when the speech does not match and commands the recognizer is listening for.
        /// </summary>
        /// <param name="sender">Object that send the event</param>
        /// <param name="e">Event args</param>
        void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            Console.WriteLine("\nSpeech Rejected: \t{0}", e.Result.Text);
        }

        /// <summary>
        /// Occurs when speech has been hypothesized.
        /// </summary>
        /// <param name="sender">Object that send the event</param>
        /// <param name="e">Event args</param>
        void SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            Console.Write("\rSpeech Hypothesized: \t{0}\tConfidence:\t{1}", e.Result.Text, e.Result.Confidence);
        }

        #endregion
    }
}
