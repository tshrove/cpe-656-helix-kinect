using System.Xml.Serialization;
using Iava.Core.Math;
using Iava.Input.Camera;

namespace Iava.Gesture {

    /// <summary>
    /// Contains state information for a single joint of a defined IavaSnapshot
    /// </summary>
    public class IavaBodyPart {

        #region Public Properties

        /// <summary>
        /// Gets/Sets the postion of the IavaBodyPart in X,Y,Z coordinates.
        /// </summary>
        [XmlElement("Position")]
        public IavaSkeletonPoint Position { get; set; }

        /// <summary>
        /// Gets/Sets the IavaBodyPart's tracking state.
        /// </summary>
        [XmlElement("Tracking")]
        public bool Tracking { get; set; }

        /// <summary>
        /// Gets/Sets the JointType of the IavaBodyPart
        /// </summary>
        [XmlAttribute("Name")]
        public IavaJointType JointID { get; set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        private IavaBodyPart()
            : this(0, new IavaSkeletonPoint()) { }

        /// <summary>
        /// Creates a IavaBodyPart containing the specified JointType
        /// </summary>
        /// <param name="jointID">The JointType of the IavaBodyPart</param>
        public IavaBodyPart(IavaJointType jointID)
            : this(jointID, new IavaSkeletonPoint()) {
            // Nothing to do
        }

        /// <summary>
        /// Creates a IavaBodyPart containing the specified JointType and position
        /// </summary>
        /// <param name="jointID">The JointType of the IavaBodyPart</param>
        /// <param name="position">Position of the IavaBodyPart</param>
        public IavaBodyPart(IavaJointType jointID, IavaSkeletonPoint position) {
            this.JointID = jointID;
            this.Position = position;
        }

        #endregion Constructors
    }
}