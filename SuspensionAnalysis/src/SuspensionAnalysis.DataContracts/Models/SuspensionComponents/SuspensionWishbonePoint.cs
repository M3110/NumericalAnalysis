using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.DataContracts.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the point to suspension wishbone.
    /// </summary>
    public class SuspensionWishbonePoint
    {
        /// <summary>
        /// The point of fastening with steering knuckle.
        /// </summary>
        public string WishboneOuterBallJoint { get; set; }

        /// <summary>
        /// The pivot point.
        /// This geometry has two pivot point.
        /// </summary>
        public string WishboneFrontPivot { get; set; }

        /// <summary>
        /// The pivot point.
        /// This geometry has two pivot point.
        /// </summary>
        public string WishboneRearPivot { get; set; }

        /// <summary>
        /// This method creates a <see cref="SuspensionWishbonePoint"/> based on <see cref="SuspensionWishbone{TProfile}"/>.
        /// </summary>
        /// <typeparam name="TProfile"></typeparam>
        /// <param name="suspensionWishbone"></param>
        /// <returns></returns>
        public static SuspensionWishbonePoint Create<TProfile>(SuspensionWishbone<TProfile> suspensionWishbone)
            where TProfile : Profile
        {
            return new SuspensionWishbonePoint
            {
                WishboneOuterBallJoint = suspensionWishbone.WishboneOuterBallJoint,
                WishboneFrontPivot = suspensionWishbone.WishboneFrontPivot,
                WishboneRearPivot = suspensionWishbone.WishboneRearPivot
            };
        }
    }
}
