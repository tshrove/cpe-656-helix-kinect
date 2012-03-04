using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iava.Core.Math {
    public static class Geometry {
        public static IavaVector Translate(IavaVector original, IavaVector translation)
        {
            IavaVector returnPoint = new IavaVector();
            // Translate the Points
            returnPoint.X = original.X - translation.X;
            returnPoint.Y = original.Y - translation.Y;
            returnPoint.Z = original.Z - translation.Z;
            return returnPoint;
        }
    }
}

