using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.DataContracts.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the point to suspension A-arm.
    /// </summary>
    public class SuspensionAArmPoint
    {
        /// <summary>
        /// The poitn of fastening with steering knuckle.
        /// </summary>
        public string KnucklePoint { get; set; }

        /// <summary>
        /// The pivot point.
        /// This geometry has two pivot point.
        /// </summary>
        public string PivotPoint1 { get; set; }

        /// <summary>
        /// The pivot point.
        /// This geometry has two pivot point.
        /// </summary>
        public string PivotPoint2 { get; set; }

        /// <summary>
        /// This method creates a <see cref="SuspensionAArmPoint"/> based on <see cref="SuspensionAArm{TProfile}"/>.
        /// </summary>
        /// <typeparam name="TProfile"></typeparam>
        /// <param name="suspensionAArm"></param>
        /// <returns></returns>
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
