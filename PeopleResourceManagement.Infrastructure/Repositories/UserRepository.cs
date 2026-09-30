using Microsoft.EntityFrameworkCore;
using PeopleResourceManagement.Domain.Entities;
using PeopleResourceManagement.Domain.Repositories;
using PeopleResourceManagement.Infrastructure.Data;

namespace PeopleResourceManagement.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(user => user.Username == username);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }
}
