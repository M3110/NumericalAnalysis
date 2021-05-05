using SuspensionAnalysis.DataContracts.Models.Enums;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using SuspensionAnalysis.DataContracts.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.RunAnalysis;

namespace SuspensionAnalysis.UnitTest.Helper.DataContracts
{
    /// <summary>
    /// It contains method and properties to help the testing the RunAnalysis operation.
    /// </summary>
    public static class RunAnalysisHelper
    {
        public static RunAnalysisRequest<CircularProfile> CreateCircularProfileRequest()
        {
            return new RunAnalysisRequest<CircularProfile>
            {
                Origin = "0,0.75,0",
                NumberOfDecimalsToRound = 2,
                ShouldRoundResults = true,
                Material = MaterialType.Steel1020,
                ForceApplied = "0,0,1000",
                ShockAbsorber = new ShockAbsorber
                {
                    FasteningPoint = "-0.005,0.645,0.180",
                    PivotPoint = "-0.005,0.485,0.430"
                },
                LowerWishbone = new SuspensionWishbone<CircularProfile>
                {
                    Profile = new CircularProfile
                    {
                        Diameter = 25.4e-3,
                        Thickness = 0.9e-3
                    },
                    WishboneOuterBallJoint = "-0.012,0.685,0.150",
                    WishboneFrontPivot = "0.250,0.350,0.150",
                    WishboneRearPivot = "-0.100,0.350,0.130"
                },
                UpperWishbone = new SuspensionWishbone<CircularProfile>
                {
                    Profile = new CircularProfile
                    {
                        Diameter = 25.4e-3,
                        Thickness = 0.9e-3
                    },
                    WishboneOuterBallJoint = "0.012,0.660,0.410",
                    WishboneFrontPivot = "0.200,0.450,0.362",
                    WishboneRearPivot = "-0.080,0.450,0.362"
                },
                TieRod = new TieRod<CircularProfile>
                {
                    Profile = new CircularProfile
                    {
                        Diameter = 25.4e-3,
                        Thickness = 0.9e-3
                    },
                    PivotPoint = "-0.125,0.370,0.176",
                    FasteningPoint = "-0.120,0.668,0.200"
                }
            };
        }

        public static RunAnalysisRequest<RectangularProfile> CreateRectangularProfileRequest()
        {
            return new RunAnalysisRequest<RectangularProfile>
            {
                Origin = "0,0,0",
                NumberOfDecimalsToRound = 2,
                ShouldRoundResults = true,
                Material = MaterialType.Steel1020,
                ForceApplied = "0,0,1000",
                ShockAbsorber = new ShockAbsorber
                {
                    FasteningPoint = "-0.005,0.645,0.180",
                    PivotPoint = "-0.005,0.485,0.430"
                },
                LowerWishbone = new SuspensionWishbone<RectangularProfile>
                {
                    Profile = new RectangularProfile
                    {
                        Height = 10e-3,
                        Width = 10e-3,
                        Thickness = 1e-3
                    },
                    WishboneOuterBallJoint = "-0.012,0.685,0.150",
                    WishboneFrontPivot = "0.250,0.350,0.150",
                    WishboneRearPivot = "-0.100,0.350,0.130"
                },
                UpperWishbone = new SuspensionWishbone<RectangularProfile>
                {
                    Profile = new RectangularProfile
                    {
                        Height = 10e-3,
                        Width = 10e-3,
                        Thickness = 1e-3
                    },
                    WishboneOuterBallJoint = "0.012,0.660,0.410",
                    WishboneFrontPivot = "0.200,0.450,0.362",
                    WishboneRearPivot = "-0.080,0.450,0.362"
                },
                TieRod = new TieRod<RectangularProfile>
                {
                    Profile = new RectangularProfile
                    {
                        Height = 10e-3,
                        Width = 10e-3,
                        Thickness = 1e-3
                    },
                    PivotPoint = "-0.125,0.370,0.176",
                    FasteningPoint = "-0.120,0.668,0.200"
                }
            };
        }
    }
}
