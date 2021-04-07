using FluentAssertions;
using Moq;
using SuspensionAnalysis.Core.Mapper;
using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.UnitTest.Helper;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Operation = SuspensionAnalysis.Core.Operations.CalculateReactions;

namespace SuspensionAnalysis.UnitTest.Core.Operations.CalculateReactions
{
    public class CalculateReactionsTest
    {
        private readonly Mock<IMappingResolver> _mappingResolverMock;

        private readonly CalculateReactionsRequest _requestStub;
        private readonly SuspensionSystem _suspensionSystem;
        private readonly CalculateReactionsResponse _expectedResponse;
        private readonly Operation.CalculateReactions _operation;
        private readonly Vector3D _forceApplied;

        public CalculateReactionsTest()
        {
            this._requestStub = CalculateReactionsHelper.CreateRequest();

            this._expectedResponse = CalculateReactionsHelper.CreateResponse();

            this._suspensionSystem = CalculateReactionsHelper.CreateSuspensionSystem();

            this._forceApplied = Vector3D.Create(this._requestStub.AppliedForce);

            this._mappingResolverMock = new Mock<IMappingResolver>();
            this._mappingResolverMock
                .Setup(mr => mr.MapFrom(this._requestStub))
                .Returns(this._suspensionSystem);

            this._operation = new Operation.CalculateReactions(this._mappingResolverMock.Object);
        }

        [Theory]
        [InlineData("0,0,0")]
        [InlineData("0.0,0,0")]
        [InlineData("0.0,0.0,0")]
        [InlineData("0.0,0.0,0.0")]
        public async Task ValidateOperationAsync_InvalidForce_Should_ReturnBadRequest(string invalidForce)
        {
            // Arrange
            this._requestStub.AppliedForce = invalidForce;

            // Act
            CalculateReactionsResponse response = await this._operation.ValidateOperationAsync(this._requestStub).ConfigureAwait(false);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeFalse();
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Errors.Should().HaveCountGreaterOrEqualTo(1);
        }

        [Fact]
        public async Task ValidateOperationAsync_NullRequest_Should_ReturnBadRequest()
        {
            // Act
            CalculateReactionsResponse response = await this._operation.ValidateOperationAsync(null).ConfigureAwait(false);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeFalse();
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Errors.Should().HaveCountGreaterOrEqualTo(1);
        }

        [Fact]
        public void BuildDisplacementMatrix_ValidParameters_Should_Return_ValidMatrix()
        {
            // Act
            double[,] result = this._operation.BuildDisplacementMatrix(this._suspensionSystem, CalculateReactionsHelper.CreateOrigin());

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().NotContain(0);
        }

        [Fact]
        public void BuildReactionVector_ValidParameters_Should_Return_ValidVector()
        {
            // Arrange
            var effortExpected = new double[] { this._forceApplied.X, this._forceApplied.Y, this._forceApplied.Z, 0, 0, 0 };

            // Act
            double[] result = this._operation.BuildEffortVector(this._forceApplied);

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void MapToResponse_ShouldRound_ValidParameters_Should_Return_ValidReactions()
        {
            // Act


            // Assert
        }
    }
}