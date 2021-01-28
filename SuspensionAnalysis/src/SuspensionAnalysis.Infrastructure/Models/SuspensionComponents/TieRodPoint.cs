using SuspensionAnalysis.Infrastructure.Models.Profiles;

namespace SuspensionAnalysis.Infrastructure.Models.SuspensionComponents
{
    public class TieRodPoint : SingleComponentPoint 
    {
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
