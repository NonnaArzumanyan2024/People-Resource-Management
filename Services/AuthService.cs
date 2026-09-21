using Microsoft.EntityFrameworkCore;
using People_Specification.Api.Data;
using People_Specification.Api.DTOs;
using People_Specification.Api.Models;

namespace People_Specification.Api.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher _passwordHasher;
    private readonly JwtService _jwtService;

    public AuthService(
        AppDbContext context,
        PasswordHasher passwordHasher,
        JwtService jwtService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task RegisterAsync(RegisterRequestDto request)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username);

        if (existingUser is not null)
        {
            throw new InvalidOperationException("Username already exists.");
        }

        var employee = new Employee
        {
            EmployeeNumber = request.EmployeeNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            WorkEmail = request.WorkEmail,
            PhoneNumber = request.PhoneNumber,
            Department = request.Department,
            Position = request.Position,
            HireDate = request.HireDate,
            IsActive = request.IsActive
        };

        _context.Employees.Add(employee);

        await _context.SaveChangesAsync();

        var user = new User
        {
            Username = request.Username,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            Role = request.Role,
            EmployeeId = employee.Id,
            IsActive = true
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }

    public async Task<string?> LoginAsync(LoginRequestDto request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user is null)
        {
            return null;
        }

        if (!user.IsActive)
        {
            return null;
        }

        var isPasswordValid = _passwordHasher.VerifyPassword(
            request.Password,
            user.PasswordHash);

        if (!isPasswordValid)
        {
            return null;
        }

        return _jwtService.GenerateToken(user);
    }
}