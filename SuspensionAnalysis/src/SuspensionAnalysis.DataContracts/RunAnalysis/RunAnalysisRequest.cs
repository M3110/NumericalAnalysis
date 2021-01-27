using SuspensionAnalysis.DataContracts.OperationBase;
using SuspensionAnalysis.Infraestructure.Models;
using SuspensionAnalysis.Infraestructure.Models.Enums;
using SuspensionAnalysis.Infraestructure.Models.Profiles;
using SuspensionAnalysis.Infraestructure.Models.SuspensionComponents;

namespace SuspensionAnalysis.DataContracts.RunAnalysis
{
    public class RunAnalysisRequest<TProfile> : OperationRequestBase
        where TProfile : Profile
    {
        public Material Material { get; set; }

        public Point3D Origin { get; set; }

        public Point3D ForceApplied { get; set; }

        public ShockAbsorber ShockAbsorber { get; set; }

        public SuspensionAArm<TProfile> SuspensionAArmUpper { get; set; }

        public SuspensionAArm<TProfile> SuspensionAArmLower { get; set; }

        public TieRod<TProfile> TieRod { get; set; }
    }
}
