using People_Specification.Api.Models;

namespace People_Specification.Api.Repositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync();

    Task<Employee?> GetByIdAsync(int id);

    Task<Employee> AddAsync(Employee employee);

    Task UpdateAsync(Employee employee);

    Task DeleteAsync(Employee employee);
}