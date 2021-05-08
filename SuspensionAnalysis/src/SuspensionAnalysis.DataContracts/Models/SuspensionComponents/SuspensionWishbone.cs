using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.DataContracts.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the suspension wishbone.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class SuspensionWishbone<TProfile> : SuspensionWishbonePoint
        where TProfile : Profile
    {
        /// <summary>
        /// The profile.
        /// </summary>
        public TProfile Profile { get; set; }
    }
}
