using People_Specification.Api.Common;

namespace People_Specification.Api.Models;

public class User : EntityBase
{
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public UserRole Role { get; set; }
    public int EmployeeId { get; set; }
    public bool IsActive { get; set; }
}
