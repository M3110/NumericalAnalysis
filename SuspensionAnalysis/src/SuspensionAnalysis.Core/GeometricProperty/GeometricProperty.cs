using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.GeometricProperty
{
    /// <summary>
    /// It is responsible to calculate the geometric properties to a profile.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class GeometricProperty<TProfile> : IGeometricProperty<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// This method calculates the area.
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public abstract double CalculateArea(TProfile profile);

        /// <summary>
        /// This method calculates the moment of inertia.
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public abstract double CalculateMomentOfInertia(TProfile profile);
    }
}
