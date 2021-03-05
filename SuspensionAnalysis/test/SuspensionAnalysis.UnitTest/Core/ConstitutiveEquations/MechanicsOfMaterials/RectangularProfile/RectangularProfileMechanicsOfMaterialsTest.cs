using FluentAssertions;
using Moq;
using Moq.Language;
using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials.RectangularProfile;
using SuspensionAnalysis.Core.GeometricProperties.RectangularProfile;
using System;
using Xunit;
using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;
namespace SuspensionAnalysis.UnitTest.Core.ConstitutiveEquations.MechanicsOfMaterials.RectangularProfile

/// O que precisamos fazer:
/// Revisar como funciona a classe **RectangularProfileMechanicsOfMaterials**.
/// Testar método CalculateEquivalentStress.
/// Testar método CalculateNormalStress para caso de falha.
/// Testar método Calculate Normal Stress para o caso de dados válidos.
/// Testar método CalculateCriticalBucklingForce para caso de falha.
/// Testar método CalculateCriticalBucklingForce para o caso de dados válidos.
/// Testar método CalculateColumnEffectiveLengthFactor para caso de falha.
/// Testar método CalculateColumnEffectiveLengthFactor para o caso de dados válidos.
/// Testar método CalculateColumnEffectiveLengthFactor para o caso de dados válidos.
/// **[OPCIONAL]** Testar método GenerateResult que recebe SuspensionAArm para o caso de dados válidos.
///**[OPCIONAL]** Testar método GenerateResult que recebe TieRod para caso de falha.
///**[OPCIONAL]** Testar método GenerateResult que recebe TieRod para o caso de dados válidos.

{
    public class RectangularProfileMechanicsOfMaterialsTest
    {
        private readonly RectangularProfileMechanicsOfMaterials _operation;
        private readonly Mock<IRectangularProfileGeometricProperty> _geometricPropertyMock;

        private const double _precision = 1e-3;

        public RectangularProfileMechanicsOfMaterialsTest()
        {

            this._geometricPropertyMock = new Mock<IRectangularProfileGeometricProperty>();
            this._geometricPropertyMock
                .Setup(gp => gp.CalculateArea(It.IsAny<DataContract.RectangularProfile>()))
                .Returns(1);
            this._geometricPropertyMock
                .Setup(gp => gp.CalculateMomentOfInertia(It.IsAny<DataContract.RectangularProfile>()))
                .Returns(1);

            this._operation = new RectangularProfileMechanicsOfMaterials(this._geometricPropertyMock.Object);
        }

        [Theory(DisplayName = "Feature: CalculateNormalStress | When: Call Method | Given: Valid Parameters| Should: Return a valid value to normal stress ")]
        [InlineData(0, 1, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(1000, 0.5, 2000)]
        [InlineData(1000, 7.92e-4, 1262626.263)]

        public void CalculateNormalStress_ValidParameters_Should_ReturnValidValue(double normalForce, double area, double expectedValue)

        {
            //Act
            double result = this._operation.CalculateNormalStress(normalForce, area);

            //Assert
            result.Should().BeApproximately(expectedValue, _precision);
        }

        [Theory(DisplayName = "Feature:  | When: Call Method | Given: Invalid Parameters | Should: Throw Excepction")]
        [InlineData(NaN)]
        [InlineData(PositiveInfinite)]
        [InlineData(NegativeInfinite)]
        [InlineData(MaxValue)]
        [InlineData(MinValue)]
        [InlineData(0)]
        [InlineData(-1)]

        public void CalculateNormalStress_InalidParameters_Should_ThrowExepction(double area)
        { 
            //Act
            Action.act = () => this._operation.CalculateNormalStress (normalForce = 1, area);
            //Assert

        }

    }
}
