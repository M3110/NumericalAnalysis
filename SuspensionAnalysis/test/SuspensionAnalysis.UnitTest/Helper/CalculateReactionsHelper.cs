using SuspensionAnalysis.Core.Models.SuspensionComponents;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.DataContracts.Models;
using SuspensionAnalysis.DataContracts.Models.SuspensionComponents;
using ShockAbsorber = SuspensionAnalysis.Core.Models.SuspensionComponents.ShockAbsorber;

namespace SuspensionAnalysis.UnitTest.Helper
{
    /// <summary>
    /// It contains method and properties to help testing the CalculateReactions operation.
    /// </summary>
    public static class CalculateReactionsHelper
    {
        public static Point3D CreateOrigin()
        {
            return new Point3D { X = 0, Y = 0.75, Z = 0 };
        }

        public static SuspensionSystem CreateSuspensionSystem()
        {
            return new SuspensionSystem
            {
                ShockAbsorber = new ShockAbsorber
                {
                    FasteningPoint = new Point3D { X = -0.005, Y = 0.645, Z = 0.180 },
                    PivotPoint = new Point3D { X = -0.005, Y = 0.485, Z = 0.430 }
                },
                SuspensionAArmLower = new SuspensionAArm
                {
                    KnucklePoint = new Point3D { X = -0.012, Y = 0.685, Z = 0.150 },
                    PivotPoint1 = new Point3D { X = -0.100, Y = 0.350, Z = 0.130 },
                    PivotPoint2 = new Point3D { X = 0.250, Y = 0.350, Z = 0.150 }
                },
                SuspensionAArmUpper = new SuspensionAArm
                {
                    KnucklePoint = new Point3D { X = 0.012, Y = 0.660, Z = 0.410 },
                    PivotPoint1 = new Point3D { X = -0.080, Y = 0.450, Z = 0.362 },
                    PivotPoint2 = new Point3D { X = 0.200, Y = 0.450, Z = 0.362 }
                },
                TieRod = new TieRod
                {
                    FasteningPoint = new Point3D { X = -0.120, Y = 0.668, Z = 0.200 },
                    PivotPoint = new Point3D { X = -0.125, Y = 0.370, Z = 0.176 }
                }
            };
        }


        public static CalculateReactionsRequest CreateRequest()
        {
            return new CalculateReactionsRequest
            {
                Origin = "0,0.75,0",
                ShouldRoundResults = false,
                AppliedForce = "1000,-1000,1000",
                ShockAbsorber = new ShockAbsorberPoint
                {
                    FasteningPoint = "-0.005,0.645,0.180",
                    PivotPoint = "-0.005,0.485,0.430"
                },
                SuspensionAArmLower = new SuspensionAArmPoint
                {
                    KnucklePoint = "-0.012,0.685,0.150",
                    PivotPoint1 = "-0.100,0.350,0.130",
                    PivotPoint2 = "0.250,0.350,0.150"
                },
                SuspensionAArmUpper = new SuspensionAArmPoint
                {
                    KnucklePoint = "0.012,0.660,0.410",
                    PivotPoint1 = "-0.080,0.450,0.362",
                    PivotPoint2 = "0.200,0.450,0.362"
                },
                TieRod = new TieRodPoint
                {
                    FasteningPoint = "-0.120,0.668,0.200",
                    PivotPoint = "-0.125,0.370,0.176"
                }
            };
        }

        public static CalculateReactionsResponse CreateResponse()
        {
            var response = new CalculateReactionsResponse { Data = CreateResponseData() };
            response.SetSuccessOk();

            return response;
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