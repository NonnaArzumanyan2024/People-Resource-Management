using PeopleResourceManagement.Domain.Entities;
using PeopleResourceManagement.Domain.Specifications;

namespace PeopleResourceManagement.Domain.Repositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<Employee> AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(Employee employee);
    Task<List<Employee>> GetBySpecificationAsync(
        ISpecification specification
    );
}

