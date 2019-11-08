using System;
using System.IO;
using System.Text.Json;
using ElectronNET.API;
using Newtonsoft.Json.Linq;
using Spect.Net.Shell.Server.Messaging;
using Spect.Net.Shell.Shared.Messaging;
using Spect.Net.Shell.Shared.State.Redux;

namespace Spect.Net.Shell.Server.State
{
    public class AppActionMessageProcessor: MessageProcessorBase
    {
        /// <summary>
        /// Initializes a message processor to listen to the specified channel
        /// </summary>
        public AppActionMessageProcessor(BrowserWindow renderer) 
            : base(renderer, ChannelNames.APP_STATE_MESSAGE)
        {
        }

        /// <summary>
        /// Processes the specified message
        /// </summary>
        /// <param name="type">Message type</param>
        /// <param name="correlationId">Correlation ID</param>
        /// <param name="message">The JObject instance that represents the message</param>
        /// <returns></returns>
        protected override MessageBase ProcessMessage(string type, int? correlationId, JObject message)
        {
            // --- Message type contains the .NET type name of the message
            File.WriteAllText("C:\\Temp\\appmessages.txt", $"AppMessage: {type}\n");
            var msgType = Type.GetType(type);
            if (msgType == null) return null;

            File.AppendAllText("C:\\Temp\\appmessages.txt", "Ready to get payload\n");
            var payloadObj = message.GetValue("payLoad");
            if (payloadObj != null)
            {
                var payload = JsonSerializer.Deserialize(payloadObj.ToString(), msgType);
                File.AppendAllText("C:\\Temp\\appmessages.txt", $"{JsonSerializer.Serialize(payload)}\n");
                if (payload is IReducerAction reducerAction)
                {
                    MainProcessStore.Dispatch(reducerAction);
                    File.AppendAllText("C:\\Temp\\appmessages.txt", "Dispatched.");
                }
            }

            return null;
        }
    }
}