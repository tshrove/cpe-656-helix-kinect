namespace Iava.Input.Camera {

    /// <summary>
    /// Specifies the joint tracking state.
    /// </summary>
    public enum IavaJointTrackingState {
        /// <summary>
        /// Not tracked state.
        /// </summary>
        NotTracked = 0,
        /// <summary>
        /// Inferred state.
        /// </summary>
        Inferred = 1,
        /// <summary>
        /// Tracked state.
        /// </summary>
        Tracked = 2,
    }
}