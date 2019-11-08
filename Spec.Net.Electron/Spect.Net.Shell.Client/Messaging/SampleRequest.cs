using Spect.Net.Shell.Shared.Messaging;

namespace Spect.Net.Shell.Client.Messaging
{
    public class SampleRequest: MessageBase
    {
        public string Argument { get; set; }
    }
}