using SuspensionAnalysis.Infrastructure.Models.Profiles;

namespace SuspensionAnalysis.Infrastructure.Models.SuspensionComponents
{
    public class TieRod<TProfile> : TieRodPoint
        where TProfile : Profile
    {
        public TProfile Profile { get; set; }
    }
}
