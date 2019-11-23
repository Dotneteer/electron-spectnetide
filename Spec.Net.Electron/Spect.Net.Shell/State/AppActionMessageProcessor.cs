using System;
using System.Text.Json;
using ElectronNET.API;
using Newtonsoft.Json.Linq;
using Spect.Net.Shell.Messaging;
using ChannelNames = Spect.Net.Shell.Messaging.ChannelNames;
using IpcMessage = Spect.Net.Shell.Messaging.IpcMessage;
using IReducerAction = Spect.Net.Shell.State.Redux.IReducerAction;

namespace Spect.Net.Shell.State
{
    /// <summary>
    /// This message processor handles application state messages (action messages)
    /// </summary>
    public class AppActionMessageProcessor: MessageProcessorBase
    {
        /// <summary>
        /// Initializes a message processor to listen to the specified channel
        /// </summary>
        public AppActionMessageProcessor(BrowserWindow renderer) 
            : base(renderer, ChannelNames.APP_STATE_TO_MAIN)
        {
        }

        /// <summary>
        /// Processes the specified message
        /// </summary>
        /// <param name="type">Message type</param>
        /// <param name="correlationId">Correlation ID</param>
        /// <param name="message">The JObject instance that represents the message</param>
        /// <returns></returns>
        protected override IpcMessage ProcessMessage(string type, int? correlationId, JObject message)
        {
            // --- Message type contains the .NET type name of the message
            var msgType = Type.GetType(type);
            if (msgType == null) return null;

            // --- The payload contains the JSON representation of the action
            var payloadObj = message.GetValue("payLoad");
            if (payloadObj == null) return null;

            // --- Get the payload and process the actions
            var payload = JsonSerializer.Deserialize(payloadObj.ToString(), msgType);
            if (payload is IReducerAction reducerAction)
            {
                MainProcessStore.Dispatch(reducerAction);
            }

            return null;
        }
    }
}