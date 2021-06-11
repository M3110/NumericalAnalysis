using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis.Static.RectangularProfile
{
    /// <summary>
    /// It is responsible to run the analysis to suspension system considering rectangular profile.
    /// </summary>
    public interface IRunRectangularProfileStaticAnalysis : IRunStaticAnalysis<DataContract.RectangularProfile> { }
}