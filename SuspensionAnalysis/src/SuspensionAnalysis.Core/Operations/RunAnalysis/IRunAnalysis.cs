using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using SuspensionAnalysis.DataContracts.RunAnalysis;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis
{
    public interface IRunAnalysis<TProfile> : IOperationBase<RunAnalysisRequest<TProfile>, RunAnalysisResponse, RunAnalysisResponseData> 
        where TProfile : Profile
    { }
}
