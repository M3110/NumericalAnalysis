using Moq;
using SuspensionAnalysis.Core.Mapper;
using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.UnitTest.Helper;
using Operation = SuspensionAnalysis.Core.Operations.CalculateReactions;

namespace SuspensionAnalysis.UnitTest.Core.Operations.CalculateReactions
{
    public class CalculateReactionsTest
    {
        private readonly Operation.CalculateReactions _operation;

        private readonly Mock<IMappingResolver> _mappingResolverMock;

        private readonly CalculateReactionsRequest _requestStub;

        private readonly SuspensionSystem _suspensionSystem;

        public CalculateReactionsTest()
        {
            this._requestStub = CalculateReactionsHelper.CreateRequest();

            this._mappingResolverMock = new Mock<IMappingResolver>();
            this._mappingResolverMock
                .Setup(mr => mr.MapFrom(this._requestStub))
                .Returns(_suspensionSystem);
                
            this._operation = new Operation.CalculateReactions(this._mappingResolverMock.Object);
        }
    }
}