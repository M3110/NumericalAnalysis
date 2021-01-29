using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.Models;

namespace SuspensionAnalysis.Core.ExtensionMethods
{
    public static class SingleComponentExtension
    {
        public static Vector3D CalculateNormalizedVector(this SingleComponent component)
        {
            return component.PivotPoint
                .BuildVector(component.FasteningPoint)
                .NormalizeVector();
        }

        public static Vector3D CalculateOriginReference(this SingleComponent component, Point3D origin)
        {
            return new Vector3D(
                origin.X - component.PivotPoint.X,
                origin.Y - component.PivotPoint.Y,
                origin.Z - component.PivotPoint.Z);
        }
    }
}
