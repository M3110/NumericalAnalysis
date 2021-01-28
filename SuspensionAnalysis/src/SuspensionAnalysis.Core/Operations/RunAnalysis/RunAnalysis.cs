using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials;
using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.Core.Operations.CalculateReactions;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.OperationBase;
using SuspensionAnalysis.DataContracts.RunAnalysis;
using SuspensionAnalysis.Infrastructure.Models.Profiles;
using SuspensionAnalysis.Infrastructure.Models.SuspensionComponents;
using System.Net;
using System.Threading.Tasks;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis
{
    public class RunAnalysis<TProfile> : OperationBase<RunAnalysisRequest<TProfile>, RunAnalysisResponse, RunAnalysisResponseData>, IRunAnalysis<TProfile>
        where TProfile : Profile
    {
        private readonly ICalculateReactions _calculateReactions;
        private readonly IMechanicsOfMaterials _mechanicsOfMaterials;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="calculateReactions"></param>
        public RunAnalysis(ICalculateReactions calculateReactions, IMechanicsOfMaterials mechanicsOfMaterials)
        {
            this._calculateReactions = calculateReactions;
            this._mechanicsOfMaterials = mechanicsOfMaterials;
        }

        protected override async Task<RunAnalysisResponse> ProcessOperation(RunAnalysisRequest<TProfile> request)
        {
            var response = new RunAnalysisResponse { Data = new RunAnalysisResponseData() };

            CalculateReactionsResponse calculateReactionsResponse = await this._calculateReactions.Process(this.BuildCalculateReactionsRequest(request)).ConfigureAwait(false);
            if (calculateReactionsResponse.Success == false)
            {
                response.AddError(OperationErrorCode.InternalServerError, "Occurred error while calculating the reactions to suspension system.", HttpStatusCode.InternalServerError);
                response.AddErrors(calculateReactionsResponse.Errors, calculateReactionsResponse.HttpStatusCode);

                return response;
            }

            
            
            return response;
        }

        public CalculateReactionsRequest BuildCalculateReactionsRequest(RunAnalysisRequest<TProfile> request)
        {
            return new CalculateReactionsRequest
            {
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
