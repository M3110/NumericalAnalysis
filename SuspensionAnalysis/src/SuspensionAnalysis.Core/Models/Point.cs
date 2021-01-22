namespace SuspensionAnalysis.Core.Models
{
    public class Point
    {
        public double AxisX { get; set; }

        public double AxisY { get; set; }

        public double AxisZ { get; set; }
    }

    public class PointFactory
    {
        public static double[] CreateVector(Point inicialPoint, Point finalPoint)
        {
            return new double[3]
            {
                finalPoint.AxisX - inicialPoint.AxisX,
                finalPoint.AxisY - inicialPoint.AxisY,
                finalPoint.AxisZ - inicialPoint.AxisZ,
            };
        }
    }
}
