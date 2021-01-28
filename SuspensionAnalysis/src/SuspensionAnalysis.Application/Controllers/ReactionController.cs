using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuspensionAnalysis.Application.Extensions;
using SuspensionAnalysis.Core.Operations.CalculateReactions;
using SuspensionAnalysis.DataContracts.CalculateReactions;
using System.Threading.Tasks;

namespace SuspensionAnalysis.Application.Controllers
{
    [Route("api/v1/suspension-reactions")]
    public class ReactionController : Controller
    {
        /// <summary>
        /// This operation calculates the reactions to suspension system.
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
        [HttpPost("calculate")]
        public async Task<ActionResult<CalculateReactionsResponse>> CalculateReactions(
            [FromServices] ICalculateReactions operation,
            [FromBody] CalculateReactionsRequest request)
        {
            CalculateReactionsResponse response = await operation.Process(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }
    }
}
