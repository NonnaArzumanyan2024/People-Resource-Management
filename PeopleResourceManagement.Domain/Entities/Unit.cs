namespace PeopleResourceManagement.Domain.Entities;

public class Unit : EntityBase
{
    public required string Name { get; set; }
    public int? ParentUnitId { get; set; }
    public Unit? ParentUnit { get; set; }
    public ICollection<Unit> ChildUnits { get; set; } = new List<Unit>();
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    
    public void AddChild(Unit childUnit)
    {
        ArgumentNullException.ThrowIfNull(childUnit);

        if (childUnit == this)
        {
            throw new InvalidOperationException("A unit cannot be a child of itself.");
        }

        childUnit.ParentUnit = this;
        ChildUnits.Add(childUnit);
    }
}
