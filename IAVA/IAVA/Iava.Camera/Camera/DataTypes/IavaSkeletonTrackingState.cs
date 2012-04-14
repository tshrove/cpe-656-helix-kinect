namespace Iava.Input.Camera {

    /// <summary>
    /// Specifies a skeleton's tracking state.
    /// </summary>
    public enum IavaSkeletonTrackingState {
        /// <summary>
        /// Not tracked state.
        /// </summary>
        NotTracked = 0,
        /// <summary>
        /// Position only tracking state.
        /// </summary>
        PositionOnly = 1,
        /// <summary>
        /// Tracked state.
        /// </summary>
        Tracked = 2,
    }
}