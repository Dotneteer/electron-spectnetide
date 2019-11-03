using System.IO;
using ElectronNET.API;
using Newtonsoft.Json.Linq;
using Spect.Net.Shell.Shared.Messaging;

namespace Spect.Net.Shell.Server.Messaging
{
    public class SampleMessageProcessor: MessageProcessorBase
    {
        /// <summary>
        /// Initializes a message processor to listen to the specified channel
        /// </summary>
        public SampleMessageProcessor(BrowserWindow renderer) 
            : base(renderer, "sample-message")
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
            switch (type)
            {
                case "sample-request":
                    return new SampleResponse
                    {
                        MessageType = "sample-request",
                        CorrelationId = correlationId,
                        Argument = "Welcome!"
                    };
            }
            return null;
        }
    }
}