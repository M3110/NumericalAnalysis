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

            return vector.Select(item => item / vectorNorm).ToArray();
        }

        /// <summary>
        /// This method calculates the vector norm. 
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static double CalculateNorm(this double[] vector)
        {
            return Math.Sqrt(vector.Sum(v => Math.Pow(v, 2)));
        }

        /// <summary>
        /// This method inverses a matrix using the Gauss-Jordan method.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>The inversed matrix using the Gauss-Jordan method.</returns>
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
        /// <returns>A new vector with the result of multiplication between a matrix and a vector.</returns>
        public static double[] Multiply(this double[,] matrix, double[] vector)
        {
            if (matrix.GetLength(1) != vector.Length)
                throw new ArgumentOutOfRangeException("Matrix and vector", "The matrix row size must be equals to vector size.");

            int rows1 = matrix.GetLength(0);
            int columns1 = matrix.GetLength(1);

            double[] result = new double[rows1];

            for (int i = 0; i < rows1; i++)
            {
                double sum = 0;

                for (int j = 0; j < columns1; j++)
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
        /// <returns>A new vector with the result of multiplication between a number and a vector.</returns>
        public static double[] Multiply(this double[] vector, double number)
        {
            return vector.Select(item => item * number).ToArray();
        }

        /// <summary>
        /// This method sums vectors.
        /// </summary>
        /// <param name="mainVector"></param>
        /// <param name="vectors"></param>
        /// <returns>A new array with the result of sum between vectors.</returns>
        public static double[] Sum(this double[] mainVector, params double[][] vectors) => MathOperation(mainVector, (v1, v2) => v1 + v2, vectors);

        /// <summary>
        /// This method sums vectors.
        /// </summary>
        /// <param name="mainVector"></param>
        /// <param name="vectors"></param>
        /// <returns>A new array with the result of sum between vectors.</returns>
        public static double[] Subtract(this double[] mainVector, params double[][] vectors) => MathOperation(mainVector, (v1, v2) => v1 - v2, vectors);

        /// <summary>
        /// This method do a mathematic operation.
        /// </summary>
        /// <param name="mainVector"></param>
        /// <param name="vectors"></param>
        /// <returns>A new array with the result of a mathematic operation.</returns>
        private static double[] MathOperation(this double[] mainVector, Func<double, double, double> func, params double[][] vectors)
        {
            double[] result = new double[mainVector.Length];

            foreach (double[] vector in vectors)
            {
                result = mainVector.Zip(vector, func).ToArray();
            }

            return result;
        }
    }
}
