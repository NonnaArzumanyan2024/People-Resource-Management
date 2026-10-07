using PeopleResourceManagement.Domain.Enums;

namespace PeopleResourceManagement.Domain.Entities;

public class User : EntityBase
{
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public UserRole Role { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public bool IsActive { get; set; }
}