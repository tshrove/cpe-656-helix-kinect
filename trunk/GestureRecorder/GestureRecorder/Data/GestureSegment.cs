using System.Collections.Generic;
using System.Linq;
using Iava.Input.Camera;

using tempuri.org.GestureDefinition.xsd;

namespace GestureRecorder.Data {
    public class GestureSegment : tempuri.org.GestureDefinition.xsd.Gesture.SegmentLocalType {

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public GestureSegment()
            : this(null) { }
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="skeleton">Skeleton data used to populate the body segment positions</param>
        public GestureSegment(SkeletonData skeleton) {
            BodyParts = new Dictionary<JointID, BodyPart>();

            // Initialize the key value pair
            BodyParts.Add(JointID.AnkleLeft,       this.AnkleLeft);
            BodyParts.Add(JointID.AnkleRight,      this.AnkleRight);
            BodyParts.Add(JointID.ElbowLeft,       this.ElbowLeft);
            BodyParts.Add(JointID.ElbowRight,      this.ElbowRight);
            BodyParts.Add(JointID.FootLeft,        this.FootLeft);
            BodyParts.Add(JointID.FootRight,       this.FootRight);
            BodyParts.Add(JointID.HandLeft,        this.HandLeft);
            BodyParts.Add(JointID.HandRight,       this.HandRight);
            BodyParts.Add(JointID.Head,            this.Head);
            BodyParts.Add(JointID.HipCenter,       this.HipCenter);
            BodyParts.Add(JointID.HipLeft,         this.HipLeft);
            BodyParts.Add(JointID.HipRight,        this.HipRight);
            BodyParts.Add(JointID.KneeLeft,        this.KneeLeft);
            BodyParts.Add(JointID.KneeRight,       this.KneeRight);
            BodyParts.Add(JointID.ShoulderCenter,  this.ShoulderCenter);
            BodyParts.Add(JointID.ShoulderLeft,    this.ShoulderLeft);
            BodyParts.Add(JointID.ShoulderRight,   this.ShoulderRight);
            BodyParts.Add(JointID.Spine,           this.Spine);
            BodyParts.Add(JointID.WristLeft,       this.WristLeft);
            BodyParts.Add(JointID.WristRight,      this.WristRight);

            if (skeleton == null) { return; }

            // Set the body part positions
            foreach (Joint joint in skeleton.Joints) {
                BodyParts[joint.ID].Position.X = joint.Position.X;
                BodyParts[joint.ID].Position.Y = joint.Position.Y;
                BodyParts[joint.ID].Position.Z = joint.Position.Z;
            }
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Sets the Tracking state of all segment BodyParts to false
        /// </summary>
        public void ClearTrackingJoints() {
            foreach (BodyPart bodyPart in BodyParts.Values) { bodyPart.Tracking = false; }
        }

        /// <summary>
        /// Sets the tracking state of the specified BodyParts to true
        /// </summary>
        /// <param name="joints">The joints to track</param>
        public void SetTrackingJoints(params JointID[] joints) {
            foreach (JointID id in joints) { BodyParts[id].Tracking = true; }
        }

        #endregion Public Methods

        #region Public Properties

        /// <summary>
        /// Returns the BodyPart of the corresponding JointID
        /// </summary>
        public Dictionary<JointID, BodyPart> BodyParts { get; private set; }

        #endregion Public Properties
    }

    public class GestureWrapper : tempuri.org.GestureDefinition.xsd.Gesture {
        public List<GestureSegment> Segments { get; set; }
    }
}
