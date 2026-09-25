
using People_Specification.Api.Common;

namespace People_Specification.Api.DTOs;

public class RegisterRequestDto
{
    public required string Username { get; set; }

    public required string Password { get; set; }

    public UserRole Role { get; set; }

    public required string EmployeeNumber { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string PhoneNumber { get; set; }

    public required string Department { get; set; }

    public required string Position { get; set; }

    public DateTime HireDate { get; set; }

    public bool IsActive { get; set; }
}
