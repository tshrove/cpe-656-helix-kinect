using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iava.Input.Camera;
using System.Xml.Serialization;

namespace GestureRecorder.Data {
    public class BodyPart {

        #region Public Properties

        /// <summary>
        /// Gets the postion of the body part in x and y axis.
        /// </summary>
        [XmlElement("Position")]
        public Vector Position { get; set; }

        /// <summary>
        /// Gets or sets whether the body part should be tracked.
        /// </summary>
        [XmlElement("Tracking")]
        public bool Tracking { get; set; }

        /// <summary>
        /// The ID of the Body Part
        /// </summary>
        [XmlAttribute("Name")]
        public JointID JointID { get; set; }

        #endregion Public Properties

        #region Constructors

        private BodyPart()
            : this(0, new Vector()) { }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BodyPart(JointID jointID)
            : this(jointID, new Vector()) {
            // Nothing to do
        }

        /// <summary>
        /// Constructor that sets the position to the parameter of position.
        /// </summary>
        /// <param name="position"></param>
        public BodyPart(JointID jointID, Vector position) {
            this.JointID = jointID;
            this.Position = position;
        }

        #endregion Constructors
    }
}