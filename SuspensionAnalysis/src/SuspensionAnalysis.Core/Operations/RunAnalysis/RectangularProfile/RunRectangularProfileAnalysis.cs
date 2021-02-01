using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials.RectangularProfile;
using SuspensionAnalysis.Core.Mapper;
using SuspensionAnalysis.Core.Operations.CalculateReactions;
using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis.RectangularProfile
{
    /// <summary>
    /// It is responsible to run the analysis to suspension system considering rectangular profile.
    /// </summary>
    public class RunRectangularProfileAnalysis : RunAnalysis<DataContract.RectangularProfile>, IRunRectangularProfileAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="calculateReactions"></param>
        /// <param name="mechanicsOfMaterials"></param>
        /// <param name="mappingResolver"></param>
        public RunRectangularProfileAnalysis(
            ICalculateReactions calculateReactions,
            IRectangularProfileMechanicsOfMaterials mechanicsOfMaterials,
            IMappingResolver mappingResolver)
            : base(calculateReactions, mechanicsOfMaterials, mappingResolver)
        { }
    }
}
