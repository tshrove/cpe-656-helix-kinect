using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapControlLib
{
    /// <summary>
    /// API for controlling a GIS map.
    /// </summary>
    public class MapControl
    {
        /// <summary>
        /// Reference to the speech recognition object.
        /// </summary>
        private readonly SpeechRecognizer recognizer = new SpeechRecognizer();

        public MapControl()
        {
            // TODO: Initialize the Kinect
        }

        public void StartVoiceRecognition(string keyword, Dictionary<string, Action> commandMap)
        {
            recognizer.Start(keyword, commandMap);
        }

        public void StopVoiceRecognition()
        {
            recognizer.Stop();
        }
    }
}
