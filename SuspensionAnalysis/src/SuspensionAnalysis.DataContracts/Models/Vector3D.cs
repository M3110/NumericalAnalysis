using System;
using System.Collections.Generic;
using System.Linq;

namespace SuspensionAnalysis.DataContracts.Models
{
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

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public double Length => Math.Sqrt(this.LengthSquared);

        public double LengthSquared
            => Math.Pow(this.X, 2) + Math.Pow(this.Y, 2) + Math.Pow(this.Z, 2);

        public static Vector3D Create(Point3D point1, Point3D point2)
        {
            return new Vector3D(
                point1.X - point2.X,
                point1.Y - point2.Y,
                point1.Z - point2.Z);
        }

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
