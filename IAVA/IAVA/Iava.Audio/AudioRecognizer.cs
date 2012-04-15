using System;
using System.Collections.Generic;
using System.Speech.AudioFormat;
using System.Speech.Recognition;
using System.Threading;
using System.Threading.Tasks;
using Iava.Core;
using Iava.Core.Logging;
using Microsoft.Kinect;

namespace Iava.Audio {

    /// <summary>
    /// Audio Callback for when a audio command is detected.
    /// </summary>
    /// <param name="e">event arguments</param>
    public delegate void AudioCallback(AudioEventArgs e);

    /// <summary>
    /// Recognizes audio commands and invokes a callback when an audio command
    /// is recognized.
    /// </summary>
    public class AudioRecognizer : Recognizer {
        #region Public Attributes

        /// <summary>
        /// The level of confidence of the audio being recognized.  Anything below this level is
        /// ignored.
        /// </summary>
        public float AudioConfidenceThreshold {
            get {
                return audioConfidenceThreshold;
            }
            set {
                if (value < 0.0f || value > 1.0f) {
                    throw new ArgumentOutOfRangeException("value", "Confidence level must be in the range of [0, 1.0].");
                }

                audioConfidenceThreshold = value;
            }
        }

        /// <summary>
        /// The command that syncs the recognizer.
        /// </summary>
        public string SyncCommand {
            get {
                return syncCommand;
            }
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("value", "Sync command was either null, empty, or consisted of only whitespace.");
                }

                syncCommand = value;

                if (Status == RecognizerStatus.Running) {
                    Stop();
                    Start();
                }
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Starts the recognizer.
        /// </summary>
        public override void Start() {
            if (Status != RecognizerStatus.Running) {
                Task.Factory.StartNew(() => SetupAudioDevice(m_tokenSource.Token), m_tokenSource.Token);

                // Wait until the setup thread has finished
                m_resetEvent.WaitOne();

                // Reset the event
                m_resetEvent.Reset();

                OnStarted(this, new EventArgs());
            }
        }

        /// <summary>
        /// Stops the recognizer.
        /// </summary>
        public override void Stop() {
            m_tokenSource.Cancel();
            m_tokenSource = new CancellationTokenSource();
            speechEngine.RecognizeAsyncStop();
            audioSource.Stop();

            Status = RecognizerStatus.Ready;

            OnStopped(this, new EventArgs());
        }

        /// <summary>
        /// Used to map a spoken command to a callback.  When the command is recognized, 
        /// the callback is invoked.
        /// </summary>
        /// <param name="name">The audio command to listen for.</param>
        /// <param name="callBack">The method to invoke when the spoken command is recognized.</param>
        public void Subscribe(string name, AudioCallback callBack) {
            if (string.IsNullOrEmpty(name)) {
                throw new ArgumentException("Name argument was either null or empty.", "name");
            }

            if (callBack == null) {
                throw new ArgumentException("Callback argument was null.", "callback");
            }

            if (!this.AudioCallbacks.ContainsKey(name)) {
                AudioCallbacks.Add(name, callBack);

                // To update the grammar the recognizer needs to be restarted
                if (Status == RecognizerStatus.Running) {
                    Stop();
                    Start();
                }
            }
            else {
                throw new ArgumentException("A key already exists with the name " + name + ".", "name");
            }
        }

        /// <summary>
        /// Unsubscribe the spoken command from being recognized.
        /// </summary>
        /// <param name="name">Spoken command to stop being recognized.</param>
        public void Unsubscribe(string name) {
            if (!string.IsNullOrEmpty(name) &&
                this.AudioCallbacks.ContainsKey(name)) {
                AudioCallbacks.Remove(name);
                // To update the grammar the recognizer needs to be restarted
                if (Status == RecognizerStatus.Running) {
                    Stop();
                    Start();
                }
            }
            else {
                throw new ArgumentException("Name was either null or name is not contained within audio callback map.", "name");
            }
        }
        #endregion

        #region Constructors and Initialization
        /// <summary>
        /// Constructor.
        /// </summary>
        public AudioRecognizer()
            : base() {
            Initialize();
            speechEngine = new SpeechRecognitionEngineWrapper();
        }

        /// <summary>
        /// Constructor.  Used for unit testing.
        /// </summary>
        /// <param name="engine">Speech recognition engine</param>
        internal AudioRecognizer(ISpeechRecognitionEngine engine)
            : base() {
            Initialize();
            speechEngine = engine;
        }

