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
            .AsNoTracking()
            .ToListAsync();
    }

    // 2. Get all units with their employees
    public async Task<List<Unit>> GetAllWithEmployeesAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .Include(unit => unit.Employees)
            .ToListAsync();
    }

    // 3. Get unit by Id
    public async Task<Unit?> GetByIdAsync(int id)
    {
        return await _context.Units
            .AsNoTracking()
            .FirstOrDefaultAsync(unit => unit.Id == id);
    }

    // 4. Get root
    public async Task<Unit?> GetRootAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .FirstOrDefaultAsync(
                unit => unit.ParentUnitId == null);
    }

    // 5. Get parent of a unit
    public async Task<Unit?> GetParentAsync(int unitId)
    {
        var parentId = await _context.Units
            .AsNoTracking()
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

    // 6. Get direct children
    public async Task<List<Unit>> GetChildrenAsync(int parentUnitId)
    {
        return await _context.Units
            .AsNoTracking()
            .Where(unit =>
                unit.ParentUnitId == parentUnitId)
            .ToListAsync();
    }

    // 7. Get siblings
    public async Task<List<Unit>> GetSiblingsAsync(int unitId)
    {
        var parentUnitId = await _context.Units
            .AsNoTracking()
            .Where(unit => unit.Id == unitId)
            .Select(unit => unit.ParentUnitId)
            .FirstOrDefaultAsync();

        return await _context.Units
            .AsNoTracking()
            .Where(unit =>
                unit.ParentUnitId == parentUnitId &&
                unit.Id != unitId)
            .ToListAsync();
    }

    // 8. Get leaf units
    public async Task<List<Unit>> GetLeafUnitsAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .Where(unit =>
                !_context.Units.Any(child =>
                    child.ParentUnitId == unit.Id))
            .ToListAsync();
    }

    // 9. Get units that have children
    public async Task<List<Unit>> GetUnitsWithChildrenAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .Where(unit =>
                _context.Units.Any(child =>
                    child.ParentUnitId == unit.Id))
            .ToListAsync();
    }

    // 10. Get employees of a unit
    public async Task<List<Employee>> GetEmployeesAsync(int unitId)
    {
        return await _context.Employees
            .AsNoTracking()
            .Where(employee =>
                employee.UnitId == unitId)
            .ToListAsync();
    }

    // 11. Get active employees of a unit
    public async Task<List<Employee>> GetActiveEmployeesAsync(int unitId)
    {
        return await _context.Employees
            .AsNoTracking()
            .Where(employee =>
                employee.UnitId == unitId &&
                employee.IsActive)
            .ToListAsync();
    }

    // 12. Get employees by position
    public async Task<List<Employee>> GetEmployeesByPositionAsync(
        string position)
    {
        return await _context.Employees
            .AsNoTracking()
            .Where(employee =>
                employee.Position == position)
            .ToListAsync();
    }

    // 13. Get units that have employees
    public async Task<List<Unit>> GetUnitsWithEmployeesAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .Where(unit =>
                _context.Employees.Any(employee =>
                    employee.UnitId == unit.Id))
            .ToListAsync();
    }

    // 14. Get units without employees
    public async Task<List<Unit>> GetUnitsWithoutEmployeesAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .Where(unit =>
                !_context.Employees.Any(employee =>
                    employee.UnitId == unit.Id))
            .ToListAsync();
    }

    // 15. Get leaf units that have employees
    public async Task<List<Unit>> GetLeafUnitsWithEmployeesAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .Where(unit =>
                !_context.Units.Any(child =>
                    child.ParentUnitId == unit.Id)
                &&
                _context.Employees.Any(employee =>
                    employee.UnitId == unit.Id))
            .ToListAsync();
    }

    // 16. Get units that have both children and employees
    public async Task<List<Unit>> GetUnitsWithChildrenAndEmployeesAsync()
    {
        return await _context.Units
            .AsNoTracking()
            .Where(unit =>
                _context.Units.Any(child =>
                    child.ParentUnitId == unit.Id)
                &&
                _context.Employees.Any(employee =>
                    employee.UnitId == unit.Id))
            .ToListAsync();
    }

    // 17. Count direct children
    //CountAsync()-ը database-ից ամբողջ Unit object-ները չի բերում, դրա համար .AsNoTracking() չեմ գրում
    public async Task<int> GetChildrenCountAsync(int unitId)
    {
        return await _context.Units
            .CountAsync(unit =>
                unit.ParentUnitId == unitId);
    }

    // 18. Count direct employees, նույնը այստեղ է
    public async Task<int> GetEmployeeCountAsync(int unitId)
    {
        return await _context.Employees
            .CountAsync(employee =>
                employee.UnitId == unitId);
    }

    // 19. Search units by name
    public async Task<List<Unit>> SearchUnitsByNameAsync(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return new List<Unit>();
        }

        searchText = searchText.Trim();

        return await _context.Units
            .AsNoTracking()
            .Where(unit =>
                EF.Functions.ILike(unit.Name, $"%{searchText}%"))
            .ToListAsync();
    }

    // 20. Check if unit has children, AnyAsync() entity-ներ չի բերում memory և track չի անում դրանք
    public async Task<bool> HasChildrenAsync(int unitId)
    {
        return await _context.Units
            .AnyAsync(unit =>
                unit.ParentUnitId == unitId);
    }

    // 21. Check if unit has employees
    public async Task<bool> HasEmployeesAsync(int unitId)
    {
        return await _context.Employees
            .AsNoTracking()
            .AnyAsync(employee =>
                employee.UnitId == unitId);
    }

    // 22․ Get by Name 
    public async Task<List<Employee>> GetEmployeesByNameAsync(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return new List<Employee>();
        }

        searchText = searchText.Trim();

        return await _context.Employees
            .AsNoTracking()
            .Where(employee =>
                EF.Functions.ILike(employee.FirstName, $"%{searchText}%") ||
                EF.Functions.ILike(employee.LastName, $"%{searchText}%"))
            .ToListAsync();
    }

    // Get by hire date range 1
    public async Task<List<Employee>> GetEmployeesByHireDateRangeAsync1(DateTime from, DateTime to)
    {
        return await _context.Employees
            .AsNoTracking()
            .Where(employee =>
                employee.HireDate >= from &&
                employee.HireDate <= to)
            .ToListAsync();
    }

    // Get by hire date range 2
    public async Task<List<Employee>> GetEmployeesByHireDateRangeAsync2(DateTime from, DateTime to)
    {
        if (from > to)
        {
            throw new ArgumentException("'from' date cannot be later than 'to' date.");
        }

        return await _context.Employees
            .AsNoTracking()
            .Where(employee =>
                employee.HireDate >= from &&
                employee.HireDate <= to)
            .ToListAsync();
    }

