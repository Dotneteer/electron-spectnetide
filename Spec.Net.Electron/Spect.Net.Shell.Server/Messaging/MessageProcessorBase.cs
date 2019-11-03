using System.IO;
using ElectronNET.API;
using Newtonsoft.Json.Linq;
using Spect.Net.Shell.Shared.Messaging;

namespace Spect.Net.Shell.Server.Messaging
{
    /// <summary>
    /// This class is intended to be the base class for all message processors
    /// working in the main process.
    /// </summary>
    public abstract class MessageProcessorBase
    {
        /// <summary>
        /// The BrowserWindow that represents the renderer process
        /// </summary>
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
            File.WriteAllText("C:\\Temp\\messages.txt", $"MessageProcessor for {channel} created.");
            Electron.IpcMain.On(channel, args =>
            {
                if (args is JObject jObject)
                {
                    var type = jObject["messageType"].ToString();
                    File.WriteAllText("C:\\Temp\\messages.txt", $"Type extracted: {type}");
                    if (!int.TryParse(jObject["correlationId"].ToString(), out var correlationId))
                    {
                        correlationId = -1;
                    }
                    
                    File.WriteAllText("C:\\Temp\\messages.txt", $"CorrelationID extracted: {correlationId}");

                    var response = ProcessMessage(type, correlationId < 0 ? (int?)null : correlationId, jObject);
                    if (response?.CorrelationId != null)
                    {
                        Electron.IpcMain.Send(Renderer, Channel, response);
                    }
                }
            });
        }

        /// <summary>
        /// The channel this processor listens to.
        /// </summary>
        public string Channel { get; }

        /// <summary>
        /// Processes the specified message
        /// </summary>
        /// <param name="type">Message type</param>
        /// <param name="correlationId">Correlation ID</param>
        /// <param name="message">The JObject instance that represents the message</param>
        /// <returns>Response message</returns>
        protected abstract MessageBase ProcessMessage(string type, int? correlationId, JObject message);
    }
}