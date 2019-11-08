using Microsoft.JSInterop;
using Spect.Net.Shell.Shared.Messaging;
using System.Threading.Tasks;

namespace Spect.Net.Shell.Shared
{
    public static class SpectNetShellInterop
    {
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
        /// <param name="messageBase">Message to send</param>
        public static async Task SendMessage(this IJSRuntime jsRuntime, string channel, MessageBase messageBase)
        {
            await jsRuntime.InvokeAsync<object>("SpectNetShell.sendMessage", channel, messageBase);
        }

        /// <summary>
        /// Sets up a listener in the renderer process that listens to the specified channel.
        /// </summary>
        /// <param name="jsRuntime">JS runtime object</param>
        public static async Task<bool> IsBrowserWindowMaximized(this IJSRuntime jsRuntime)
        {
            return await jsRuntime.InvokeAsync<bool>("SpectNetShell.isBrowserWindowMaximized");
        }
    }
}
