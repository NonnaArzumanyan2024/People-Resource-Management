using Microsoft.EntityFrameworkCore;
using People_Specification.Api.Data;
using People_Specification.Api.Models;

namespace People_Specification.Api.Repositories;

public class UnitRepository : IUnitRepository
{
    private readonly AppDbContext _context;

    public UnitRepository(AppDbContext context)
    {
        _context = context;
    }

    // 1. Get all units
    public async Task<List<Unit>> GetAllAsync()
    {
        return await _context.Units
            .ToListAsync();
    }

    // 2. Get unit by Id
    public async Task<Unit?> GetByIdAsync(int id)
    {
        return await _context.Units
            .FirstOrDefaultAsync(unit => unit.Id == id);
    }

    // 3. Get root
    public async Task<Unit?> GetRootAsync()
    {
        return await _context.Units
            .FirstOrDefaultAsync(
                unit => unit.ParentUnitId == null);
    }

    // 4. Get parent of a unit
    public async Task<Unit?> GetParentAsync(int unitId)
    {
        return await _context.Units
            .Where(parent =>
                _context.Units.Any(child =>
                    child.Id == unitId &&
                    child.ParentUnitId == parent.Id))
            .FirstOrDefaultAsync();
    }

    // 5. Get direct children
    public async Task<List<Unit>> GetChildrenAsync(int parentUnitId)
    {
        return await _context.Units
            .Where(unit =>
                unit.ParentUnitId == parentUnitId)
            .ToListAsync();
    }

    // 6. Get siblings
    public async Task<List<Unit>> GetSiblingsAsync(int unitId)
    {
        return await _context.Units
            .Where(unit =>
                _context.Units.Any(current =>
                    current.Id == unitId &&
                    current.ParentUnitId == unit.ParentUnitId)
                &&
                unit.Id != unitId)
            .ToListAsync();
    }

    // 7. Get leaf units
    public async Task<List<Unit>> GetLeafUnitsAsync()
    {
        return await _context.Units
            .Where(unit =>
                !_context.Units.Any(child =>
                    child.ParentUnitId == unit.Id))
            .ToListAsync();
    }

    // 8. Get units that have children
    public async Task<List<Unit>> GetUnitsWithChildrenAsync()
    {
        return await _context.Units
            .Where(unit =>
                _context.Units.Any(child =>
                    child.ParentUnitId == unit.Id))
            .ToListAsync();
    }

    // 9. Get employees of a unit
    public async Task<List<Employee>> GetEmployeesAsync(int unitId)
    {
        return await _context.Employees
            .Where(employee =>
                employee.UnitId == unitId)
            .ToListAsync();
    }

    // 10. Get active employees of a unit
    public async Task<List<Employee>> GetActiveEmployeesAsync(int unitId)
    {
        return await _context.Employees
            .Where(employee =>
                employee.UnitId == unitId &&
                employee.IsActive)
            .ToListAsync();
    }

    // 11. Get employees by position
    public async Task<List<Employee>> GetEmployeesByPositionAsync(
        string position)
    {
        return await _context.Employees
            .Where(employee =>
                employee.Position == position)
            .ToListAsync();
    }

    // 12. Get units that have employees
    public async Task<List<Unit>> GetUnitsWithEmployeesAsync()
    {
        return await _context.Units
            .Where(unit =>
                _context.Employees.Any(employee =>
                    employee.UnitId == unit.Id))
            .ToListAsync();
    }

    // 13. Get units without employees
    public async Task<List<Unit>> GetUnitsWithoutEmployeesAsync()
    {
        return await _context.Units
            .Where(unit =>
                !_context.Employees.Any(employee =>
                    employee.UnitId == unit.Id))
            .ToListAsync();
    }

    // 14. Get leaf units that have employees
    public async Task<List<Unit>> GetLeafUnitsWithEmployeesAsync()
    {
        return await _context.Units
            .Where(unit =>
                !_context.Units.Any(child =>
                    child.ParentUnitId == unit.Id)
                &&
                _context.Employees.Any(employee =>
                    employee.UnitId == unit.Id))
            .ToListAsync();
    }

    // 15. Get units that have both children and employees
    public async Task<List<Unit>> GetUnitsWithChildrenAndEmployeesAsync()
    {
        return await _context.Units
            .Where(unit =>
                _context.Units.Any(child =>
                    child.ParentUnitId == unit.Id)
                &&
                _context.Employees.Any(employee =>
                    employee.UnitId == unit.Id))
            .ToListAsync();
    }

    // 16. Count direct children
    public async Task<int> GetChildrenCountAsync(int unitId)
    {
        return await _context.Units
            .CountAsync(unit =>
                unit.ParentUnitId == unitId);
    }

    // 17. Count direct employees
    public async Task<int> GetEmployeeCountAsync(int unitId)
    {
        return await _context.Employees
            .CountAsync(employee =>
                employee.UnitId == unitId);
    }

    // 18. Search units by name
    public async Task<List<Unit>> SearchUnitsByNameAsync(
        string searchText)
    {
        return await _context.Units
            .Where(unit =>
                unit.Name.Contains(searchText))
            .ToListAsync();
    }

    // 19. Check if unit has children
    public async Task<bool> HasChildrenAsync(int unitId)
    {
        return await _context.Units
            .AnyAsync(unit =>
                unit.ParentUnitId == unitId);
    }

    // 20. Check if unit has employees
    public async Task<bool> HasEmployeesAsync(int unitId)
    {
        return await _context.Employees
            .AnyAsync(employee =>
                employee.UnitId == unitId);
    }

    public async Task<Unit> AddAsync(Unit unit)
    {
        _context.Units.Add(unit);

        await _context.SaveChangesAsync();

        return unit;
    }

    public async Task UpdateAsync(Unit unit)
    {
        _context.Units.Update(unit);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Unit unit)
    {
        _context.Units.Remove(unit);

        await _context.SaveChangesAsync();
    }
}
