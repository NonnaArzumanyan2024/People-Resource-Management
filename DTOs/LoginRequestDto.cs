namespace People_Specification.Api.DTOs;

public class LoginRequestDto
{
    public required string Username { get; set; }

    public required string Password { get; set; }
}