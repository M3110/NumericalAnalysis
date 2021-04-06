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
        private readonly Operation.CalculateReactions _operation;

        private readonly Mock<IMappingResolver> _mappingResolverMock;

        private readonly CalculateReactionsRequest _requestStub;

        private readonly SuspensionSystem _suspensionSystem;

        private readonly CalculateReactionsResponse _expectedResponse;

        private const double _precision = 1e-3;

        public CalculateReactionsTest()
        {
            this._requestStub = CalculateReactionsHelper.CreateRequest();

            this._mappingResolverMock = new Mock<IMappingResolver>();
            this._mappingResolverMock
                .Setup(mr => mr.MapFrom(this._requestStub))
                .Returns(_suspensionSystem);
                
            this._operation = new Operation.CalculateReactions(this._mappingResolverMock.Object);
        }

        [MemberData(nameof(DisplacementMatrixParameters))]
        public void BuildDisplacementMatrix_ValidParameters_Should_ReturnValidMatrix(SuspensionSystem suspensionSystem, Point3D origin, double _expectedResponse, double result)
        {
            // Act
            double[,] response = this._operation.BuildDisplacementMatrix(suspensionSystem, origin);

            // Assert
            result.Should().BeApproximately(_expectedResponse, _precision);
        }
    }
}