using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Iava.Core.Math {
    public static class Geometry {
        public static Point Translate(Point original, Point translation) {
            Point returnPoint = new Point();

            // Translate the Points
            returnPoint.X = original.X - translation.X;
            returnPoint.Y = original.Y - translation.Y;

            return returnPoint;
        }
    }
}

