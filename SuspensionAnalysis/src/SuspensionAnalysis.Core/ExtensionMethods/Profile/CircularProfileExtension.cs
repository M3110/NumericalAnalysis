using SuspensionAnalysis.Infrastructure.Models.Profiles;
using System;

namespace SuspensionAnalysis.Core.ExtensionMethods.Profile
{
    public static class CircularProfileExtension
    {
        public static double CalculateArea(this CircularProfile profile)
            => profile.Thickness.HasValue ?
            Math.PI / 4 * (Math.Pow(profile.Diameter, 2) - Math.Pow(profile.Diameter - 2 * profile.Thickness.Value, 2))
            : Math.PI / 4 * Math.Pow(profile.Diameter, 2);

        public static double CalculateMomentOfInertia(this CircularProfile profile)
            => profile.Thickness.HasValue ?
            Math.PI / 64 * (Math.Pow(profile.Diameter, 4) - Math.Pow(profile.Diameter - 2 * profile.Thickness.Value, 4))
            : Math.PI / 64 * Math.Pow(profile.Diameter, 4);
    }
}
