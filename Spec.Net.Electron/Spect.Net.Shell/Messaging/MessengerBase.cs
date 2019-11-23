using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Spect.Net.Shell.Messaging
{
    /// <summary>
    /// This abstract class implements correlated messaging between 
    /// the renderer and main processes.
    /// </summary>
    /// <remarks>
    /// You can post fire-and-forget type messages, or send messages where you
    /// expect a correlated response.
    ///
    /// You need to instantiate this class in the renderer process, as it can initiate communication
    /// only with the main process.
    ///
    /// You can use this class as a base class to implement your messages.
    /// </remarks>
    public abstract class MessengerBase
    {
        /// <summary>
        /// Access the JS Interop object
        /// </summary>
        public IJSRuntime JsRuntime { get; }

        private bool _channelInitialized;

        /// <summary>
        /// The channel this messanger communicates on
        /// </summary>
        public string Channel { get; }

        /// <summary>
        /// Initializes the channel
        /// </summary>
        public Task InitAsync()
        {
            return EnsureChannelAsync();
        }

        /// <summary>
        /// Instantiates a messenger that communicates through the specified channel
        /// </summary>
        /// <param name="jsRuntime">JS Interop object</param>
        /// <param name="channel">Channel name</param>
        protected MessengerBase(IJSRuntime jsRuntime, string channel)
        {
            JsRuntime = jsRuntime;
            Channel = channel;
            _channelInitialized = false;
        }

        /// <summary>
        /// Sends a message through this objects channel, and 
        /// </summary>
        /// <param name="ipcMessage"></param>
        /// <returns></returns>
        public async Task SendAsync(IpcMessage ipcMessage)
        {
            await EnsureChannelAsync();
            await JsRuntime.SendMessage(Channel, ipcMessage);
        }

        /// <summary>
        /// Ensures that the communication channel is established
        /// </summary>
        private async Task EnsureChannelAsync()
        {
            if (!_channelInitialized)
            {
                await JsRuntime.SetupListener(Channel);
                _channelInitialized = true;
            }
        }
    }
}
