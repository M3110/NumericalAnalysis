using System;

namespace SuspensionAnalysis.DataContracts.Models.Analysis
{
    /// <summary>
    /// It contains the necessary informations to execute the analysis to suspension wihsbone.
    /// </summary>
    public class SuspensionWishboneAnalysisResult
    {
        /// <summary>
        /// The critical buckling force to one segment of suspension wishbone.
        /// Unit: N (Newton).
        /// </summary>
        public double CriticalBucklingForce1 { get; set; }

        /// <summary>
        /// The critical buckling force to another segment of suspension wishbone.
        /// Unit: N (Newton).
        /// </summary>
        public double CriticalBucklingForce2 { get; set; }

        /// <summary>
        /// The applied force to one segment of suspension wishbone.
        /// Unit: N (Newton).
        /// </summary>
        public double AppliedForce1 { get; set; }

        /// <summary>
        /// The applied force to another segment of suspension wishbone.
        /// Unit: N (Newton).
        /// </summary>
        public double AppliedForce2 { get; set; }

        /// <summary>
        /// The safety factor to buckling analysis.
        /// Unit: Dimensionless.
        /// </summary>
        public double BucklingSafetyFactor
            => Math.Round(Math.Min(
                Math.Abs(this.CriticalBucklingForce1 / this.AppliedForce1),
                Math.Abs(this.CriticalBucklingForce2 / this.AppliedForce2)), 2);

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
        /// The analysis safety factor.
        /// Unit: Dimensionless.
        /// </summary>
        public double SafetyFactor => Math.Min(this.StressSafetyFactor, this.BucklingSafetyFactor);
    }
}
