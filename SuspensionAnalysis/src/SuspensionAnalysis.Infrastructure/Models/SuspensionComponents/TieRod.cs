using SuspensionAnalysis.Infraestructure.Models.Profiles;

namespace SuspensionAnalysis.Infraestructure.Models.SuspensionComponents
{
    public class TieRod<TProfile> : TieRodPoint
        where TProfile : Profile
    {
        public TProfile Profile { get; set; }
    }
}
