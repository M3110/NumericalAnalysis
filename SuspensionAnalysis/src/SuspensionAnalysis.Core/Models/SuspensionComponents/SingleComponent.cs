using SuspensionAnalysis.DataContracts.Models;

namespace SuspensionAnalysis.Core.Models.SuspensionComponents
{
    /// <summary>
    /// It contains the points to a single suspension component.
    /// </summary>
    public abstract class SingleComponent
    {
        /// <summary>
        /// The pivot point at chassis.
        /// </summary>
        public Point3D PivotPoint { get; set; }

        /// <summary>
        /// The fastening point.
        /// </summary>
        public Point3D FasteningPoint { get; set; }
    }
}
