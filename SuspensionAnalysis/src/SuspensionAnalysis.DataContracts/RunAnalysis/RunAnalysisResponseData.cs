using SuspensionAnalysis.DataContracts.OperationBase;

namespace SuspensionAnalysis.DataContracts.RunAnalysis
{
    public class RunAnalysisResponseData : OperationResponseData
    {
        public bool AnalisysFailed => this.SafetyFactor < 1;

        public double SafetyFactor { get; set; }

        public double MaximumStress { get; set; }
    }
}
