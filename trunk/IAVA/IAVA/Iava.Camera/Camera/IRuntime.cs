using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iava.Input.Camera {
    /// <summary>
    /// Speech recognition interface.
    /// </summary>
    internal interface IRuntime {
        event EventHandler<IavaSkeletonEventArgs> SkeletonReady;

        event EventHandler<IavaColorImageFrameReadyEventArgs> ColorImageFrameReady;

        event EventHandler<IavaSkeletonFrameReadyEventArgs> SkeletonFrameReady;

        void Initialize();

        void Uninitialize();
    }
}
