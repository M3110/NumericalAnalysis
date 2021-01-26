using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.DataContracts.CalculateReactions;

namespace SuspensionAnalysis.Core.Operations.CalculateReactions
{
    public interface ICalculateReactions : IOperationBase<CalculateReactionsRequest, CalculateReactionsResponse, CalculateReactionsResponseData>
    {
    }
}
