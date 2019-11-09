using Spect.Net.Shell.Shared.Messaging;

namespace Spect.Net.Shell.Client.Messaging
{
    /// <summary>
    /// This class represents a sample message
    /// </summary>
    public class SampleRequest: IpcMessage
    {
        public string Argument { get; set; }
    }
}