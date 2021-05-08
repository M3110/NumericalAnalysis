using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.DataContracts.CalculateSteeringKnuckleReactions;
using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.DataContracts.Models.Enums;

namespace SuspensionAnalysis.Core.Operations.CalculateStearingKnuckleReactions
{
    public interface ICalculateSteeringKnuckleReactions : IOperationBase<CalculateSteeringKnuckleReactionsRequest, CalculateSteeringKnuckleReactionsResponse, CalculateSteeringKnuckleReactionsResponseData>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tieRodReaction"></param>
        /// <param name="steeringWheelForce"></param>
        /// <param name="suspensionPosition"></param>
        /// <returns></returns>
        public Force CalculateTieRodReactions(Force tieRodReaction, string steeringWheelForce, SuspensionPosition suspensionPosition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public double CalculateBearingReaction(CalculateSteeringKnuckleReactionsRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public (Force Reaction1, Force Reaction2) CalculateBrakeCaliperReactions(CalculateSteeringKnuckleReactionsRequest request);
    }
}
