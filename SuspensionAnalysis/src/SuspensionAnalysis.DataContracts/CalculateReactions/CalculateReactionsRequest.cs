using SuspensionAnalysis.DataContracts.OperationBase;
using SuspensionAnalysis.Infraestructure.Models;
using SuspensionAnalysis.Infraestructure.Models.SuspensionComponents;

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
