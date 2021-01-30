﻿using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials;
using SuspensionAnalysis.Core.Mapper;
using SuspensionAnalysis.Core.Models;
using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.Core.Operations.CalculateReactions;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models.Analysis;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using SuspensionAnalysis.DataContracts.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.OperationBase;
using SuspensionAnalysis.DataContracts.RunAnalysis;
using System.Net;
using System.Threading.Tasks;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis
{
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
        public RunAnalysis(ICalculateReactions calculateReactions, IMechanicsOfMaterials<TProfile> mechanicsOfMaterials, IMappingResolver mappingResolver)
        {
            this._calculateReactions = calculateReactions;
            this._mechanicsOfMaterials = mechanicsOfMaterials;
            this._mappingResolver = mappingResolver;
        }

        protected override async Task<RunAnalysisResponse> ProcessOperation(RunAnalysisRequest<TProfile> request)
        {
            var response = new RunAnalysisResponse();

            CalculateReactionsResponse calculateReactionsResponse = await this._calculateReactions.Process(this.BuildCalculateReactionsRequest(request)).ConfigureAwait(false);
            if (calculateReactionsResponse.Success == false)
            {
                response.AddError(OperationErrorCode.InternalServerError, "Occurred error while calculating the reactions to suspension system.", HttpStatusCode.InternalServerError);
                response.AddErrors(calculateReactionsResponse.Errors, calculateReactionsResponse.HttpStatusCode);

                return response;
            }

            SuspensionSystem<TProfile> suspensionSystem = this._mappingResolver.MapFrom(request, calculateReactionsResponse.Data);

            SuspensionAnalysisInput<TProfile> input = new SuspensionAnalysisInput<TProfile>();

            response.Data = new RunAnalysisResponseData
            {
                ShockAbsorber = this._mechanicsOfMaterials.GenerateResult(input.ShockAbsorberInput),
                SuspensionAArmLowerResult = this._mechanicsOfMaterials.GenerateResult(input.SuspensionAArmLowerInput),
                SuspensionAArmUpperResult = this._mechanicsOfMaterials.GenerateResult(input.SuspensionAArmUpperInput),
                TieRod = this._mechanicsOfMaterials.GenerateResult(input.TieRodInput)
            };
            
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