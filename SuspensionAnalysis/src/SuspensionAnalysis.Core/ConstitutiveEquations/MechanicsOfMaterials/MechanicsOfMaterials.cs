using SuspensionAnalysis.Core.Models;
using System;

namespace SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials
{
    public class MechanicsOfMaterials : IMechanicsOfMaterials
    {
        public double CalculateNormalStress(double normalForce, double area)
        {
            return normalForce / area;
        }

        public double CalculateCriticalBucklingForce(double youngModulus, double momentOfInertia, double length, FasteningType fasteningType)
        {
            return Math.Pow(Math.PI, 2) * youngModulus * momentOfInertia / Math.Pow(length * this.CalculateColumnEffectiveLengthFactor(fasteningType), 2);
        }

        private double CalculateColumnEffectiveLengthFactor(FasteningType fasteningType)
        {
            return fasteningType switch
            {
                FasteningType.BothEndhPinned => 1,
                FasteningType.BothEndhFixed => 0.5,
                FasteningType.OneEndFixedOneEndPinned => Math.Sqrt(2) / 2,
                FasteningType.OneEndFixed => 2,
                _ => throw new Exception($"Invalid fastening type: {fasteningType}.")
            };
        }
    }
}
