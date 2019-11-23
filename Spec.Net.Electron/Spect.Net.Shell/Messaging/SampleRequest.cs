namespace Spect.Net.Shell.Messaging
{
    /// <summary>
    /// This class represents a sample message
    /// </summary>
    public class SampleRequest: IpcMessage
    {
        public string Argument { get; set; }
    }
}