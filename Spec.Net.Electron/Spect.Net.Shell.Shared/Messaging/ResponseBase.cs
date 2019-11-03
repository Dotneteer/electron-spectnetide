namespace Spect.Net.Shell.Shared.Messaging
{
    /// <summary>
    /// This class defines a base class for a response that can convey exception
    /// </summary>
    public abstract class ResponseBase : MessageBase
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
