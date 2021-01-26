using SuspensionAnalysis.DataContracts.Models;
using System.Windows.Media.Media3D;

namespace SuspensionAnalysis.Core.ExtensionMethods
{
    public static class SuspensionAArmPointExtension
    {
        public static (Vector3D, Vector3D) CalculateNormalizedVectors(this SuspensionAArmPoint suspensionAArmPoint)
        {
            return 
                (suspensionAArmPoint.PivotPoint1
                    .BuildVector(suspensionAArmPoint.KnucklePoint)
                    .NormalizeVector(),
                suspensionAArmPoint.PivotPoint2
                    .BuildVector(suspensionAArmPoint.KnucklePoint)
                    .NormalizeVector());
        }
    }
}
