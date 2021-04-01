using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.DataContracts.Models.SuspensionComponents;

namespace SuspensionAnalysis.UnitTest.Helper
{
    /// <summary>
    /// It contains method and properties to help the testing the CalculateReactions operation.
    /// </summary>
    public static class CalculateReactionsHelper
    {
        public static CalculateReactionsRequest CreateRequest()
        {
            return new CalculateReactionsRequest
            {
                Origin = "0,0.75,0",
                NumberOfDecimalsToRound = 2,
                ShouldRoundResults = true,
                ForceApplied = "0,0,1000",
                ShockAbsorber = new ShockAbsorberPoint
                {
                    FasteningPoint = "-0.005,0.645,0.180",
                    PivotPoint = "-0.005,0.485,0.430"
                },
                SuspensionAArmLower = new SuspensionAArmPoint
                {
                    KnucklePoint = "-0.012,0.685,0.150",
                    PivotPoint1 = "0.250,0.350,0.150",
                    PivotPoint2 = "-0.100,0.350,0.130"
                },
                SuspensionAArmUpper = new SuspensionAArmPoint
                {
                    KnucklePoint = "0.012,0.660,0.410",
                    PivotPoint1 = "0.200,0.450,0.362",
                    PivotPoint2 = "-0.080,0.450,0.362"
                },
                TieRod = new TieRodPoint
                {
                    PivotPoint = "-0.125,0.370,0.176",
                    FasteningPoint = "-0.120,0.668,0.200"
                }
            };
        }

        public static CalculateReactionsResponseData CreateResponseData()
        {
            return new CalculateReactionsResponseData
            {
                AArmLowerReaction1 = new Force
                {
                    AbsolutValue = -706.844136886457
                },
                AArmLowerReaction2 = new Force
                {
                    AbsolutValue = 2318.54871728814
                },
                AArmUpperReaction1 = new Force
                {
                    AbsolutValue = 410.390832183452
                },
                AArmUpperReaction2 = new Force
                {
                    AbsolutValue = -693.435739188224
                },
                ShockAbsorberReaction = new Force
                {
                    AbsolutValue = 1046.35331054682
                },
                TieRodReaction = new Force
                {
                    AbsolutValue = -568.377481091224
                }
            };
        }
    }
}
