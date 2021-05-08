using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials;
using SuspensionAnalysis.Core.ExtensionMethods;
using SuspensionAnalysis.Core.GeometricProperties;
using SuspensionAnalysis.Core.Mapper;
using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.Core.Operations.CalculateReactions;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.DataContracts.Models.Analysis;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using SuspensionAnalysis.DataContracts.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.OperationBase;
using SuspensionAnalysis.DataContracts.RunAnalysis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreModels = SuspensionAnalysis.Core.Models.SuspensionComponents;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis
{
    /// <summary>
    /// It is responsible to run the analysis to suspension system.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class RunAnalysis<TProfile> : OperationBase<RunAnalysisRequest<TProfile>, RunAnalysisResponse, RunAnalysisResponseData>, IRunAnalysis<TProfile>
        where TProfile : Profile
    {
        protected readonly ICalculateReactions _calculateReactions;
        protected readonly IMechanicsOfMaterials<TProfile> _mechanicsOfMaterials;
        protected readonly IGeometricProperty<TProfile> _geometricProperty;
        protected readonly IMappingResolver _mappingResolver;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="calculateReactions"></param>
        /// <param name="mechanicsOfMaterials"></param>
        /// <param name="geometricProperty"></param>
        /// <param name="mappingResolver"></param>
        public RunAnalysis(
            ICalculateReactions calculateReactions,
            IMechanicsOfMaterials<TProfile> mechanicsOfMaterials,
            IGeometricProperty<TProfile> geometricProperty,
            IMappingResolver mappingResolver)
        {
            this._calculateReactions = calculateReactions;
            this._mechanicsOfMaterials = mechanicsOfMaterials;
            this._geometricProperty = geometricProperty;
            this._mappingResolver = mappingResolver;
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
                AppliedForce = request.ForceApplied,
                Origin = request.Origin,
                ShockAbsorber = ShockAbsorberPoint.Create(request.ShockAbsorber),
                LowerWishbone = SuspensionWishbonePoint.Create(request.LowerWishbone),
                UpperWishbone = SuspensionWishbonePoint.Create(request.UpperWishbone),
                TieRod = TieRodPoint.Create(request.TieRod)
            };
        }

        /// <summary>
        /// Asynchronously, this method generates the analysis result to shock absorber.
        /// </summary>
        /// <param name="shockAbsorberReaction"></param>
        /// <param name="shouldRoundResults"></param>
        /// <returns></returns>
        public virtual Task<Force> GenerateShockAbsorberResultAsync(Force shockAbsorberReaction, bool shouldRoundResults, int numberOfDecimalsToRound)
        {
            if (shouldRoundResults)
                return Task.FromResult(shockAbsorberReaction.Round(numberOfDecimalsToRound));

            return Task.FromResult(shockAbsorberReaction);
        }

        /// <summary>
        /// Asynchronously, this method generates the analysis result to tie rod.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="shouldRound"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public virtual Task<TieRodAnalysisResult> GenerateTieRodResultAsync(CoreModels.TieRod<TProfile> component, bool shouldRound, int decimals = 0)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "The object tie rod cannot be null to calculate the results.");

            // Step 1 - Calculates the geometric properties.
            double area = this._geometricProperty.CalculateArea(component.Profile);
            double momentOfInertia = this._geometricProperty.CalculateMomentOfInertia(component.Profile);

            // Step 2 - Calculates the equivalent stress.
            // For that case, just is considered the normal stress because the applied force is at same axis of geometry.
            double equivalentStress = this._mechanicsOfMaterials.CalculateNormalStress(component.AppliedForce, area);

            // Step 3 - Builds the analysis result.
            var result = new TieRodAnalysisResult()
            {
                AppliedForce = component.AppliedForce,
                CriticalBucklingForce = this._mechanicsOfMaterials.CalculateCriticalBucklingForce(component.Material.YoungModulus, momentOfInertia, component.Length),
                EquivalentStress = equivalentStress / 1e6, // It is necessary to convert Pa to MPa.
                StressSafetyFactor = Math.Abs(component.Material.YieldStrength / equivalentStress)
            };

            if (shouldRound == false)
                return Task.FromResult(result);

            return Task.FromResult(new TieRodAnalysisResult
            {
                AppliedForce = Math.Round(result.AppliedForce, decimals),
                CriticalBucklingForce = Math.Round(result.CriticalBucklingForce, decimals),
                EquivalentStress = Math.Round(result.EquivalentStress, decimals),
                StressSafetyFactor = Math.Round(result.StressSafetyFactor, decimals)
            });
        }

        /// <summary>
        /// Asynchronously, this method generates the analysis result to suspension wishbone.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="shouldRound"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public virtual Task<SuspensionWishboneAnalysisResult> GenerateSuspensionAArmResultAsync(CoreModels.SuspensionWishbone<TProfile> component, bool shouldRound, int decimals = 0)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "The object suspension wishbone cannot be null to calculate the results.");

            // Step 1 - Calculates the geometric properties.
            double area = this._geometricProperty.CalculateArea(component.Profile);
            double momentOfInertia = this._geometricProperty.CalculateMomentOfInertia(component.Profile);

            // Step 2 - Calculates the equivalent stress.
            // For that case, just is considered the normal stress because the applied force is at same axis of geometry.
            double equivalentStress = Math.Max(
                this._mechanicsOfMaterials.CalculateNormalStress(component.AppliedForce1, area),
                this._mechanicsOfMaterials.CalculateNormalStress(component.AppliedForce2, area));

            // Step 3 - Builds the analysis result.
            var result = new SuspensionWishboneAnalysisResult()
            {
                AppliedForce1 = component.AppliedForce1,
                AppliedForce2 = component.AppliedForce2,
                CriticalBucklingForce1 = this._mechanicsOfMaterials.CalculateCriticalBucklingForce(component.Material.YoungModulus, momentOfInertia, component.Length1),
                CriticalBucklingForce2 = this._mechanicsOfMaterials.CalculateCriticalBucklingForce(component.Material.YoungModulus, momentOfInertia, component.Length2),
                EquivalentStress = equivalentStress / 1e6, // It is necessary to convert Pa to MPa.
                StressSafetyFactor = Math.Abs(component.Material.YieldStrength / equivalentStress)
            };

            if (shouldRound == false)
                return Task.FromResult(result);

            return Task.FromResult(new SuspensionWishboneAnalysisResult
            {
                AppliedForce1 = Math.Round(result.AppliedForce1, decimals),
                AppliedForce2 = Math.Round(result.AppliedForce2, decimals),
                CriticalBucklingForce1 = Math.Round(result.CriticalBucklingForce1, decimals),
                CriticalBucklingForce2 = Math.Round(result.CriticalBucklingForce2, decimals),
                EquivalentStress = Math.Round(result.EquivalentStress, decimals),
                StressSafetyFactor = Math.Round(result.StressSafetyFactor, decimals)
            });
        }

        /// <summary>
        /// Asynchronously, this method runs the analysis to suspension system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override async Task<RunAnalysisResponse> ProcessOperationAsync(RunAnalysisRequest<TProfile> request)
        {
            var response = new RunAnalysisResponse();
            response.SetSuccessOk();

            // Step 1 - Calculates the reactions at suspension system.
            CalculateReactionsResponse calculateReactionsResponse = await this._calculateReactions.ProcessAsync(this.BuildCalculateReactionsRequest(request)).ConfigureAwait(false);
            if (calculateReactionsResponse.Success == false)
            {
                response.SetInternalServerError(OperationErrorCode.InternalServerError, "Occurred error while calculating the reactions to suspension system.");
                response.AddErrors(calculateReactionsResponse.Errors, calculateReactionsResponse.HttpStatusCode);

                return response;
            }

            // Step 2 - Builds the suspension system based on CalculateReaction operation response and request.
            // Here is built the main information to be used at analysis.
            SuspensionSystem<TProfile> suspensionSystem = this._mappingResolver.MapFrom(request, calculateReactionsResponse.Data);

            // Step 3 - Generate the result and maps the response.
            response.Data = new RunAnalysisResponseData();

            var tasks = new List<Task>();

            tasks.Add(Task.Run(async () =>
            {
                response.Data.ShockAbsorber = await this.GenerateShockAbsorberResultAsync(calculateReactionsResponse.Data.ShockAbsorberReaction, request.ShouldRoundResults, request.NumberOfDecimalsToRound.GetValueOrDefault()).ConfigureAwait(false);
            }));

            tasks.Add(Task.Run(async () =>
            {
                response.Data.LowerWishboneResult = await this.GenerateSuspensionAArmResultAsync(suspensionSystem.LowerWishbone, request.ShouldRoundResults, request.NumberOfDecimalsToRound.GetValueOrDefault()).ConfigureAwait(false);
            }));

            tasks.Add(Task.Run(async () =>
            {
                response.Data.UpperWishboneResult = await this.GenerateSuspensionAArmResultAsync(suspensionSystem.UpperWishbone, request.ShouldRoundResults, request.NumberOfDecimalsToRound.GetValueOrDefault()).ConfigureAwait(false);
            }));

            tasks.Add(Task.Run(async () =>
            {
                response.Data.TieRod = await this.GenerateTieRodResultAsync(suspensionSystem.TieRod, request.ShouldRoundResults, request.NumberOfDecimalsToRound.GetValueOrDefault());
            }));

            await Task.WhenAll(tasks);

            return response;
        }
    }
}
