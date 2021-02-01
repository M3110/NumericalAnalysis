using SuspensionAnalysis.Core.ExtensionMethods;
using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.DataContracts.Models.Enums;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using DataContract = SuspensionAnalysis.DataContracts.Models.SuspensionComponents;

namespace SuspensionAnalysis.Core.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the suspension A-arm.
    /// </summary>
    public class SuspensionAArm
    {
        /// <summary>
        /// The absolut applied force to one segment of suspension A-arm.
        /// </summary>
        public double AppliedForce1 { get; set; }

        /// <summary>
        /// The absolut applied force to another segment of suspension A-arm.
        /// </summary>
        public double AppliedForce2 { get; set; }

        /// <summary>
        /// The length to one segment of suspension A-arm.
        /// </summary>
        public double Length1 => Vector3D.Create(this.KnucklePoint, this.PivotPoint1).Length;

        /// <summary>
        /// The length to another segment of suspension A-arm.
        /// </summary>
        public double Length2 => Vector3D.Create(this.KnucklePoint, this.PivotPoint2).Length;

        /// <summary>
        /// The vector that represents the direction of suspension A-arm to one segment of suspension A-arm.
        /// </summary>
        public Vector3D VectorDirection1 => Vector3D.Create(this.KnucklePoint, this.PivotPoint1);
        
        /// <summary>
        /// The vector that represents the direction of suspension A-arm to another segment of suspension A-arm.
        /// </summary>
        public Vector3D VectorDirection2 => Vector3D.Create(this.KnucklePoint, this.PivotPoint2);

        /// <summary>
        /// The normalized vector that represents the direction of suspension A-arm to one segment of suspension A-arm.
        /// </summary>
        public Vector3D NormalizedDirection1 => this.VectorDirection1.Normalize();
        
        /// <summary>
        /// The normalized vector that represents the direction of suspension A-arm to anoher segment of suspension A-arm.
        /// </summary>
        public Vector3D NormalizedDirection2 => this.VectorDirection2.Normalize();

        /// <summary>
        /// The poitn of fastening with steering knuckle.
        /// </summary>
        public Point3D KnucklePoint { get; set; }

        /// <summary>
        /// The pivot point.
        /// This geometry has two pivot point.
        /// </summary>
        public Point3D PivotPoint1 { get; set; }

        /// <summary>
        /// The pivot point.
        /// This geometry has two pivot point.
        /// </summary>
        public Point3D PivotPoint2 { get; set; }

        /// <summary>
        /// This method creates a <see cref="SuspensionAArm"/> based on <see cref="DataContract.SuspensionAArmPoint"/>.
        /// </summary>
        /// <param name="suspensionAArm"></param>
        /// <param name="appliedForce1"></param>
        /// <param name="appliedForce2"></param>
        /// <returns></returns>
        public static SuspensionAArm Create(DataContract.SuspensionAArmPoint suspensionAArm, double appliedForce1 = 0, double appliedForce2 = 0)
        {
            return new SuspensionAArm
            {
                KnucklePoint = Point3D.Create(suspensionAArm.KnucklePoint),
                PivotPoint1 = Point3D.Create(suspensionAArm.PivotPoint1),
                PivotPoint2 = Point3D.Create(suspensionAArm.PivotPoint2),
                AppliedForce1 = appliedForce1,
                AppliedForce2 = appliedForce2
            };
        }
    }

    /// <summary>
    /// It represents the suspension A-arm.
    /// </summary>
    public class SuspensionAArm<TProfile> : SuspensionAArm
        where TProfile : Profile
    {
        /// <summary>
        /// The material.
        /// </summary>
        public Material Material { get; set; }
        
        /// <summary>
        /// The profile.
        /// </summary>
        public TProfile Profile { get; set; }

        /// <summary>
        /// This method creates a <see cref="SuspensionAArm{TProfile}"/> based on <see cref="DataContract.SuspensionAArm{TProfile}"/>.
        /// </summary>
        /// <param name="suspensionAArm"></param>
        /// <param name="material"></param>
        /// <param name="appliedForce1"></param>
        /// <param name="appliedForce2"></param>
        /// <returns></returns>
        public static SuspensionAArm<TProfile> Create(DataContract.SuspensionAArm<TProfile> suspensionAArm, MaterialType material, double appliedForce1 = 0, double appliedForce2 = 0)
        {
            return new SuspensionAArm<TProfile>
            {
                KnucklePoint = Point3D.Create(suspensionAArm.KnucklePoint),
                PivotPoint1 = Point3D.Create(suspensionAArm.PivotPoint1),
                PivotPoint2 = Point3D.Create(suspensionAArm.PivotPoint2),
                Profile = suspensionAArm.Profile,
                AppliedForce1 = appliedForce1,
                AppliedForce2 = appliedForce2,
                Material = Material.Create(material)
            };
        }
    }
}
