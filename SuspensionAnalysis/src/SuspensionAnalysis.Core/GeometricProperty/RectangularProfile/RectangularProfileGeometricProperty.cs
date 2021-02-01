using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;
using System;

namespace SuspensionAnalysis.Core.GeometricProperty.RectangularProfile
{
    /// <summary>
    /// It is responsible to calculate the geometric properties to rectangular profile.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class RectangularProfileGeometricProperty : GeometricProperty<DataContract.RectangularProfile>, IRectangularProfileGeometricProperty
    {
        /// <summary>
        /// This method calculates the area.
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public override double CalculateArea(DataContract.RectangularProfile profile)
        {
            return profile.Thickness.HasValue ?
                profile.Width * profile.Height - (profile.Width - 2 * profile.Thickness.Value) * (profile.Height - 2 * profile.Thickness.Value)
                : profile.Width * profile.Height;
        }

        /// <summary>
        /// This method calculates the moment of inertia.
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public override double CalculateMomentOfInertia(DataContract.RectangularProfile profile)
        {
            return profile.Thickness.HasValue ?
                (Math.Pow(profile.Height, 3) * profile.Width - Math.Pow(profile.Height - 2 * profile.Thickness.Value, 3) * (profile.Width - 2 * profile.Thickness.Value)) / 12
                : Math.Pow(profile.Height, 3) * profile.Width / 12;
        }
    }
}
