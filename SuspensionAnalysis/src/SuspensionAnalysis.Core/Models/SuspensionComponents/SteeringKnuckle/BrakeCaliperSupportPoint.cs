using SuspensionAnalysis.DataContracts.Models;
using DataContract = SuspensionAnalysis.DataContracts.Models.SuspensionComponents.SteeringKnuckle;

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

        public static BrakeCaliperSupportPoint Create(DataContract.BrakeCaliperSupportPoint brakeCaliperSupportPoint)
        {
            return new BrakeCaliperSupportPoint
            {
                Point1 = Point3D.Create(brakeCaliperSupportPoint.Point1),
                Point2 = Point3D.Create(brakeCaliperSupportPoint.Point2)
            };
        }
    }
}
