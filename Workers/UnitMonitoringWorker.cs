using PeopleResourceManagement.Application.Jobs;

namespace People_Specification.Api.Workers;

public class UnitMonitoringWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<UnitMonitoringWorker> _logger;

    public UnitMonitoringWorker(
        IServiceScopeFactory scopeFactory,
        ILogger<UnitMonitoringWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        _logger.LogInformation("UnitMonitoringWorker started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();

                var job = scope.ServiceProvider
                    .GetRequiredService<CheckEmptyUnitsJob>();

                await job.ExecuteAsync(stoppingToken);

                _logger.LogInformation(
                    "Unit monitoring job completed successfully");
            }
            catch (OperationCanceledException)
                when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error while monitoring units");
            }

            await Task.Delay(
                TimeSpan.FromSeconds(30),
                stoppingToken);
        }

        _logger.LogInformation("UnitMonitoringWorker stopped");
    }
}