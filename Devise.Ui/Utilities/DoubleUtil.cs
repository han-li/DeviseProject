using System;

namespace Devise.Ui.Utilities
{
    public static class DoubleUtil
    {
        private const double Tolerance = 0.000001d;

        public static bool AreClosed(double a, double b)
        {
            return Math.Abs(a - b) < Tolerance;
        }
    }
}
