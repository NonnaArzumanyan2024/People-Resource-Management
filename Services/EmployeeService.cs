using People_Specification.Api.Models;
using People_Specification.Api.Repositories;

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
}