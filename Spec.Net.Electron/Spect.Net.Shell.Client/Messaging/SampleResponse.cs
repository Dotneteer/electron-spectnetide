﻿using Spect.Net.Shell.Shared.Messaging;

namespace Spect.Net.Shell.Client.Messaging
{
    /// <summary>
    /// This class represents a sample response
    /// </summary>
    public class SampleResponse: IpcResponse
    {
        public string Argument { get; set; }
    }
}