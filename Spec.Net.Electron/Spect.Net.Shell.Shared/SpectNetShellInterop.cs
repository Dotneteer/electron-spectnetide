using Microsoft.JSInterop;
using Spect.Net.Shell.Shared.Messaging;
using System.Threading.Tasks;

namespace Spect.Net.Shell.Shared
{
    public static class SpectNetShellInterop
    {
        /// <summary>
        /// Creates a new editor with the specified id
        /// </summary>
        /// <param name="jsRuntime">JS runtime object</param>
        /// <param name="options">Options to create the edito</param>
        public static async Task Hello(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeAsync<object>("SpectNetShell.hello");
        }

        /// <summary>
        /// Sets up a listener in the renderer process that listens to the specified channel.
        /// </summary>
        /// <param name="jsRuntime">JS runtime object</param>
        /// <param name="channel">Channel name</param>
        public static async Task SetupListener(this IJSRuntime jsRuntime, string channel)
        {
            await jsRuntime.InvokeAsync<object>("SpectNetShell.setupListener", channel);
        }

        /// <summary>
        /// Sends a message to the specified channel
        /// </summary>
        /// <param name="jsRuntime">JS runtime object</param>
        /// <param name="channel">Channel name</param>
        /// <param name="message">Message to send</param>
        public static async Task SendMessage(this IJSRuntime jsRuntime, string channel, AbstractMessage message)
        {
            await jsRuntime.InvokeAsync<object>("SpectNetShell.sendMessage", channel, message);
        }
    }
}
