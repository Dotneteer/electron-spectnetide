namespace Spect.Net.Shell.Messaging
{
    /// <summary>
    /// This class defines response message structure that can convey an exception
    /// code and description.
    /// </summary>
    public class IpcResponse : IpcMessage
    {
        /// <summary>
        /// Optional exception code
        /// </summary>
        public string ExceptionCode { get; set; }

        /// <summary>
        /// Optional description
        /// </summary>
        public string Description { get; set; }
    }
}
