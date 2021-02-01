using System.Collections.Generic;
using System.Linq;

namespace SuspensionAnalysis.DataContracts.Models
{
    /// <summary>
    /// It represents a point with 3 dimensions: x, y and z.
    /// </summary>
    public struct Point3D
    {
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

        /// <inheritdoc/>
        public override string ToString() => $"({this.X}, {this.Y}, {this.Z})";

        /// <summary>
        /// This method creates a <see cref="Point3D"/> based on a string.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Point3D Create(string point)
        {
            List<string> points = point.Split(',').ToList();

            return new Point3D
            {
                X = double.Parse(points[0]),
                Y = double.Parse(points[1]),
                Z = double.Parse(points[2]),
            };
        }
    }
}
