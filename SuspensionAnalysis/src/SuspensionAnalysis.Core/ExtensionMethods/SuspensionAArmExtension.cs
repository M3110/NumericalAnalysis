using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.ExtensionMethods
{
    public static class SuspensionAArmExtension
    {
        public static (Vector3D, Vector3D) CalculateNormalizedVectors<TProfile>(this SuspensionAArm<TProfile> suspensionAArm)
            where TProfile : Profile
        {
            return (
                suspensionAArm.PivotPoint1
                    .BuildVector(suspensionAArm.KnucklePoint)
                    .NormalizeVector(),
                suspensionAArm.PivotPoint2
                    .BuildVector(suspensionAArm.KnucklePoint)
                    .NormalizeVector());
        }

        public static (Vector3D, Vector3D) CalculateOriginReferences<TProfile>(this SuspensionAArm<TProfile> suspensionAArm, Point3D origin)
            where TProfile : Profile
        {
            return (
                new Vector3D(
                    origin.X - suspensionAArm.PivotPoint1.X,
                    origin.Y - suspensionAArm.PivotPoint1.Y,
                    origin.Z - suspensionAArm.PivotPoint1.Z),
                new Vector3D(
                    origin.X - suspensionAArm.PivotPoint2.X,
                    origin.Y - suspensionAArm.PivotPoint2.Y,
                    origin.Z - suspensionAArm.PivotPoint2.Z));
        }
    }
}
