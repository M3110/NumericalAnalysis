using System;

namespace SuspensionAnalysis.DataContracts.Models
{
    /// <summary>
    /// It represents the force.
    /// </summary>
    public class Force
    {
        /// <summary>
        /// the absolut value to force.
        /// </summary>
        public double AbsolutValue { get; set; }

        /// <summary>
        /// The force at axis X.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The force at axis Y.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// The force at axis Z.
        /// </summary>
        public double Z { get; set; }
        /// <summary>
        /// This method rounds each value at <see cref="Force"/> to a specified number of fractional
        /// digits, and rounds midpoint values to the nearest even number.
        /// </summary>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public Force Round(int decimals)
        {
            return new Force
            {
                AbsolutValue = Math.Round(this.AbsolutValue, decimals),
                X = Math.Round(this.X, decimals),
                Y = Math.Round(this.Y, decimals),
                Z = Math.Round(this.Z, decimals)
            };
        }

        /// <summary>
        /// This method creates the <see cref="Force"/> based on the absolut value and the normalized direction.
        /// </summary>
        /// <param name="absolutValue"></param>
        /// <param name="normalizedDirection"></param>
        /// <returns></returns>
        public static Force Create(double absolutValue, Vector3D normalizedDirection)
        {
            return new Force
            {
                AbsolutValue = absolutValue,
                X = absolutValue * normalizedDirection.X,
                Y = absolutValue * normalizedDirection.Y,
                Z = absolutValue * normalizedDirection.Z
            };
        }
    }
}
