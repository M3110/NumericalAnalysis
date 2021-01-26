using System.Windows.Media.Media3D;

namespace SuspensionAnalysis.Core.ExtensionMethods
{
    public static class Point3DExtension
    {
        public static Vector3D BuildVector(this Point3D point1, Point3D point2)
        {
            return new Vector3D(
                point1.X - point2.X,
                point1.Y - point2.Y,
                point1.Z - point2.Z);
        }
    }
}
