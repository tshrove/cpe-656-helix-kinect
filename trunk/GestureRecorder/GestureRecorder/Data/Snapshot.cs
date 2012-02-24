using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Iava.Input.Camera;

namespace GestureRecorder.Data
{
    public class Snapshot
    {
        #region Private Members
        private SerializableDictionary<JointID, BodyPart> m_pBodyParts = new SerializableDictionary<JointID, BodyPart>();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the list of bodyparts associated with this snapshot.
        /// </summary>
        public SerializableDictionary<JointID, BodyPart> BodyParts
        {
            get
            {
                return this.m_pBodyParts;
            }
            set
            {
                this.m_pBodyParts = value;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the Tracking state of all segment BodyParts to false
        /// </summary>
        public void ClearTrackingJoints()
        {
            foreach (BodyPart bodyPart in BodyParts.Values) { bodyPart.Tracking = false; }
        }
        /// <summary>
        /// Sets the tracking state of the specified BodyParts to true
        /// </summary>
        /// <param name="joints">The joints to track</param>
        public void SetTrackingJoints(params JointID[] joints)
        {
            foreach (JointID id in joints) { BodyParts[id].Tracking = true; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Snapshot()
            : this(null)
        {
            // Nothing to do.
        }
        /// <summary>
        /// Constructor that takes in a skeleton and sets all the body
        /// parts according to the data provided in skeleton.
        /// </summary>
        /// <param name="skeleton"></param>
        public Snapshot(SkeletonData skeleton)
        {
            // Initialize the key value pair
            BodyParts.Add(JointID.AnkleLeft, new BodyPart());
            BodyParts.Add(JointID.AnkleRight, new BodyPart());
            BodyParts.Add(JointID.ElbowLeft, new BodyPart());
            BodyParts.Add(JointID.ElbowRight, new BodyPart());
            BodyParts.Add(JointID.FootLeft, new BodyPart());
            BodyParts.Add(JointID.FootRight, new BodyPart());
            BodyParts.Add(JointID.HandLeft, new BodyPart());
            BodyParts.Add(JointID.HandRight, new BodyPart());
            BodyParts.Add(JointID.Head, new BodyPart());
            BodyParts.Add(JointID.HipCenter, new BodyPart());
            BodyParts.Add(JointID.HipLeft, new BodyPart());
            BodyParts.Add(JointID.HipRight, new BodyPart());
            BodyParts.Add(JointID.KneeLeft, new BodyPart());
            BodyParts.Add(JointID.KneeRight, new BodyPart());
            BodyParts.Add(JointID.ShoulderCenter, new BodyPart());
            BodyParts.Add(JointID.ShoulderLeft, new BodyPart());
            BodyParts.Add(JointID.ShoulderRight, new BodyPart());
            BodyParts.Add(JointID.Spine, new BodyPart());
            BodyParts.Add(JointID.WristLeft, new BodyPart());
            BodyParts.Add(JointID.WristRight, new BodyPart());

            if (skeleton == null) { return; }

            // Set the body part positions
            foreach (Joint joint in skeleton.Joints)
            {
                BodyParts[joint.ID].Position = joint.Position;
            }
        }
        #endregion
    }
}
