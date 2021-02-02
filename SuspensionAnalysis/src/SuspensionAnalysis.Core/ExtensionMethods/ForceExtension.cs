using SuspensionAnalysis.DataContracts.Models;
using System;

namespace SuspensionAnalysis.Core.ExtensionMethods
{
    /// <summary>
    /// It contains the extension methods to Force.
    /// </summary>
    public static class ForceExtension
    {
        /// <summary>
        /// This method rounds each value at <see cref="Force"/> to a specified number of fractional
        /// digits, and rounds midpoint values to the nearest even number.
        /// </summary>
        /// <param name="force"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static Force Round(this Force force, int decimals)
        {
            return new Force
            {
                AbsolutValue = Math.Round(force.AbsolutValue, decimals),
                X = Math.Round(force.X, decimals),
                Y = Math.Round(force.Y, decimals),
                Z = Math.Round(force.Z, decimals)
            };
        }
    }
}
