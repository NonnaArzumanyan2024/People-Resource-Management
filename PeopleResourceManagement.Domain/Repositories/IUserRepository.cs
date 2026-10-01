using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
}
