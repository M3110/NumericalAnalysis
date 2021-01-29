using SuspensionAnalysis.DataContracts.Models.Profiles;
using System;

namespace SuspensionAnalysis.Core.GeometricProperty
{
    /// <summary>
    /// It is responsible to calculate the geometric properties to circular profile.
    /// </summary>
    public class CircularProfileGeometricProperty : GeometricProperty<CircularProfile>, ICircularProfileGeometricProperty
    {
        /// <summary>
        /// This method calculates the area.
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public override double CalculateArea(CircularProfile profile)
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
        public override double CalculateMomentOfInertia(CircularProfile profile)
        {
            return profile.Thickness.HasValue ?
                Math.PI / 64 * (Math.Pow(profile.Diameter, 4) - Math.Pow(profile.Diameter - 2 * profile.Thickness.Value, 4))
                : Math.PI / 64 * Math.Pow(profile.Diameter, 4);
        }
    }
}
