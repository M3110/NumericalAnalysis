using SuspensionAnalysis.Core.GeometricProperties;
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

        /// <summary>
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
