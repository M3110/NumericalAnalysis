using FluentAssertions;
using Moq;
using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials.CircularProfile;
using SuspensionAnalysis.Core.GeometricProperties.CircularProfile;
using SuspensionAnalysis.Core.Models;
using SuspensionAnalysis.DataContracts.Models.Enums;
using System;
using System.Collections.Generic;
using Xunit;
using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.UnitTest.Core.ConstitutiveEquations.MechanicsOfMaterials.CircularProfile
{
    public class CircularProfileMechanicsOfMaterialsTest
    {
        private readonly CircularProfileMechanicsOfMaterials _operation;
        private readonly Mock<ICircularProfileGeometricProperty> _geometricPropertyMock;

        private const double _precision = 1e-3;

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
        public void CircularProfileMechanicsOfMaterials_NullGeometricProperty_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new CircularProfileMechanicsOfMaterials(null);

            // Assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [MemberData(nameof(EquivalentStressParameters))]
        [Theory(DisplayName = "Feature: CalculateEquivalentStress | Given: Valid parameters. | When: Call method. | Should: Return a valid value to equivalent stress.")]
        public void CalculateEquivalentStress_ValidParameters_Should_ReturnValidValue(double normalStress, double flexuralStress, double shearStress, double torsionalStress, double expected)
        {
            // Act
            double result = this._operation.CalculateEquivalentStress(normalStress, flexuralStress, shearStress, torsionalStress);

            // Assert
            result.Should().BeApproximately(expected, _precision);
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
            result.Should().BeApproximately(expectedValue, _precision);
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

        [MemberData(nameof(CriticalBucklingForceParameters))]
        [Theory(DisplayName = "Feature: CalculateCriticalBucklingForce | Given: Valid parameters. | When: Call method. | Should: Return valid parameters.")]
        public void CalculateCriticalBucklingForce_ValidParameters_Should_ReturnValidParameters(double youngModulus, double momentOfInertia, double length, FasteningType fasteningType, double expected)
        {
            // Act
            double result = this._operation.CalculateCriticalBucklingForce(youngModulus, momentOfInertia, length, fasteningType);

            // Assert
            result.Should().BeApproximately(expected, _precision);
        }

        [MemberData(nameof(CalculateCriticalBucklingForceInvalidMomentOfInertiaAndLength))]
        [Theory(DisplayName = "Feature: CalculateCriticalBucklingForce | Given: Invalid parameters. | When: Call method. | Should: Throw new ArgumentOutOfRangeException.")]
        public void CalculateCriticalBucklingForce_InvalidParameters_Should_ThrowArgumentOutOfRangeException(double momentOfInertia, double length)
        {
            // Act
            Action act = () => this._operation.CalculateCriticalBucklingForce(youngModulus: 1, momentOfInertia, length);

            // Assert
            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [MemberData(nameof(ColumnEffectiveLengthFactorParameters))]
        [Theory(DisplayName = "Feature: CalculateColumnEffectiveLengthFactor | Given: Valid fastening type. | When: Call method. | Should: Return valid parameters.")]
        public void CalculateColumnEffectiveLengthFactor_ValidFasteningType_Should_ReturnValidParameters(FasteningType fasteningType, double expected)
        {
            // Act
            double result = this._operation.CalculateColumnEffectiveLengthFactor(fasteningType);

            // Assert
            result.Should().BeApproximately(expected, _precision);
        }

        [Fact(DisplayName = "Feature: CalculateColumnEffectiveLengthFactor | Given: Invalid parameters. | When: Call method. | Should: Throw new ArgumentOutOfRangeException.")]
        public void CalculateColumnEffectiveLengthFactor_InvalidFasteningType_Should_ThrowArgumentOutOfRangeException()
        {
            // Act
            Action act = () => this._operation.CalculateColumnEffectiveLengthFactor(default);

            // Assert
            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
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

        public static IEnumerable<object[]> CriticalBucklingForceParameters()
        {
            yield return new object[] { 1, 1, 1, FasteningType.BothEndPinned, Math.Pow(Math.PI, 2) };
            yield return new object[] { 0.5, 0.5, 0.5, FasteningType.BothEndFixed, 4 * Math.Pow(Math.PI, 2) };
        }

        public static IEnumerable<object[]> CalculateCriticalBucklingForceInvalidMomentOfInertiaAndLength()
        {
            List<double> invalidLengthList = new List<double>(Constants.InvalidValues) { 0, -1 };
            List<double> invalidMomenOfInertiaList = new List<double>(Constants.InvalidValues) { 0, -1 };

            foreach (double length in invalidLengthList)
            {
                foreach (double momentOfInertia in invalidMomenOfInertiaList)
                {
                    yield return new object[] { momentOfInertia, length };
                }
            }
        }

        public static IEnumerable<object[]> ColumnEffectiveLengthFactorParameters()
        {
            yield return new object[] { FasteningType.BothEndPinned, 1 };
            yield return new object[] { FasteningType.BothEndFixed, 0.5 };
            yield return new object[] { FasteningType.OneEndFixedOneEndPinned, Math.Sqrt(2) / 2 };
            yield return new object[] { FasteningType.OneEndFixed, 2 };
        }
    }
}
