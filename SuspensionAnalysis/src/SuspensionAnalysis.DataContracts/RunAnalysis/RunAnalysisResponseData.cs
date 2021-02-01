using SuspensionAnalysis.DataContracts.Models;
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
            => (new List<double> { this.SuspensionAArmUpperResult.SafetyFactor, this.SuspensionAArmLowerResult.SafetyFactor, this.TieRod.SafetyFactor }).Min();

        /// <summary>
        /// The force reactions at shock absorber.
        /// </summary>
        public Force ShockAbsorber { get; set; }

        /// <summary>
        /// The analysis result to suspension A-arm upper.
        /// </summary>
        public SuspensionAArmAnalysisResult SuspensionAArmUpperResult { get; set; }

        /// <summary>
        /// The analysis result to suspension A-arm lower.
        /// </summary>
        public SuspensionAArmAnalysisResult SuspensionAArmLowerResult { get; set; }

        /// <summary>
        /// The analysis result to tie rod.
        /// </summary>
        public TieRodAnalysisResult TieRod { get; set; }
    }
}
