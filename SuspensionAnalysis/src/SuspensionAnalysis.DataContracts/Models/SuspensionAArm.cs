using SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.DataContracts.Models
{
    public class SuspensionAArm<TProfile> : SuspensionAArmPoint
        where TProfile : Profile
    {
        public TProfile Profile { get; set; }
    }
}
