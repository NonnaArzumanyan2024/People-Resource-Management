using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PeopleResourceManagement.Application.Interfaces;
using PeopleResourceManagement.Application.Jobs;

namespace People_Specification.Api.Workers;

public class UnitMonitoringWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<UnitMonitoringWorker> _logger;

    private const long UnitMonitoringLockKey = 1001;

    public UnitMonitoringWorker(IServiceProvider serviceProvider, ILogger<UnitMonitoringWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("UnitMonitoringWorker started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await using var scope = _serviceProvider.CreateAsyncScope();
                var job = scope.ServiceProvider.GetRequiredService<CheckEmptyUnitsJob>();
                var distributedLockService = scope.ServiceProvider.GetRequiredService<IDistributedLockService>();
                var lockAcquired = await distributedLockService.TryAcquireAsync(UnitMonitoringLockKey, stoppingToken);

                if (!lockAcquired)
                {
                    _logger.LogInformation("Unit monitoring skipped because another replica owns the lock.");
                }
                else
                {
                    try
                    {
                        await job.ExecuteAsync(stoppingToken);
                        _logger.LogInformation("Unit monitoring job completed successfully");
                    }
                    finally
                    {
                        await distributedLockService.ReleaseAsync(UnitMonitoringLockKey, CancellationToken.None);
                        _logger.LogInformation("Unit monitoring distributed lock released");
                    }
                }
            }
            catch (OperationCanceledException)
                when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occurred while running unit monitoring job");
            }

            try
            {
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
            catch (OperationCanceledException)
                when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
        }
        

        _logger.LogInformation("UnitMonitoringWorker stopped");
    }
}