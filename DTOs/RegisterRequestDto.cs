namespace People_Specification.Api.DTOs;

public class RegisterRequestDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public int EmployeeId { get; set; }
}
