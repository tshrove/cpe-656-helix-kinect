using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iava.Input.Camera {

    /// <summary>
    /// Color data options.
    /// </summary>
    public enum IavaColorImageFormat {
        /// <summary>
        /// Undefined format.
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Specifies an RGB color format with a 640x480 resolution at 30 frames per second.
        /// </summary>
        RgbResolution640x480Fps30 = 1,
        /// <summary>
        /// Specifies an RGB color format with a 1280x960 resolution at 12 frames per second.
        /// </summary>
        RgbResolution1280x960Fps12 = 2,
        /// <summary>
        /// Specifies a YUV color format with a 640x480 resolution at 15 frames per second.
        /// </summary>
        YuvResolution640x480Fps15 = 3,
        /// <summary>
        /// Specifies a raw YUV color format with a 640x480 resolution at 15 frames per second.
        /// </summary>
        RawYuvResolution640x480Fps15 = 4
    }
}
