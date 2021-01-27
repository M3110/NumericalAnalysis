using System;

namespace SuspensionAnalysis.Infraestructure.Models
{
    public class Vector3D
    {
        /// <summary>
        /// Basic constructor.
        /// </summary>
        public Vector3D() { }

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

        public double LengthSquared => Math.Pow(this.X, 2) + Math.Pow(this.Y, 2) + Math.Pow(this.Z, 2);
    }
}
