using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iava.Input.Camera {

    /// <summary>
    /// Color data options.
    /// </summary>
    public enum IavaColorImageFormat {
        Undefined = 0,
        RgbResolution640x480Fps30 = 1,
        RgbResolution1280x960Fps12 = 2,
        YuvResolution640x480Fps15 = 3,
        RawYuvResolution640x480Fps15 = 4
    }
}
