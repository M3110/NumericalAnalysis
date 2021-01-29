using SuspensionAnalysis.DataContracts.Models.Analysis;
using SuspensionAnalysis.DataContracts.OperationBase;
using System.Collections.Generic;
using System.Linq;

namespace SuspensionAnalysis.DataContracts.RunAnalysis
{
    /// <summary>
    /// It represents the 'data' content of RunAnalysis operation response.
    /// </summary>
    public class RunAnalysisResponseData : OperationResponseData
    {
        /// <summary>
        /// True, if analysis failed. False, otherwise.
        /// </summary>
        public bool AnalisysFailed => this.SafetyFactor < 1;

        /// <summary>
        /// The safety factor.
        /// </summary>
        public double SafetyFactor
            => (new List<double> { this.ShockAbsorber.SafetyFactor, this.SuspensionAArmUpperResult.SafetyFactor, this.SuspensionAArmLowerResult.SafetyFactor, this.TieRod.SafetyFactor }).Min();

        /// <summary>
        /// The analysis result to shock absorber.
        /// </summary>
        public AnalysisResult ShockAbsorber { get; set; }

        /// <summary>
        /// The analysis result to suspension A-arm upper.
        /// </summary>
        public AnalysisResult SuspensionAArmUpperResult { get; set; }

        /// <summary>
        /// The analysis result to suspension A-arm lower.
        /// </summary>
        public AnalysisResult SuspensionAArmLowerResult { get; set; }

        /// <summary>
        /// The analysis result to tie rod.
        /// </summary>
        public AnalysisResult TieRod { get; set; }
    }
}
