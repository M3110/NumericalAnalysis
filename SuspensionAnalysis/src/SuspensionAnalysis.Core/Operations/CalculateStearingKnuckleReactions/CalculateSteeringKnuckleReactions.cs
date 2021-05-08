using SuspensionAnalysis.Core.ExtensionMethods;
using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.Core.Models.SuspensionComponents.SteeringKnuckle;
using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.Core.Operations.CalculateReactions;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.CalculateSteeringKnuckleReactions;
using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.DataContracts.Models.Enums;
using SuspensionAnalysis.DataContracts.OperationBase;
using System;
using System.Collections.Generic;
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

        public Force CalculateTieRodReactions(Force tieRodReaction, string steeringWheelForce, SuspensionPosition suspensionPosition)
        {
            if (suspensionPosition == SuspensionPosition.Rear)
                return tieRodReaction;

            return tieRodReaction.Sum(Force.Create(steeringWheelForce));
        }

        public double CalculateBearingReaction(CalculateSteeringKnuckleReactionsRequest request)
        {
            var bearing = Bearing.Create(request.BearingType);

            return (request.BearingTorque / bearing.EffectiveRadius) * bearing.RadialLoadFactor;
        }

        public (Force Reaction1, Force Reaction2) CalculateBrakeCaliperReactions(CalculateSteeringKnuckleReactionsRequest request)
        {
            var brakeCaliperSupport = BrakeCaliperSupportPoint.Create(request.SteeringKnuckle.BrakeCaliperSupportPoint);

            var inertialForce = Vector3D.Create(request.InertialForce);
            var inertialForceCoordinate = Point3D.Create(request.InertialForceCoordinate);

            var vector1 = Vector3D.Create(brakeCaliperSupport.Point2, brakeCaliperSupport.Point1);
            var vector2 = Vector3D.Create(inertialForceCoordinate, brakeCaliperSupport.Point1);

            double[] effort = new double[6]
            {
                inertialForce.X,
                inertialForce.Y,
                inertialForce.Z,
                -vector2.X * inertialForce.Y + vector2.Y * inertialForce.X,
                -vector2.Y * inertialForce.Z + vector2.Z * inertialForce.Y,
                -vector2.Z * inertialForce.X + vector2.X * inertialForce.Z
            };

            double[,] displacement = new double[6, 6]
            {
                { 1, 0, 0, 1, 0, 0 },
                { 0, 1, 0, 0, 1, 0 },
                { 0, 0, 1, 0, 0, 1 },
                { 0, 0, 0, -vector1.Y, vector1.X, 0 },
                { 0, 0, 0, 0, -vector1.Z, vector1.Y },
                { 0, 0, 0, vector1.Z, 0, -vector1.X }
            };

            double[] result = displacement
                .InverseMatrix()
                .Multiply(effort);

            return (new Force(result[0], result[1], result[2]), new Force(result[3], result[4], result[5]));
        }

        protected override async Task<CalculateSteeringKnuckleReactionsResponse> ProcessOperationAsync(CalculateSteeringKnuckleReactionsRequest request)
        {
            var response = new CalculateSteeringKnuckleReactionsResponse();

            CalculateReactionsResponseData suspensionSystemEfforts = null;
            if (request.CalculateReactionsResponseData != null)
            {
                suspensionSystemEfforts = request.CalculateReactionsResponseData;
            }
            else
            {
                var calculateReactionsResponse = await this._calculateReactions.ProcessAsync(request.CalculateReactionsRequest).ConfigureAwait(false);
                if (calculateReactionsResponse.Success == false)
                {
                    response.AddErrors(calculateReactionsResponse.Errors);
                    response.SetInternalServerError(OperationErrorCode.InternalServerError, "Occurred error while calculating reactions on suspension system.");
                    return response;
                }

                suspensionSystemEfforts = calculateReactionsResponse.Data;
            }

            // Step X - Calculates the reactions.
            response.Data.UpperWishboneReaction = suspensionSystemEfforts.UpperWishboneReaction1.Sum(suspensionSystemEfforts.UpperWishboneReaction2);
            response.Data.LowerWishboneReaction = suspensionSystemEfforts.LowerWishboneReaction1.Sum(suspensionSystemEfforts.LowerWishboneReaction2);
            response.Data.TieRodReaction = this.CalculateTieRodReactions(suspensionSystemEfforts.TieRodReaction, request.SteeringWheelForce, request.SuspensionPosition);
            response.Data.BearingReaction = this.CalculateBearingReaction(request);
            (response.Data.BrakeCaliperSupportReaction1, response.Data.BrakeCaliperSupportReaction2) = this.CalculateBrakeCaliperReactions(request);

            return response;
        }
    }
}
