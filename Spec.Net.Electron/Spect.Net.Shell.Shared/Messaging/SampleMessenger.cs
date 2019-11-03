using Microsoft.JSInterop;

namespace Spect.Net.Shell.Shared.Messaging
{
    public class SampleMessenger : MessengerServiceBase
    {
        /// <summary>
        /// Instantiates a sample messenger
        /// </summary>
        public SampleMessenger(IJSRuntime jsRuntime) : base(jsRuntime, "sample-message")
        {
        }
    }
}