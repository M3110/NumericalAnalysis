using System.Collections.Generic;

namespace SuspensionAnalysis.Core.Models
{
    /// <summary>
    /// It contains the constants used in the project.
    /// </summary>
    public static class Constants
    {
        public static List<double> InvalidValues = new List<double> 
        { 
            double.NaN, 
            double.PositiveInfinity, 
            double.NegativeInfinity, 
            double.MaxValue, 
            double.MinValue 
        };

        public static double GravityAcceleration = 9.81;
    }
}
