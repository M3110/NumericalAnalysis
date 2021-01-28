using SuspensionAnalysis.DataContracts.OperationBase;
using SuspensionAnalysis.Infrastructure.Models;
using SuspensionAnalysis.Infrastructure.Models.SuspensionComponents;

namespace SuspensionAnalysis.DataContracts.CalculateReactions
{
    public class CalculateReactionsRequest : OperationRequestBase
    {
        public Point3D Origin { get; set; }

        public Point3D ForceApplied { get; set; }

        public ShockAbsorberPoint ShockAbsorber { get; set; }

        public SuspensionAArmPoint SuspensionAArmUpper { get; set; }
        
        public SuspensionAArmPoint SuspensionAArmLower { get; set; }

        public TieRodPoint TieRod { get; set; }
    }
}
