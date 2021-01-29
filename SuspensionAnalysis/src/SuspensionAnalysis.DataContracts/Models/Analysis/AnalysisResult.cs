using System;

namespace SuspensionAnalysis.DataContracts.Models.Analysis
{
    /// <summary>
    /// It contains the essential result to analysis.
    /// </summary>
    public class AnalysisResult
    {
        /// <summary>
        /// The critical buckling force.
        /// Unity: N (Newton).
        /// </summary>
        public double CriticalBucklingForce { get; set; }

        /// <summary>
        /// The applied force.
        /// Unity: N (Newton).
        /// </summary>
        public double AppliedForce { get; set; }

        /// <summary>
        /// The stress calculated using Von-Misses method.
        /// Unity: Pa (Pascal).
        /// </summary>
        public double EquivalentStress { get; set; }

        /// <summary>
        /// The Von-Misses safety factor.
        /// Unity: Pa (Pascal).
        /// </summary>
        public double StressSafetyFactor { get; set; }

        /// <summary>
        /// The safety factor to buckling analysis.
        /// Unity: Dimensionless.
        /// </summary>
        public double BucklingSafetyFactor => this.AppliedForce / this.CriticalBucklingForce;

        /// <summary>
        /// The analysis safety factor.
        /// Unity: Dimensionless.
        /// </summary>
        public double SafetyFactor => Math.Min(this.StressSafetyFactor, this.BucklingSafetyFactor);
    }
}