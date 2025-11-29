namespace Inventive.Workers;

internal sealed class Worker(ILogger<Worker> logger) : BackgroundService
#pragma warning restore CA1812
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {DateTimeOffset}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
        }
    }
}
