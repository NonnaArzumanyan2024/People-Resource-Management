namespace PeopleResourceManagement.Domain.UnitOfWork;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}