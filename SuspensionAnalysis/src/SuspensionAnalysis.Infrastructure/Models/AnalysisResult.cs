using System;

namespace SuspensionAnalysis.Infrastructure.Models
{
    /// <summary>
    /// It contains the essential information to analysis result.
    /// </summary>
    public class AnalysisResult
    {
        /// <summary>
        /// The critical buckling force.
        /// </summary>
        public double CriticalBucklingForce { get; set; }

        /// <summary>
        /// The applied force.
        /// </summary>
        public double AppliedForce { get; set; }

        /// <summary>
        /// The stress calculated using Von-Misses method.
        /// </summary>
        public double VonMissesStress { get; set; }

        /// <summary>
        /// The Von-Misses safety factor.
        /// </summary>
        public double VonMissesSafetyFactor { get; set; }

        /// <summary>
        /// The safety factor to buckling analysis.
        /// </summary>
        public double BucklingSafetyFactor => this.AppliedForce / this.CriticalBucklingForce;

        /// <summary>
        /// The analysis safety factor.
        /// </summary>
        public double SafetyFactor => Math.Min(this.VonMissesSafetyFactor, this.BucklingSafetyFactor);
    }
}