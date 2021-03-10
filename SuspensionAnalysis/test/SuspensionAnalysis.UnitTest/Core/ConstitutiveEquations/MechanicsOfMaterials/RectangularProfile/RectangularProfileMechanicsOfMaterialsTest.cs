using FluentAssertions;
using Moq;
using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials.RectangularProfile;
using SuspensionAnalysis.Core.GeometricProperties.RectangularProfile;
using System;
using System.Collections.Generic;
using Xunit;
using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.UnitTest.Core.ConstitutiveEquations.MechanicsOfMaterials.RectangularProfile
{
    public class RectangularProfileMechanicsOfMaterialsTest
    {
        private readonly Mock<IRectangularProfileGeometricProperty> _geometricPropertyMock;
        private readonly RectangularProfileMechanicsOfMaterials _operation;
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

        [Theory(DisplayName = "Feature: CalculateNormalStress.| Given: Valid Parameters. | When: Call Method. | Should: Return a valid value to normal stress. ")]
        [InlineData(0, 1, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(1000, 0.5, 2000)]
        [InlineData(1000, 7.92e-4, 1262626.263)]
        public void CalculateNormalStress_ValidParameters_Should_ReturnValidValue(double normalForce, double area, double expectedValue)
        {
            // Act
            double result = this._operation.CalculateNormalStress(normalForce, area);

            // Assert
            result.Should().BeApproximately(expectedValue, _precision);
        }

        [Theory(DisplayName = "Feature: CalculateNormalStress. | Given: Invalid Parameters. | When: Call Method. | Should: Throw Excepction. ")]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.MaxValue)]
        [InlineData(double.MinValue)]
        [InlineData(0)]
        [InlineData(-1)]
        public void CalculateNormalStress_InvalidParameters_Should_ThrowExepction(double area)
        {
            // Act
            Action act = () => this._operation.CalculateNormalStress(normalForce: 1, area);

            // Assert
            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [MemberData(nameof(EquivalentStressParameters))]
        [Theory(DisplayName = "Feature: CalculateEquivalentStress. | Given: Valid Parameters. | When: Call Method. | Should: Return a valid value to normal stress. ")]
        public void CalculateEquivalentStress_ValidParameters_Should_ReturnValidValue(double normalStress, double flexuralStress, double shearStress, double torsionalStress, double expectedValue)
        {
            // Act  
            double result = this._operation.CalculateEquivalentStress(normalStress, flexuralStress, torsionalStress, shearStress);

            // Assert
            result.Should().BeApproximately(expectedValue, _precision);
        }

        public static IEnumerable<object[]> EquivalentStressParameters()
        {
            yield return new object[] { 0, 0, 0, 0, 0 };
            yield return new object[] { 1, 0, 0, 0, 1 };
            yield return new object[] { 0, 1, 0, 0, 1 };
            yield return new object[] { 0, 0, 1, 0, Math.Sqrt(3) };
            yield return new object[] { 0, 0, 0, 1, Math.Sqrt(3) };
            yield return new object[] { 1, 1, 0, 0, 2 };
            yield return new object[] { 1, 0, 1, 0, 2 };
            yield return new object[] { 1, 0, 0, 1, 2 };
            yield return new object[] { 0, 1, 1, 0, 2 };
            yield return new object[] { 0, 1, 0, 1, 2 };
            yield return new object[] { 0, 0, 1, 1, Math.Sqrt(12) };
            yield return new object[] { 1, 1, 1, 0, Math.Sqrt(7) };
            yield return new object[] { 1, 1, 0, 1, Math.Sqrt(7) };
            yield return new object[] { 1, 0, 1, 1, Math.Sqrt(13) };
            yield return new object[] { 0, 1, 1, 1, Math.Sqrt(13) };
            yield return new object[] { 1, 1, 1, 1, 4 };
        }
        [MemberData(nameof(CriticalBucklingForceParameters))]
        [Theory(DisplayName = "Feature: CalculateCriticalBucklingForce.  | Given: Valid parameters. |When: Call Method.|Should: Return a valid value to critical buckling force. ")]
        public void CalculateCriticalBucklingForce_ValidParameters_Should_ReturnValidValue(double youngModulus, double momentOfInertia, double length, double expectedValue)
        {
            // Act  
            double result = this._operation.CalculateCriticalBucklingForce(double youngModulus, double momentOfInertia, double length,);

            // Assert
            result.Should().BeApproximately(expectedValue, _precision);

        }
}
/// O que precisamos fazer:
/// Revisar como funciona a classe **RectangularProfileMechanicsOfMaterials**. FEITO
/// Testar método CalculateNormalStress para caso de falha. FEITO
/// Testar método Calculate Normal Stress para o caso de dados válidos. FEITO
/// Testar método CalculateEquivalentStress. FEITO
/// Testar método CalculateCriticalBucklingForce para caso de falha.
/// Testar método CalculateCriticalBucklingForce para o caso de dados válidos. PROGRESSO :)
/// Testar método CalculateColumnEffectiveLengthFactor para caso de falha.
/// Testar método CalculateColumnEffectiveLengthFactor para o caso de dados válidos.
/// **[OPCIONAL]** Testar método GenerateResult que recebe SuspensionAArm para o caso de dados válidos.
/// **[OPCIONAL]** Testar método GenerateResult que recebe TieRod para caso de falha.
/// **[OPCIONAL]** Testar método GenerateResult que recebe TieRod para o caso de dados válidos.