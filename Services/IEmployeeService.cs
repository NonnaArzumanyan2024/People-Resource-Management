using People_Specification.Api.Models;

namespace People_Specification.Api.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllAsync();

    Task<Employee?> GetByIdAsync(int id);

    Task<Employee> AddAsync(Employee employee);

    Task UpdateAsync(Employee employee);

    Task DeleteAsync(int id);
}