using SuspensionAnalysis.DataContracts.Models;

namespace SuspensionAnalysis.Core.Models.SuspensionComponents.SteeringKnuckle
{
    /// <summary>
    /// It represents the brake caliper support. 
    /// </summary>
    public class BrakeCaliperSupportPoint
    {
        /// <summary>
        /// The first pivot point of brake caliper support.
        /// </summary>
        public Point3D Point1 { get; set; }

        /// <summary>
        /// The second pivot point of brake caliper support. 
        /// </summary>
        public Point3D Point2 { get; set; }
    }
}
