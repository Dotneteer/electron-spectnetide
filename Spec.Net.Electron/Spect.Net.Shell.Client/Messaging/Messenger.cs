using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Spect.Net.Shell.Client.State;
using Spect.Net.Shell.Shared;
using Spect.Net.Shell.Shared.Messaging;
using Spect.Net.Shell.Shared.State.Redux;

namespace Spect.Net.Shell.Client.Messaging
{

    public static class Messenger
    {
        private static readonly Dictionary<int, TaskCompletionSource<JsonElement>> s_OngoingRequests
            = new Dictionary<int, TaskCompletionSource<JsonElement>>();

        private static SampleMessenger s_SampleMessenger;
        private static AppStateMessenger s_AppStateMessenger;

        /// <summary>
        /// ID of the next message
        /// </summary>
        public static int MessageId { get; private set; }

        /// <summary>
        /// Initializes the messenger
        /// </summary>
        /// <param name="jsRuntime">JS interop object</param>
        public static async Task Init(IJSRuntime jsRuntime)
        {
            s_SampleMessenger = new SampleMessenger(jsRuntime);
            s_AppStateMessenger = new AppStateMessenger(jsRuntime);
            MessageId = 1;
            await jsRuntime.SetupListener(ChannelNames.APP_STATE_FORWARD);
        }

        [JSInvokable]
        // ReSharper disable once UnusedMember.Global
        public static Task HandleMessage(string channel, object message)
        {
            Console.WriteLine($"Message received on {channel}: {JsonSerializer.Serialize(message)}");
            if (!(message is JsonElement jsonElement))
            {
                // --- We cannot handle this response
                return Task.FromResult(0);
            }

            var handled = false;

            // --- Check if response arrived for a special channel
            switch (channel)
            {
                case ChannelNames.APP_STATE_FORWARD:
                    handled = true;
                    var type = jsonElement.GetProperty("messageType").ToString();
                    var msgType = Type.GetType(type);
                    if (msgType == null) return null;

                    var payloadObj = jsonElement.GetProperty("payLoad").ToString();
                    var payload = JsonSerializer.Deserialize(payloadObj, msgType);
                    if (payload is IReducerAction reducerAction)
                    {
                        // --- Avoid infinite renderer-main message loop
                        reducerAction.IsLocal = true;
                        RendererProcessStore.Dispatch(reducerAction);
                        Console.WriteLine($"Renderer state updated: {JsonSerializer.Serialize(RendererProcessStore.GetState())}");
                    }
                    break;
            }

            if (handled)
            {
                return Task.FromResult(0);
            }

            var id = jsonElement.GetProperty("correlationId").GetInt32();
            if (!s_OngoingRequests.TryGetValue(id, out var completion))
            {
                return Task.FromResult(0);
            }

            completion.SetResult(jsonElement);
            return Task.FromResult(0);
        }

        public static Task<SampleResponse> Sample(string message)
        {
            return SendRequest<SampleResponse>(s_SampleMessenger, new SampleRequest
            {
                MessageType = "sample-request",
                Argument = message
            });
        }

        /// <summary>
        /// Sends an application action message
        /// </summary>
        /// <param name="action">Application action to send</param>
        public static Task SendAppAction(IReducerAction action)
        {
            return s_AppStateMessenger.Post(action);
        }

        /// <summary>
        /// This method sends a message with the specified service object, and waits
        /// while a correlated response arrives bakc.
        /// </summary>
        /// <typeparam name="TResponse">Type of expected response</typeparam>
        /// <param name="service">Service object to send the message</param>
        /// <param name="request">Request to send</param>
        /// <returns></returns>
        private static async Task<TResponse> SendRequest<TResponse>(MessengerServiceBase service, MessageBase request)
            where TResponse: MessageBase
        {
            request.CorrelationId = MessageId++;
            var taskCompletion = new TaskCompletionSource<JsonElement>();
            s_OngoingRequests[request.CorrelationId.Value] = taskCompletion;
            await service.Send(request);
            var response = await taskCompletion.Task;
            Console.WriteLine($"JSON received: {response.ToString()}");
            return JsonSerializer.Deserialize<TResponse>(response.ToString(), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}