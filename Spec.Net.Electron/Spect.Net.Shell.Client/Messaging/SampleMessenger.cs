using Microsoft.JSInterop;

namespace Spect.Net.Shell.Client.Messaging
{
    /// <summary>
    /// This class implements a sample message
    /// </summary>
    public class SampleMessenger : MessengerBase
    {
        /// <summary>
        /// Instantiates a sample messenger
        /// </summary>
        public SampleMessenger(IJSRuntime jsRuntime) : base(jsRuntime, "sample-message")
        {
        }
    }
}