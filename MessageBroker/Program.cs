using MessageBroker;
using MessageBroker.Common;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IListener, Listener>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
