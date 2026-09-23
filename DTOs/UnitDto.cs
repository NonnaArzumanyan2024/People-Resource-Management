namespace People_Specification.Api.DTOs;

public class UnitDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int? ParentUnitId { get; set; }
}
