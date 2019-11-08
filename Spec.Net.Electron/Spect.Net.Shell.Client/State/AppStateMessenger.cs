using Microsoft.JSInterop;
using Spect.Net.Shell.Client.Messaging;
using Spect.Net.Shell.Shared.Messaging;
using Spect.Net.Shell.Shared.State.Redux;
using System.Threading.Tasks;

namespace Spect.Net.Shell.Client.State
{
    /// <summary>
    /// This class is responsible for sending app state messages from the renderer
    /// process to the main process.
    /// </summary>
    public class AppStateMessenger : MessengerServiceBase
    {
        /// <summary>
        /// Instantiates a sample messenger
        /// </summary>
        public AppStateMessenger(IJSRuntime jsRuntime)
            : base(jsRuntime, ChannelNames.APP_STATE_MESSAGE)
        {
        }

        /// <summary>
        /// osts the specified application action
        /// </summary>
        /// <param name="action">Action to post</param>
        public async Task Post(IReducerAction action)
        {
            var message = new AppActionMessage(action.GetType().AssemblyQualifiedName, action);
            await Post(message);
        }
    }
}