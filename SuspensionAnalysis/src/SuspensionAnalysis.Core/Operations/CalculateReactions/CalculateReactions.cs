using SuspensionAnalysis.Core.ExtensionMethods;
using SuspensionAnalysis.Core.Operations.Base;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace SuspensionAnalysis.Core.Operations.CalculateReactions
{
    public class CalculateReactions : OperationBase<CalculateReactionsRequest, CalculateReactionsResponse, CalculateReactionsResponseData>, ICalculateReactions
    {
        protected override async Task<CalculateReactionsResponse> ProcessOperation(CalculateReactionsRequest request)
        {
            var response = new CalculateReactionsResponse { Data = new CalculateReactionsResponseData() };



            return response;
        }

        public double[,] BuildDisplacementMatrix(CalculateReactionsRequest request)
        {
            (Vector3D u1, Vector3D u2) = request.SuspensionAArmLower.CalculateNormalizedVectors();

            (Vector3D u3, Vector3D u4) = request.SuspensionAArmUpper.CalculateNormalizedVectors();

            Vector3D u5 = request.ShockAbsorber.PivotPoint
                .BuildVector(request.ShockAbsorber.AArmFasteningPoint)
                .NormalizeVector();

            Vector3D u6 = request.TieRod.PivotPoint
                .BuildVector(request.TieRod.AArmFasteningPoint)
                .NormalizeVector();

            Vector3D 

            return new double[6, 6]
            {
                u1.X, u2.X, u3.X, u4.X, u5.X, u6.X
                u1.Y, u2.Y, u3.Y, u4.Y, u5.Y, u6.Y
                u1.Z, u2.Z, u3.Z, u4.Z, u5.Z, u6.Z

            };
        }
    }
}
