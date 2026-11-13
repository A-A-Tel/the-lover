using DSharpPlus;

namespace RocBot;

public class Worker(
    DiscordClient client
    ) : BackgroundService
{
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await client.ConnectAsync();

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await client.DisconnectAsync();
        await base.StopAsync(cancellationToken);
    }
}