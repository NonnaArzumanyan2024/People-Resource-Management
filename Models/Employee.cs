namespace People_Specification.Api.Models;

public class Employee
{
    public int Id { get; set; }
    public required string EmployeeNumber { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string WorkEmail { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Department { get; set; }
    public required string Position { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; }
}