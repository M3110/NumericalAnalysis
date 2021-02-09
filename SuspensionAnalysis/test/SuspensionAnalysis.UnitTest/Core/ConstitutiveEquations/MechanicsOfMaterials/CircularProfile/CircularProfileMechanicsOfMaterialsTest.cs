using FluentAssertions;
using Moq;
using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials.CircularProfile;
using SuspensionAnalysis.Core.GeometricProperty.CircularProfile;
using System;
using Xunit;
using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.UnitTest.Core.ConstitutiveEquations.MechanicsOfMaterials.CircularProfile
{
    public class CircularProfileMechanicsOfMaterialsTest
    {
        private readonly CircularProfileMechanicsOfMaterials _operation;
        private readonly Mock<ICircularProfileGeometricProperty> _geometricPropertyMock;

        public CircularProfileMechanicsOfMaterialsTest()
        {
            this._geometricPropertyMock = new Mock<ICircularProfileGeometricProperty>();
            this._geometricPropertyMock
                .Setup(gp => gp.CalculateArea(It.IsAny<DataContract.CircularProfile>()))
                .Returns(1);
            this._geometricPropertyMock
                .Setup(gp => gp.CalculateMomentOfInertia(It.IsAny<DataContract.CircularProfile>()))
                .Returns(1);

            this._operation = new CircularProfileMechanicsOfMaterials(this._geometricPropertyMock.Object);
        }

        [Theory(DisplayName = @"Feature: CalculateNormalStress | Given: Valid parameters. | When: Call method. | Should: Return a valid value to normal stress.")]
        [InlineData(0, 1, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(1000, 0.5, 2000)]
        [InlineData(1000, 7.92e-4, 1262626.263)]
        public void CalculateNormalStress_ValidParameters_Should_ReturnValidValue(double normalForce, double area, double expectedValue)
        {
            // Act
            double result = this._operation.CalculateNormalStress(normalForce, area);

            // Assert
            result.Should().BeApproximately(expectedValue, precision: 1e-3);
        }

        [Theory(DisplayName = @"Feature: CalculateNormalStress | Given: Invalid area. | When: Call method. | Should: Throw exception.")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.MaxValue)]
        [InlineData(double.MinValue)]
        public void CalculateNormalStress_InvalidArea_Should_ThrowException(double area)
        {
            // Act
            Action act = () => this._operation.CalculateNormalStress(normalForce: 1, area);

            // Assert
            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
    }
}
