﻿using Microsoft.Kinect;

namespace Iava.Core.Math {

    public struct IavaSkeletonPoint {

        #region Public Properties

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        #endregion Public Properties

        #region Operator Overloads

        public static IavaSkeletonPoint operator +(IavaSkeletonPoint vector1, IavaSkeletonPoint vector2) {
            IavaSkeletonPoint returnVector = new IavaSkeletonPoint();

            // Add the vectors
            returnVector.X = vector1.X + vector2.X;
            returnVector.Y = vector1.Y + vector2.Y;
            returnVector.Z = vector1.Z + vector2.Z;

            // Return the sum
            return returnVector;
        }

        public static IavaSkeletonPoint operator -(IavaSkeletonPoint vector1, IavaSkeletonPoint vector2) {
            IavaSkeletonPoint returnVector = new IavaSkeletonPoint();

            // Subtract the vectors
            returnVector.X = vector1.X - vector2.X;
            returnVector.Y = vector1.Y - vector2.Y;
            returnVector.Z = vector1.Z - vector2.Z;

            // Return the difference
            return returnVector;
        }

        public static bool operator ==(IavaSkeletonPoint vector1, IavaSkeletonPoint vector2) {
            return (vector1.X == vector2.X &&
                    vector1.Y == vector2.Y &&
                    vector1.Z == vector2.Z);
        }

        public static bool operator !=(IavaSkeletonPoint vector1, IavaSkeletonPoint vector2) {
            return (vector1.X != vector2.X &&
                    vector1.Y != vector2.Y &&
                    vector1.Z != vector2.Z);
        }

        public static explicit operator IavaSkeletonPoint(SkeletonPoint value) {
            return new IavaSkeletonPoint()
            {
                X = value.X,
                Y = value.Y,
                Z = value.Z
            };
        }

        #endregion Operator Overloads

        #region Static Property
        public static IavaSkeletonPoint Zero {
            get {
                return new IavaSkeletonPoint()
                    {
                        X = 0.0,
                        Y = 0.0,
                        Z = 0.0
                    };
            }
        }
        #endregion Static Property
    }
}