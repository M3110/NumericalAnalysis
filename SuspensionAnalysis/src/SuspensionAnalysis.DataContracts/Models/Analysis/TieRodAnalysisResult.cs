using System;

namespace SuspensionAnalysis.DataContracts.Models.Analysis
{
    /// <summary>
    /// It contains the essential result to analysis.
    /// </summary>
    public class TieRodAnalysisResult
    {
        /// <summary>
        /// The critical buckling force.
        /// Unit: N (Newton).
        /// </summary>
        public double CriticalBucklingForce { get; set; }

        /// <summary>
        /// The applied force.
        /// Unit: N (Newton).
        /// </summary>
        public double AppliedForce { get; set; }

        /// <summary>
        /// The stress calculated using Von-Misses method.
        /// Unit: MPa (Megapascal).
        /// </summary>
        public double EquivalentStress { get; set; }

        /// <summary>
        /// The Von-Misses equivalent stress safety factor.
        /// Unit: Dimensionless.
        /// </summary>
        public double StressSafetyFactor { get; set; }

        /// <summary>
        /// The safety factor to buckling analysis.
        /// Unit: Dimensionless.
        /// </summary>
        public double BucklingSafetyFactor => Math.Round(Math.Abs(this.CriticalBucklingForce / this.AppliedForce), 2);

        /// <summary>
        /// The analysis safety factor.
        /// Unit: Dimensionless.
        /// </summary>
        public double SafetyFactor => Math.Min(this.StressSafetyFactor, this.BucklingSafetyFactor);
    }
}