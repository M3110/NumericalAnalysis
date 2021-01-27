using SuspensionAnalysis.DataContracts.OperationBase;
using SuspensionAnalysis.Infraestructure.Models;

namespace SuspensionAnalysis.DataContracts.CalculateReactions
{
    public class CalculateReactionsResponseData : OperationResponseData
    {
        public Force AArmUpperReaction1 { get; set; }

        public Force AArmUpperReaction2 { get; set; }

        public Force AArmLowerReaction1 { get; set; }

        public Force AArmLowerReaction2 { get; set; }

        public Force ShockAbsorberReaction { get; set; }

        public Force TieRodReaction { get; set; }
    }
}
