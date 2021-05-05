using SuspensionAnalysis.DataContracts.Models.Enums;
using System;

namespace SuspensionAnalysis.DataContracts.Models.SuspensionComponents
{
    /// <summary>
    /// It contains the necessary information about whatever bearing.
    /// </summary>
    public struct Bearing
    {
        /// <summary>
        /// It contains the necessary information about bearing 1. 
        /// </summary>
        public static readonly Bearing Bearing1 = new Bearing(effectiveRadius: 45.3e-3, axialLoadFactor: 1, radialLoadFactor: 1.6);

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="effectiveRadius"></param>
        /// <param name="axialLoadFactor"></param>
        /// <param name="radialLoadFactor"></param>
        private Bearing(double effectiveRadius, double axialLoadFactor, double radialLoadFactor)
        {
            EffectiveRadius = effectiveRadius;
            AxialLoadFactor = axialLoadFactor;
            RadialLoadFactor = radialLoadFactor;
        }

        public double EffectiveRadius { get; }

        public double AxialLoadFactor { get; }

        public double RadialLoadFactor { get; }

        public static Bearing Create(BearingType bearingType)
        {
            return bearingType switch
            {
                BearingType.Bearing1 => Bearing.Bearing1,
                _ => throw new Exception($"Invalid bearing: '{bearingType}'.")
            };
        }
    }
}