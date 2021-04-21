using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis.CircularProfile
{
    /// <summary>
    /// It is responsible to run the analysis to suspension system considering circular profile.
    /// </summary>
    public interface IRunCircularProfileAnalysis : IRunAnalysis<DataContract.CircularProfile> { }
}