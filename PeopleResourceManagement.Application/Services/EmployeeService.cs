using PeopleResourceManagement.Domain.Entities;
using PeopleResourceManagement.Application.Interfaces;
using PeopleResourceManagement.Domain.Repositories;
using PeopleResourceManagement.Domain.Specifications.Employees;
using PeopleResourceManagement.Domain.UnitOfWork;

namespace PeopleResourceManagement.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IEmployeeRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
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
        var addedEmployee = await _repository.AddAsync(employee);
        await _unitOfWork.SaveChangesAsync();
        return addedEmployee;
    }

    public async Task UpdateAsync(Employee employee)
    {
        await _repository.UpdateAsync(employee);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await _repository.GetByIdAsync(id);

        if (employee == null)
        {
            return;
        }

        await _repository.DeleteAsync(employee);
        await _unitOfWork.SaveChangesAsync();
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

