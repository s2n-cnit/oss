
using System.Text.Json;
using System.Text.Json.Serialization;

using MediatR;

using Oss.Model.Response;
using Oss.Services;

namespace Oss.Handlers
{
    public abstract class AbstractHandler<TCommand, TResult>: IRequestHandler<TCommand, TResult> where TCommand: IRequest<TResult>
    {
        private static JsonSerializerOptions jsonSerializerOptions_ = new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, WriteIndented = true };

        protected readonly ILogger logger_;

        private readonly RestClient restClient_;
        private readonly IBackgroundTaskQueue taskQueue_;

        public AbstractHandler(ILogger logger, RestClient restClient, IBackgroundTaskQueue taskQueue)
        {
            logger_ = logger;
            restClient_ = restClient;
            taskQueue_ = taskQueue;
        }

        public abstract Task<TResult> Handle(TCommand request, CancellationToken cancellationToken);

        protected void RunBackground(Task task)
        {
            var writeTask = taskQueue_.QueueBackgroundWorkItemAsync((t) => task);

            if (!writeTask.Wait(TimeSpan.FromSeconds(5)))
            {
                throw new Exception("The background queue is currently full and no background task can be executed.");
            };
        }

        protected async Task Notify(VasInfo vasInfo, string callbackUrl)
        {
            if (string.IsNullOrWhiteSpace(callbackUrl))
            {
                return;
            }

            logger_.LogInformation("Notifying to {url}", callbackUrl);

            var reply = await restClient_.Post(new Uri(callbackUrl), RestClient.CreateJsonContent(Serialize(vasInfo)));

            logger_.LogInformation("Notified to {url}, result: {sc}", callbackUrl, reply.StatusCode);
        }

        private string Serialize<TObject>(TObject obj)
        {
            return JsonSerializer.Serialize(obj, jsonSerializerOptions_);
        }
    }
}
