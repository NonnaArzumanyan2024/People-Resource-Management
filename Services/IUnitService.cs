using People_Specification.Api.Models;

namespace People_Specification.Api.Services;

public interface IUnitService
{
    Task<List<Unit>> GetAllAsync();
    Task<List<Unit>> GetAllWithEmployeesAsync();
    Task<Unit?> GetByIdAsync(int id);
    Task<Unit?> GetParentAsync(int unitId);
    Task<List<Unit>> GetChildrenAsync(int parentUnitId);
    Task<Unit> AddAsync(Unit unit);
    Task UpdateAsync(Unit unit);
    Task DeleteAsync(int id);
    Task<Unit?> GetRootAsync();
    Task<List<Unit>> GetSiblingsAsync(int unitId);
    Task<List<Unit>> GetLeafUnitsAsync();
    Task<List<Unit>> GetUnitsWithChildrenAsync();
}
