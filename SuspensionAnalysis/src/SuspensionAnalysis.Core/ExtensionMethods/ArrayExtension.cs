using System;
using System.Linq;

namespace SuspensionAnalysis.Core.ExtensionMethods
{
    /// <summary>
    /// It contains extension methods to arrays.
    /// </summary>
    public static class ArrayExtension
    {
        /// <summary>
        /// This method normalizes the vector. 
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static double[] NormalizeVector(this double[] vector)
        {
            double vectorNorm = vector.CalculateNorm();

            return vector
                .Select(item => item / vectorNorm)
                .ToArray();
        }

        /// <summary>
        /// This method calculates the vector norm. 
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static double CalculateNorm(this double[] vector)
        {
            double result = vector.Sum(v => Math.Pow(v, 2));

            return Math.Sqrt(result);
        }

        /// <summary>
        /// This method calculates the cross product. 
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static double[] CalculateCrossProduct(this double[] vector1, double[] vector2)
        {
            if (vector1.Length != 3 || vector2.Length != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(CalculateCrossProduct), "The vector should have lenght equals to 3");
            }

            return new double[3]
            {
                vector1[1] * vector2[2] - vector1[2] * vector2[1],
                vector1[2] * vector2[0] - vector1[0] * vector2[2],
                vector1[0] * vector2[1] - vector1[1] * vector2[0],
            };
        }
    }
}
