using SuspensionAnalysis.Infraestructure.Models.Profiles;

namespace SuspensionAnalysis.Infraestructure.Models.SuspensionComponents
{
    public class SuspensionAArm<TProfile> : SuspensionAArmPoint
        where TProfile : Profile
    {
        public TProfile Profile { get; set; }
    }
}
