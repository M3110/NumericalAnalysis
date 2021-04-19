using SuspensionAnalysis.Core.ExtensionMethods;
using SuspensionAnalysis.DataContracts.Models;

namespace SuspensionAnalysis.Core.Models.SuspensionComponents
{
    /// <summary>
    /// It contains the points to a single suspension component.
    /// </summary>
    public abstract class SingleComponent
    {
        /// <summary>
        /// The absolut applied force.
        /// </summary>
        public double AppliedForce { get; set; }

        /// <summary>
        /// The pivot point at chassis.
        /// </summary>
        public Point3D PivotPoint { get; set; }

        /// <summary>
        /// The fastening point.
        /// </summary>
        public Point3D FasteningPoint { get; set; }

        /// <summary>
        /// The vector that represents the direction of single component.
        /// </summary>
        public Vector3D VectorDirection => Vector3D.Create(this.PivotPoint, this.FasteningPoint);

        /// <summary>
        /// The normalized vector that represents the direction of single component.
        /// </summary>
        public Vector3D NormalizedDirection => this.VectorDirection.Normalize();

        /// <summary>
        /// The length.
        /// </summary>
        public double Length => this.VectorDirection.Length;
    }
}
