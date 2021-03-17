using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.Models.Analysis;
using SuspensionAnalysis.DataContracts.Models.Enums;
using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials
{
    /// <summary>
    /// It contains the Mechanics of Materials constitutive equations.
    /// </summary>
    public interface IMechanicsOfMaterials<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// This method generates the analysis result to tie rod.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="shouldRound"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        TieRodAnalysisResult GenerateResult(TieRod<TProfile> component, bool shouldRound, int decimals = 0);

        /// <summary>
        /// This method generates the analysis result to suspension A-arm.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="shouldRound"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        SuspensionAArmAnalysisResult GenerateResult(SuspensionAArm<TProfile> component, bool shouldRound, int decimals = 0);

        /// <summary>
        /// This method calcultes the equivalent stress using Von-Misses method.
        /// </summary>
        /// <param name="normalStress"></param>
        /// <param name="flexuralStress"></param>
        /// <param name="shearStress"></param>
        /// <param name="torsionalStress"></param>
        /// <returns>The equivalent stress. Unit: Pa (Pascal).</returns>
        double CalculateEquivalentStress(double normalStress = 0, double flexuralStress = 0, double shearStress = 0, double torsionalStress = 0);

        /// <summary>
        /// This method calculates the normal stress.
        /// </summary>
        /// <param name="normalForce"></param>
        /// <param name="area"></param>
        /// <returns>The normal stress. Unit: Pa (Pascal).</returns>
        double CalculateNormalStress(double normalForce, double area);

        /// <summary>
        /// This method calculates the critical buckling force.
        /// </summary>
        /// <param name="youngModulus"></param>
        /// <param name="momentOfInertia"></param>
        /// <param name="length"></param>
        /// <param name="fasteningType"></param>
        /// <returns>The critical buckling force. Unit: N (Newton).</returns>
        double CalculateCriticalBucklingForce(double youngModulus, double momentOfInertia, double length, FasteningType fasteningType);

        /// <summary>
        /// This method calculates the effective length factor to buckling analysis based on fastening type.
        /// </summary>
        /// <param name="fasteningType"></param>
        /// <returns>The effective length factor. Dimensionless.</returns>
        double CalculateColumnEffectiveLengthFactor(FasteningType fasteningType);
    }
}