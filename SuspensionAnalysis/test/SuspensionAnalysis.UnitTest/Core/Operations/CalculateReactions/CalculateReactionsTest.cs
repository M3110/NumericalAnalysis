using FluentAssertions;
using Moq;
using SuspensionAnalysis.Core.Mapper;
using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.UnitTest.Helper;
using Xunit;
using Operation = SuspensionAnalysis.Core.Operations.CalculateReactions;

namespace SuspensionAnalysis.UnitTest.Core.Operations.CalculateReactions
{
    public class CalculateReactionsTest
    {
        private readonly Mock<IMappingResolver> _mappingResolverMock;

        private readonly CalculateReactionsRequest _requestStub;
        private readonly SuspensionSystem _suspensionSystem;
        private readonly Vector3D _forceApplied;
        private readonly CalculateReactionsResponse _expectedResponse;
        private readonly Operation.CalculateReactions _operation;

        public CalculateReactionsTest()
        {
            this._requestStub = CalculateReactionsHelper.CreateRequest();

            this._expectedResponse = CalculateReactionsHelper.CreateResponse();

            this._suspensionSystem = CalculateReactionsHelper.CreateSuspensionSystem();

            this._forceApplied = Vector3D.Create(_requestStub.ForceApplied);

            this._mappingResolverMock = new Mock<IMappingResolver>();
            this._mappingResolverMock
                .Setup(mr => mr.MapFrom(this._requestStub))
                .Returns(this._suspensionSystem);

            this._operation = new Operation.CalculateReactions(this._mappingResolverMock.Object);
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
            // Act
            double[] result = this._operation.BuildReactionVector(this._forceApplied);

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