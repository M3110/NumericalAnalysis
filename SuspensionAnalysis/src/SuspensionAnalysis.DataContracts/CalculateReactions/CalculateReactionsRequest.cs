using SuspensionAnalysis.DataContracts.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.OperationBase;

namespace SuspensionAnalysis.DataContracts.CalculateReactions
{
    /// <summary>
    /// It represents the request content to CalculateReactions operation.
    /// </summary>
    public class CalculateReactionsRequest : OperationRequestBase
    {
        /// <summary>
        /// The origin considered to analysis.
        /// </summary>
        /// <example>x,y,z</example>
        public string Origin { get; set; }

        /// <summary>
        /// The applied force.
        /// Unity: N (Newton).
        /// </summary>
        /// <example>x,y,z</example>
        public string ForceApplied { get; set; }

        /// <summary>
        /// The shock absorber points.
        /// </summary>
        public ShockAbsorberPoint ShockAbsorber { get; set; }

        /// <summary>
        /// The suspension A-arm upper points.
        /// </summary>
        public SuspensionAArmPoint SuspensionAArmUpper { get; set; }

        /// <summary>
        /// The suspension A-arm lower points.
        /// </summary>
        public SuspensionAArmPoint SuspensionAArmLower { get; set; }

        /// <summary>
        /// The tie rod points.
        /// </summary>
        public TieRodPoint TieRod { get; set; }
    }
}
