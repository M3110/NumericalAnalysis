namespace SuspensionAnalysis.DataContracts.OperationBase
{
    /// <summary>
    /// It represents the content 'data' of the error class used in application.
    /// </summary>
    public sealed class OperationError
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public OperationError(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        /// <summary>
        /// The erro message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The error code.
        /// </summary>
        public string Code { get; }
    }
}
