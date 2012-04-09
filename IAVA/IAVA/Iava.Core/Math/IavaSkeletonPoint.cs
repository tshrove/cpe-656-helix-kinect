using System;
using Microsoft.Kinect;

namespace Iava.Core.Math {

    /// <summary>
    /// Contains the state information of an IavaSkeletonPoint in 3D space.
    /// </summary>
    public struct IavaSkeletonPoint {

        #region Public Properties

        /// <summary>
        /// Gets/Sets the X coordinate of the IavaSkeletonPoint.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets/Sets the Y coordinate of the IavaSkeletonPoint.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets/Sets the Z coordinate of the IavaSkeletonPoint.
        /// </summary>
        public double Z { get; set; }

        /// <summary>
        /// Returns an IavaSkeletonPoint located at the origin.
        /// </summary>
        public static readonly IavaSkeletonPoint Zero {
            get { return new IavaSkeletonPoint() { X = 0.0, Y = 0.0, Z = 0.0 }; }
        }

        #endregion Public Properties

        #region Operator Overloads

        /// <summary>
        /// Adds two IavaSkeletonPoint instances.
        /// </summary>
        /// <param name="point1">A IavaSkeletonPoint to compare to add.</param>
        /// <param name="point2">A IavaSkeletonPoint to compare to add.</param>
        /// <returns>The resulting IavaSkeletonPoint of the addition operator.</returns>
        public static IavaSkeletonPoint operator +(IavaSkeletonPoint point1, IavaSkeletonPoint point2) {
            IavaSkeletonPoint returnVector = new IavaSkeletonPoint();

            // Add the vectors
            returnVector.X = vector1.X + vector2.X;
            returnVector.Y = vector1.Y + vector2.Y;
            returnVector.Z = vector1.Z + vector2.Z;

            // Return the sum
            return returnVector;
        }

        /// <summary>
        /// Subtracts two IavaSkeletonPoint instances.
        /// </summary>
        /// <param name="point1">A IavaSkeletonPoint to compare to subtract.</param>
        /// <param name="point2">A IavaSkeletonPoint to compare to subtract.</param>
        /// <returns>The resulting IavaSkeletonPoint of the subtraction operator.</returns>
        public static IavaSkeletonPoint operator -(IavaSkeletonPoint point1, IavaSkeletonPoint point2) {
            IavaSkeletonPoint returnVector = new IavaSkeletonPoint();

            // Subtract the vectors
            returnVector.X = vector1.X - vector2.X;
            returnVector.Y = vector1.Y - vector2.Y;
            returnVector.Z = vector1.Z - vector2.Z;

            // Return the difference
            return returnVector;
        }

        /// <summary>
        /// Determines whether two IavaSkeletonPoint instances are equal.
        /// </summary>
        /// <param name="point1">A IavaSkeletonPoint to compare for equality.</param>
        /// <param name="point2">A IavaSkeletonPoint to compare for equality.</param>
        /// <returns>TRUE if the two IavaSkeletonPoint instances are equal, else FALSE</returns>
        public static bool operator ==(IavaSkeletonPoint point1, IavaSkeletonPoint point2) {
            return (vector1.X.Equals(vector2.X) &&
                    vector1.Y.Equals(vector2.Y) &&
                    vector1.Z.Equals(vector2.Z));
        }

        /// <summary>
        /// Determines whether two IavaSkeletonPoint instances are not equal.
        /// </summary>
        /// <param name="point1">A IavaSkeletonPoint to compare for inequality.</param>
        /// <param name="point2">A IavaSkeletonPoint to compare for inequality.</param>
        /// <returns>TRUE if the two IavaSkeletonPoint instances are not equal, else FALSE</returns>
        public static bool operator !=(IavaSkeletonPoint point1, IavaSkeletonPoint point2) {
            return (!vector1.X.Equals(vector2.X) &&
                    !vector1.Y.Equals(vector2.Y) &&
                    !vector1.Z.Equals(vector2.Z));
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current IavaSkeletonPoint. 
        /// </summary>
        /// <param name="obj">Object to compare with the current IavaSkeletonPoint.</param>
        /// <returns>TRUE if the specified object is equal to the current IavaSkeletonPoint, else FALSE. </returns>
        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            try {
                IavaSkeletonPoint point = (IavaSkeletonPoint)obj;

                // Do a field by field comparison
                return (point.X.Equals(this.X) &&
                        point.Y.Equals(this.Y) &&
                        point.Z.Equals(this.Z));
            }
            // If parameter cannot be cast, return false.
            catch (InvalidCastException) {
                return false;
            }
        }

        /// <summary>
        /// Casts the specified SkeletonPoint to an IavaSkeletonPoint
        /// </summary>
        /// <param name="value">SkeletonPoint to cast to an IavaSkeletonPoint</param>
        /// <returns>IavaSkeletonPoint representation of the SkeletonPoint</returns>
        public static explicit operator IavaSkeletonPoint(SkeletonPoint value) {
            return new IavaSkeletonPoint()
            {
                X = value.X,
                Y = value.Y,
                Z = value.Z
            };
        }

        #endregion Operator Overloads
    }
}
