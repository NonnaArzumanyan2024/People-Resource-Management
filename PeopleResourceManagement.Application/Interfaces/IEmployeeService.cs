using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Application.Interfaces;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllAsync();
    Task<List<Employee>> GetSortedAsync(int pageNumber, int pageSize);
    Task<Employee?> GetByIdAsync(int id);
    Task<Employee?> GetByEmailAsync(string email);
    Task<Employee> AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(int id);
    Task<List<Employee>> GetActiveEmployeesAsync();
    Task<List<Employee>> GetInactiveEmployeesAsync();
    Task<List<Employee>> GetByDepartmentAsync(string department);
    Task<List<Employee>> GetByPositionAsync(string position);
    Task<List<Employee>> GetByHireDateAsync(DateTime hireDate);
}

