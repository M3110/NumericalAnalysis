using SuspensionAnalysis.DataContracts.Models;
using DataContract = SuspensionAnalysis.DataContracts.Models.SuspensionComponents;

namespace SuspensionAnalysis.Core.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the shock absorber.
    /// </summary>
    public class ShockAbsorber : SingleComponent 
    {
        /// <summary>
        /// The absolut applied force.
        /// </summary>
        public double AppliedForce { get; set; }

        /// <summary>
        /// The length.
        /// </summary>
        public double Length => Vector3D.Create(this.FasteningPoint, this.PivotPoint).Length;

        /// <summary>
        /// This method creates a <see cref="ShockAbsorber"/> based on <see cref="DataContract.ShockAbsorberPoint"/>.
        /// </summary>
        /// <param name="shockAbsorber"></param>
        /// <param name="appliedForce"></param>
        /// <returns></returns>
        public static ShockAbsorber Create(DataContract.ShockAbsorberPoint shockAbsorber, double appliedForce = 0)
        {
            return new ShockAbsorber
            {
                FasteningPoint = Point3D.Create(shockAbsorber.FasteningPoint),
                PivotPoint = Point3D.Create(shockAbsorber.PivotPoint),
                AppliedForce = appliedForce
            };
        }
    }
}
