namespace People_Specification.Api.Models;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public UserRole Role { get; set; }
    public int EmployeeId { get; set; }
    public bool IsActive { get; set; }
}
