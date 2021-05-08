﻿using FluentAssertions;
using Moq;
using SuspensionAnalysis.Core.Mapper;
using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.UnitTest.Helper;
using System;
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
        private readonly CalculateReactionsResponse _expectedResponse;
        private readonly SuspensionSystem _suspensionSystem;
        private readonly double[] _reactions;
        private readonly Operation.CalculateReactions _operation;

        public CalculateReactionsTest()
        {
            this._requestStub = CalculateReactionsHelper.CreateRequest();

            this._expectedResponse = CalculateReactionsHelper.CreateResponse();

            this._suspensionSystem = CalculateReactionsHelper.CreateSuspensionSystem();

            this._reactions = new double[6] { 706.844136886457, -2318.54871728814, -410.390832183452, 693.435739188224, -1046.35331054682, 568.377481091224 };

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

        [Theory(DisplayName = "Feature: MapToResponseData | Given: Valid parameters. | When: ShouldRound is true. | Should: Return valid reactions for components of suspension system.")]
        [InlineData(0)]
        [InlineData(2)]
        public void MapToResponseData_ValidParameters_When_ShouldRound_Is_True_Should_Return_ValidReactions(int decimals)
        {
            // Arrange
            this._expectedResponse.Data.LowerWishboneReaction1.AbsolutValue = Math.Round(-this._reactions[0], decimals);
            this._expectedResponse.Data.LowerWishboneReaction2.AbsolutValue = Math.Round(-this._reactions[1], decimals);
            this._expectedResponse.Data.UpperWishboneReaction1.AbsolutValue = Math.Round(-this._reactions[2], decimals);
            this._expectedResponse.Data.UpperWishboneReaction2.AbsolutValue = Math.Round(-this._reactions[3], decimals);
            this._expectedResponse.Data.ShockAbsorberReaction.AbsolutValue = Math.Round(-this._reactions[4], decimals);
            this._expectedResponse.Data.TieRodReaction.AbsolutValue = Math.Round(-this._reactions[5], decimals);

            // Act
            CalculateReactionsResponseData responseData = this._operation.MapToResponseData(this._suspensionSystem, this._reactions, true, decimals);

            // Assert
            responseData.Should().NotBeNull();
            responseData.LowerWishboneReaction1.AbsolutValue.Should().Be(this._expectedResponse.Data.LowerWishboneReaction1.AbsolutValue);
            responseData.LowerWishboneReaction2.AbsolutValue.Should().Be(this._expectedResponse.Data.LowerWishboneReaction2.AbsolutValue);
            responseData.UpperWishboneReaction1.AbsolutValue.Should().Be(this._expectedResponse.Data.UpperWishboneReaction1.AbsolutValue);
            responseData.UpperWishboneReaction2.AbsolutValue.Should().Be(this._expectedResponse.Data.UpperWishboneReaction2.AbsolutValue);
            responseData.ShockAbsorberReaction.AbsolutValue.Should().Be(this._expectedResponse.Data.ShockAbsorberReaction.AbsolutValue);
            responseData.TieRodReaction.AbsolutValue.Should().Be(this._expectedResponse.Data.TieRodReaction.AbsolutValue);
        }

        [Fact(DisplayName = "Feature: MapToResponseData| Given: Valid parameters. | When: ShouldRound is false. | Should: Return valid reactions for components of suspension system.")]
        public void MapToResponseData_ValidParameters_When_ShouldRound_Is_False_Should_Return_ValidReactions()
        {
            // Act
            CalculateReactionsResponseData responseData = this._operation.MapToResponseData(this._suspensionSystem, this._reactions, false, null);

            // Assert
            responseData.Should().NotBeNull();
            responseData.LowerWishboneReaction1.AbsolutValue.Should().Be(this._expectedResponse.Data.LowerWishboneReaction1.AbsolutValue);
            responseData.LowerWishboneReaction2.AbsolutValue.Should().Be(this._expectedResponse.Data.LowerWishboneReaction2.AbsolutValue);
            responseData.UpperWishboneReaction1.AbsolutValue.Should().Be(this._expectedResponse.Data.UpperWishboneReaction1.AbsolutValue);
            responseData.UpperWishboneReaction2.AbsolutValue.Should().Be(this._expectedResponse.Data.UpperWishboneReaction2.AbsolutValue);
            responseData.ShockAbsorberReaction.AbsolutValue.Should().Be(this._expectedResponse.Data.ShockAbsorberReaction.AbsolutValue);
            responseData.TieRodReaction.AbsolutValue.Should().Be(this._expectedResponse.Data.TieRodReaction.AbsolutValue);
        }

        [Fact(DisplayName = "Feature: ProcessAsync | Given: Valid parameters. | When: Call method. | Should: Return expected response.")]
        public async Task ProcessAsync_ValidParameters_Should_ReturnExpectedResponse()
        {
            // Arrange
            double precision = 1e-6;

            // Act
            CalculateReactionsResponse response = await this._operation.ProcessAsync(this._requestStub).ConfigureAwait(false);

            //Assert
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(this._expectedResponse.HttpStatusCode);
            response.Success.Should().Be(this._expectedResponse.Success);
            response.Errors.Should().BeEquivalentTo(this._expectedResponse.Errors);
            response.Data.Should().NotBeNull();
            response.Data.LowerWishboneReaction1.AbsolutValue.Should().BeApproximately(this._expectedResponse.Data.LowerWishboneReaction1.AbsolutValue, precision);
            response.Data.LowerWishboneReaction2.AbsolutValue.Should().BeApproximately(this._expectedResponse.Data.LowerWishboneReaction2.AbsolutValue, precision);
            response.Data.UpperWishboneReaction1.AbsolutValue.Should().BeApproximately(this._expectedResponse.Data.UpperWishboneReaction1.AbsolutValue, precision);
            response.Data.UpperWishboneReaction2.AbsolutValue.Should().BeApproximately(this._expectedResponse.Data.UpperWishboneReaction2.AbsolutValue, precision);
            response.Data.ShockAbsorberReaction.AbsolutValue.Should().BeApproximately(this._expectedResponse.Data.ShockAbsorberReaction.AbsolutValue, precision);
            response.Data.TieRodReaction.AbsolutValue.Should().BeApproximately(this._expectedResponse.Data.TieRodReaction.AbsolutValue, precision);
        }
    }
}