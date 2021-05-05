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
        /// The reaction to suspension upper wishbone. 
        /// This component has two reactions.
        /// Unit: N (Newton).
        /// </summary>
        public Force UpperWishboneReaction1 { get; set; }

        /// <summary>
        /// The reaction to suspension upper wishbone.
        /// This component has two reactions.
        /// Unit: N (Newton).
        /// </summary>
        public Force UpperWishboneReaction2 { get; set; }

        /// <summary>
        /// The reaction to suspension lower wishbone.
        /// This component has two reactions.
        /// Unit: N (Newton).
        /// </summary>
        public Force LowerWishboneReaction1 { get; set; }

        /// <summary>
        /// The reaction to suspension lower wishbone.
        /// This component has two reactions.
        /// Unit: N (Newton).
        /// </summary>
        public Force LowerWishboneReaction2 { get; set; }

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
                LowerWishboneReaction1 = this.LowerWishboneReaction1.Round(decimals),
                LowerWishboneReaction2 = this.LowerWishboneReaction2.Round(decimals),
                UpperWishboneReaction1 = this.UpperWishboneReaction1.Round(decimals),
                UpperWishboneReaction2 = this.UpperWishboneReaction2.Round(decimals),
                ShockAbsorberReaction = this.ShockAbsorberReaction.Round(decimals),
                TieRodReaction = this.TieRodReaction.Round(decimals)
            };
        }
    }
}
