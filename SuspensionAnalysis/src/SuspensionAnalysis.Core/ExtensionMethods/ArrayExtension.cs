using System;
using System.Linq;

namespace SuspensionAnalysis.Core.ExtensionMethods
{
    public static class ArrayExtension
    {
        public static double[] NormalizeVector(this double[] vector)
        {
            double vectorNorm = vector.CalculateNorm();

            return vector
                .Select(item => item / vectorNorm)
                .ToArray();
        }

        public static double CalculateNorm(this double[] vector)
        {
            double result = vector.Sum(v => Math.Pow(v, 2));

            return Math.Sqrt(result);
        }
        public static double[] CalculateCrossProduct(this double[] vector1, double[] vector2)
    }
}
