using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.DataContracts.OperationBase;

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
        /// Unit: N (Newton).
        /// </summary>
        public Force AArmUpperReaction1 { get; set; }

        /// <summary>
        /// The reaction to suspension A-arm upper.
        /// This component has two reactions.
        /// Unit: N (Newton).
        /// </summary>
        public Force AArmUpperReaction2 { get; set; }

        /// <summary>
        /// The reaction to suspension A-arm lower.
        /// This component has two reactions.
        /// Unit: N (Newton).
        /// </summary>
        public Force AArmLowerReaction1 { get; set; }

        /// <summary>
        /// The reaction to suspension A-arm lower.
        /// This component has two reactions.
        /// Unit: N (Newton).
        /// </summary>
        public Force AArmLowerReaction2 { get; set; }

        /// <summary>
        /// The reaction to shock absorber.
        /// Unit: N (Newton).
        /// </summary>
        public Force ShockAbsorberReaction { get; set; }

        /// <summary>
        /// The reaction to tie rod.
        /// Unit: N (Newton).
        /// </summary>
        public Force TieRodReaction { get; set; }

        /// <summary>
        /// This method rounds each value for each <see cref="Force"/> at <see cref="CalculateReactionsResponseData"/> 
        /// to a specified number of fractional digits, and rounds midpoint values to the nearest even number.
        /// </summary>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public CalculateReactionsResponseData Round(int decimals)
        {
            return new CalculateReactionsResponseData
            {
                AArmLowerReaction1 = this.AArmLowerReaction1.Round(decimals),
                AArmLowerReaction2 = this.AArmLowerReaction2.Round(decimals),
                AArmUpperReaction1 = this.AArmUpperReaction1.Round(decimals),
                AArmUpperReaction2 = this.AArmUpperReaction2.Round(decimals),
                ShockAbsorberReaction = this.ShockAbsorberReaction.Round(decimals),
                TieRodReaction = this.TieRodReaction.Round(decimals)
            };
        }
    }
}
