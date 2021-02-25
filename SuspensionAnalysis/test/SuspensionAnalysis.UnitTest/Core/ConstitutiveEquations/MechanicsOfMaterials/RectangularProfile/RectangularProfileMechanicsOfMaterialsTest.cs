using Moq;
using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials.RectangularProfile;
using SuspensionAnalysis.Core.GeometricProperties.RectangularProfile;

namespace SuspensionAnalysis.UnitTest.Core.ConstitutiveEquations.MechanicsOfMaterials.RectangularProfile
{
    public class RectangularProfileMechanicsOfMaterialsTest
    {
        private readonly RectangularProfileMechanicsOfMaterials _operation;
        private readonly Mock<IRectangularProfileGeometricProperty> _geometrypropertyMock;

        public RectangularProfileMechanicsOfMaterialsTest()
        {

            this._geometrypropertyMock = new Mock<IRectangularProfileGeometricProperty>();
            this._geometrypropertyMock.Setup()



        }
    }
}
