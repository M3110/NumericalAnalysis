using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.DataContracts.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the suspension A-arm.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class SuspensionAArm<TProfile> : SuspensionAArmPoint
        where TProfile : Profile
    {
        /// <summary>
        /// The profile.
        /// </summary>
        public TProfile Profile { get; set; }
    }
}
