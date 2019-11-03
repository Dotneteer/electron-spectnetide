namespace Spect.Net.Shell.Shared.Messaging
{
    /// <summary>
    /// This interface represents a general response that retrieves a void value
    /// </summary>
    public class VoidResponse : OperationResponseBase
    {
        /// <summary>
        /// Initializes a response.
        /// </summary>
        /// <param name="request">The request to send this response</param>
        /// <param name="exceptionCode">Optional exception code</param>
        /// <param name="description">Optional description</param>
        public VoidResponse(AbstractMessage request, 
            string exceptionCode = null, 
            string description = null) : 
            base(request, "void-response", exceptionCode, description)
        {
        }
    }
}
