using SuspensionAnalysis.Core.GeometricProperties.RectangularProfile;
using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials.RectangularProfile
{
    /// <summary>
    /// It contains the Mechanics of Materials constitutive equations applied to rectangular profile.
    /// </summary>
    public class RectangularProfileMechanicsOfMaterials : MechanicsOfMaterials<DataContract.RectangularProfile>, IRectangularProfileMechanicsOfMaterials
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="geometricProperty"></param>
        public RectangularProfileMechanicsOfMaterials(IRectangularProfileGeometricProperty geometricProperty) : base(geometricProperty) { }
    }   
}
