namespace PeopleResourceManagement.Domain.Entities;

public class Log
{
    public int Id { get; set; }
    public int UnitId { get; set; }
    public string Descriptor { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Unit Unit { get; set; } = null!;
}