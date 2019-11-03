using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Spect.Net.Shell.Shared.Messaging;

namespace Spect.Net.Shell.Client.Messaging
{

    public static class Messenger
    {
        private static readonly Dictionary<int, TaskCompletionSource<JsonElement>> _ongoingRequests
            = new Dictionary<int, TaskCompletionSource<JsonElement>>();

        private static SampleMessenger s_SampleMessenger;

        /// <summary>
        /// ID of the next message
        /// </summary>
        public static int MessageId { get; private set; }

        /// <summary>
        /// Initializes the messenger
        /// </summary>
        /// <param name="jsRuntime">JS interop object</param>
        public static void Init(IJSRuntime jsRuntime)
        {
            s_SampleMessenger = new SampleMessenger(jsRuntime);
            MessageId = 1;
        }

        [JSInvokable]
        public static Task HandleMessage(object message)
        {
            Console.WriteLine("HandleMessage invoked.");
            if (!(message is JsonElement jsonElement))
            {
                return Task.FromResult(0);
            }

            var id = jsonElement.GetProperty("correlationId").GetInt32();
            if (!_ongoingRequests.TryGetValue(id, out var completion))
            {
                return Task.FromResult(0);
            }

            completion.SetResult(jsonElement);
            return Task.FromResult(0);
        }

        public static Task<SampleResponse> Sample(string message)
        {
            return DoSend<SampleResponse>(s_SampleMessenger, new SampleRequest
            {
                MessageType = "sample-request",
                Argument = message
            });
        }

        private static async Task<TResponse> DoSend<TResponse>(MessengerServiceBase service, MessageBase messageBase)
            where TResponse: MessageBase
        {
            messageBase.CorrelationId = MessageId++;
            var taskCompletion = new TaskCompletionSource<JsonElement>();
            _ongoingRequests[messageBase.CorrelationId.Value] = taskCompletion;
            await service.Send(messageBase);
            var response = await taskCompletion.Task;
            Console.WriteLine($"JSON received: {response.ToString()}");
            return JsonSerializer.Deserialize<TResponse>(response.ToString(), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}