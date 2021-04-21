using SuspensionAnalysis.DataContracts.OperationBase;
using System.Threading.Tasks;

namespace SuspensionAnalysis.Core.Operations.Base
{
    /// <summary>
    /// It represents the base for all operations in the application.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TResponseData"></typeparam>
    public interface IOperationBase<TRequest, TResponse, TResponseData>
        where TRequest : OperationRequestBase
        where TResponse : OperationResponseBase<TResponseData>, new()
        where TResponseData : OperationResponseData, new()
    {
        /// <summary>
        /// The main method of all operations.
        /// Asynchronously, this method orchestrates the operations.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> ProcessAsync(TRequest request);
    }
}