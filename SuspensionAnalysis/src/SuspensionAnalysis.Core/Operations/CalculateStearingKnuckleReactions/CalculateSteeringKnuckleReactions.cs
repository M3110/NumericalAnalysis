using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.Core.Operations.CalculateReactions;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.CalculateSteeringKnuckleReactions;
using SuspensionAnalysis.DataContracts.OperationBase;
using System.Threading.Tasks;

namespace SuspensionAnalysis.Core.Operations.CalculateStearingKnuckleReactions
{
    /// <summary>
    /// It is responsible to calculate the reactions to steering knuckle. 
    /// </summary>
    public class CalculateSteeringKnuckleReactions : OperationBase<CalculateSteeringKnuckleReactionsRequest, CalculateSteeringKnuckleReactionsResponse, CalculateSteeringKnuckleReactionsResponseData>, ICalculateSteeringKnuckleReactions
    {
        private readonly ICalculateReactions _calculateReactions;

        public CalculateSteeringKnuckleReactions(ICalculateReactions calculateReactions)
        {
            this._calculateReactions = calculateReactions;
        }

        protected override async Task<CalculateSteeringKnuckleReactionsResponse> ProcessOperationAsync(CalculateSteeringKnuckleReactionsRequest request)
        {
            var response = new CalculateSteeringKnuckleReactionsResponse();

            CalculateReactionsResponseData suspensionSystemEfforts = null;

            if (request.CalculateReactionsResponseData != null)
            {
                suspensionSystemEfforts = request.CalculateReactionsResponseData;

                return response;
            }
            else
            {
                var calculateReactionsResponse = await this._calculateReactions.ProcessAsync(request.CalculateReactionsRequest).ConfigureAwait(false);
                if (calculateReactionsResponse.Success == false)
                {
                    response.SetInternalServerError(OperationErrorCode.InternalServerError, "Occurred error while calculating reactions on suspension system.");

                    return response;
                }
                else
                {
                    response.SetSuccessOk();

                    return response;
                }
            }
        }

        public override async Task<CalculateSteeringKnuckleReactionsResponse> ValidateOperationAsync(CalculateSteeringKnuckleReactionsRequest request)
        {
            CalculateSteeringKnuckleReactionsResponse response = await base.ValidateOperationAsync(request).ConfigureAwait(false);
            if (response.Success == false)
            {
                return response;
            }

            if (request.CalculateReactionsRequest == null && request.CalculateReactionsResponseData == null)
            {
                response.SetBadRequestError(OperationErrorCode.RequestValidationError, "The forces applied to the steering knukle or the suspension points must be passed on request");
            }

            return response;
        }
    }
}
