namespace PeopleResourceManagement.Application.Interfaces;

public interface IDistributedLockService
{
    Task<bool> TryAcquireAsync(long lockKey, CancellationToken cancellationToken = default);

    Task ReleaseAsync(long lockKey, CancellationToken cancellationToken = default);
}