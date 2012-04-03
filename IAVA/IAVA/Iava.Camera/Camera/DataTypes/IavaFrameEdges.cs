using System;

namespace Iava.Input.Camera {

    /// <summary>
    /// Specifies how much of the skeleton is visible.
    /// </summary>
    [Flags]
    public enum IavaFrameEdges {
        None = 0,
        Right = 1,
        Left = 2,
        Top = 4,
        Bottom = 8
    }
}