        /// <summary>
        /// Initializes the recognizer
        /// </summary>
        private void Initialize() {
            this.AudioCallbacks = new Dictionary<string, AudioCallback>();
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Holds the callbacks for each gesture.
        /// </summary>
        private Dictionary<string, AudioCallback> AudioCallbacks {
            get;
            set;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates Grammar objects based on the spoken commands to listen for and loads 
        /// them into the speech recognition engine.
        /// </summary>
        private void CreateGrammars() {
            Choices choices = new Choices();
            choices.Add(new[] { syncCommand });

            Choices wildcardChoices = new Choices();
            bool wildcardChoiceAdded = false;

            foreach (string phrase in AudioCallbacks.Keys) {
                string command = phrase;
                if (command.EndsWith("*")) {
                    command = command.TrimEnd('*');
                    GrammarBuilder builder = new GrammarBuilder(command);
                    builder.AppendDictation();
                    wildcardChoices.Add(builder);
                    wildcardChoiceAdded = true;
                }
                else {
                    choices.Add(command);
                }
            }

            GrammarBuilder commandsBuilder = new GrammarBuilder(choices);
            Grammar grammar = new Grammar(commandsBuilder);
            grammar.Enabled = true;
            grammar.Name = syncCommand;
            speechEngine.LoadGrammar(grammar);

            // An exception is thrown if no items exist in the builder so only create a Grammar
            // if at least one wildcard statement exists
            if (wildcardChoiceAdded) {
                GrammarBuilder wildcardCommandsBuilder = new GrammarBuilder(wildcardChoices);
                Grammar wildcardGrammar = new Grammar(wildcardCommandsBuilder);
                wildcardGrammar.Enabled = true;
                wildcardGrammar.Name = syncCommand + " - Wildcards";
                speechEngine.LoadGrammar(wildcardGrammar);
            }
        }

        /// <summary>
        /// Sets up the Kinect Audio Source.
        /// </summary>
        /// <param name="token">Cancellation token argument</param>
        private void SetupAudioDevice(CancellationToken token) {
            try {
                CreateGrammars();
                speechEngine.SpeechRecognized += SpeechRecognized;
                speechEngine.SpeechHypothesized += SpeechHypothesized;
                speechEngine.SpeechRecognitionRejected += SpeechRejected;

                if (!token.IsCancellationRequested) {
                    audioSource = KinectSensor.KinectSensors[0].AudioSource;
                    audioSource.EchoCancellationMode = EchoCancellationMode.CancellationOnly;
                    audioSource.AutomaticGainControlEnabled = false;
                    audioSource.BeamAngleMode = BeamAngleMode.Adaptive;

                    var kinectStream = audioSource.Start();
                    speechEngine.SetInputToAudioStream(kinectStream, new SpeechAudioFormatInfo(
                                                          EncodingFormat.Pcm, 16000, 16, 1,
                                                          32000, 2, null));

                    speechEngine.RecognizeAsync(RecognizeMode.Multiple);

                    Status = RecognizerStatus.Running;
                }
            }
            catch (OperationCanceledException) {
                Status = RecognizerStatus.Error;
            }
            catch (Exception exception) {
                Status = RecognizerStatus.Error;
                StatusLogger.LogMessage(new Message("Failed to detect Kinect or start speech recognition engine.",
                                                    GetType().Name,
                                                    MessageType.Error,
                                                    exception));
            }
            finally {
                m_resetEvent.Set();
            }
        }

        /// <summary>
        /// Occurs when speech has been hypothesized.
        /// </summary>
        /// <param name="sender">Object that send the event</param>
        /// <param name="e">Event args</param>
        private void SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e) {
            Console.Write("\rSpeech Hypothesized: \t{0}\tConfidence:\t{1}", e.Result.Text, e.Result.Confidence);
        }

        /// <summary>
        /// Called when a word or phrase was recognized.
        /// </summary>
        /// <param name="sender">Sender of event</param>
        /// <param name="e">Event args</param>
        private void SpeechRecognized(object sender, IavaSpeechRecognizedEventArgs e) {
            Console.WriteLine("\rSpeech Recognized: \t{0}\tConfidence:\t{1}", e.Text, e.Confidence);

            // If we just synced, set the flag and return
            if (e.Text.Contains(syncCommand)) {
                m_syncContext.Post(new SendOrPostCallback(delegate(object state) { OnSynced(this, e); }), null);
                return;
            }

            if (m_isSynced) {
                foreach (string command in AudioCallbacks.Keys) {
                    if (e.Text.Contains(command.TrimEnd(new[] { '*' })) && e.Confidence > AudioConfidenceThreshold) {
                        try {
                            // We found a command, reset the timer
                            ResetTimer();

                            AudioEventArgs args = new AudioEventArgs
                                {
                                    Command = command
                                };

                            if (command.EndsWith("*")) {
                                // Extract the wildcard words spoken and split them into
                                string recognizedPhrase = e.Text.Substring(command.Length - 1).ToLower();
                                args.CommandWildcards = new List<string>(recognizedPhrase.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                            }

                            // Invoke the callback
                            m_syncContext.Post(new SendOrPostCallback(delegate(object state) { AudioCallbacks[command].Invoke(args); }), null);
                        }
                        catch (Exception exception) {
                            StatusLogger.LogMessage(new Message("Error occured in speech recogition method or speech recognition callback.",
                                                    GetType().Name,
                                                    MessageType.Error,
                                                    exception));
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
        private void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e) {
            Console.WriteLine("\nSpeech Rejected: \t{0}", e.Result.Text);
        }

        #endregion Private Methods

        #region Private Fields

        /// <summary>
        /// Confidence threshold.
        /// </summary>
        private float audioConfidenceThreshold = 0.8f;

        /// <summary>
        /// Reference to the Kinect audio source.
        /// </summary>
        private KinectAudioSource audioSource;

        /// <summary>
        /// Speech recognition engine instance.
        /// </summary>
        private ISpeechRecognitionEngine speechEngine;

        /// <summary>
        /// The sync command.
        /// </summary>
        private string syncCommand = "IAVA";

        #endregion Private Fields
    }
}
