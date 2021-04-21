using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.GeometricProperties.CircularProfile
{
    /// <summary>
    /// It is responsible to calculate the geometric properties to circular profile.
    /// </summary>
    public interface ICircularProfileGeometricProperty : IGeometricProperty<DataContract.CircularProfile> { }
}