using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Research.Kinect.Nui;

using Iava.Input.Camera;

namespace Iava.Gesture {
    public interface IGestureSegment {
        GestureResult CheckGesture(IavaSkeletonData skeleton);
    }
}
