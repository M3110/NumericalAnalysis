using SuspensionAnalysis.DataContracts.Models.Enums;
using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.DataContracts.Models.Analysis
{
    /// <summary>
    /// It contains the necessary informations to execute the analysis.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class AnalysisInput<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// The material used.
        /// </summary>
        public Material Material { get; set; }

        /// <summary>
        /// The fastening type.
        /// </summary>
        public FasteningType FasteningType { get; set; }

        /// <summary>
        /// The profile.
        /// </summary>
        public TProfile Profile { get; set; }

        /// <summary>
        /// The length.
        /// Unity: m (meter).
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// The applied force.
        /// Unity: N (Newton).
        /// </summary>
        public double AppliedForce { get; set; }
    }
}
