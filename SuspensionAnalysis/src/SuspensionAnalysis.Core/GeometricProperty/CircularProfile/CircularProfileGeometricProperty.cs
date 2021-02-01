using System;
using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.GeometricProperty.CircularProfile
{
    /// <summary>
    /// It is responsible to calculate the geometric properties to circular profile.
    /// </summary>
    public class CircularProfileGeometricProperty : GeometricProperty<DataContract.CircularProfile>, ICircularProfileGeometricProperty
    {
        /// <summary>
        /// This method calculates the area.
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public override double CalculateArea(DataContract.CircularProfile profile)
        {
            return profile.Thickness.HasValue ?
                Math.PI / 4 * (Math.Pow(profile.Diameter, 2) - Math.Pow(profile.Diameter - 2 * profile.Thickness.Value, 2))
                : Math.PI / 4 * Math.Pow(profile.Diameter, 2);
        }

        /// <summary>
        /// This method calculates the moment of inertia.
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public override double CalculateMomentOfInertia(DataContract.CircularProfile profile)
        {
            return profile.Thickness.HasValue ?
                Math.PI / 64 * (Math.Pow(profile.Diameter, 4) - Math.Pow(profile.Diameter - 2 * profile.Thickness.Value, 4))
                : Math.PI / 64 * Math.Pow(profile.Diameter, 4);
        }
    }
}
