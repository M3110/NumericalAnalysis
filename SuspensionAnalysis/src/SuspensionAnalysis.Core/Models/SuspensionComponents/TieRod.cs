using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.DataContracts.Models.Enums;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using DataContract = SuspensionAnalysis.DataContracts.Models.SuspensionComponents;

namespace SuspensionAnalysis.Core.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the tie rod.
    /// </summary>
    public class TieRod : SingleComponent
    {
        /// <summary>
        /// This method creates a <see cref="TieRod"/> based on <see cref="DataContract.TieRodPoint"/>.
        /// </summary>
        /// <param name="tieRod"></param>
        /// <param name="appliedForce"></param>
        /// <returns></returns>
        public static TieRod Create(DataContract.TieRodPoint tieRod, double appliedForce = 0)
        {
            return new TieRod
            {
                FasteningPoint = Point3D.Create(tieRod.FasteningPoint),
                PivotPoint = Point3D.Create(tieRod.PivotPoint),
                AppliedForce = appliedForce
            };
        }
    }

    /// <summary>
    /// It represents the tie rod.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class TieRod<TProfile> : TieRod
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
        /// This method creates a <see cref="TieRod{TProfile}"/> based on <see cref="DataContract.TieRod{TProfile}"/>.
        /// </summary>
        /// <param name="tieRod"></param>
        /// <param name="material"></param>
        /// <param name="appliedForce"></param>
        /// <returns></returns>
        public static TieRod<TProfile> Create(DataContract.TieRod<TProfile> tieRod, MaterialType material, double appliedForce = 0)
        {
            return new TieRod<TProfile>
            {
                FasteningPoint = Point3D.Create(tieRod.FasteningPoint),
                PivotPoint = Point3D.Create(tieRod.PivotPoint),
                AppliedForce = appliedForce,
                Profile = tieRod.Profile,
                Material = Material.Create(material)
            };
        }
    }
}
