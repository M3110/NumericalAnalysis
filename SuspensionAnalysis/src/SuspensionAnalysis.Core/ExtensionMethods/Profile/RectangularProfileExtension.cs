using SuspensionAnalysis.Infrastructure.Models.Profiles;
using System;

namespace SuspensionAnalysis.Core.ExtensionMethods.Profile
{
    public static class RectangularProfileExtension
    {
        public static double CalculateArea(this RectangularProfile profile)
            => profile.Thickness.HasValue ?
            profile.Width * profile.Height - (profile.Width - 2 * profile.Thickness.Value) * (profile.Height - 2 * profile.Thickness.Value)
            : profile.Width * profile.Height;

        public static double CalculateMomentOfInertia(this RectangularProfile profile)
            => profile.Thickness.HasValue ?
            (Math.Pow(profile.Height, 3) * profile.Width - Math.Pow(profile.Height - 2 * profile.Thickness.Value, 3) * (profile.Width - 2 * profile.Thickness.Value)) / 12
            : Math.Pow(profile.Height, 3) * profile.Width / 12;
    }
}
