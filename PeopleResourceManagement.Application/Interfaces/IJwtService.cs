using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}
