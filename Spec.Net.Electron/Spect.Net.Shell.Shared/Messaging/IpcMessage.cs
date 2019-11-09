namespace Spect.Net.Shell.Shared.Messaging
{
    /// <summary>
    /// Represents a message that can be send between the main and
    /// renderer processes (in any direction) through Electron's
    /// IPC mechanism.
    /// </summary>
    /// <remarks>
    /// When processing the messages, you can use the MessageType property
    /// to determine how you handle is. As you're developing with .NET,
    /// you can uses derived message types, too.
    /// </remarks>
    public class IpcMessage
    {
        /// <summary>
        /// Type of the message. You can use this information
        /// to process the payload of the message.
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// Message correlation ID. You can use this ID to handle
        /// correlated request-response message pairs.
        /// </summary>
        public int? CorrelationId { get; set; }
    }
}
