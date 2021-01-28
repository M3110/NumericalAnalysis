namespace SuspensionAnalysis.Infrastructure.Models
{
    public struct Point3D
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public override string ToString() => $"({this.X}, {this.Y}, {this.Z})";
    }
}
