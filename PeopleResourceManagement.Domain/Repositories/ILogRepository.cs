using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Domain.Repositories;

public interface ILogRepository
{
    Task<Log?> GetByUnitIdAsync(int unitId);
    Task AddAsync(Log log);
    Task UpdateAsync(Log log);
    Task<List<Log>> GetEmptyUnitLogsAsync();
}