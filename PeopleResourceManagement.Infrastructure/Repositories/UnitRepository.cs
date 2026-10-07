using Microsoft.EntityFrameworkCore;
using PeopleResourceManagement.Infrastructure.Data;
using PeopleResourceManagement.Domain.Entities;
using PeopleResourceManagement.Domain.Repositories;

namespace PeopleResourceManagement.Infrastructure.Repositories;

public class UnitRepository : GenericRepository<Unit>, IUnitRepository
{
    public UnitRepository(AppDbContext context)
        : base(context)
    {
    }

    // 1
    public async Task<List<Unit>> GetAllWithEmployeesAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .Include(unit => unit.Employees)
            .ToListAsync();
    }

    // 2
    public async Task<Unit?> GetRootAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .FirstOrDefaultAsync(unit => unit.ParentUnitId == null);
    }

    // 3
    public async Task<Unit?> GetParentAsync(int unitId)
    {
        var parentId = await _context.Units
            .Where(unit => unit.Id == unitId)
            .Select(unit => unit.ParentUnitId)
            .FirstOrDefaultAsync();

        if (parentId == null)
        {
            return null;
        }

        return await _context.Units
            .AsNoTracking()
            .FirstOrDefaultAsync(unit => unit.Id == parentId);
    }

    // 4
    public async Task<List<Unit>> GetChildrenAsync(int parentUnitId)
    {
        return await _context.Units
            .AsNoTracking()
            .Where(unit => unit.ParentUnitId == parentUnitId)
            .ToListAsync();
    }

    // 5
    public async Task<List<Unit>> GetSiblingsAsync(int unitId)
    {
        var unit = await _context.Units
            .AsNoTracking()
            .FirstOrDefaultAsync(unit => unit.Id == unitId);

        if (unit == null)
        {
            return new List<Unit>();
        }

        return await _context.Units
            .AsNoTracking()
            .Where(otherUnit =>
                otherUnit.ParentUnitId == unit.ParentUnitId &&
                otherUnit.Id != unitId)
            .ToListAsync();
    }

    // 6
    public async Task<List<Unit>> GetLeafUnitsAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .Where(unit =>
                !_context.Units.Any(
                    child => child.ParentUnitId == unit.Id))
            .ToListAsync();
    }

    // 7
    public async Task<List<Unit>> GetUnitsWithChildrenAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .Where(unit =>
                _context.Units.Any(
                    child => child.ParentUnitId == unit.Id))
            .ToListAsync();
    }

    // 8
    public async Task<List<Employee>> GetEmployeesAsync(int unitId)
    {
        return await _context.Employees
            .AsNoTracking()
            .Where(employee => employee.UnitId == unitId)
            .ToListAsync();
    }

    // 9
    public async Task<List<Employee>> GetActiveEmployeesAsync(int unitId)
    {
        return await _context.Employees
            .AsNoTracking()
            .Where(employee =>
                employee.UnitId == unitId &&
                employee.IsActive)
            .ToListAsync();
    }

    // 10
    public async Task<List<Employee>> GetEmployeesByPositionAsync(
        string position)
    {
        return await _context.Employees
            .AsNoTracking()
            .Where(employee =>
                EF.Functions.ILike(
                    employee.Position,
                    $"%{position.Trim()}%"))
            .ToListAsync();
    }

    // 11
    public async Task<List<Unit>> GetUnitsWithEmployeesAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .Where(unit => unit.Employees.Any())
            .ToListAsync();
    }

    // 12
    public async Task<List<Unit>> GetUnitsWithoutEmployeesAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .Where(unit => !unit.Employees.Any())
            .ToListAsync();
    }

    // 13
    public async Task<List<Unit>> GetLeafUnitsWithEmployeesAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .Where(unit =>
                !unit.ChildUnits.Any() &&
                unit.Employees.Any())
            .ToListAsync();
    }

    // 14
    public async Task<List<Unit>> GetUnitsWithChildrenAndEmployeesAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .Where(unit =>
                unit.ChildUnits.Any() &&
                unit.Employees.Any())
            .ToListAsync();
    }

    // 15
    public async Task<int> GetChildrenCountAsync(int unitId)
    {
        return await _context.Units
            .CountAsync(unit => unit.ParentUnitId == unitId);
    }

    // 16
    public async Task<int> GetEmployeeCountAsync(int unitId)
    {
        return await _context.Employees
            .CountAsync(employee => employee.UnitId == unitId);
    }

    // 17
    public async Task<List<Unit>> SearchUnitsByNameAsync(
        string searchText)
    {
        searchText = searchText.Trim();

        return await _context.Units
            .AsNoTracking()
            .Where(unit =>
                EF.Functions.ILike(
                    unit.Name,
                    $"%{searchText}%"))
            .ToListAsync();
    }

    // 18
    public async Task<bool> HasChildrenAsync(int unitId)
    {
        return await _context.Units
            .AnyAsync(unit => unit.ParentUnitId == unitId);
    }

    // 19
    public async Task<bool> HasEmployeesAsync(int unitId)
    {
        return await _context.Employees
            .AnyAsync(employee => employee.UnitId == unitId);
    }
    
    //20
    public async Task<List<Unit>> GetEmptyUnitsAsync()
    {
        return await _context.Units
            .Where(unit => !unit.Employees.Any())
            .ToListAsync();
    }
    
}

