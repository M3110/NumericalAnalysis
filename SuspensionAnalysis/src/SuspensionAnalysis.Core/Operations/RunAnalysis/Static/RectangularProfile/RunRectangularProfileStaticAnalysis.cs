﻿using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials.RectangularProfile;
using SuspensionAnalysis.Core.GeometricProperties.RectangularProfile;
using SuspensionAnalysis.Core.Mapper;
using SuspensionAnalysis.Core.Operations.CalculateReactions;
using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.Operations.RunAnalysis.Static.RectangularProfile
{
    /// <summary>
    /// It is responsible to run the analysis to suspension system considering rectangular profile.
    /// </summary>
    public class RunRectangularProfileStaticAnalysis : RunStaticAnalysis<DataContract.RectangularProfile>, IRunRectangularProfileStaticAnalysis
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="calculateReactions"></param>
        /// <param name="mechanicsOfMaterials"></param>
        /// <param name="mappingResolver"></param>
        public RunRectangularProfileStaticAnalysis(
            ICalculateReactions calculateReactions,
            IRectangularProfileMechanicsOfMaterials mechanicsOfMaterials,
            IRectangularProfileGeometricProperty geometricProperty,
            IMappingResolver mappingResolver)
            : base(calculateReactions, mechanicsOfMaterials, geometricProperty, mappingResolver)
        { }
    }
}
