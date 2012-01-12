using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Research.Kinect.Nui;

using Iava.Input.Camera;

namespace Iava.Gesture.GestureStuff {
    public interface IGestureSegment {
        GestureResult CheckGesture(SkeletonData skeleton);
    }
}
