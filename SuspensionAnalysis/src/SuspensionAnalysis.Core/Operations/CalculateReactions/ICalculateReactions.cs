using SuspensionAnalysis.Core.Models;
using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.Operations.CalculateReactions
{
    public interface ICalculateReactions : IOperationBase<CalculateReactionsRequest, CalculateReactionsResponse, CalculateReactionsResponseData> 
    {
        double[] BuildReactionVector(Vector3D force);

        double[,] BuildDisplacementMatrix(SuspensionSystem<Profile> suspensionSystem, Point3D origin);

        CalculateReactionsResponseData MapToResponse(double[] result);
    }
}
