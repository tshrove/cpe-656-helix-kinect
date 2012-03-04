namespace Iava.Core.Math {
    public static class Geometry {
        public static IavaVector Translate(IavaVector original, IavaVector translation)
        {
            IavaVector returnPoint = new IavaVector();
            // Translate the Points
            returnPoint.X = original.X - translation.X;
            returnPoint.Y = original.Y - translation.Y;
            returnPoint.Z = original.Z - translation.Z;
            return returnPoint;
        }

        public static double Magnitude2D(IavaVector point1, IavaVector point2) {
            IavaVector difference = point1 - point2;

            return System.Math.Sqrt(System.Math.Pow(difference.X, 2.0) +
                                    System.Math.Pow(difference.Y, 2.0));
        }

        public static double Magnitude3D(IavaVector point1, IavaVector point2) {
            IavaVector difference = point1 - point2;

            return System.Math.Sqrt(System.Math.Pow(difference.X, 2.0) +
                                    System.Math.Pow(difference.Y, 2.0) +
                                    System.Math.Pow(difference.Z, 2.0));
        }
    }
}

