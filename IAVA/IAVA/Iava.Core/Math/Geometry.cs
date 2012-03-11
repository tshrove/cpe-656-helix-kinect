namespace Iava.Core.Math {
    public static class Geometry {
        public static IavaSkeletonPoint Translate(IavaSkeletonPoint original, IavaSkeletonPoint translation) {
            IavaSkeletonPoint returnPoint = new IavaSkeletonPoint();
            // Translate the Points
            returnPoint.X = original.X - translation.X;
            returnPoint.Y = original.Y - translation.Y;
            returnPoint.Z = original.Z - translation.Z;
            return returnPoint;
        }

        public static double Magnitude2D(IavaSkeletonPoint point1, IavaSkeletonPoint point2) {
            IavaSkeletonPoint difference = point1 - point2;

            return System.Math.Sqrt(System.Math.Pow(difference.X, 2.0) +
                                    System.Math.Pow(difference.Y, 2.0));
        }

        public static double Magnitude3D(IavaSkeletonPoint point1, IavaSkeletonPoint point2) {
            IavaSkeletonPoint difference = point1 - point2;

            return System.Math.Sqrt(System.Math.Pow(difference.X, 2.0) +
                                    System.Math.Pow(difference.Y, 2.0) +
                                    System.Math.Pow(difference.Z, 2.0));
        }
    }
}

