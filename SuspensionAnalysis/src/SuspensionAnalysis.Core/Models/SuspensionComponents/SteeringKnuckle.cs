﻿using SuspensionAnalysis.DataContracts.Models;

namespace SuspensionAnalysis.Core.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the steering knuckle.
    /// </summary>
    public class SteeringKnuckle
    {
        /// <summary>
        /// The point of fastening with A-arm upper. 
        /// </summary>
        public Point3D AArmUpperPoint { get; set; }

        /// <summary>
        /// The point of fastening with A-arm lower.
        /// </summary>
        public Point3D AArmLowerPoint { get; set; }

        /// <summary>
        /// The point of fastening with tie rod.
        /// </summary>
        public Point3D TieRodPoint { get; set; }
    }
}