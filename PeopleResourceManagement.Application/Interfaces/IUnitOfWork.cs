namespace PeopleResourceManagement.Application.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
