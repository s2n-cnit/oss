
using System.Threading.Channels;

using Microsoft.Extensions.Options;

namespace Oss.Services
{
    public interface IBackgroundTaskQueue
    {
        Task QueueBackgroundWorkItemAsync(Func<CancellationToken, Task> workItem);

        Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }

    internal class BackgroundTaskQueueOptions
    {
        public BoundedChannelOptions BoundedChannelOptions { get; set; }
        public UnboundedChannelOptions UnboundedChannelOptions { get; set; }
    }

    internal class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<Func<CancellationToken, Task>> queue_;

        public BackgroundTaskQueue(IOptions<BackgroundTaskQueueOptions> options)
        {
            var boundedChannelOptions = options.Value.BoundedChannelOptions;
            var unboundedChannelOptions = options.Value.UnboundedChannelOptions;

            if (boundedChannelOptions is not null && unboundedChannelOptions is not null)
            {
                throw new ArgumentException("Options cannot be both not null");
            }

            if (boundedChannelOptions is not null)
            {
                queue_ = Channel.CreateBounded<Func<CancellationToken, Task>>(boundedChannelOptions);
            }
            else if (unboundedChannelOptions is not null)
            {
                queue_ = Channel.CreateUnbounded<Func<CancellationToken, Task>>(unboundedChannelOptions);
            }
            else
            {
                throw new ArgumentException("Options cannot be null");
            }
        }

        public async Task QueueBackgroundWorkItemAsync(Func<CancellationToken, Task> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            await queue_.Writer.WriteAsync(workItem);
        }

        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            return await queue_.Reader.ReadAsync(cancellationToken);
        }
    }
}
