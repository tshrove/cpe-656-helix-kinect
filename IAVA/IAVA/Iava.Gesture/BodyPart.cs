using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iava.Input.Camera;
using System.Xml.Serialization;

namespace Iava.Gesture {
    public class BodyPart {

        #region Public Properties

        /// <summary>
        /// Gets the postion of the body part in x and y axis.
        /// </summary>
        [XmlElement("Position")]
        public IavaVector Position { get; set; }

        /// <summary>
        /// Gets or sets whether the body part should be tracked.
        /// </summary>
        [XmlElement("Tracking")]
        public bool Tracking { get; set; }

        /// <summary>
        /// The ID of the Body Part
        /// </summary>
        [XmlAttribute("Name")]
        public IavaJointID JointID { get; set; }

        #endregion Public Properties

        #region Constructors

        private BodyPart()
            : this(0, new IavaVector()) { }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BodyPart(IavaJointID jointID)
            : this(jointID, new IavaVector()) {
            // Nothing to do
        }

        /// <summary>
        /// Constructor that sets the position to the parameter of position.
        /// </summary>
        /// <param name="position"></param>
        public BodyPart(IavaJointID jointID, IavaVector position) {
            this.JointID = jointID;
            this.Position = position;
        }

        #endregion Constructors
    }
}