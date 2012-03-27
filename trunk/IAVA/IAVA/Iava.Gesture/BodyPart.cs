﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iava.Core.Math;
using Iava.Input.Camera;
using System.Xml.Serialization;

namespace Iava.Gesture {
    public class BodyPart {

        #region Public Properties

        /// <summary>
        /// Gets the postion of the body part in x and y axis.
        /// </summary>
        [XmlElement("Position")]
        public IavaSkeletonPoint Position { get; set; }

        /// <summary>
        /// Gets or sets whether the body part should be tracked.
        /// </summary>
        [XmlElement("Tracking")]
        public bool Tracking { get; set; }

        /// <summary>
        /// The JointType of the Body Part
        /// </summary>
        [XmlAttribute("Name")]
        public IavaJointType JointID { get; set; }

        #endregion Public Properties

        #region Constructors

        private BodyPart()
            : this(0, new IavaSkeletonPoint()) { }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BodyPart(IavaJointType jointID)
            : this(jointID, new IavaSkeletonPoint()) {
            // Nothing to do
        }

        /// <summary>
        /// Constructor that sets the position to the parameter of position.
        /// </summary>
        /// <param name="position"></param>
        public BodyPart(IavaJointType jointID, IavaSkeletonPoint position) {
            this.JointID = jointID;
            this.Position = position;
        }

        #endregion Constructors
    }
}