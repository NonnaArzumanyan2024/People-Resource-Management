namespace PeopleResourceManagement.Application.DTOs;

public class UnitPatchDto
{
    public string Name { get; set; } = string.Empty;

    public ICollection<int> ChildUnitIds { get; set; }
        = new List<int>();
}