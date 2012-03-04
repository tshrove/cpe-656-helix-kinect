using Microsoft.Research.Kinect.Nui;

namespace Iava.Core.Math {

    public struct IavaVector {

        #region Public Properties

        public float W { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        #endregion Public Properties

        #region Operator Overloads

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
    }
}
