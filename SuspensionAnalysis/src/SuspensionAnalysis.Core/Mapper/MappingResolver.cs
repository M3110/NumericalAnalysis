using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using SuspensionAnalysis.DataContracts.RunAnalysis;

namespace SuspensionAnalysis.Core.Mapper
{
    /// <summary>
    /// It is responsible to map an object to another.
    /// </summary>
    public class MappingResolver : IMappingResolver
    {
        /// <summary>
        /// This method creates a <see cref="SuspensionSystem"/> based on <see cref="CalculateReactionsRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SuspensionSystem MapFrom(CalculateReactionsRequest request)
        {
            if (request == null)
            {
                return null;
            }

            return new SuspensionSystem
            {
                ShockAbsorber = ShockAbsorber.Create(request.ShockAbsorber),
                SuspensionAArmLower = SuspensionAArm.Create(request.SuspensionAArmLower),
                SuspensionAArmUpper = SuspensionAArm.Create(request.SuspensionAArmUpper),
                TieRod = TieRod.Create(request.TieRod)
            };
        }

        /// <summary>
        /// This method craetes a <see cref="SuspensionAArm{TProfile}"/> based on <see cref="RunAnalysisRequest{TProfile}"/> and <see cref="CalculateReactionsResponseData"/>.
        /// </summary>
        /// <typeparam name="TProfile"></typeparam>
        /// <param name="runAnalysisRequest"></param>
        /// <param name="calculateReactionsResponseData"></param>
        /// <returns></returns>
        public SuspensionSystem<TProfile> MapFrom<TProfile>(RunAnalysisRequest<TProfile> runAnalysisRequest, CalculateReactionsResponseData calculateReactionsResponseData)
            where TProfile : Profile
        {
            if (runAnalysisRequest == null)
            {
                return null;
            }

            return new SuspensionSystem<TProfile>
            {
                ShockAbsorber = ShockAbsorber.Create(runAnalysisRequest.ShockAbsorber, calculateReactionsResponseData.ShockAbsorberReaction.AbsolutValue),
                SuspensionAArmLower = SuspensionAArm<TProfile>.Create(runAnalysisRequest.SuspensionAArmLower, runAnalysisRequest.Material, calculateReactionsResponseData.AArmLowerReaction1.AbsolutValue, calculateReactionsResponseData.AArmLowerReaction2.AbsolutValue),
                SuspensionAArmUpper = SuspensionAArm<TProfile>.Create(runAnalysisRequest.SuspensionAArmUpper, runAnalysisRequest.Material, calculateReactionsResponseData.AArmUpperReaction1.AbsolutValue, calculateReactionsResponseData.AArmUpperReaction2.AbsolutValue),
                TieRod = TieRod<TProfile>.Create(runAnalysisRequest.TieRod, runAnalysisRequest.Material, calculateReactionsResponseData.TieRodReaction.AbsolutValue)
            };
        }
    }
}
