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
        /// The reaction from suspension A-arm upper.
        /// Unit: N (Newton).
        /// </summary>
        public Force AArmUpperReaction { get; set; }

        /// <summary>
        /// The reaction from suspension A-arm lower.
        /// Unit: N (Newton).
        /// </summary>
        public Force AArmLowerReaction { get; set; }

        /// <summary>
        /// The reaction from tie rod.
        /// Unit: N (Newton).
        /// </summary>
        public Force TieRodReaction { get; set; }

        /// <summary>
        /// The reaction from brake caliper support.
        /// This component has two reactions.
        /// </summary>
        public Force BrakeCaliperSupportReaction1 { get; set; }

        /// <summary>
        /// The reaction from brake caliper support.
        /// This component has two reactions.
        /// </summary>
        public Force BrakeCaliperSupportReaction2 { get; set; }

        /// <summary>
        /// The reaction from bearing.
        /// </summary>
        public double BearingReaction { get; set; }
    }
}
