using Microsoft.AspNetCore.Mvc;
using SuspensionAnalysis.DataContracts.OperationBase;
using System.Net;

namespace SuspensionAnalysis.Application.Extensions
{
    /// <summary>
    /// It is responsible to build the HTTP response.
    /// </summary>
    public static class HttpResponseBuilder
    {
        /// <summary>
        /// This method builds the HTTP response .
        /// </summary>
        /// <typeparam name="TResponseData"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public static JsonResult BuildHttpResponse<TResponseData>(this OperationResponseBase<TResponseData> response)
            where TResponseData : OperationResponseData, new()
        {
            // BadRequest Status Code.
            if (response.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                response.SetBadRequestError();

                return new JsonResult(response)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            // Unauthorized Status Code.
            if (response.HttpStatusCode == HttpStatusCode.Unauthorized)
            {
                response.SetUnauthorizedError();

                return new JsonResult(response)
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
            }

            // InternalServerError Status Code.
            if (response.HttpStatusCode == HttpStatusCode.InternalServerError)
            {
                response.SetInternalServerError();

                return new JsonResult(response)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

            // NotImplemented Status Code.
            if (response.HttpStatusCode == HttpStatusCode.NotImplemented)
            {
                response.SetNotImplementedError();

                return new JsonResult(response)
                {
                    StatusCode = (int)HttpStatusCode.NotImplemented
                };
            }

            response.SetSuccessOk();
            return new JsonResult(response)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
