using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials.CircularProfile;
using SuspensionAnalysis.Core.GeometricProperties.CircularProfile;
using SuspensionAnalysis.Core.Mapper;
using SuspensionAnalysis.Core.Operations.CalculateReactions;
using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis.Static.CircularProfile
{
    /// <summary>
    /// It is responsible to run the analysis to suspension system considering circular profile.
    /// </summary>
    public class RunCircularProfileStaticAnalysis : RunStaticAnalysis<DataContract.CircularProfile>, IRunCircularProfileStaticAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="calculateReactions"></param>
        /// <param name="mechanicsOfMaterials"></param>
        /// <param name="geometricProperty"></param>
        /// <param name="mappingResolver"></param>
        public RunCircularProfileStaticAnalysis(
            ICalculateReactions calculateReactions,
            ICircularProfileMechanicsOfMaterials mechanicsOfMaterials,
            ICircularProfileGeometricProperty geometricProperty,
            IMappingResolver mappingResolver)
            : base(calculateReactions, mechanicsOfMaterials, geometricProperty, mappingResolver)
        { }
    }
}
