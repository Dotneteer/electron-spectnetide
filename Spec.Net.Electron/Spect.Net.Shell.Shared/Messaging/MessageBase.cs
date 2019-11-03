namespace Spect.Net.Shell.Shared.Messaging
{
    /// <summary>
    /// Represents an abstract message type that can be either a request or a 
    /// response.
    /// </summary>
    public abstract class MessageBase
    {
        /// <summary>
        /// Type of the message
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// Message correlation ID
        /// </summary>
        public int? CorrelationId { get; set; }
    }
}
