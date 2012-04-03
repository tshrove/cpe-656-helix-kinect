using System.Collections;
using Microsoft.Kinect;
using System;

namespace Iava.Input.Camera {

    /// <summary>
    /// IEnumerable containing a collection of IavaJoint objects.
    /// </summary>
    public class IavaJointCollection : IEnumerable {

        #region Public Properties

        /// <summary>
        /// Gets the number of elements in the collection
        /// </summary>
        public int Count { get { return _joints.Length; } }

        /// <summary>
        /// Gets the specified element of the collection
        /// </summary>
        /// <param name="i">The joint identifier</param>
        /// <returns></returns>
        public IavaJoint this[IavaJointType i] {
            get { return _joints[(int)i]; }
            set { _joints[(int)i] = value; }
        }

        #endregion Public Properties

        #region Private Fields

        /// <summary>
        /// Collection of joints
        /// </summary>
        private IavaJoint[] _joints;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public IavaJointCollection() {
            // Initialize the JointArray
            _joints = new IavaJoint[(int)IavaJointType.Count];
        }

        #endregion Constructors

        #region Operator Overloads

        /// <summary>
        /// Determines whether two IavaJointCollection instances are equal.
        /// </summary>
        /// <param name="collection1">A IavaJointCollection to compare for equality.</param>
        /// <param name="collection2">A IavaJointCollection to compare for equality.</param>
        /// <returns>TRUE if the two IavaJointCollection instances are equal, else FALSE</returns>
        public static bool operator ==(IavaJointCollection collection1, IavaJointCollection collection2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(collection1, collection2)) { return true; }

            // If just one is null, return false.
            if (((object)collection1 == null) || ((object)collection2 == null)) { return false; }

            for (IavaJointType i = 0; i < IavaJointType.Count; i++) {
                if (!collection1[i].Equals(collection2[i])) { return false; }
            }

            // If we made it here they are the same
            return true;
        }

        /// <summary>
        /// Determines whether two IavaJointCollection instances are not equal.
        /// </summary>
        /// <param name="collection1">A IavaJointCollection to compare for inequality.</param>
        /// <param name="collection2">A IavaJointCollection to compare for inequality.</param>
        /// <returns>TRUE if the two IavaJointCollection instances are not equal, else FALSE</returns>
        public static bool operator !=(IavaJointCollection collection1, IavaJointCollection collection2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(collection1, collection2)) { return false; }

            // If just one is null, return true.
            if (((object)collection1 == null) || ((object)collection2 == null)) { return true; }

            for (IavaJointType i = 0; i < IavaJointType.Count; i++) {
                if (!collection1[i].Equals(collection2[i])) { return true; }
            }

            // If we made it here they are the same
            return false;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current IavaJointCollection. 
        /// </summary>
        /// <param name="obj">Object to compare with the current IavaJointCollection.</param>
        /// <returns>TRUE if the specified object is equal to the current IavaJointCollection, else FALSE. </returns>
        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            try {
                IavaJointCollection collection = (IavaJointCollection)obj;

                // Do a element by element comparison
                for (IavaJointType i = 0; i < IavaJointType.Count; i++) {
                    if (!collection[i].Equals(this[i])) { return false; }
                }

                // If we made it here they are equal
                return true;
            }
            // If parameter cannot be cast, return false.
            catch (InvalidCastException) {
                return false;
            }
        }

        /// <summary>
        /// Casts the specified JointCollection to an IavaJointCollection
        /// </summary>
        /// <param name="value">JointCollection to cast to an IavaJointCollection</param>
        /// <returns>IavaJointCollection representation of the JointCollection</returns>
        public static explicit operator IavaJointCollection(JointCollection value) {
            if (value == null) { return null; }

            IavaJointCollection jointsCollection = new IavaJointCollection();

            for (IavaJointType i = 0; i < IavaJointType.Count; i++) { jointsCollection[i] = (IavaJoint)value[(JointType)i]; }

            return jointsCollection;
        }

        #endregion Operator Overloads

        #region IEnumerable Members

        public IEnumerator GetEnumerator() { return _joints.GetEnumerator(); }

        #endregion IEnumerable Members
    }
}