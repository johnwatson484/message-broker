using MessageBroker.Common;

namespace MessageBroker;

public class Worker : BackgroundService
{
    private readonly IListener listener;

    public Worker(IListener listener)
    {
        this.listener = listener;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        listener.Start(stoppingToken);
    }
}
