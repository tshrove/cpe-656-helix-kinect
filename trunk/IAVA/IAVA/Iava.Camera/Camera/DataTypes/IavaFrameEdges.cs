using System;

namespace Iava.Input.Camera {

    /// <summary>
    /// Specifies how much of the skeleton is visible.
    /// </summary>
    [Flags]
    public enum IavaFrameEdges {
        /// <summary>
        /// No edges are clipped.
        /// </summary>
        None = 0,
        /// <summary>
        /// Right edge is clipped.
        /// </summary>
        Right = 1,
        /// <summary>
        /// Left edge is clipped.
        /// </summary>
        Left = 2,
        /// <summary>
        /// Top edge is clipped.
        /// </summary>
        Top = 4,
        /// <summary>
        /// Bottom edge is clipped.
        /// </summary>
        Bottom = 8
    }
}