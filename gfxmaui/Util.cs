using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GfxMaui
{
    internal static class Util
    {

        public static double Lerp(double a, double b, double t)
        {
            return a + (b - a) * t;
        }

    }
}
