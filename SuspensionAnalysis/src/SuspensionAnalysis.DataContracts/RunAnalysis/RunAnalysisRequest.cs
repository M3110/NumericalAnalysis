using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SuspensionAnalysis.DataContracts.Models.Enums;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using SuspensionAnalysis.DataContracts.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.OperationBase;

namespace SuspensionAnalysis.DataContracts.RunAnalysis
{
    /// <summary>
    /// It represents the request content of RunAnalysis operation.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public class RunAnalysisRequest<TProfile> : OperationRequestBase
        where TProfile : Profile
    {
        /// <summary>
        /// True, if result should be rounded. False, otherwise.
        /// </summary>
        public bool ShouldRoundResults { get; set; }

        /// <summary>
        /// The number of decimals that should be rounded in results.
        /// </summary>
        public int? NumberOfDecimalsToRound { get; set; }
        
        /// <summary>
        /// The material.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public MaterialType Material { get; set; }

        /// <summary>
        /// The origin considered to analysis.
        /// </summary>
        /// <example>x,y,z</example>
        public string Origin { get; set; }

        /// <summary>
        /// The applied force.
        /// Unit: N (Newton).
        /// </summary>
        /// <example>x,y,z</example>
        public string ForceApplied { get; set; }

        /// <summary>
        /// The shock absorber.
        /// </summary>
        public ShockAbsorber ShockAbsorber { get; set; }

        /// <summary>
        /// The suspension A-arm upper.
        /// </summary>
        public SuspensionAArm<TProfile> SuspensionAArmUpper { get; set; }

        /// <summary>
        /// The suspension A-arm lower.
        /// </summary>
        public SuspensionAArm<TProfile> SuspensionAArmLower { get; set; }

        /// <summary>
        /// The tie rod.
        /// </summary>
        public TieRod<TProfile> TieRod { get; set; }
    }
}
