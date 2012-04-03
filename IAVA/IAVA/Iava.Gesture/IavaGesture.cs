using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Iava.Input.Camera;

namespace Iava.Gesture {

    /// <summary>
    /// Contains state information for a single 'pose' of a defined IavaGesture
    /// </summary>
    [XmlRoot("Gesture", Namespace = "urn:Gestures")]
    public class IavaGesture {

        #region Public Events

        /// <summary>
        /// Fired when the gesture is recognized
        /// </summary>
        public event EventHandler<GestureEventArgs> GestureRecognized;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// Gets the list of snapshots for the current Gesture.
        /// </summary>
        [XmlElement("Snapshot")]
        public List<IavaSnapshot> Snapshots {
            get {
                return this._snapshots;
            }
            set {
                this._snapshots = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the gesture.
        /// </summary>
        [XmlAttribute("Name")]
        public string Name {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fudginess of the gesture.
        /// </summary>
        [XmlAttribute("FudgeFactor")]
        public double FudgeFactor {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Saves the all the IavaGesture into an xml file.
        /// </summary>
        /// <param name="gesture">IavaGesture to be saved</param>
        /// <param name="path">Filepath where the gesture should be written to</param>
        public static void Save(IavaGesture gesture, string path) {
            foreach (IavaSnapshot snapshot in gesture.Snapshots) {
                // Get the hipcenter
                IavaBodyPart hipCenter = snapshot.BodyParts[(int)IavaJointType.HipCenter];

                // Translate each bodypart position based on hipcenter
                foreach (IavaBodyPart bodyPart in snapshot.BodyParts) {
                    bodyPart.Position = Iava.Core.Math.Geometry.Translate(bodyPart.Position, hipCenter.Position);
                }
            }
            XmlSerializer serializer = new XmlSerializer(typeof(IavaGesture));
            TextWriter textWriter = new StreamWriter(path);
            serializer.Serialize(textWriter, gesture);
            textWriter.Close();
        }

        /// <summary>
        /// Creates a gesture from the current xml file path.
        /// </summary>
        /// <param name="path">Filepath to the gesture</param>
        /// <returns>IavaGesture defined in the xml file</returns>
        public static IavaGesture Load(string path) {
            IavaGesture newGesture = null;
            XmlSerializer deserializer = new XmlSerializer(typeof(IavaGesture));
            TextReader textReader = new StreamReader(path);
            newGesture = (IavaGesture)deserializer.Deserialize(textReader);
            textReader.Close();

            return newGesture;
        }

        /// <summary>
        /// Sets all the snapshots to track this specified point.
        /// </summary>
        /// <param name="point"></param>
        public void SetTrackingJoints(params IavaJointType[] joints) {
            foreach (var segment in this.Snapshots) {
                segment.SetTrackingJoints(joints);
            }
        }

        public void CheckGesture(IavaSkeleton skeleton) {/*
            // NEM: Need to describe what this code is doing...
            if (_paused) {
                if (_frameCount == _pausedFrameCount) {
                    _paused = false;
                }

                _frameCount++;
            }*/

            // For now hard code Fudgefactor
            FudgeFactor = 0.3;

            // Check to see if the skeleton data matches our snapshot
            if (Snapshots[_currentGestureSegment].CheckSnapshot(skeleton, FudgeFactor)) {
                _currentGestureSegment++;

                // We have detected an entire gesture
                if (_currentGestureSegment == Snapshots.Count) {
                    // Throw the event
                    if (GestureRecognized != null) { GestureRecognized(null, new GestureEventArgs(Name)); }

                    // Reset the gesture state
                    Reset();
                }

                // We have only detected part of athegesture, prepare to look for the next snapshot
                else {
                    _frameCount = 0;
                    //_pausedFrameCount = 10;
                    //_paused = true;
                }
            }

            else {
                // We've timed out, start over on detecting the gesture
                if (_frameCount >= 50) {
                    _currentGestureSegment = 0;
                    _frameCount = 0;
                }

                else { _frameCount++; }
                /*
                // Change to the paused state
                _pausedFrameCount = 5;
                _paused = true;*/
            }
        }

        public void Reset() {
            _currentGestureSegment = 0;
            _frameCount = 0;
            //_pausedFrameCount = 5;
            //_paused = true;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        internal IavaGesture() {
            //Nothing to do
        }

        /// <summary>
        /// Creates an IavaGesture containing the specified name and list of snapshots that make up the IavaGesture
        /// </summary>
        /// <param name="name">Name of the gesture</param>
        /// <param name="snapshots">'Poses' defining the gesture</param>
        public IavaGesture(string name, List<IavaSnapshot> snapshots) {
            Name = name;
            Snapshots = snapshots;
        }

        #endregion Constructors

        #region Private Fields

        /// <summary>
        /// Snapshots defining the IavaGesture
        /// </summary>
        private List<IavaSnapshot> _snapshots = new List<IavaSnapshot>();

        /// <summary>
        /// Specifies the number of frames we have analyzed for this gesture
        /// </summary>
        [XmlIgnore()]
        private int _frameCount = 0;

        /// <summary>
        /// Specifies the current gesture segment we are trying to detect
        /// </summary>
        [XmlIgnore()]
        private int _currentGestureSegment = 0;

        #endregion Private Fields
    }
}
