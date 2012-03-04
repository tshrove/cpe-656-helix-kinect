﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Iava.Input.Camera;

namespace Iava.Gesture {
    public class Snapshot {

        #region Public Properties

        /// <summary>
        /// Gets the list of bodyparts associated with this snapshot.
        /// </summary>
        [XmlElement("BodyPart")]
        public List<BodyPart> BodyParts {
            get { return _bodyParts; }
            set { _bodyParts = value; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Sets the Tracking state of all segment BodyParts to false
        /// </summary>
        public void ClearTrackingJoints() {
            BodyParts.ForEach(x => x.Tracking = false);
        }

        /// <summary>
        /// Sets the tracking state of the specified BodyParts to true
        /// </summary>
        /// <param name="joints">The joints to track</param>
        public void SetTrackingJoints(params IavaJointID[] joints) {
            foreach (IavaJointID id in joints) {
                BodyParts.Where(x => x.JointID == id).Single(x => x.Tracking = true);
            }
        }

        public bool CheckSnapshot(IavaSkeletonData skeleton) {
            foreach (BodyPart bodyPart in BodyParts) {
                if (bodyPart.Tracking) {
                    if (bodyPart.Position.X == skeleton.Joints[bodyPart.JointID].Position.X &&
                        bodyPart.Position.Y == skeleton.Joints[bodyPart.JointID].Position.Y) {


                    }
                }
            }
            return false;
        }

        #endregion Public Methods

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Snapshot()
            : this(null) {
            // Nothing to do.
        }

        /// <summary>
        /// Constructor that takes in a skeleton and sets all the body
        /// parts according to the data provided in skeleton.
        /// </summary>
        /// <param name="skeleton"></param>
        public Snapshot(IavaSkeletonData skeleton) {
            // ROFL, I didn't even know this was allowed...
            for (IavaJointID i = 0; i < IavaJointID.Count; i++) {
                BodyParts.Add(new BodyPart(i, skeleton.Joints[i].Position));
            }

            if (skeleton == null) { return; }

            // Set the body part positions
            foreach (IavaJoint joint in skeleton.Joints) {
                BodyParts.Where(x => x.JointID == joint.ID).Single().Position = joint.Position;
            }
        }

        #endregion Constructors

        #region Private Fields

        List<BodyPart> _bodyParts = new List<BodyPart>();

        #endregion Private Fields
    }
}
