using System.Threading.Tasks;
using Microsoft.JSInterop;
using Spect.Net.Shell.Messaging;
using Spect.Net.Shell.State.Redux;
using ChannelNames = Spect.Net.Shell.Messaging.ChannelNames;

namespace Spect.Net.Shell.State
{
    /// <summary>
    /// This class is responsible for sending app state messages from the renderer
    /// process to the main process.
    /// </summary>
    internal class AppStateMessenger : MessengerBase
    {
        /// <summary>
        /// Instantiates a sample messenger
        /// </summary>
        public AppStateMessenger(IJSRuntime jsRuntime)
            : base(jsRuntime, ChannelNames.APP_STATE_TO_MAIN)
        {
        }

        /// <summary>
        /// osts the specified application action
        /// </summary>
        /// <param name="action">Action to post</param>
        public async Task Post(IReducerAction action)
        {
            var message = new AppActionMessage(action.GetType().AssemblyQualifiedName, action);
            await SendAsync(message);
        }
    }
}