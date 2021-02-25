using SuspensionAnalysis.Core.GeometricProperties.CircularProfile;
using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials.CircularProfile
{
    /// <summary>
    /// It contains the Mechanics of Materials constitutive equations applied to circular profile.
    /// </summary>
    public class CircularProfileMechanicsOfMaterials : MechanicsOfMaterials<DataContract.CircularProfile>, ICircularProfileMechanicsOfMaterials
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="geometricProperty"></param>
        public CircularProfileMechanicsOfMaterials(ICircularProfileGeometricProperty geometricProperty) : base(geometricProperty) { }
    }
}
