using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Domain.Repositories;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee?> GetByEmailAsync(string email);
}