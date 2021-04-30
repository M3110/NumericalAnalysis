using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.DataContracts.OperationBase;

namespace SuspensionAnalysis.DataContracts.CalculateSteeringKnuckleReactions
{
    /// <summary>
    /// It represents the 'data' content of operation CalculateSteeringKnuckleReactions response.
    /// </summary>
    public class CalculateSteeringKnuckleReactionsResponseData : OperationResponseData
    {
        /// <summary>
        /// The reaction to suspension A-arm upper.
        /// This component has two reactions.
        /// Unit: N (Newton).
        /// </summary>
        public Force AArmUpperReaction { get; set; }

        /// <summary>
        /// The reaction to suspension A-arm lower.
        /// This component has two reactions.
        /// Unit: N (Newton).
        /// </summary>
        public Force AArmLowerReaction { get; set; }

        /// <summary>
        /// The reaction to tie rod.
        /// Unit: N (Newton).
        /// </summary>
        public Force TieRodReaction { get; set; }
    }
}
