using Microsoft.Research.Kinect.Nui;

namespace Iava.Core.Math {

    public struct IavaVector {

        #region Public Properties

        public double W { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        #endregion Public Properties

        #region Operator Overloads

        public static IavaVector operator +(IavaVector vector1, IavaVector vector2) {
            IavaVector returnVector = new IavaVector();

            // Subtract the vectors
            returnVector.W = vector1.W + vector2.W;
            returnVector.X = vector1.X + vector2.X;
            returnVector.Y = vector1.Y + vector2.Y;
            returnVector.Z = vector1.Z + vector2.Z;

            // Return the difference
            return returnVector;
        }

        public static IavaVector operator -(IavaVector vector1, IavaVector vector2) {
            IavaVector returnVector = new IavaVector();

            // Subtract the vectors
            returnVector.W = vector1.W - vector2.W;
            returnVector.X = vector1.X - vector2.X;
            returnVector.Y = vector1.Y - vector2.Y;
            returnVector.Z = vector1.Z - vector2.Z;

            // Return the difference
            return returnVector;
        }

        public static bool operator ==(IavaVector vector1, IavaVector vector2) {
            return (vector1.W == vector2.W &&
                    vector1.X == vector2.X &&
                    vector1.Y == vector2.Y &&
                    vector1.Z == vector2.Z);
        }

        public static bool operator !=(IavaVector vector1, IavaVector vector2) {
            return (vector1.W != vector2.W &&
                    vector1.X != vector2.X &&
                    vector1.Y != vector2.Y &&
                    vector1.Z != vector2.Z);
        }

        public static explicit operator IavaVector(Vector value) {
            return new IavaVector()
            {
                W = value.W,
                X = value.X,
                Y = value.Y,
                Z = value.Z
            };
        }

        #endregion Operator Overloads

        #region Static Property
        public static IavaVector Zero
        {
            get
            {
                return new IavaVector()
                    {
                        W = 0f,
                        X = 0f,
                        Y = 0f,
                        Z = 0f
                    };
            }
        }
        #endregion
    }
}
