using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.DataContracts.RunAnalysis.Dynamic.HalfCar;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis.Dynamic.HalfCar
{
    public interface IRunHalfCarDynamicAnalysis : IOperationBase<RunHalfCarDynamicAnalysisRequest, RunHalfCarDynamicAnalysisResponse, RunHalfCarDynamicAnalysisResponseData>
    {
    }
}