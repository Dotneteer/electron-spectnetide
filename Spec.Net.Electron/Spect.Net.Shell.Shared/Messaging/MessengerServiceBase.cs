using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
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
    public class MessengerServiceBase<TRequest, TResponse>
        where TRequest: AbstractMessage
        where TResponse: AbstractMessage
    {
        /// <summary>
        /// Access the state service of the app
        /// </summary>
        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        private readonly Dictionary<int, TaskCompletionSource<TResponse>> _ongoingRequests
            = new Dictionary<int, TaskCompletionSource<TResponse>>();

        private bool _channelInitialized;
        private int _messageId = 1;

        /// <summary>
        /// The channel this messanger communicates on
        /// </summary>
        public string Channel { get; }

        /// <summary>
        /// Instantiates a messenger that communicates through the specified channel
        /// </summary>
        /// <param name="channel">Channel name</param>
        protected MessengerServiceBase(string channel)
        {
            Channel = channel;
            _channelInitialized = false;
        }

        public async Task Post<TRequestMessage>(TRequestMessage message)
            where TRequestMessage: TRequest
        {
            await EnsureChannel();
            await SpectNetShellInterop.SendMessage(JsRuntime, Channel, message);
        }

        /// <summary>
        /// Ensures that the communication channel is established
        /// </summary>
        private async Task EnsureChannel()
        {
            if (!_channelInitialized)
            {
                await SpectNetShellInterop.SetupListener(JsRuntime, Channel);
                _channelInitialized = true;
            }
        }
    }
}
