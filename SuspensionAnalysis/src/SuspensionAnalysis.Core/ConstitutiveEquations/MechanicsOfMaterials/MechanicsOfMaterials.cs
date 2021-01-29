using SuspensionAnalysis.Core.GeometricProperty;
using SuspensionAnalysis.DataContracts.Models.Analysis;
using SuspensionAnalysis.DataContracts.Models.Enums;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using System;

namespace SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials
{
    /// <summary>
    /// It contains the Mechanics of Materials constitutive equations.
    /// </summary>
    public class MechanicsOfMaterials<TProfile> : IMechanicsOfMaterials<TProfile>
        where TProfile : Profile
    {
        private readonly IGeometricProperty<TProfile> _geometricProperty;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="geometricProperty"></param>
        public MechanicsOfMaterials(IGeometricProperty<TProfile> geometricProperty)
        {
            this._geometricProperty = geometricProperty;
        }

        /// <summary>
        /// This method generates the analysis result.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public AnalysisResult GenerateResult(AnalysisInput<TProfile> input)
        {
            // Step 1 - Calculates the geometric properties.
            double area = this._geometricProperty.CalculateArea(input.Profile);
            double momentOfInertia = this._geometricProperty.CalculateMomentOfInertia(input.Profile);

            // Step 2 - Calculates the equivalent stress.
            double equivalentStress = this.CalculateEquivalentStress(this.CalculateNormalStress(input.AppliedForce, area));

            // Step 3 - Builds the analysis result.
            return new AnalysisResult()
            {
                AppliedForce = input.AppliedForce,
                CriticalBucklingForce = this.CalculateCriticalBucklingForce(input.Material.YoungModulus, momentOfInertia, input.Length, input.FasteningType),
                EquivalentStress = equivalentStress,
                StressSafetyFactor = input.Material.YieldStrength / equivalentStress
            };
        }

        /// <summary>
        /// This method calcultes the equivalent stress using Von-Misses method.
        /// </summary>
        /// <param name="normalStress"></param>
        /// <param name="flexuralStress"></param>
        /// <param name="shearStress"></param>
        /// <param name="torsionalStress"></param>
        /// <returns>The equivalent stress. Unity: Pa (Pascal).</returns>
        public double CalculateEquivalentStress(double normalStress = 0, double flexuralStress = 0, double shearStress = 0, double torsionalStress = 0)
        {
            return Math.Sqrt(Math.Pow(normalStress + flexuralStress, 2) + 3 * Math.Pow(shearStress + torsionalStress, 2));
        }

        /// <summary>
        /// This method calculates the normal stress.
        /// </summary>
        /// <param name="normalForce"></param>
        /// <param name="area"></param>
        /// <returns>The normal stress. Unity: Pa (Pascal).</returns>
        public double CalculateNormalStress(double normalForce, double area)
        {
            return normalForce / area;
        }

        /// <summary>
        /// This method calculates the critical buckling force.
        /// </summary>
        /// <param name="youngModulus"></param>
        /// <param name="momentOfInertia"></param>
        /// <param name="length"></param>
        /// <param name="fasteningType"></param>
        /// <returns>The critical buckling force. Unity: N (Newton).</returns>
        public double CalculateCriticalBucklingForce(double youngModulus, double momentOfInertia, double length, FasteningType fasteningType)
        {
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
                _ => throw new Exception($"Invalid fastening type: {fasteningType}.")
            };
        }
    }
}
