using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis.Static.CircularProfile
{
    /// <summary>
    /// It is responsible to run the analysis to suspension system considering circular profile.
    /// </summary>
    public interface IRunCircularProfileStaticAnalysis : IRunStaticAnalysis<DataContract.CircularProfile> { }
}