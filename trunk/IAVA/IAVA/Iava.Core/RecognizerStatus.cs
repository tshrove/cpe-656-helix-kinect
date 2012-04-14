namespace Iava.Core {

    /// <summary>
    /// Recognizer status enumeration.
    /// </summary>
    public enum RecognizerStatus {
        /// <summary>
        /// Indicates the recognizer is in a ready state.
        /// </summary>
        Ready,
        /// <summary>
        /// Indicates the recognizer is not ready.  This is the default state.
        /// </summary>
        NotReady,
        /// <summary>
        /// Indicates the recognizer is running.
        /// </summary>
        Running,
        /// <summary>
        /// Indicates the recognizer is in an error state.
        /// </summary>        
        Error
    }
}