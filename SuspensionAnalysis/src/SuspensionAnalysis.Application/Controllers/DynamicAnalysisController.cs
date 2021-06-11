using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuspensionAnalysis.Application.Extensions;
using SuspensionAnalysis.Core.Operations.RunAnalysis.Dynamic.HalfCar;
using SuspensionAnalysis.DataContracts.RunAnalysis;
using SuspensionAnalysis.DataContracts.RunAnalysis.Dynamic.HalfCar;
using System.Threading.Tasks;

namespace SuspensionAnalysis.Application.Controllers
{
    [Route("api/v1/dynamic-analysis")]
    public class DynamicAnalysisController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="request"></param>
        /// <response code="200">Returns the reactions value.</response>
        /// <response code="400">If some validation do not passed.</response>
        /// <response code="500">If occurred some error in process.</response>
        /// <response code="501">If some resource is not implemented.</response>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("half-car/run")]
        public async Task<ActionResult<RunStaticAnalysisResponse>> RunHalfCarAnalysis(
            [FromServices] IRunHalfCarDynamicAnalysis operation,
            [FromBody] RunHalfCarDynamicAnalysisRequest request)
        {
            RunHalfCarDynamicAnalysisResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }
    }
}
