using SuspensionAnalysis.Infraestructure.Models;
using SuspensionAnalysis.Infraestructure.Models.SuspensionComponents;

namespace SuspensionAnalysis.Core.ExtensionMethods
{
    public static class SingleComponentPointExtension
    {
        public static Vector3D CalculateNormalizedVector(this SingleComponentPoint component)
        {
            return component.PivotPoint
                .BuildVector(component.FasteningPoint)
                .NormalizeVector();
        }

        public static Vector3D CalculateOriginReference(this SingleComponentPoint component, Point3D origin)
        {
            return new Vector3D(
                origin.X - component.PivotPoint.X,
                origin.Y - component.PivotPoint.Y,
                origin.Z - component.PivotPoint.Z);
        }
    }
}
