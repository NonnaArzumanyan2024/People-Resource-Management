using People_Specification.Api.DTOs;

namespace People_Specification.Api.Services;

public interface IAuthService
{
    Task RegisterAsync(RegisterRequestDto request);
    Task<string?> LoginAsync(LoginRequestDto request);
}