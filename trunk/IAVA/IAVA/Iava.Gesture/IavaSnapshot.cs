using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Iava.Core.Math;
using Iava.Input.Camera;

namespace Iava.Gesture {

    /// <summary>
    /// Contains state information for a single 'pose' of a defined IavaGesture
    /// </summary>
    public class IavaSnapshot {

        #region Public Properties

        /// <summary>
        /// The list of bodyparts associated with this gesture snapshot.
        /// </summary>
        [XmlElement("BodyPart")]
        public List<IavaBodyPart> BodyParts {
            get { return _bodyParts; }
            set { _bodyParts = value; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Checks each IavaSkeletonJoint to see if it falls within the expected bounds defining this gesture snapshot
        /// </summary>
        /// <param name="skeleton">Skeleton object being checked</param>
        /// <param name="fudgeFactor">Area around the expected point, that constitutes a hit</param>
        /// <returns>TRUE if the skeleton satisfies the gesture IavaSnapshot, else FALSE</returns>
        public bool CheckSnapshot(IavaSkeleton skeleton, double fudgeFactor) {
            List<bool> results = new List<bool>();

            foreach (IavaBodyPart bodyPart in BodyParts) {

                if (bodyPart.Tracking) {
                    results.Add(Geometry.Magnitude2D(bodyPart.Position, skeleton.Joints[bodyPart.JointID].Position) <= fudgeFactor);
                }
            }

            // We aren't tracking any joints, impossible to match
            if (results.Count == 0) { return false; }

            return results.TrueForAll(x => x == true);
        }
        
        /// <summary>
        /// Clears the tracking state of all segment BodyParts
        /// </summary>
        public void ClearTrackingJoints() {
            BodyParts.ForEach(x => x.Tracking = false);
        }

        /// <summary>
        /// Sets the tracking state of the specified BodyParts to true
        /// </summary>
        /// <param name="joints">The joints to track</param>
        public void SetTrackingJoints(params IavaJointType[] joints) {
            foreach (IavaJointType id in joints) {
                BodyParts.Where(x => x.JointID == id).Single(x => x.Tracking = true);
            }
        }

        #endregion Public Methods

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        private IavaSnapshot()
            : this(null) {
            // Nothing to do.
        }

        /// <summary>
        /// Creates a IavaSnapshot defined by the provided Skeleton object.
        /// </summary>
        /// <param name="skeleton">Skeleton defining the joint states of the IavaSnapshot</param>
        public IavaSnapshot(IavaSkeleton skeleton) {
            if (skeleton == null) { return; }

            // ROFL, I didn't even know this was allowed...
            for (IavaJointType i = 0; i < IavaJointType.Count; i++) {
                BodyParts.Add(new IavaBodyPart(i, skeleton.Joints[i].Position));
            }

            // Set the body part positions
            foreach (IavaJoint joint in skeleton.Joints) {
                BodyParts.Where(x => x.JointID == joint.JointType).Single().Position = joint.Position;
            }
        }

        #endregion Constructors

        #region Private Fields

        /// <summary>
        /// Body parts making up this gesture snapshot
        /// </summary>
        List<IavaBodyPart> _bodyParts = new List<IavaBodyPart>();

        #endregion Private Fields
    }
}
