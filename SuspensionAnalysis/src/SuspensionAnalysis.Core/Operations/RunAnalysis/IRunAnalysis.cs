using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using SuspensionAnalysis.DataContracts.RunAnalysis;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis
{
    /// <summary>
    /// It is responsible to run the analysis to suspension system.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public interface IRunAnalysis<TProfile> : IOperationBase<RunAnalysisRequest<TProfile>, RunAnalysisResponse, RunAnalysisResponseData> 
        where TProfile : Profile
    {
        /// <summary>
        /// This method builds <see cref="CalculateReactionsRequest"/> based on <see cref="RunAnalysisRequest{TProfile}"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        CalculateReactionsRequest BuildCalculateReactionsRequest(RunAnalysisRequest<TProfile> request);
    }
}
