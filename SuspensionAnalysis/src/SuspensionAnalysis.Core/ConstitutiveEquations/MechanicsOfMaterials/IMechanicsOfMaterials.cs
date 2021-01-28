using SuspensionAnalysis.Core.Models;

namespace SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials
{
    public interface IMechanicsOfMaterials
    {
        double CalculateNormalStress(double normalForce, double area);

        double CalculateCriticalBucklingForce(double youngModulus, double momentOfInertia, double length, FasteningType fasteningType);
    }
}