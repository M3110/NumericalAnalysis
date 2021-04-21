using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.DataContracts.Models.SuspensionComponents
{
    /// <summary>
    /// It represents the points of tie rod.
    /// </summary>
    public class TieRodPoint : SingleComponentPoint 
    {
        /// <summary>
        /// This method creates a <see cref="TieRodPoint"/> based on <see cref="TieRod{TProfile}"/>.
        /// </summary>
        /// <typeparam name="TProfile"></typeparam>
        /// <param name="tieRod"></param>
        /// <returns></returns>
        public static TieRodPoint Create<TProfile>(TieRod<TProfile> tieRod)
            where TProfile : Profile
        {
            return new TieRodPoint
            {
                FasteningPoint = tieRod.FasteningPoint,
                PivotPoint = tieRod.PivotPoint
            };
        }
    }
}
