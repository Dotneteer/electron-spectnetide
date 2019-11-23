using Spect.Net.Shell.Messaging;

namespace Spect.Net.Shell.State
{
    /// <summary>
    /// This class conveys an application state message between the
    /// renderer and main process in both directions.
    /// </summary>
    /// <remarks>
    /// The message type is set to the assembly qualified name of the
    /// message object type. The Payload holds the action object to convey.
    /// </remarks>
    public class AppActionMessage: IpcMessage
    {
        /// <summary>
        /// Instantiates the message
        /// </summary>
        /// <param name="messageType">Message type</param>
        /// <param name="payLoad">Message payload object</param>
        public AppActionMessage(string messageType, object payLoad)
        {
            MessageType = messageType;
            PayLoad = payLoad;
        }

        /// <summary>
        /// The payload of the message
        /// </summary>
        public object PayLoad { get; }
    }
}