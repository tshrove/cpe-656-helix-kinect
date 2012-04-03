using System;

namespace Iava.Input.Camera {

    /// <summary>
    /// KinectSensor interface
    /// </summary>
    internal interface IRuntime {

        event EventHandler<IavaColorImageFrameReadyEventArgs> ColorImageFrameReady;

        event EventHandler<IavaSkeletonEventArgs> SkeletonReady;

        event EventHandler<IavaSkeletonFrameReadyEventArgs> SkeletonFrameReady;

        void Initialize();

        void Uninitialize();
    }
}
