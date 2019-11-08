using Spect.Net.Shell.Shared.Messaging;

namespace Spect.Net.Shell.Client.State
{
    /// <summary>
    /// This class conveys application state change actions
    /// </summary>
    public class AppActionMessage: MessageBase
    {
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