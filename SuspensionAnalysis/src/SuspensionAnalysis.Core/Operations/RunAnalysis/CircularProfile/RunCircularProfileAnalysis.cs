using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials.CircularProfile;
using SuspensionAnalysis.Core.Mapper;
using SuspensionAnalysis.Core.Operations.CalculateReactions;
using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis.CircularProfile
{
    /// <summary>
    /// It is responsible to run the analysis to suspension system considering circular profile.
    /// </summary>
    public class RunCircularProfileAnalysis : RunAnalysis<DataContract.CircularProfile>, IRunCircularProfileAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="calculateReactions"></param>
        /// <param name="mechanicsOfMaterials"></param>
        /// <param name="mappingResolver"></param>
        public RunCircularProfileAnalysis(
            ICalculateReactions calculateReactions,
            ICircularProfileMechanicsOfMaterials mechanicsOfMaterials,
            IMappingResolver mappingResolver)
            : base(calculateReactions, mechanicsOfMaterials, mappingResolver)
        { }
    }
}
