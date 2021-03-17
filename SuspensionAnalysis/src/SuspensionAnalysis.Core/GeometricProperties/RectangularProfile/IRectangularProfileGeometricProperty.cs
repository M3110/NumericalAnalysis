using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.GeometricProperties.RectangularProfile
{
    /// <summary>
    /// It is responsible to calculate the geometric properties to rectangular profile.
    /// </summary>
    public interface IRectangularProfileGeometricProperty : IGeometricProperty<DataContract.RectangularProfile> { }
}