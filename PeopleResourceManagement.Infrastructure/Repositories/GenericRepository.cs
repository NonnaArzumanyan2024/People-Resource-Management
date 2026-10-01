using Microsoft.EntityFrameworkCore;
using PeopleResourceManagement.Domain.Entities;
using PeopleResourceManagement.Domain.Repositories;
using PeopleResourceManagement.Domain.Specifications;
using PeopleResourceManagement.Infrastructure.Data;

namespace PeopleResourceManagement.Infrastructure.Repositories;

public class GenericRepository<T> : IRepository<T>
    where T : EntityBase
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<List<T>> GetBySpecificationAsync(
        ISpecification<T> specification)
    {
        return await _dbSet
            .Where(specification.Criteria)
            .ToListAsync();
    }
}