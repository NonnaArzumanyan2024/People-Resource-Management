using PeopleResourceManagement.Domain.Entities;
using PeopleResourceManagement.Domain.Specifications;

namespace PeopleResourceManagement.Domain.Repositories;

public interface IRepository<T>
    where T : EntityBase
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);

    Task<List<T>> GetBySpecificationAsync(
        ISpecification<T> specification);
}