using Microsoft.EntityFrameworkCore;
using PeopleResourceManagement.Domain.Entities;
using PeopleResourceManagement.Domain.Repositories;
using PeopleResourceManagement.Infrastructure.Data;

namespace PeopleResourceManagement.Infrastructure.Repositories;

public class LogRepository : ILogRepository
{
    private readonly AppDbContext _context;

    public LogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Log?> GetByUnitIdAsync(int unitId)
    {
        return await _context.Logs.FirstOrDefaultAsync(log => log.UnitId == unitId);
    }

    public async Task AddAsync(Log log)
    {
        await _context.Logs.AddAsync(log);
    }

    public Task UpdateAsync(Log log)
    {
        _context.Logs.Update(log);
        return Task.CompletedTask;
    }
    
    public async Task<List<Log>> GetEmptyUnitLogsAsync()
    {
        return await _context.Logs
            .Where(log => log.Descriptor == "Unit has no employees")
            .ToListAsync();
    }
}