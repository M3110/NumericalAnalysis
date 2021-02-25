using SuspensionAnalysis.Core.Models;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using System;

namespace SuspensionAnalysis.Core.GeometricProperties
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

    /// <summary>
    /// It is responsible to calculate the geometric properties to a profile.
    /// </summary>
    public class GeometricProperty
    {
        /// <summary>
        /// This method validates a geometric property.
        /// Parameters that can be validated: length, area, moment of inertia.
        /// </summary>
        /// <param name="geometricProperty"></param>
        /// <param name="nameOfVariable"></param>
        public static void Validate(double geometricProperty, string nameOfVariable)
        {
            if (geometricProperty <= 0 || Constants.InvalidValues.Contains(geometricProperty))
            {
                throw new ArgumentOutOfRangeException(nameof(geometricProperty), $"The {nameOfVariable} cannot be equals to {geometricProperty}. The {nameOfVariable} must be grether than zero.");
            }
        }
    }
}
