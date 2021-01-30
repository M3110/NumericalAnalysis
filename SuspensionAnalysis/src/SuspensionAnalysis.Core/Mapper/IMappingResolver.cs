using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using SuspensionAnalysis.DataContracts.RunAnalysis;

namespace SuspensionAnalysis.Core.Mapper
{
    /// <summary>
    /// It is responsible to map an object to another.
    /// </summary>
    public interface IMappingResolver
    {
        /// <summary>
        /// This method creates a <see cref="SuspensionSystem"/> based on <see cref="CalculateReactionsRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SuspensionSystem MapFrom(CalculateReactionsRequest request);

        /// <summary>
        /// This method craetes a <see cref="SuspensionAArm{TProfile}"/> based on <see cref="RunAnalysisRequest{TProfile}"/> and <see cref="CalculateReactionsResponseData"/>.
        /// </summary>
        /// <typeparam name="TProfile"></typeparam>
        /// <param name="runAnalysisRequest"></param>
        /// <param name="calculateReactionsResponseData"></param>
        /// <returns></returns>
        SuspensionSystem<TProfile> MapFrom<TProfile>(RunAnalysisRequest<TProfile> runAnalysisRequest, CalculateReactionsResponseData calculateReactionsResponseData)
            where TProfile : Profile;
    }
}