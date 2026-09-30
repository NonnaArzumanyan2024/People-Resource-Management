using Microsoft.EntityFrameworkCore;
using PeopleResourceManagement.Infrastructure.Data;
using PeopleResourceManagement.Domain.Entities;
using PeopleResourceManagement.Domain.Specifications;
using PeopleResourceManagement.Domain.Repositories;

namespace PeopleResourceManagement.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    //1
    public async Task<List<Employee>> GetAllAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    //2
    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    //3
    public async Task<Employee> AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        return employee;
    }

    //3
    public Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        return Task.CompletedTask;
    }

    //4
    public Task DeleteAsync(Employee employee)
    {
        _context.Employees.Remove(employee);
        return Task.CompletedTask;
    }

    //5
    public async Task<List<Employee>> GetBySpecificationAsync(
        ISpecification specification)
    {
        return await _context.Employees
            .Where(specification.Criteria)
            .ToListAsync();
    }
    
}

