using PeopleResourceManagement.Application.DTOs;

namespace PeopleResourceManagement.Application.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterRequestDto request);
    Task<string?> LoginAsync(LoginRequestDto request);
}
