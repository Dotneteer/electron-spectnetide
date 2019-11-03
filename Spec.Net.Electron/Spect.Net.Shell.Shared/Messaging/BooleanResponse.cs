namespace Spect.Net.Shell.Shared.Messaging
{
    /// <summary>
    /// This class represents a general response that retrieves a boolean value.
    /// </summary>
    public class BooleanResponse : OperationResponseBase
    {
        /// <summary>
        /// The result value
        /// </summary>
        public bool Result { get; }

        /// <summary>
        /// Initializes a response.
        /// </summary>
        /// <param name="request">The request to send this response</param>
        /// <param name="result">Result value</param>
        /// <param name="exceptionCode">Optional exception code</param>
        /// <param name="description">Optional description</param>
        public BooleanResponse(AbstractMessage request, bool result, string exceptionCode = null, string description = null) 
            : base(request, "boolean-response", exceptionCode, description)
        {
            Result = result;
        }
    }
}
