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
        private readonly CalculateReactionsResponse _expectedResponse;
        private readonly Operation.CalculateReactions _operation;

        public CalculateReactionsTest()
        {
            this._requestStub = CalculateReactionsHelper.CreateRequest();

            this._expectedResponse = CalculateReactionsHelper.CreateResponse();

            this._suspensionSystem = CalculateReactionsHelper.CreateSuspensionSystem();

            this._mappingResolverMock = new Mock<IMappingResolver>();
            this._mappingResolverMock
                .Setup(mr => mr.MapFrom(this._requestStub))
                .Returns(this._suspensionSystem);
                
            this._operation = new Operation.CalculateReactions(this._mappingResolverMock.Object);
        }

        [Fact]
        public void BuildDisplacementMatrix_ValidParameters_Should_ReturnValidMatrix()
        {
            // Act
            double[,] result = this._operation.BuildDisplacementMatrix(this._suspensionSystem, CalculateReactionsHelper.CreateOrigin());

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().NotContain(0);
        }
    }
}