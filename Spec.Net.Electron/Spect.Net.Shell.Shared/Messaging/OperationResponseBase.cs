namespace Spect.Net.Shell.Shared.Messaging
{
    /// <summary>
    /// This class defines a base class for a response that can convey exception
    /// </summary>
    public abstract class OperationResponseBase : AbstractMessage
    {
        /// <summary>
        /// Initializes a response
        /// </summary>
        /// <param name="request">The request to send this response</param>
        /// <param name="messageType">Response type</param>
        /// <param name="exceptionCode">Optional exception code</param>
        /// <param name="description">Optional description</param>
        protected OperationResponseBase(AbstractMessage request, 
            string messageType, 
            string exceptionCode = null, 
            string description = null) 
            : base(messageType, request.CorrelationId)
        {
            ExceptionCode = exceptionCode;
            Description = description;
        }

        /// <summary>
        /// Optional exception code
        /// </summary>
        public string ExceptionCode { get; }

        /// <summary>
        /// Optional description
        /// </summary>
        public string Description { get; }
    }
}
