using SuspensionAnalysis.Core.ExtensionMethods;
using SuspensionAnalysis.Core.Models;
using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using SuspensionAnalysis.Infrastructure.Models;
using System.Threading.Tasks;

namespace SuspensionAnalysis.Core.Operations.CalculateReactions
{
    public class CalculateReactions : OperationBase<CalculateReactionsRequest, CalculateReactionsResponse, CalculateReactionsResponseData>, ICalculateReactions
    {
        public SuspensionNormalizedVector SuspensionNormalizedVector { get; set; }

        /// <summary>
        /// This method calculates the reactions to suspension system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<CalculateReactionsResponse> ProcessOperation(CalculateReactionsRequest request)
        {
            var response = new CalculateReactionsResponse { Data = new CalculateReactionsResponseData() };

            double[,] displacement = this.BuildDisplacementMatrix(request);
            double[] reaction = this.BuildReactionVector(request.ForceApplied);

            double[] result = displacement
                .InverseMatrix()
                .Multiply(reaction);

            response.Data = this.MapToResponse(result);

            return Task.FromResult(response);
        }

        public double[] BuildReactionVector(Point3D force) => new double[] { force.X, force.Y, force.Z, 0, 0, 0 };

        public double[,] BuildDisplacementMatrix(CalculateReactionsRequest request)
        {
            (Vector3D u1, Vector3D u2) = request.SuspensionAArmLower.CalculateNormalizedVectors();
            (Vector3D r1, Vector3D r2) = request.SuspensionAArmLower.CalculateOriginReferences(request.Origin);

            (Vector3D u3, Vector3D u4) = request.SuspensionAArmUpper.CalculateNormalizedVectors();
            (Vector3D r3, Vector3D r4) = request.SuspensionAArmUpper.CalculateOriginReferences(request.Origin);

            Vector3D u5 = request.ShockAbsorber.CalculateNormalizedVector();
            Vector3D r5 = request.ShockAbsorber.CalculateOriginReference(request.Origin);

            Vector3D u6 = request.TieRod.CalculateNormalizedVector();
            Vector3D r6 = request.TieRod.CalculateOriginReference(request.Origin);

            // This is necessary to use the vectors in another method.
            this.SuspensionNormalizedVector = new SuspensionNormalizedVector
            {
                SuspensionAArmLowerU1 = u1,
                SuspensionAArmLowerU2 = u2,
                SuspensionAArmUpperU3 = u3,
                SuspensionAArmUpperU4 = u4,
                ShockAbsorberU5 = u5,
                TieRodU6 = u6
            };

            return new double[6, 6]
            {
                { u1.X, u2.X, u3.X, u4.X, u5.X, u6.X },
                { u1.Y, u2.Y, u3.Y, u4.Y, u5.Y, u6.Y },
                { u1.Z, u2.Z, u3.Z, u4.Z, u5.Z, u6.Z },
                { r1.CrossProduct(u1).X, r2.CrossProduct(u2).X, r3.CrossProduct(u3).X, r4.CrossProduct(u4).X, r5.CrossProduct(u5).X, r6.CrossProduct(u6).X },
                { r1.CrossProduct(u1).Y, r2.CrossProduct(u2).Y, r3.CrossProduct(u3).Y, r4.CrossProduct(u4).Y, r5.CrossProduct(u5).Y, r6.CrossProduct(u6).Y },
                { r1.CrossProduct(u1).Z, r2.CrossProduct(u2).Z, r3.CrossProduct(u3).Z, r4.CrossProduct(u4).Z, r5.CrossProduct(u5).Z, r6.CrossProduct(u6).Z }
            };
        }

        public CalculateReactionsResponseData MapToResponse(double[] result)
        {
            return new CalculateReactionsResponseData
            {
                AArmLowerReaction1 = new Force
                {
                    AbsolutValue = result[0],
                    X = result[0] * this.SuspensionNormalizedVector.SuspensionAArmLowerU1.X,
                    Y = result[0] * this.SuspensionNormalizedVector.SuspensionAArmLowerU1.Y,
                    Z = result[0] * this.SuspensionNormalizedVector.SuspensionAArmLowerU1.Z
                },
                AArmLowerReaction2 = new Force
                {
                    AbsolutValue = result[1],
                    X = result[1] * this.SuspensionNormalizedVector.SuspensionAArmLowerU2.X,
                    Y = result[1] * this.SuspensionNormalizedVector.SuspensionAArmLowerU2.Y,
                    Z = result[1] * this.SuspensionNormalizedVector.SuspensionAArmLowerU2.Z
                },
                AArmUpperReaction1 = new Force
                {
                    AbsolutValue = result[2],
                    X = result[2] * this.SuspensionNormalizedVector.SuspensionAArmUpperU3.X,
                    Y = result[2] * this.SuspensionNormalizedVector.SuspensionAArmUpperU3.Y,
                    Z = result[2] * this.SuspensionNormalizedVector.SuspensionAArmUpperU3.Z
                },
                AArmUpperReaction2 = new Force
                {
                    AbsolutValue = result[3],
                    X = result[3] * this.SuspensionNormalizedVector.SuspensionAArmUpperU4.X,
                    Y = result[3] * this.SuspensionNormalizedVector.SuspensionAArmUpperU4.Y,
                    Z = result[3] * this.SuspensionNormalizedVector.SuspensionAArmUpperU4.Z
                },
                ShockAbsorberReaction = new Force
                {
                    AbsolutValue = result[4],
                    X = result[4] * this.SuspensionNormalizedVector.ShockAbsorberU5.X,
                    Y = result[4] * this.SuspensionNormalizedVector.ShockAbsorberU5.Y,
                    Z = result[4] * this.SuspensionNormalizedVector.ShockAbsorberU5.Z
                },
                TieRodReaction = new Force
                {
                    AbsolutValue = result[5],
                    X = result[5] * this.SuspensionNormalizedVector.TieRodU6.X,
                    Y = result[5] * this.SuspensionNormalizedVector.TieRodU6.Y,
                    Z = result[5] * this.SuspensionNormalizedVector.TieRodU6.Z
                },
            };
        }
    }
}
