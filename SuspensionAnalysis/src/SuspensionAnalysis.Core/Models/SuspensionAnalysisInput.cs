using SuspensionAnalysis.DataContracts.Models.Analysis;
using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.Models
{
    /// <summary>
    /// It contains the necessary informations to execute the analysis to suspension system.
    /// </summary>
    public class SuspensionAnalysisInput<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// The analysis input to shock absorber.
        /// </summary>
        public AnalysisInput<TProfile> ShockAbsorberInput { get; set; }

        /// <summary>
        /// The analysis input to suspension A-arm upper.
        /// </summary>
        public AnalysisInput<TProfile> SuspensionAArmUpperInput { get; set; }

        /// <summary>
        /// The analysis input to suspension A-arm lower.
        /// </summary>
        public AnalysisInput<TProfile> SuspensionAArmLowerInput { get; set; }

        /// <summary>
        /// The analysis input to tie rod.
        /// </summary>
        public AnalysisInput<TProfile> TieRodInput { get; set; }
    }
}
