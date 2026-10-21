using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PeopleResourceManagement.Infrastructure.Data;
using PeopleResourceManagement.Domain.Entities;
using PeopleResourceManagement.Domain.Repositories;

namespace PeopleResourceManagement.Infrastructure.Repositories;

public class EmployeeRepository
    : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<Employee?> GetByEmailAsync(string email)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Email == email);
        }
        finally
        {
            stopwatch.Stop();

            Console.WriteLine(
                $"GET BY EMAIL | Execution Time: " +
                $"{stopwatch.Elapsed.TotalMilliseconds:F3} ms"
            );
        }
    }
}

