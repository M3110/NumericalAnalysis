using System.Collections.Generic;
using System.Net;

namespace SuspensionAnalysis.DataContracts.OperationBase
{
    /// <summary>
    /// It represents the response content for all operations.
    /// </summary>
    /// <typeparam name="TResponseData"></typeparam>
    public class OperationResponseBase<TResponseData>
        where TResponseData : OperationResponseData, new()
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        public OperationResponseBase()
        {
            this.Errors = new List<OperationError>();
            this.Data = new TResponseData();
        }

        /// <summary>
        /// The success status of operation.
        /// </summary>
        public bool Success { get; protected set; }

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; protected set; }

        /// <summary>
        /// The list of errors.
        /// </summary>
        public List<OperationError> Errors { get; protected set; }

        /// <summary>
        /// It represents the 'data' content of all operation response.
        /// </summary>
        public TResponseData Data { get; set; }
        
        /// <summary>
        /// This method adds errors on list of errors.
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="httpStatusCode"></param>
        public void AddErrors(List<OperationError> errors, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            this.Errors.AddRange(errors);
            this.HttpStatusCode = httpStatusCode;
            this.Success = false;
        }

        /// <summary>
        /// This method sets Success to true and the HttpStatusCode to 200 (OK).
        /// </summary>
        public void SetSuccessOk() => this.SetSuccess(HttpStatusCode.OK);

        /// <summary>
        /// This method sets Success to true and the HttpStatusCode to 201 (Created).
        /// </summary>
        public void SetSuccessCreated() => this.SetSuccess(HttpStatusCode.Created);

        /// <summary>
        /// This method sets Success to true and the HttpStatusCode to 202 (Accepted).
        /// </summary>
        public void SetSuccessAccepted() => this.SetSuccess(HttpStatusCode.Accepted);

        /// <summary>
        /// This method sets Success to false and the HttpStatusCode to 400 (BadRequest).
        /// </summary>
        /// <param name="errorMessage"></param>
        public void SetBadRequestError(string errorCode = null, string errorMessage = null) => this.SetError(HttpStatusCode.BadRequest, errorCode, errorMessage);

        /// <summary>
        /// This method sets Success to false and the HttpStatusCode to 401 (Unauthorized).
        /// </summary>
        /// <param name="errorMessage"></param>
        public void SetUnauthorizedError(string errorCode = null, string errorMessage = null) => this.SetError(HttpStatusCode.Unauthorized, errorCode, errorMessage);

        /// <summary>
        /// This method sets Success to false and the HttpStatusCode to 500 (InternalServerError).
        /// </summary>
        /// <param name="errorMessage"></param>
        public void SetInternalServerError(string errorCode = null, string errorMessage = null) => this.SetError(HttpStatusCode.InternalServerError, errorCode, errorMessage);

        /// <summary>
        /// This method sets Success to false and the HttpStatusCode to 501 (NotImplemented).
        /// </summary>
        /// <param name="errorMessage"></param>
        public void SetNotImplementedError(string errorCode = null, string errorMessage = null) => this.SetError(HttpStatusCode.NotImplemented, errorCode, errorMessage);

        /// <summary>
        /// This method adds error on list of errors.
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <param name="httpStatusCode"></param>
        protected void AddError(string errorCode, string errorMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            this.Errors.Add(new OperationError(errorCode, errorMessage));
            this.HttpStatusCode = httpStatusCode;
            this.Success = false;
        }

        /// <summary>
        /// This method sets Sucess to true.
        /// </summary>
        /// <param name="httpStatusCode"></param>
        protected void SetSuccess(HttpStatusCode httpStatusCode)
        {
            this.HttpStatusCode = httpStatusCode;
            this.Success = true;
        }

        /// <summary>
        /// This method sets Success to false.
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="errorMessage"></param>
        protected void SetError(HttpStatusCode httpStatusCode, string errorCode = null, string errorMessage = null)
        {
            if (errorMessage != null)
                this.Errors.Add(new OperationError(errorCode, errorMessage));

            this.HttpStatusCode = httpStatusCode;
            this.Success = false;
        }
    }
}
