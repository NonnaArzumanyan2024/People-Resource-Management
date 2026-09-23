using People_Specification.Api.Models;
using People_Specification.Api.Repositories;
using People_Specification.Api.Specifications.Employees;

namespace People_Specification.Api.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Employee> AddAsync(Employee employee)
    {
        return await _repository.AddAsync(employee);
    }

    public async Task UpdateAsync(Employee employee)
    {
        await _repository.UpdateAsync(employee);
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await _repository.GetByIdAsync(id);

        if (employee == null)
        {
            return;
        }

        await _repository.DeleteAsync(employee);
    }

    public async Task<List<Employee>> GetActiveEmployeesAsync()
    {
        var specification = new ActiveEmployeesSpecification();

        return await _repository.GetBySpecificationAsync(specification);
    }

    public async Task<List<Employee>> GetInactiveEmployeesAsync()
    {
        var specification = new InactiveEmployeesSpecification();

        return await _repository.GetBySpecificationAsync(specification);
    }

    public async Task<List<Employee>> GetByDepartmentAsync(string department)
    {
        var specification = new EmployeesByDepartmentSpecification(department);

        return await _repository.GetBySpecificationAsync(specification);
    }

    public async Task<List<Employee>> GetByPositionAsync(string position)
    {
        var specification = new EmployeesByPositionSpecification(position);

        return await _repository.GetBySpecificationAsync(specification);
    }

    public async Task<List<Employee>> GetByHireDateAsync(DateTime hireDate)
    {
        var specification = new EmployeesByHireDateSpecification(hireDate);

        return await _repository.GetBySpecificationAsync(specification);
    }
}
