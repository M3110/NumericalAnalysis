using FluentAssertions;
using Moq;
using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials.CircularProfile;
using SuspensionAnalysis.Core.GeometricProperties.CircularProfile;
using System;
using System.Collections;
using System.Collections.Generic;
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

        [Fact(DisplayName = "Feature: CircularProfileMechanicsOfMaterials | Given: Null GeometricProperty. | When: Instantiate class. | Should: Throw an ArgumentNullException.")]
        public void AddChangeLog_NullLogger_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new CircularProfileMechanicsOfMaterials(null);

            // Assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [ClassData(typeof(EquivalentStressParameters))]
        [Theory(DisplayName = "")]
        public void CalculateEquivalentStress_ValidParameters_Should_ReturnValidValue(double normalStress, double flexuralStress, double shearStress, double torsionalStress, double expected)
        {
            // Act
            double result = this._operation.CalculateEquivalentStress(normalStress, flexuralStress, shearStress, torsionalStress);

            // Assert
            result.Should().BeApproximately(expected, precision: 1e-3);
        }

        [InlineData(0, 1, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(1000, 0.5, 2000)]
        [InlineData(1000, 7.92e-4, 1262626.263)]
        [Theory(DisplayName = "Feature: CalculateNormalStress | Given: Valid parameters. | When: Call method. | Should: Return a valid value to normal stress.")]
        public void CalculateNormalStress_ValidParameters_Should_ReturnValidValue(double normalForce, double area, double expectedValue)
        {
            // Act
            double result = this._operation.CalculateNormalStress(normalForce, area);

            // Assert
            result.Should().BeApproximately(expectedValue, precision: 1e-3);
        }

        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.MaxValue)]
        [InlineData(double.MinValue)]
        [Theory(DisplayName = "Feature: CalculateNormalStress | Given: Invalid area. | When: Call method. | Should: Throw exception.")]
        public void CalculateNormalStress_InvalidArea_Should_ThrowException(double area)
        {
            // Act
            Action act = () => this._operation.CalculateNormalStress(normalForce: 1, area);

            // Assert
            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
    }

    public class EquivalentStressParameters : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { 0, 0, 0, 0, 0 },
            new object[] { 1, 0, 0, 0, 1 },
            new object[] { 0, 1, 0, 0, 1 },
            new object[] { 0, 0, 1, 0, Math.Sqrt(3) },
            new object[] { 0, 0, 0, 1, Math.Sqrt(3) },
            new object[] { 1, 1, 0, 0, 2 },
            new object[] { 1, 0, 1, 0, 2 },
            new object[] { 1, 0, 0, 1, 2 },
            new object[] { 0, 1, 1, 0, 2 },
            new object[] { 0, 1, 0, 1, 2 },
            new object[] { 0, 0, 1, 1, Math.Sqrt(12) },
            new object[] { 1, 1, 1, 0, Math.Sqrt(7) },
            new object[] { 1, 1, 0, 1, Math.Sqrt(7) },
            new object[] { 1, 0, 1, 1, Math.Sqrt(13) },
            new object[] { 0, 1, 1, 1, Math.Sqrt(13) },
            new object[] { 1, 1, 1, 1, 4 },
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
