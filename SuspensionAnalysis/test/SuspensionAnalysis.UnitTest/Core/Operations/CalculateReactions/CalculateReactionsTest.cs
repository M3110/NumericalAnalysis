using Moq;
using SuspensionAnalysis.Core.Mapper;
using Operation = SuspensionAnalysis.Core.Operations.CalculateReactions;

namespace SuspensionAnalysis.UnitTest.Core.Operations.CalculateReactions
{
    public class CalculateReactionsTest
    {
        private readonly Operation.CalculateReactions _operation;

        private readonly Mock<IMappingResolver> _mappingResolverMock;

        public CalculateReactionsTest()
        {
            this._mappingResolverMock = new Mock<IMappingResolver>();

            this._operation = new Operation.CalculateReactions(this._mappingResolverMock.Object);
        }
    }
}