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
        /// Gets/Sets the fudginess of the gesture.
        /// </summary>
        [XmlAttribute("FudgeFactor")]
        public double FudgeFactor { get; set; }

        /// <summary>
        /// Gets/Sets the name of the gesture.
        /// </summary>
        [XmlAttribute("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets/Sets the list of snapshots for the gesture.
        /// </summary>
        [XmlElement("Snapshot")]
        public List<IavaSnapshot> Snapshots {
            get { return this._snapshots; }
            set { this._snapshots = value; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Checks the skeleton position to see if the gesture has been performed
        /// </summary>
        /// <param name="skeleton">IavaSkeleton to inspect for gesture</param>
        public void CheckGesture(IavaSkeleton skeleton) {
            // For now hard code Fudgefactor
            //FudgeFactor = 0.3;

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

                // We have only detected part of a gesture, prepare to look for the next snapshot
                else { _frameCount = 0; }
            }

            else {
                // We've timed out, start over on detecting the gesture
                if (_frameCount >= 40) {
                    _currentGestureSegment = 0;
                    _frameCount = 0;
                }

                else { _frameCount++; }
            }
        }

        /// <summary>
        /// Creates an IavaGesture object from the specified xml file.
        /// </summary>
        /// <param name="filepath">Filepath to the gesture</param>
        /// <returns>IavaGesture defined in the xml file</returns>
        public static IavaGesture Load(string filepath) {
            IavaGesture newGesture = null;
            XmlSerializer deserializer = new XmlSerializer(typeof(IavaGesture));
            TextReader textReader = new StreamReader(filepath);
            newGesture = (IavaGesture)deserializer.Deserialize(textReader);
            textReader.Close();

            return newGesture;
        }

        /// <summary>
        /// Resets the partially recognized status of the gesture.
        /// </summary>
        public void Reset() {
            _currentGestureSegment = 0;
            _frameCount = 0;
        }

        /// <summary>
        /// Saves the IavaGesture state to the specified xml file.
        /// </summary>
        /// <param name="gesture">IavaGesture to be saved</param>
        /// <param name="filepath">Filepath where the gesture should be written to</param>
        public static void Save(IavaGesture gesture, string filepath) {
            foreach (IavaSnapshot snapshot in gesture.Snapshots) {
                // Create a hipCenter joint placeholder
                IavaBodyPart hipCenter = new IavaBodyPart(IavaJointType.HipCenter);

                // Get the HipCenter position...
                hipCenter.Position = new Core.Math.IavaSkeletonPoint()
                {
                    X = snapshot.BodyParts[(int)IavaJointType.HipCenter].Position.X,
                    Y = snapshot.BodyParts[(int)IavaJointType.HipCenter].Position.Y,
                    Z = snapshot.BodyParts[(int)IavaJointType.HipCenter].Position.Z
                };

                // Translate each bodypart position based on original hipcenter position
                foreach (IavaBodyPart bodyPart in snapshot.BodyParts) {
                    bodyPart.Position = Iava.Core.Math.Geometry.Translate(bodyPart.Position, hipCenter.Position);
                }
            }
            XmlSerializer serializer = new XmlSerializer(typeof(IavaGesture));
            TextWriter textWriter = new StreamWriter(filepath);
            serializer.Serialize(textWriter, gesture);
            textWriter.Close();
        }

        /// <summary>
        /// Sets the snapshots to track the specified joints.
        /// </summary>
        /// <param name="joints">Joint type array</param>
        public void SetTrackingJoints(params IavaJointType[] joints) {
            foreach (var segment in this.Snapshots) {
                segment.SetTrackingJoints(joints);
            }
        }

        #endregion Public Methods

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public IavaGesture() {
            Name = string.Empty;
            Snapshots = new List<IavaSnapshot>();
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
        /// Specifies the current gesture segment we are trying to detect
        /// </summary>
        [XmlIgnore()]
        private int _currentGestureSegment = 0;

        /// <summary>
        /// Specifies the number of frames we have analyzed for this gesture
        /// </summary>
        [XmlIgnore()]
        private int _frameCount = 0;

        /// <summary>
        /// Snapshots defining the IavaGesture
        /// </summary>
        private List<IavaSnapshot> _snapshots = new List<IavaSnapshot>();

        #endregion Private Fields
    }
}
