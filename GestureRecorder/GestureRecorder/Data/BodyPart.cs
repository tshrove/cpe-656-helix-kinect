using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iava.Input.Camera;
using System.Xml.Serialization;

namespace GestureRecorder.Data
{
    public class BodyPart
    {
        /// <summary>
        /// Gets the postion of the body part in x and y axis.
        /// </summary>
        [XmlElement("Position")]
        public Vector Position
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets whether the body part should be tracked.
        /// </summary>
        [XmlElement("Tracking")]
        public bool Tracking
        {
            get;
            set;
        }

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BodyPart()
            :this(new Vector())
        {
            // Nothing to do
        }
        /// <summary>
        /// Constructor that sets the position to the parameter of position.
        /// </summary>
        /// <param name="position"></param>
        public BodyPart(Vector position)
        {
            this.Position = position;
        }
        #endregion
    }
}
