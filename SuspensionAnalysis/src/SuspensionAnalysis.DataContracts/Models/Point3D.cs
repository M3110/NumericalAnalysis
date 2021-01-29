using System.Collections.Generic;
using System.Linq;

namespace SuspensionAnalysis.DataContracts.Models
{
    public struct Point3D
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public override string ToString() => $"({this.X}, {this.Y}, {this.Z})";

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
