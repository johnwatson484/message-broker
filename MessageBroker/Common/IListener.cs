namespace MessageBroker.Common;

public interface IListener
{
    void Start(CancellationToken stoppingToken);
}
