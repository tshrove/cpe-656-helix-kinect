using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Iava.Input.Camera;
using Iava.Core.Math;

namespace Iava.Gesture {
    [XmlRoot("Gesture", Namespace = "urn:Gestures")]
    public class IavaGesture {

        #region Public Events

        public event EventHandler<GestureEventArgs> GestureRecognized;

        #endregion Public Events

        #region Private Fields

        private List<Snapshot> m_pSnapshots = new List<Snapshot>();

        /// <summary>
        /// Indicates if the current gesture is in the paused state
        /// </summary>
        [XmlIgnore()]
        private bool _paused = false;

        /// <summary>
        /// Specifies how many frames the gesture has been in the paused state
        /// </summary>
        [XmlIgnore()]
        private int _pausedFrameCount = 10;

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

        #region Public Properties

        /// <summary>
        /// Gets the list of snapshots for the current Gesture.
        /// </summary>
        [XmlElement("Snapshot")]
        public List<Snapshot> Snapshots {
            get {
                return this.m_pSnapshots;
            }
            set {
                this.m_pSnapshots = value;
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

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public IavaGesture() {
            //Nothing to do
        }

        public IavaGesture(string name, List<Snapshot> snapshots) {
            Name = name;
            Snapshots = snapshots;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Sets all the snapshots to track this specified joint.
        /// </summary>
        /// <param name="joint"></param>
        public void SetTrackingJoints(params IavaJointID[] joints) {
            foreach (var segment in this.Snapshots) {
                segment.SetTrackingJoints(joints);
            }
        }

        public void CheckForGesture(IavaSkeletonData skeleton) {
            throw new NotImplementedException();
        }

        #endregion

        #region Static Methods
        /// <summary>
        /// Save the gesture into an xml file.
        /// </summary>
        /// <param name="gesture"></param>
        /// <param name="path"></param>
        public static void Save(IavaGesture gesture, string path) {
            foreach (Snapshot snapshot in gesture.Snapshots)
            {
                // Get the hipcenter
                BodyPart hipCenter = snapshot.BodyParts[(int)IavaJointID.HipCenter];
                // make the translation vector
                IavaVector hipCenterTranslationPoint = new IavaVector()
                {
                    X = hipCenter.Position.X,
                    Y = hipCenter.Position.Y,
                    Z = hipCenter.Position.Z
                };
                // Translate each bodypart position
                foreach (BodyPart bodyPart in snapshot.BodyParts)
                {
                    bodyPart.Position = Iava.Core.Math.Geometry.Translate(bodyPart.Position, hipCenterTranslationPoint);
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
        /// <param name="path"></param>
        /// <returns></returns>
        public static IavaGesture Load(string path) {
            IavaGesture newGesture = null;
            XmlSerializer deserializer = new XmlSerializer(typeof(IavaGesture));
            TextReader textReader = new StreamReader(path);
            newGesture = (IavaGesture)deserializer.Deserialize(textReader);
            textReader.Close();

            return newGesture;
        }
        #endregion

        public void CheckGesture(IavaSkeletonData skeleton) {/*
            // NEM: Need to describe what this code is doing...
            if (_paused) {
                if (_frameCount == _pausedFrameCount) {
                    _paused = false;
                }

                _frameCount++;
            }*/

            // Check to see if the skeleton data matches our snapshot
            if (Snapshots[_currentGestureSegment].CheckSnapshot(skeleton)) {
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
    }
}
