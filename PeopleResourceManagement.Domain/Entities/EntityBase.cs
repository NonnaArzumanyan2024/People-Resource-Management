namespace PeopleResourceManagement.Domain.Entities;

public abstract class EntityBase
{
    public int Id { get; set; }
    public DateTime CreatedDate {get;set;}
    public DateTime UpdatedDate {get;set;}
    public bool IsDeleted {get; set;}
}
