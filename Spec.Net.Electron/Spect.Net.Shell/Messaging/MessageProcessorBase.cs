using ElectronNET.API;
using Newtonsoft.Json.Linq;

namespace Spect.Net.Shell.Messaging
{
    /// <summary>
    /// This class is intended to be the base class for all message processors
    /// that work in the main process.
    /// </summary>
    public abstract class MessageProcessorBase
    {
        /// <summary>
        /// The BrowserWindow that represents the renderer process.
        /// </summary>
        /// <remarks>
        /// The message processor sends response messages to that window.
        /// </remarks>
        public BrowserWindow Renderer { get; }

        /// <summary>
        /// Initializes a message processor to listen to the specified channel
        /// </summary>
        /// <param name="renderer">The BrowserWindow that represents the renderer process</param>
        /// <param name="channel">Channel name</param>
        protected MessageProcessorBase(BrowserWindow renderer, string channel)
        {
            Renderer = renderer;
            Channel = channel;

            // --- Process the message received on the specified channel
            Electron.IpcMain.On(channel, args =>
            {
                // --- We accept only JSON messages
                if (!(args is JObject jObject)) return;

                // --- Extract the type of message
                var type = jObject["messageType"].ToString();

                // --- Extract the correlation ID
                if (!int.TryParse(jObject["correlationId"].ToString(), out var correlationId))
                {
                    // --- No correlation ID
                    correlationId = -1;
                }

                // --- Let the processor process the message 
                var response = ProcessMessage(type, correlationId < 0 ? (int?)null : correlationId, jObject);

                // --- If there's a correlated response, send it back to the renderer
                if (response?.CorrelationId != null && response.CorrelationId == correlationId)
                {
                    Electron.IpcMain.Send(Renderer, Channel, response);
                }
            });
        }

        /// <summary>
        /// The channel this processor listens to.
        /// </summary>
        public string Channel { get; }

        /// <summary>
        /// Processes the specified message.
        /// </summary>
        /// <param name="type">Message type</param>
        /// <param name="correlationId">Correlation ID</param>
        /// <param name="message">The JObject instance that represents the message</param>
        /// <returns>Response message</returns>
        protected abstract IpcMessage ProcessMessage(string type, int? correlationId, JObject message);
    }
}