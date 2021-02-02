using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials;
using SuspensionAnalysis.Core.ExtensionMethods;
using SuspensionAnalysis.Core.Mapper;
using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.Core.Operations.CalculateReactions;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using SuspensionAnalysis.DataContracts.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.OperationBase;
using SuspensionAnalysis.DataContracts.RunAnalysis;
using System.Net;
using System.Threading.Tasks;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis
{
    /// <summary>
    /// It is responsible to run the analysis to suspension system.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class RunAnalysis<TProfile> : OperationBase<RunAnalysisRequest<TProfile>, RunAnalysisResponse, RunAnalysisResponseData>, IRunAnalysis<TProfile>
        where TProfile : Profile
    {
        private readonly ICalculateReactions _calculateReactions;
        private readonly IMechanicsOfMaterials<TProfile> _mechanicsOfMaterials;
        private readonly IMappingResolver _mappingResolver;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="calculateReactions"></param>
        /// <param name="mechanicsOfMaterials"></param>
        /// <param name="mappingResolver"></param>
        public RunAnalysis(ICalculateReactions calculateReactions, IMechanicsOfMaterials<TProfile> mechanicsOfMaterials, IMappingResolver mappingResolver)
        {
            this._calculateReactions = calculateReactions;
            this._mechanicsOfMaterials = mechanicsOfMaterials;
            this._mappingResolver = mappingResolver;
        }

        /// <summary>
        /// This method runs the analysis to suspension system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override async Task<RunAnalysisResponse> ProcessOperation(RunAnalysisRequest<TProfile> request)
        {
            var response = new RunAnalysisResponse();
            response.SetSuccessOk();

            // Step 1 - Calculates the reactions at suspension system.
            CalculateReactionsResponse calculateReactionsResponse = await this._calculateReactions.Process(this.BuildCalculateReactionsRequest(request)).ConfigureAwait(false);
            if (calculateReactionsResponse.Success == false)
            {
                response.AddError(OperationErrorCode.InternalServerError, "Occurred error while calculating the reactions to suspension system.", HttpStatusCode.InternalServerError);
                response.AddErrors(calculateReactionsResponse.Errors, calculateReactionsResponse.HttpStatusCode);

                return response;
            }

            // Step 2 - Builds the suspension system based on CalculateReaction operation response and request.
            // Here is built the main information to be used at analysis.
            SuspensionSystem<TProfile> suspensionSystem = this._mappingResolver.MapFrom(request, calculateReactionsResponse.Data);
            
            // Step 3 - Generate the result and maps the response.
            response.Data = new RunAnalysisResponseData
            {
                ShockAbsorber = request.ShouldRoundResults == true ? calculateReactionsResponse.Data.ShockAbsorberReaction.Round(request.NumberOfDecimalsToRound.GetValueOrDefault()) : calculateReactionsResponse.Data.ShockAbsorberReaction,
                SuspensionAArmLowerResult = this._mechanicsOfMaterials.GenerateResult(suspensionSystem.SuspensionAArmLower, request.ShouldRoundResults, request.NumberOfDecimalsToRound.GetValueOrDefault()),
                SuspensionAArmUpperResult = this._mechanicsOfMaterials.GenerateResult(suspensionSystem.SuspensionAArmUpper, request.ShouldRoundResults, request.NumberOfDecimalsToRound.GetValueOrDefault()),
                TieRod = this._mechanicsOfMaterials.GenerateResult(suspensionSystem.TieRod, request.ShouldRoundResults, request.NumberOfDecimalsToRound.GetValueOrDefault())
            };

            return response;
        }

        /// <summary>
        /// This method builds <see cref="CalculateReactionsRequest"/> based on <see cref="RunAnalysisRequest{TProfile}"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CalculateReactionsRequest BuildCalculateReactionsRequest(RunAnalysisRequest<TProfile> request)
        {
            return new CalculateReactionsRequest
            {
                ShouldRoundResults = false,
                ForceApplied = request.ForceApplied,
                Origin = request.Origin,
                ShockAbsorber = ShockAbsorberPoint.Create(request.ShockAbsorber),
                SuspensionAArmLower = SuspensionAArmPoint.Create(request.SuspensionAArmLower),
                SuspensionAArmUpper = SuspensionAArmPoint.Create(request.SuspensionAArmUpper),
                TieRod = TieRodPoint.Create(request.TieRod)
            };
        }
    }
}
