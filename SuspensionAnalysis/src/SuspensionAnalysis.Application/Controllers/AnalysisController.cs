using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuspensionAnalysis.Application.Extensions;
using SuspensionAnalysis.Core.Models.Profiles;
using SuspensionAnalysis.Core.Operations.RunAnalysis;
using SuspensionAnalysis.DataContracts.Models.Profiles;
using SuspensionAnalysis.DataContracts.RunAnalysis;
using System.Threading.Tasks;

namespace SuspensionAnalysis.Application.Controllers
{
    [Route("api/v1/analysis")]
    public class AnalysisController : Controller
    {
        /// <summary>
        /// This operation run the analysis considering that all geometry uses a cicular beam profile.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="request"></param>
        /// <response code="200">Returns the reactions value.</response>
        /// <response code="400">If some validation do not passed.</response>
        /// <response code="401">If the client does not have authorization.</response>
        /// <response code="500">If occurred some error in process.</response>
        /// <response code="501">If some resource is not implemented.</response>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("circular-profile/run")]
        public async Task<ActionResult<RunAnalysisResponse>> RunAnalysis(
            [FromServices] IRunAnalysis<Core.Models.Profiles.CircularProfile> operation,
            [FromQuery] RunAnalysisRequest<Core.Models.Profiles.CircularProfile> request)
        {
            RunAnalysisResponse response = await operation.Process(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// This operation run the analysis considering that all geometry uses a rectangular beam profile.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="request"></param>
        /// <response code="200">Returns the reactions value.</response>
        /// <response code="400">If some validation do not passed.</response>
        /// <response code="401">If the client does not have authorization.</response>
        /// <response code="500">If occurred some error in process.</response>
        /// <response code="501">If some resource is not implemented.</response>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("rectangular-profile/run")]
        public async Task<ActionResult<RunAnalysisResponse>> RunAnalysis(
            [FromServices] IRunAnalysis<Core.Models.Profiles.RectangularProfile> operation,
            [FromQuery] RunAnalysisRequest<Core.Models.Profiles.RectangularProfile> request)
        {
            RunAnalysisResponse response = await operation.Process(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }
    }
}
