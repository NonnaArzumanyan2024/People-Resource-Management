using People_Specification.Api.Models;

namespace People_Specification.Api.Services;


public interface IEmployeeService
{
    Task<List<Employee>> GetAllAsync();

    Task<Employee?> GetByIdAsync(int id);

    Task<Employee> AddAsync(Employee employee);

    Task UpdateAsync(Employee employee);

    Task DeleteAsync(int id);

    Task<List<Employee>> GetActiveEmployeesAsync();

    Task<List<Employee>> GetInactiveEmployeesAsync();

    Task<List<Employee>> GetByDepartmentAsync(string department);

    Task<List<Employee>> GetByPositionAsync(string position);

    Task<List<Employee>> GetByHireDateAsync(DateTime hireDate);
}
