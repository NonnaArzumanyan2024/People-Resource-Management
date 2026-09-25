using People_Specification.Api.Models;

namespace People_Specification.Api.Repositories;

public interface IUnitRepository
{
    Task<List<Unit>> GetAllAsync();
    Task<List<Unit>> GetAllWithEmployeesAsync();
    Task<Unit?> GetByIdAsync(int id);
    Task<Unit?> GetRootAsync();
    Task<Unit?> GetParentAsync(int unitId);
    Task<List<Unit>> GetChildrenAsync(int parentUnitId);
    Task<List<Unit>> GetSiblingsAsync(int unitId);
    Task<List<Unit>> GetLeafUnitsAsync();
    Task<List<Unit>> GetUnitsWithChildrenAsync();
    Task<List<Employee>> GetEmployeesAsync(int unitId);
    Task<List<Employee>> GetActiveEmployeesAsync(int unitId);
    Task<List<Employee>> GetEmployeesByPositionAsync(string position);
    Task<List<Unit>> GetUnitsWithEmployeesAsync();
    Task<List<Unit>> GetUnitsWithoutEmployeesAsync();
    Task<List<Unit>> GetLeafUnitsWithEmployeesAsync();
    Task<List<Unit>> GetUnitsWithChildrenAndEmployeesAsync();
    Task<int> GetChildrenCountAsync(int unitId);
    Task<int> GetEmployeeCountAsync(int unitId);
    Task<List<Unit>> SearchUnitsByNameAsync(string searchText);
    Task<bool> HasChildrenAsync(int unitId);
    Task<bool> HasEmployeesAsync(int unitId);
    Task<Unit> AddAsync(Unit unit);
    Task UpdateAsync(Unit unit);
    Task DeleteAsync(Unit unit);
}
