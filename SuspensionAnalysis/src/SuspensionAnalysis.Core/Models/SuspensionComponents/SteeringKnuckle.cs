using SuspensionAnalysis.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
        public Point3D KnucklePoint1 { get; set; }

        /// <summary>
        /// The point of fastening with A-arm lower.
        /// </summary>
        public Point3D KnucklePoint2 { get; set; }

        /// <summary>
        /// The point of fastening with tie rod.
        /// </summary>
        public Point3D KnucklePoint3 { get; set; }
    }
}
