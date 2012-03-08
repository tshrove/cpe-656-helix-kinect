using System.Collections;
using Microsoft.Research.Kinect.Nui;
using System;

namespace Iava.Input.Camera {

    public class IavaJointsCollection : IEnumerable {

        #region Public Properties

        public int Count { get { return (int)IavaJointID.Count; } }

        public IavaJoint this[IavaJointID i] {
            get { return _joints[(int)i]; }
            set { _joints[(int)i] = value; }
        }

        #endregion Public Properties

        #region Public Methods

        public IEnumerator GetEnumerator() { return _joints.GetEnumerator(); }

        #endregion Public Methods

        #region Private Fields

        private IavaJoint[] _joints = new IavaJoint[(int)IavaJointID.Count];

        #endregion Private Fields

        #region Operator Overloads

        public static bool operator ==(IavaJointsCollection collection1, IavaJointsCollection collection2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(collection1, collection2)) { return true; }

            // If just one is null, return false.
            if (((object)collection1 == null) || ((object)collection2 == null)) { return false; }

            for (IavaJointID i = 0; i < IavaJointID.Count; i++) {
                if (collection1[i] != collection2[i]) { return false; }
            }

            // If we made it here they are the same
            return true;
        }

        public static bool operator !=(IavaJointsCollection collection1, IavaJointsCollection collection2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(collection1, collection2)) { return false; }

            // If just one is null, return true.
            if (((object)collection1 == null) || ((object)collection2 == null)) { return true; }

            for (IavaJointID i = 0; i < IavaJointID.Count; i++) {
                if (collection1[i] != collection2[i]) { return true; }
            }

            // If we made it here they are the same
            return false;
        }

        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            // If parameter cannot be cast, return false.
            IavaJointsCollection collection = (IavaJointsCollection)obj;
            if ((Object)collection == null) { return false; }

            // Do a element by element comparison
            for (IavaJointID i = 0; i < IavaJointID.Count; i++) {
                if (collection[i] != this[i]) { return false; }
            }

            // If we made it here they are equal
            return true;
        }

        public static explicit operator IavaJointsCollection(JointsCollection value) {
            IavaJointsCollection jointsCollection = new IavaJointsCollection();

            for (IavaJointID i = 0; i < IavaJointID.Count; i++) { jointsCollection[i] = (IavaJoint)value[(JointID)i]; }

            return jointsCollection;
        }

        #endregion Operator Overloads
    }
}