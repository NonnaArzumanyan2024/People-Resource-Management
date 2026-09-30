using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
}
