using PeopleResourceManagement.Domain.UnitOfWork;
using PeopleResourceManagement.Infrastructure.Data;

namespace PeopleResourceManagement.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}