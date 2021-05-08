using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the suspension system.
    /// </summary>
    public class SuspensionSystem
    {
        /// <summary>
        /// The shock absorber.
        /// </summary>
        public ShockAbsorber ShockAbsorber { get; set; }

        /// <summary>
        /// The suspension wishbone upper.
        /// </summary>
        public SuspensionWishbone UpperWishbone { get; set; }

        /// <summary>
        /// The suspension wishbone lower.
        /// </summary>
        public SuspensionWishbone LowerWishbone { get; set; }

        /// <summary>
        /// The tie rod.
        /// </summary>
        public TieRod TieRod { get; set; }
    }

    /// <summary>
    /// It represents the suspension system.
    /// </summary>
    public class SuspensionSystem<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// The shock absorber.
        /// </summary>
        public ShockAbsorber ShockAbsorber { get; set; }

        /// <summary>
        /// The suspension wishbone upper.
        /// </summary>
        public SuspensionWishbone<TProfile> UpperWishbone { get; set; }

        /// <summary>
        /// The suspension wishbone lower.
        /// </summary>
        public SuspensionWishbone<TProfile> LowerWishbone { get; set; }

        /// <summary>
        /// The tie rod.
        /// </summary>
        public TieRod<TProfile> TieRod { get; set; }
    }
}
