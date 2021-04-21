using System;
using System.Collections.Generic;
using System.Linq;

namespace SuspensionAnalysis.DataContracts.Models
{
    /// <summary>
    /// It represents a vector with 3 dimensions: x, y and z.
    /// </summary>
    public struct Vector3D
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// The value at axis X.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The value at axis Y.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// The value at axis Z.
        /// </summary>
        public double Z { get; set; }

        /// <summary>
        /// The length of vector.
        /// </summary>
        public double Length => Math.Sqrt(this.LengthSquared);

        /// <summary>
        /// The lenght squared of vector.
        /// </summary>
        public double LengthSquared
            => Math.Pow(this.X, 2) + Math.Pow(this.Y, 2) + Math.Pow(this.Z, 2);

        /// <summary>
        /// This method creates a <see cref="Vector3D"/> based on two <see cref="Point3D"/>.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static Vector3D Create(Point3D point1, Point3D point2)
        {
            return new Vector3D(
                point2.X - point1.X,
                point2.Y - point1.Y,
                point2.Z - point1.Z);
        }

        /// <summary>
        /// This method creates a <see cref="Vector3D"/> based on a string.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3D Create(string vector)
        {
            List<string> vec = vector.Split(',').ToList();

            return new Vector3D
            {
                X = double.Parse(vec[0]),
                Y = double.Parse(vec[1]),
                Z = double.Parse(vec[2])
            };
        }
    }
}
