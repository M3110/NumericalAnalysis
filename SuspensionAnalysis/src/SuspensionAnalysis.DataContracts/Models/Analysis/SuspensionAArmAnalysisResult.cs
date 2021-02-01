using System;

namespace SuspensionAnalysis.DataContracts.Models.Analysis
{
    /// <summary>
    /// It contains the necessary informations to execute the analysis to suspension A-arm.
    /// </summary>
    public class SuspensionAArmAnalysisResult
    {
        /// <summary>
        /// The critical buckling force to one segment of suspension A-arm.
        /// Unity: N (Newton).
        /// </summary>
        public double CriticalBucklingForce1 { get; set; }

        /// <summary>
        /// The critical buckling force to another segment of suspension A-arm.
        /// Unity: N (Newton).
        /// </summary>
        public double CriticalBucklingForce2 { get; set; }

        /// <summary>
        /// The applied force to one segment of suspension A-arm.
        /// Unity: N (Newton).
        /// </summary>
        public double AppliedForce1 { get; set; }

        /// <summary>
        /// The applied force to another segment of suspension A-arm.
        /// Unity: N (Newton).
        /// </summary>
        public double AppliedForce2 { get; set; }

        /// <summary>
        /// The safety factor to buckling analysis.
        /// Unity: Dimensionless.
        /// </summary>
        public double BucklingSafetyFactor => Math.Min(this.AppliedForce1 / this.CriticalBucklingForce1, this.AppliedForce2 / this.CriticalBucklingForce2);

        /// <summary>
        /// The stress calculated using Von-Misses method.
        /// Unity: Pa (Pascal).
        /// </summary>
        public double EquivalentStress { get; set; }

        /// <summary>
        /// The Von-Misses equivalent stress safety factor.
        /// Unity: Pa (Pascal).
        /// </summary>
        public double StressSafetyFactor { get; set; }

        /// <summary>
        /// The analysis safety factor.
        /// Unity: Dimensionless.
        /// </summary>
        public double SafetyFactor => Math.Min(this.StressSafetyFactor, this.BucklingSafetyFactor);
    }
}
