using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.DataContracts.OperationBase;
using System.Windows.Media.Media3D;

namespace SuspensionAnalysis.DataContracts.CalculateReactions
{
    public class CalculateReactionsRequest : OperationRequestBase
    {
        public Vector3D ForceApplied { get; set; }

        public Point3D Origin { get; set; }

        public ShockAbsorberPoint ShockAbsorber { get; set; }

        public SuspensionAArmPoint SuspensionAArmUpper { get; set; }
        
        public SuspensionAArmPoint SuspensionAArmLower { get; set; }

        public TieRodPoint TieRod { get; set; }
    }
}
