namespace Spect.Net.Shell.Shared.Messaging
{
    /// <summary>
    /// Represents an abstract message type that can be either a request or a 
    /// response.
    /// </summary>
    public abstract class AbstractMessage
    {
        /// <summary>
        /// Type of the message
        /// </summary>
        public string MessageType { get; }

        /// <summary>
        /// Message correlation ID
        /// </summary>
        public int? CorrelationId { get; set; }

        protected AbstractMessage(string messageType, int? correlationId = null)
        {
            MessageType = messageType;
            CorrelationId = correlationId;
        }
    }
}
