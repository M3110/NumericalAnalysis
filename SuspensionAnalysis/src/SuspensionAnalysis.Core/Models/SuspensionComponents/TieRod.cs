using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using DataContract = SuspensionAnalysis.DataContracts.Models.SuspensionComponents;

namespace SuspensionAnalysis.Core.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the tie rod.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class TieRod<TProfile> : SingleComponent
        where TProfile : Profile
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
        /// The profile.
        /// </summary>
        public TProfile Profile { get; set; }

        /// <summary>
        /// This method creates a <see cref="TieRod{TProfile}"/> based on <see cref="DataContract.TieRod{TProfile}"/>.
        /// </summary>
        /// <param name="tieRod"></param>
        /// <param name="appliedForce"></param>
        /// <returns></returns>
        public static TieRod<TProfile> Create(DataContract.TieRod<TProfile> tieRod, double appliedForce = 0)
        {
            return new TieRod<TProfile>
            {
                FasteningPoint = Point3D.Create(tieRod.FasteningPoint),
                PivotPoint = Point3D.Create(tieRod.PivotPoint),
                AppliedForce = appliedForce,
                Profile = tieRod.Profile
            };
        }

        /// <summary>
        /// This method creates a <see cref="TieRod{Profile}"/> based on <see cref="DataContract.TieRodPoint"/>.
        /// </summary>
        /// <param name="tieRod"></param>
        /// <param name="appliedForce"></param>
        /// <returns></returns>
        public static TieRod<Profile> Create(DataContract.TieRodPoint tieRod, double appliedForce = 0)
        {
            return new TieRod<Profile>
            {
                FasteningPoint = Point3D.Create(tieRod.FasteningPoint),
                PivotPoint = Point3D.Create(tieRod.PivotPoint),
                AppliedForce = appliedForce
            };
        }
    }
}
