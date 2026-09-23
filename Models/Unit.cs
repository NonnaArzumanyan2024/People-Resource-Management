namespace People_Specification.Api.Models;

public class Unit
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int? ParentUnitId { get; set; }
    public Unit? ParentUnit { get; set; }
    public ICollection<Unit> ChildUnits { get; set; } = new List<Unit>();
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
