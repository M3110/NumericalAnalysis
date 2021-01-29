using SuspensionAnalysis.DataContracts.OperationBase;
using SuspensionAnalysis.DataContracts.Models;

namespace SuspensionAnalysis.DataContracts.CalculateReactions
{
    /// <summary>
    /// It represents the 'data' content of operation CalculateReactions response.
    /// </summary>
    public class CalculateReactionsResponseData : OperationResponseData
    {
        /// <summary>
        /// The reaction to suspension A-arm upper.
        /// This component has two reactions.
        /// Unity: N (Newton).
        /// </summary>
        public Force AArmUpperReaction1 { get; set; }

        /// <summary>
        /// The reaction to suspension A-arm upper.
        /// This component has two reactions.
        /// Unity: N (Newton).
        /// </summary>
        public Force AArmUpperReaction2 { get; set; }

        /// <summary>
        /// The reaction to suspension A-arm lower.
        /// This component has two reactions.
        /// Unity: N (Newton).
        /// </summary>
        public Force AArmLowerReaction1 { get; set; }

        /// <summary>
        /// The reaction to suspension A-arm lower.
        /// This component has two reactions.
        /// Unity: N (Newton).
        /// </summary>
        public Force AArmLowerReaction2 { get; set; }

        /// <summary>
        /// The reaction to shock absorber.
        /// Unity: N (Newton).
        /// </summary>
        public Force ShockAbsorberReaction { get; set; }

        /// <summary>
        /// The reaction to tie rod.
        /// Unity: N (Newton).
        /// </summary>
        public Force TieRodReaction { get; set; }
    }
}
