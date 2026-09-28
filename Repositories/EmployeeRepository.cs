using Microsoft.EntityFrameworkCore;
using People_Specification.Api.Data;
using People_Specification.Api.Models;
using People_Specification.Api.Specifications;

namespace People_Specification.Api.Repositories;

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
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return employee;
    }

    //3
    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    //4
    public async Task DeleteAsync(Employee employee)
    {
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
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