/*
GetUnitsByParentIdAsync(int? parentUnitId) — բերել կոնկրետ parent-ի տակ եղած unit-ները։
GetEmployeesByNameAsync(string searchText) — որոնել աշխատակցին անունով կամ ազգանունով։
GetEmployeesByDepartmentAsync(string department) — եթե Department դեռ օգտագործվում է Employee-ում։
GetEmployeesByEmailAsync(string email) — exact կամ partial email search։
GetEmployeesByHireDateRangeAsync(DateTime from, DateTime to) — բերել տվյալ ժամանակահատվածում ընդունված աշխատակիցներին։
GetActiveEmployeesAsync() — բոլոր ակտիվ աշխատակիցները՝ անկախ unit-ից։
GetInactiveEmployeesAsync() — բոլոր ոչ ակտիվ աշխատակիցները։
GetEmployeesByUnitAndPositionAsync(int unitId, string position) — միաժամանակ երկու filter։
GetUnitsByEmployeeCountAsync(int minCount) — օրինակ բերել այն unit-ները, որտեղ առնվազն 5 employee կա։
GetUnitsByNameAndParentAsync(string searchText, int? parentId) — search + hierarchy filter։
GetEmployeesByNameAsync(string searchText)
GetEmployeesByHireDateRangeAsync(DateTime from, DateTime to)
GetEmployeesByUnitAndPositionAsync(int unitId, string position)
*/
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

    private async void SetParentId(Unit unit, int parentId)
    {
        unit.ParentUnitId = parentId;
        _context.SaveChanges();
    }

}
