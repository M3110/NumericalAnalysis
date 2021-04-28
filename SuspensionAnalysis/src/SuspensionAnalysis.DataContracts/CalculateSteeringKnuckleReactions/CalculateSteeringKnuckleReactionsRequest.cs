using SuspensionAnalysis.DataContracts.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.OperationBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuspensionAnalysis.DataContracts.CalculateSteeringKnuckleReactions
{
    /// <summary>
    /// It represents the request content to  CalculateSteeringKnuckleReactions operation.
    /// </summary>
    public class CalculateSteeringKnuckleReactionsRequest : OperationRequestBase
    {
        /// <summary>
        /// True, if result should be rounded. False, otherwise.
        /// </summary>
        public bool ShouldRoundResults { get; set; }

        /// <summary>
        /// The number of decimals that should be rounded in results.
        /// </summary>
        public int? NumberOfDecimalsToRound { get; set; }

        /// <summary>
        /// The origin considered to analysis.
        /// </summary>
        /// <example>x,y,z</example>
        public string Origin { get; set; }

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

        /// <summary>
        /// The steering knuckle points. 
        /// </summary>
        public SteeringKnuckle SteeringKnucklePoint { get; set; }
    }
}
