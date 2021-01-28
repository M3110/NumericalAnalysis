using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.DataContracts.RunAnalysis;
using SuspensionAnalysis.Infrastructure.Models.Profiles;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis
{
    public interface IRunAnalysis<TProfile> : IOperationBase<RunAnalysisRequest<TProfile>, RunAnalysisResponse, RunAnalysisResponseData> 
        where TProfile : Profile
    { }
}
