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
    /// <summary>
    /// This static class aggregates messaging scenarios with all messaging operations.
    /// </summary>
    /// <remarks>
    /// Use the InitAsync method to initialize communication before sending any messages.
    ///
    /// This class handles these types of communication scenarios:
    /// - The renderer sends a fire-and-forget message to the main process.
    /// - The renderer sends a request to the main process and expects a correlated response.
    /// - The renderer receives a main process message to dispatch an application state action.
    /// </remarks>
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
        public static async Task InitAsync(IJSRuntime jsRuntime)
        {
            MessageId = 1;
            await (s_SampleMessenger = new SampleMessenger(jsRuntime)).InitAsync();
            await (s_AppStateMessenger = new AppStateMessenger(jsRuntime)).InitAsync();
            await jsRuntime.SetupListener(ChannelNames.APP_STATE_TO_RENDERER);
        }

        public static Task<SampleResponse> Sample(string message)
        {
            return SendRequestAsync<SampleResponse>(s_SampleMessenger, new SampleRequest
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


        #region Helper methods

        /// <summary>
        /// This method sends a message with the specified service object, and waits
        /// while a correlated response arrives bakc.
        /// </summary>
        /// <typeparam name="TResponse">Type of expected response</typeparam>
        /// <param name="service">Service object to send the message</param>
        /// <param name="request">Request to send</param>
        /// <returns>Response for the request</returns>
        /// <remarks>This method waits while the correlated response arrives.</remarks>
        private static async Task<TResponse> SendRequestAsync<TResponse>(MessengerBase service, IpcMessage request)
            where TResponse: IpcMessage
        {
            request.CorrelationId = MessageId++;
            var taskCompletion = new TaskCompletionSource<JsonElement>();
            s_OngoingRequests[request.CorrelationId.Value] = taskCompletion;
            await service.SendAsync(request);
            var response = await taskCompletion.Task;
            Console.WriteLine($"JSON received: {response.ToString()}");
            return JsonSerializer.Deserialize<TResponse>(response.ToString(), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        /// <summary>
        /// JavaScript calls this method whenever a message arrives to the renderer process
        /// on any of the channels previously set up from the renderer method.
        /// </summary>
        /// <param name="channel">Channel name</param>
        /// <param name="message">Message contents</param>
        /// <remarks>
        /// This method uses to correlation ID to find a response message for a specific request.
        /// </remarks>
        [JSInvokable]
        // ReSharper disable once UnusedMember.Global
        public static Task HandleMessage(string channel, object message)
        {
            if (!(message is JsonElement jsonElement))
            {
                // --- We cannot handle this response
                return Task.FromResult(0);
            }

            var handled = false;

            // --- Check if response arrived for a special channel
            switch (channel)
            {
                // --- Application state action to dispatch in the renderer
                case ChannelNames.APP_STATE_TO_RENDERER:
                    handled = true;
                    var type = jsonElement.GetProperty("messageType").ToString();
                    var msgType = Type.GetType(type);
                    if (msgType == null) return null;

                    var payloadObj = jsonElement.GetProperty("payLoad");
                    var payload = JsonSerializer.Deserialize(payloadObj.ToString(), msgType, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    if (payload is IReducerAction reducerAction)
                    {
                        RendererProcessStore.Dispatch(reducerAction);
                    }
                    break;
            }

            if (handled)
            {
                // --- Thes special message has already been completed
                return Task.FromResult(0);
            }

            // --- Obtain the correlation ID
            var id = jsonElement.GetProperty("correlationId").GetInt32();
            if (!s_OngoingRequests.TryGetValue(id, out var completion))
            {
                // --- This response does not correlate with any request
                return Task.FromResult(0);
            }

            // --- We received the response, return with it
            completion.SetResult(jsonElement);
            return Task.FromResult(0);
        }

        #endregion
    }
}