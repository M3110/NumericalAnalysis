using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.DataContracts.Models.Analysis;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using SuspensionAnalysis.DataContracts.RunAnalysis;
using System.Threading.Tasks;
using CoreModels = SuspensionAnalysis.Core.Models.SuspensionComponents;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis.Static
{
    /// <summary>
    /// It is responsible to run the analysis to suspension system.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public interface IRunStaticAnalysis<TProfile> : IOperationBase<RunStaticAnalysisRequest<TProfile>, RunStaticAnalysisResponse, RunStaticAnalysisResponseData>
        where TProfile : Profile
    {
        /// <summary>
        /// This method builds <see cref="CalculateReactionsRequest"/> based on <see cref="RunStaticAnalysisRequest{TProfile}"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        CalculateReactionsRequest BuildCalculateReactionsRequest(RunStaticAnalysisRequest<TProfile> request);

        /// <summary>
        /// Asynchronously, this method generates the analysis result to shock absorber.
        /// </summary>
        /// <param name="shockAbsorberReaction"></param>
        /// <param name="shouldRoundResults"></param>
        /// <returns></returns>
        Task<Force> GenerateShockAbsorberResultAsync(Force shockAbsorberReaction, bool shouldRoundResults, int numberOfDecimalsToRound);

        /// <summary>
        /// Asynchronously, this method generates the analysis result to tie rod.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="shouldRound"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        Task<TieRodAnalysisResult> GenerateTieRodResultAsync(CoreModels.TieRod<TProfile> component, bool shouldRound, int decimals = 0);

        /// <summary>
        /// Asynchronously, this method generates the analysis result to suspension A-arm.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="shouldRound"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        Task<SuspensionAArmAnalysisResult> GenerateSuspensionAArmResultAsync(CoreModels.SuspensionAArm<TProfile> component, bool shouldRound, int decimals = 0);
    }
}
