using System.Collections;
using Microsoft.Research.Kinect.Nui;

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

        public static explicit operator IavaJointsCollection(JointsCollection value) {
            IavaJointsCollection jointsCollection = new IavaJointsCollection();

            for (int i = 0; i < (int)IavaJointID.Count; i++) { jointsCollection[(IavaJointID)i] = (IavaJoint)value[(JointID)i]; }

            return jointsCollection;
        }

        #endregion Operator Overloads
    }
}