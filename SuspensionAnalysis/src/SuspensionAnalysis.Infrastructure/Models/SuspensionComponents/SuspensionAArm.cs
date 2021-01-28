using SuspensionAnalysis.Infrastructure.Models.Profiles;

namespace SuspensionAnalysis.Infrastructure.Models.SuspensionComponents
{
    public class SuspensionAArm<TProfile> : SuspensionAArmPoint
        where TProfile : Profile
    {
        public TProfile Profile { get; set; }
    }
}
