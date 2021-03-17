using Moq;
using SuspensionAnalysis.Core.ConstitutiveEquations.MechanicsOfMaterials.CircularProfile;
using SuspensionAnalysis.Core.Mapper;
using SuspensionAnalysis.Core.Operations.CalculateReactions;
using SuspensionAnalysis.Core.Operations.RunAnalysis.CircularProfile;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.DataContracts.RunAnalysis;
using SuspensionAnalysis.UnitTest.Helper.DataContracts.RunAnalysis;
using System;
using DataContract = SuspensionAnalysis.DataContracts.Models.Profiles;

namespace SuspensionAnalysis.UnitTest.Core.Operations.RunAnalysis.CircularProfile
{
    public class RunCircularProfileAnalysisTest
    {
        private readonly RunCircularProfileAnalysis _operation;
        private readonly RunAnalysisRequest<DataContract.CircularProfile> _requestStub;

        private readonly Mock<ICalculateReactions> _calculateReactionsMock;
        private readonly Mock<ICircularProfileMechanicsOfMaterials> _mechanicsOfMaterialsMock;
        private readonly Mock<IMappingResolver> _mappingResolverMock;

        public RunCircularProfileAnalysisTest()
        {
            this._requestStub = RunAnalysisRequestHelper.CircularProfile;

            this._calculateReactionsMock = new Mock<ICalculateReactions>();
            this._calculateReactionsMock
                .Setup(cr => cr.Process(It.IsAny<CalculateReactionsRequest>()))
                .ReturnsAsync(() =>
                {
                    var response = new CalculateReactionsResponse
                    {
                        Data = new CalculateReactionsResponseData
                        {
                            AArmLowerReaction1 = new Force { AbsolutValue = Math.Sqrt(3), X = 1, Y = 1, Z = 1 },
                            AArmLowerReaction2 = new Force { AbsolutValue = Math.Sqrt(3), X = 1, Y = 1, Z = 1 },
                            AArmUpperReaction1 = new Force { AbsolutValue = Math.Sqrt(3), X = 1, Y = 1, Z = 1 },
                            AArmUpperReaction2 = new Force { AbsolutValue = Math.Sqrt(3), X = 1, Y = 1, Z = 1 },
                            ShockAbsorberReaction = new Force { AbsolutValue = Math.Sqrt(3), X = 1, Y = 1, Z = 1 },
                            TieRodReaction = new Force { AbsolutValue = Math.Sqrt(3), X = 1, Y = 1, Z = 1 }
                        }
                    };
                    response.SetSuccessOk();

                    return response;
                });

            this._mappingResolverMock = new Mock<IMappingResolver>();
            this._mappingResolverMock
                .Setup(mr => mr.MapFrom(this._requestStub, It.IsAny<CalculateReactionsResponseData>()));

            this._mechanicsOfMaterialsMock = new Mock<ICircularProfileMechanicsOfMaterials>();

            this._operation = new RunCircularProfileAnalysis(
                this._calculateReactionsMock.Object, 
                this._mechanicsOfMaterialsMock.Object, 
                this._mappingResolverMock.Object);
        }
    }
}
