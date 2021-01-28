using SuspensionAnalysis.DataContracts.OperationBase;
using SuspensionAnalysis.Infrastructure.Models;
using SuspensionAnalysis.Infrastructure.Models.Enums;
using SuspensionAnalysis.Infrastructure.Models.SuspensionComponents;
using SuspensionAnalysis.Infrastructure.Models.Profiles;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SuspensionAnalysis.DataContracts.RunAnalysis
{
    public class RunAnalysisRequest<TProfile> : OperationRequestBase
        where TProfile : Profile
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public MaterialType Material { get; set; }

        public Point3D Origin { get; set; }

        public Point3D ForceApplied { get; set; }

        public ShockAbsorber ShockAbsorber { get; set; }

        public SuspensionAArm<TProfile> SuspensionAArmUpper { get; set; }

        public SuspensionAArm<TProfile> SuspensionAArmLower { get; set; }

        public TieRod<TProfile> TieRod { get; set; }
    }
}
