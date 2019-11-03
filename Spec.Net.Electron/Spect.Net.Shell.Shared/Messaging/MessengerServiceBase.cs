using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Spect.Net.Shell.Shared.Messaging
{
    /// <summary>
    /// This abstract class implements correlated messaging between 
    /// the renderer and main processes.
    /// </summary>
    /// <remarks>
    /// You can {@link post} fire-and-forget type messages, or {@link send} messages where you
    /// expect a correlated response.
    ///
    /// You need to instantiate this class in the renderer process, as it can initiate communication
    /// only with the main process.
    ///
    /// You can use this class as a base class to implement your messages.
    /// </remarks>
    public class MessengerServiceBase
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
        /// Instantiates a messenger that communicates through the specified channel
        /// </summary>
        /// <param name="jsRuntime">JS Interop object</param>
        /// <param name="channel">Channel name</param>
        protected MessengerServiceBase(IJSRuntime jsRuntime, string channel)
        {
            JsRuntime = jsRuntime;
            Channel = channel;
            _channelInitialized = false;
        }

        public async Task Post(MessageBase messageBase)
        {
            await EnsureChannel();
            await JsRuntime.SendMessage(Channel, messageBase);
        }

        public async Task Send(MessageBase messageBase)
        {
            await EnsureChannel();
            await JsRuntime.SendMessage(Channel, messageBase);
        }

        /// <summary>
        /// Ensures that the communication channel is established
        /// </summary>
        private async Task EnsureChannel()
        {
            if (!_channelInitialized)
            {
                await JsRuntime.SetupListener(Channel);
                _channelInitialized = true;
            }
        }
    }
}
