namespace Iava.Core.Math {

    /// <summary>
    /// Performs basic math operations on the IavaSkeletonPoint class.
    /// </summary>
    public static class Geometry {

        /// <summary>
        /// Translates an IavaSkeletonPoint according to the supplied translation point.
        /// </summary>
        /// <param name="original">The original IavaSkeletonPoint to be translated.</param>
        /// <param name="translation">The translation IavaSkeletonPoint.</param>
        /// <returns>A new IavaSkeletonPoint resulting from the translation.</returns>
        public static IavaSkeletonPoint Translate(IavaSkeletonPoint original, IavaSkeletonPoint translation) {
            IavaSkeletonPoint returnPoint = new IavaSkeletonPoint();
            // Translate the Points
            returnPoint.X = original.X - translation.X;
            returnPoint.Y = original.Y - translation.Y;
            returnPoint.Z = original.Z - translation.Z;
            return returnPoint;
        }

        /// <summary>
        /// Returns the 2D magnitude between the two supplied IavaSkeletonPoints.
        /// </summary>
        /// <param name="point1">IavaSkeletonPoint</param>
        /// <param name="point2">IavaSkeletonPoint</param>
        /// <returns>2D magnitude between the two IavaSkeletonPoints.</returns>
        public static double Magnitude2D(IavaSkeletonPoint point1, IavaSkeletonPoint point2) {
            IavaSkeletonPoint difference = point1 - point2;

            return System.Math.Sqrt(System.Math.Pow(difference.X, 2.0) +
                                    System.Math.Pow(difference.Y, 2.0));
        }

        /// <summary>
        /// Returns the 3D magnitude between the two supplied IavaSkeletonPoints.
        /// </summary>
        /// <param name="point1">IavaSkeletonPoint</param>
        /// <param name="point2">IavaSkeletonPoint</param>
        /// <returns>3D magnitude between the two IavaSkeletonPoints.</returns>
        public static double Magnitude3D(IavaSkeletonPoint point1, IavaSkeletonPoint point2) {
            IavaSkeletonPoint difference = point1 - point2;

            return System.Math.Sqrt(System.Math.Pow(difference.X, 2.0) +
                                    System.Math.Pow(difference.Y, 2.0) +
                                    System.Math.Pow(difference.Z, 2.0));
        }

        /// <summary>
        /// Returns the scaled 2D IavaSkeletonPoint
        /// </summary>
        /// <param name="point">Base IavaSkeletonPoint</param>
        /// <param name="scaleFactor">Factor to scale the IavaSkeletonPoint</param>
        /// <returns>Scaled IavaSkeletonPoint</returns>
        public static IavaSkeletonPoint Scale2D(IavaSkeletonPoint point, double scaleFactor) {
            return new IavaSkeletonPoint() { X = point.X * scaleFactor, Y = point.Y * scaleFactor, Z = point.Z };
        }
    }
}

