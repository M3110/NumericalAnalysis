using SuspensionAnalysis.Infrastructure.Models.Profiles;

namespace SuspensionAnalysis.Infrastructure.Models.SuspensionComponents
{
    public class SuspensionAArmPoint
    {
        public Point3D KnucklePoint { get; set; }

        public Point3D PivotPoint1 { get; set; }

        public Point3D PivotPoint2 { get; set; }

        public static SuspensionAArmPoint Create<TProfile>(SuspensionAArm<TProfile> suspensionAArm)
            where TProfile : Profile
        {
            return new SuspensionAArmPoint
            {
                KnucklePoint = suspensionAArm.KnucklePoint,
                PivotPoint1 = suspensionAArm.PivotPoint1,
                PivotPoint2 = suspensionAArm.PivotPoint2
            };
        }
    }
}
