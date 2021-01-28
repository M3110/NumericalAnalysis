using SuspensionAnalysis.Infrastructure.Models;
using SuspensionAnalysis.Infrastructure.Models.SuspensionComponents;

namespace SuspensionAnalysis.Core.ExtensionMethods
{
    public static class SuspensionAArmPointExtension
    {
        public static (Vector3D, Vector3D) CalculateNormalizedVectors(this SuspensionAArmPoint suspensionAArmPoint)
        {
            return (
                suspensionAArmPoint.PivotPoint1
                    .BuildVector(suspensionAArmPoint.KnucklePoint)
                    .NormalizeVector(),
                suspensionAArmPoint.PivotPoint2
                    .BuildVector(suspensionAArmPoint.KnucklePoint)
                    .NormalizeVector());
        }

        public static (Vector3D, Vector3D) CalculateOriginReferences(this SuspensionAArmPoint suspensionAArmPoint, Point3D origin)
        {
            return (
                new Vector3D(
                    origin.X - suspensionAArmPoint.PivotPoint1.X,
                    origin.Y - suspensionAArmPoint.PivotPoint1.Y,
                    origin.Z - suspensionAArmPoint.PivotPoint1.Z),
                new Vector3D(
                    origin.X - suspensionAArmPoint.PivotPoint2.X,
                    origin.Y - suspensionAArmPoint.PivotPoint2.Y,
                    origin.Z - suspensionAArmPoint.PivotPoint2.Z));
        }
    }
}
