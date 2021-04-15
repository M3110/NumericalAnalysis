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
using SuspensionAnalysis.Core.ExtensionMethods;

namespace SuspensionAnalysis.UnitTest.Core.Operations.CalculateReactions
{
    public class CalculateReactionsTest
    {
        private readonly Mock<IMappingResolver> _mappingResolverMock;

        private readonly CalculateReactionsRequest _requestStub;
        private readonly SuspensionSystem _suspensionSystem;
        private readonly Operation.CalculateReactions _operation;
        private readonly CalculateReactionsResponse _expectedResponse;

        public CalculateReactionsTest()
        {
            this._requestStub = CalculateReactionsHelper.CreateRequest();

            this._suspensionSystem = CalculateReactionsHelper.CreateSuspensionSystem();
            
            this._expectedResponse = CalculateReactionsHelper.CreateResponse();

            this._mappingResolverMock = new Mock<IMappingResolver>();
            this._mappingResolverMock
                .Setup(mr => mr.MapFrom(this._requestStub))
                .Returns(this._suspensionSystem);

            this._operation = new Operation.CalculateReactions(this._mappingResolverMock.Object);
        }

        [Theory(DisplayName = "Feature: ValidateOperationAsync | Given: Invalid parameters. | When: Call method. | Should: Return a failure for a bad request.")]
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

        [Fact(DisplayName = "Feature: ValidateOperationAsync | Given: Invalid parameters. | When: Call method. | Should: Return a failure for a bad request.")]
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

        [Fact(DisplayName = "Feature: BuildDisplacementMatrix | Given: Valid parameters. | When: Call method. | Should: Return a valid displacement matrix.")]
        public void BuildDisplacementMatrix_ValidParameters_Should_Return_ValidMatrix()
        {
            // Arrange 
            Point3D origin = CalculateReactionsHelper.CreateOrigin();

            // Act
            double[,] result = this._operation.BuildDisplacementMatrix(this._suspensionSystem, origin);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().NotContain(0);
        }

        [Fact(DisplayName = "Feature: BuildEffortsVector | Given: Valid parameters. | When: Call method. | Should: Return valid vector for the efforts.")]
        public void BuildEffortsVector_ValidParameters_Should_Return_ValidVector()
        {
            // Arrange
            Vector3D appliedForce = Vector3D.Create(this._requestStub.AppliedForce);
            var effortExpected = new double[] { appliedForce.X, appliedForce.Y, appliedForce.Z, 0, 0, 0 };

            // Act
            double[] result = this._operation.BuildEffortsVector(appliedForce);

            // Assert
            result.Should().BeEquivalentTo(effortExpected);
        }

        //[Fact(DisplayName = "Feature: MapToResponse | Given: Valid parameters. | When: ShouldRound is true. | Should: Return valid reactions for components of suspension system.")]
        //public void MapToResponse_ShouldRound_ValidParameters_Should_Return_ValidReactions()
        //{
        //    // Arrange
        //    double[,] displacement = this._operation.BuildDisplacementMatrix(this._suspensionSystem, _origin);

        //    double[] result = displacement
        //            .InverseMatrix()
        //            .Multiply(_effortExpected);

        //    // Act
        //    CalculateReactionsResponseData response = this._operation.MapToResponse(_suspensionSystem, result, shouldRound, decimals);

        //    // Assert
        //    response.Should().BeApproximately(_expectedResponse.Data,decimals);
        //    response.Should().NotBeNull();
        //    response.Should().NotBe(0);
        //}

        [Fact(DisplayName = "Feature: MapToResponse | Given: Valid parameters. | When: ShouldRound is false. | Should: Return valid reactions for components of suspension system.")]
        public void MapToResponse_ValidParameters_When_ShouldRound_Is_False_Should_Return_ValidReactions()
        {
            // Arrange
            double[] reactions = new double[6] {-706.844136886457, 2318.54871728814, 410.390832183452, -693.435739188224, 1046.35331054682, -568.377481091224 };
            double precision = 1e-6;

            // Act
            CalculateReactionsResponseData respondeData = this._operation.MapToResponse(this._suspensionSystem, reactions, false, null);

            // Assert
            respondeData.Should().NotBeNull();
            respondeData.AArmLowerReaction1.AbsolutValue.Should().BeApproximately(this._expectedResponse.Data.AArmLowerReaction1.AbsolutValue, precision);
            respondeData.AArmLowerReaction2
        }

        [Fact(DisplayName = "Feature: ProcessAsync | Given: Valid parameters. | When: Call method. | Should: Return expected response.")]
        public async Task ProcessAsync_ValidParameters_Should_ReturnExpectedResponse()
        {
            // Act
            CalculateReactionsResponse response = await this._operation.ProcessAsync(this._requestStub).ConfigureAwait(false);

            //Assert
            response.Should().BeEquivalentTo(this._expectedResponse);
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}