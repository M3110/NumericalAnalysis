using SuspensionAnalysis.DataContracts.OperationBase;
using SuspensionAnalysis.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace SuspensionAnalysis.DataContracts.RunAnalysis
{
    public class RunAnalysisResponseData : OperationResponseData
    {
        public bool AnalisysFailed => this.SafetyFactor < 1;

        public double SafetyFactor
            => (new List<double> { this.ShockAbsorber.SafetyFactor, this.SuspensionAArmUpperResult.SafetyFactor, this.SuspensionAArmLowerResult.SafetyFactor, this.TieRod.SafetyFactor }).Min();

        public AnalysisResult ShockAbsorber { get; set; }

        public AnalysisResult SuspensionAArmUpperResult { get; set; }

        public AnalysisResult SuspensionAArmLowerResult { get; set; }

        public AnalysisResult TieRod { get; set; }
    }
}
