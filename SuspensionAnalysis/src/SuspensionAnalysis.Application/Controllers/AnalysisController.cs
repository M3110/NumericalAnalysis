﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuspensionAnalysis.Application.Extensions;
using SuspensionAnalysis.Core.Operations.RunAnalysis.CircularProfile;
using SuspensionAnalysis.Core.Operations.RunAnalysis.RectangularProfile;
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
        /// <response code="500">If occurred some error in process.</response>
        /// <response code="501">If some resource is not implemented.</response>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("circular-profile/run")]
        public async Task<ActionResult<RunAnalysisResponse>> RunAnalysis(
            [FromServices] IRunCircularProfileAnalysis operation,
            [FromBody] RunAnalysisRequest<CircularProfile> request)
        {
            RunAnalysisResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// This operation run the analysis considering that all geometry uses a rectangular beam profile.
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
        [HttpPost("rectangular-profile/run")]
        public async Task<ActionResult<RunAnalysisResponse>> RunAnalysis(
            [FromServices] IRunRectangularProfileAnalysis operation,
            [FromQuery] RunAnalysisRequest<RectangularProfile> request)
        {
            RunAnalysisResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }
    }
}
