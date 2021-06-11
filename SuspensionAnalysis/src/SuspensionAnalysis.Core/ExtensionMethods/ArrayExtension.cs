using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            return vector.Select(item => item / vectorNorm).ToArray();
        }

        /// <summary>
        /// This method calculates the vector norm. 
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static double CalculateNorm(this double[] vector)
            => Math.Sqrt(vector.Sum(v => Math.Pow(v, 2)));

        /// <summary>
        /// This method inverses a matrix using the Gauss-Jordan method.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static double[,] InverseMatrix(this double[,] matrix)
        {
            double[,] matrixCopy = matrix.Clone() as double[,];

            int n = matrixCopy.GetLength(0);
            double[,] matrizInv = new double[n, n];
            double pivot, p;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrizInv[i, j] = (i == j) ? 1 : 0;
                }
            }

            // Triangularization
            for (int i = 0; i < n; i++)
            {
                pivot = matrixCopy[i, i];
                if (pivot == 0)
                {
                    throw new DivideByZeroException($"Pivot cannot be zero at line {i}.");
                }

                for (int l = 0; l < n; l++)
                {
                    matrixCopy[i, l] = matrixCopy[i, l] / pivot;
                    matrizInv[i, l] = matrizInv[i, l] / pivot;
                }

                for (int k = i + 1; k < n; k++)
                {
                    p = matrixCopy[k, i];

                    for (int j = 0; j < n; j++)
                    {
                        matrixCopy[k, j] = matrixCopy[k, j] - p * matrixCopy[i, j];
                        matrizInv[k, j] = matrizInv[k, j] - p * matrizInv[i, j];
                    }
                }
            }

            // Retrosubstitution
            for (int i = n - 1; i >= 0; i--)
            {
                for (int k = i - 1; k >= 0; k--)
                {
                    p = matrixCopy[k, i];

                    for (int j = n - 1; j >= 0; j--)
                    {
                        matrixCopy[k, j] = matrixCopy[k, j] - p * matrixCopy[i, j];
                        matrizInv[k, j] = matrizInv[k, j] - p * matrizInv[i, j];
                    }
                }
            }

            return matrizInv;
        }

        /// <summary>
        /// This method multiplicates a matrix and a vector.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static double[] Multiply(this double[,] matrix, double[] vector)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            if (columns != vector.Length)
                throw new ArgumentOutOfRangeException("Matrix and vector", "The matrix row size must be equals to vector size.");

            double[] result = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                double sum = 0;

                for (int j = 0; j < columns; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }

                result[i] = sum;
            }

            return result;
        }

        /// <summary>
        /// This method multiplicates a number and a vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static double[] Multiply(this double[] vector, double number)
            => vector.Select(item => item * number).ToArray();

        /// <summary>
        /// This method sums two vectors.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static double[] Sum(this double[] vector1, double[] vector2)
            => vector1.MathOperation((v1, v2) => v1 + v2, vector2);

        /// <summary>
        /// This method subtracts two vectors.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static double[] Subtract(this double[] vector1, double[] vector2)
            => vector1.MathOperation((v1, v2) => v1 - v2, vector2);

        /// <summary>
        /// This method do a mathematic operation for two vectors.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static double[] MathOperation(this double[] vector1, Func<double, double, double> func, double[] vector2)
        {
            if (vector1.Length != vector2.Length)
                throw new ArgumentOutOfRangeException("Vectors", "The vectors must have the same size.");

            return vector1.Zip(vector2, func).ToArray();
        }

        /// <summary>
        /// This method sums two vectors.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static double[] Sum(this double[] vector1, double[] vector2, double[] vector3)
        {
            if (vector1.Length != vector2.Length || vector1.Length != vector3.Length || vector2.Length != vector3.Length)
                throw new ArgumentOutOfRangeException("Vectors", "The vectors must have the same size.");

            double[] result = new double[vector1.Length];
            for (int i = 0; i < vector1.Length; i++)
            {
                result[i] = vector1[i] + vector2[i] + vector3[i];
            }

            return result;
        }

        /// <summary>
        /// This method subtracts three vectors.
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static double[] Subtract(this double[] vector1, double[] vector2, double[] vector3)
        {
            if (vector1.Length != vector2.Length || vector1.Length != vector3.Length || vector2.Length != vector3.Length)
                throw new ArgumentOutOfRangeException("Vectors", "The vectors must have the same size.");

            double[] result = new double[vector1.Length];
            for (int i = 0; i < vector1.Length; i++)
            {
                result[i] = vector1[i] - vector2[i] - vector3[i];
            }

            return result;
        }
    }
}
