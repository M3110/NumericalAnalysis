using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models;

namespace SuspensionAnalysis.Core.Operations.CalculateReactions
{
    /// <summary>
    /// It is responsible to calculate the reactions to suspension system.
    /// </summary>
    public interface ICalculateReactions : IOperationBase<CalculateReactionsRequest, CalculateReactionsResponse, CalculateReactionsResponseData> 
    {
        /// <summary>
        /// This method builds the reactions vector.
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        double[] BuildEffortVector(Vector3D force);

        /// <summary>
        /// This method builds the matrix with normalized force directions and displacements.
        /// </summary>
        /// <param name="suspensionSystem"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        double[,] BuildDisplacementMatrix(SuspensionSystem suspensionSystem, Point3D origin);

        /// <summary>
        /// This method maps the analysis result to response data.
        /// </summary>
        /// <param name="suspensionSystem"></param>
        /// <param name="result"></param>
        /// <param name="shouldRound"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        CalculateReactionsResponseData MapToResponse(SuspensionSystem suspensionSystem, double[] result, bool shouldRound, int decimals);
    }
}
