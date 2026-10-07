using PeopleResourceManagement.Application.DTOs;
using PeopleResourceManagement.Application.Interfaces;
using PeopleResourceManagement.Domain.Entities;
using PeopleResourceManagement.Domain.Repositories;
using PeopleResourceManagement.Domain.UnitOfWork;

namespace PeopleResourceManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IEmployeeRepository employeeRepository, IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtService jwtService, IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
    }

    public async Task RegisterAsync(RegisterRequestDto request)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(request.Username);

        if (existingUser is not null)
        {
            throw new InvalidOperationException("Username already exists.");
        }

        var employee = new Employee
        {
            EmployeeNumber = request.EmployeeNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Department = request.Department,
            Position = request.Position,
            HireDate = request.HireDate,
            IsActive = request.IsActive
        };

        await _employeeRepository.AddAsync(employee);

        var user = new User
        {
            Username = request.Username,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            Role = request.Role,
            Employee = employee,
            IsActive = true
        };

        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
        
    }

    public async Task<string?> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);

        if (user is null)
        {
            return null;
        }

        if (!user.IsActive)
        {
            return null;
        }

        var isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            return null;
        }

        return _jwtService.GenerateToken(user);
    }
}
