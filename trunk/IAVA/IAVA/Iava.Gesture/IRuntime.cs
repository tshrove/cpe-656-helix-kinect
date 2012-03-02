using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iava.Input.Camera;

namespace Iava.Gesture {
    /// <summary>
    /// Speech recognition interface.
    /// </summary>
    internal interface IRuntime {
        event EventHandler<IavaSkeletonEventArgs> SkeletonReady;

        event EventHandler<IavaImageFrameReadyEventArgs> ImageFrameReady;

        event EventHandler<IavaSkeletonFrameReadyEventArgs> SkeletonFrameReady;
    }
}
