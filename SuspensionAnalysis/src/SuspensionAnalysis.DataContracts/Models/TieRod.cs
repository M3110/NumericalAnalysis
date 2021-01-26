using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.DataContracts.Models
{
    public class TieRod<TProfile> : TieRodPoint
        where TProfile : Profile
    {
        public TProfile Profile { get; set; }
    }
}
