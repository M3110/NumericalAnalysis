using SuspensionAnalysis.Core.GeometricProperties;
using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.Models.Analysis;
using SuspensionAnalysis.DataContracts.Models.Enums;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using System;

namespace SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials
{
    /// <summary>
    /// It contains the Mechanics of Materials constitutive equations.
    /// </summary>
    public abstract class MechanicsOfMaterials<TProfile> : IMechanicsOfMaterials<TProfile>
        where TProfile : Profile
    {
        protected readonly IGeometricProperty<TProfile> _geometricProperty;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="geometricProperty"></param>
        public MechanicsOfMaterials(IGeometricProperty<TProfile> geometricProperty)
        {
            this._geometricProperty = geometricProperty ?? throw new ArgumentNullException(nameof(geometricProperty), $"'{nameof(geometricProperty)}' cannot be null in '{this.GetType().Name}' operation.");
        }

        /// <summary>
        /// This method generates the analysis result to tie rod.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="shouldRound"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public virtual TieRodAnalysisResult GenerateResult(TieRod<TProfile> component, bool shouldRound, int decimals = 0)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "The object tie rod cannot be null to calculate the results.");

            // Step 1 - Calculates the geometric properties.
            double area = this._geometricProperty.CalculateArea(component.Profile);
            double momentOfInertia = this._geometricProperty.CalculateMomentOfInertia(component.Profile);

            // Step 2 - Calculates the equivalent stress.
            // For that case, just is considered the normal stress because the applied force is at same axis of geometry.
            double equivalentStress = this.CalculateNormalStress(component.AppliedForce, area);

            // Step 3 - Builds the analysis result.
            var result = new TieRodAnalysisResult()
            {
                AppliedForce = component.AppliedForce,
                CriticalBucklingForce = this.CalculateCriticalBucklingForce(component.Material.YoungModulus, momentOfInertia, component.Length),
                EquivalentStress = equivalentStress / 1e6, // It is necessary to convert Pa to MPa.
                StressSafetyFactor = Math.Abs(component.Material.YieldStrength / equivalentStress)
            };

            if (shouldRound == false)
                return result;

            return new TieRodAnalysisResult
            {
                AppliedForce = Math.Round(result.AppliedForce, decimals),
                CriticalBucklingForce = Math.Round(result.CriticalBucklingForce, decimals),
                EquivalentStress = Math.Round(result.EquivalentStress, decimals),
                StressSafetyFactor = Math.Round(result.StressSafetyFactor, decimals)
            };
        }

        /// <summary>
        /// This method generates the analysis result to suspension A-arm.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="shouldRound"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public virtual SuspensionAArmAnalysisResult GenerateResult(SuspensionAArm<TProfile> component, bool shouldRound, int decimals = 0)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component), "The object suspension A-arm cannot be null to calculate the results.");

            // Step 1 - Calculates the geometric properties.
            double area = this._geometricProperty.CalculateArea(component.Profile);
            double momentOfInertia = this._geometricProperty.CalculateMomentOfInertia(component.Profile);

            // Step 2 - Calculates the equivalent stress.
            // For that case, just is considered the normal stress because the applied force is at same axis of geometry.
            double equivalentStress = Math.Max(
                this.CalculateNormalStress(component.AppliedForce1, area), 
                this.CalculateNormalStress(component.AppliedForce2, area));

            // Step 3 - Builds the analysis result.
            var result = new SuspensionAArmAnalysisResult()
            {
                AppliedForce1 = component.AppliedForce1,
                AppliedForce2 = component.AppliedForce2,
                CriticalBucklingForce1 = this.CalculateCriticalBucklingForce(component.Material.YoungModulus, momentOfInertia, component.Length1),
                CriticalBucklingForce2 = this.CalculateCriticalBucklingForce(component.Material.YoungModulus, momentOfInertia, component.Length2),
                EquivalentStress = equivalentStress / 1e6, // It is necessary to convert Pa to MPa.
                StressSafetyFactor = Math.Abs(component.Material.YieldStrength / equivalentStress)
            };

            if (shouldRound == false)
                return result;

            return new SuspensionAArmAnalysisResult
            {
                AppliedForce1 = Math.Round(result.AppliedForce1, decimals),
                AppliedForce2 = Math.Round(result.AppliedForce2, decimals),
                CriticalBucklingForce1 = Math.Round(result.CriticalBucklingForce1, decimals),
                CriticalBucklingForce2 = Math.Round(result.CriticalBucklingForce2, decimals),
                EquivalentStress = Math.Round(result.EquivalentStress, decimals),
                StressSafetyFactor = Math.Round(result.StressSafetyFactor, decimals)
            };
        }

        /// <summary>
        /// This method calcultes the equivalent stress using Von-Misses method.
        /// </summary>
        /// <param name="normalStress"></param>
        /// <param name="flexuralStress"></param>
        /// <param name="shearStress"></param>
        /// <param name="torsionalStress"></param>
        /// <returns>The equivalent stress. Unit: Pa (Pascal).</returns>
        public double CalculateEquivalentStress(double normalStress = 0, double flexuralStress = 0, double shearStress = 0, double torsionalStress = 0)
        {
            return Math.Sqrt(Math.Pow(normalStress + flexuralStress, 2) + 3 * Math.Pow(shearStress + torsionalStress, 2));
        }

        /// <summary>
        /// This method calculates the normal stress.
        /// </summary>
        /// <param name="normalForce"></param>
        /// <param name="area"></param>
        /// <returns>The normal stress. Unit: Pa (Pascal).</returns>
        public double CalculateNormalStress(double normalForce, double area)
        {
            GeometricProperty.Validate(area, nameof(area));

            return normalForce / area;
        }
        
le         /// <summary>
        /// This method calculates the critical buckling force.
        /// </summary>
        /// <param name="youngModulus"></param>
        /// <param name="momentOfInertia"></param>
        /// <param name="length"></param>
        /// <param name="fasteningType"></param>
        /// <returns>The critical buckling force. Unit: N (Newton).</returns>
        public double CalculateCriticalBucklingForce(double youngModulus, double momentOfInertia, double length, FasteningType fasteningType = FasteningType.BothEndPinned)
        {
            GeometricProperty.Validate(momentOfInertia, nameof(momentOfInertia));
            GeometricProperty.Validate(length, nameof(length));

            return Math.Pow(Math.PI, 2) * youngModulus * momentOfInertia / Math.Pow(length * this.CalculateColumnEffectiveLengthFactor(fasteningType), 2);
        }

        /// <summary>
        /// This method calculates the effective length factor to buckling analysis based on fastening type.
        /// </summary>
        /// <param name="fasteningType"></param>
        /// <returns>The effective length factor. Dimensionless.</returns>
        public double CalculateColumnEffectiveLengthFactor(FasteningType fasteningType)
        {
            return fasteningType switch
            {
                FasteningType.BothEndPinned => 1,
                FasteningType.BothEndFixed => 0.5,
                FasteningType.OneEndFixedOneEndPinned => Math.Sqrt(2) / 2,
                FasteningType.OneEndFixed => 2,
                _ => throw new ArgumentOutOfRangeException($"Invalid fastening type: {fasteningType}.")
            };
        }
    }
}
