using People_Specification.Api.Models;
using People_Specification.Api.Repositories;

namespace People_Specification.Api.Services;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;
    public UnitService(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public async Task<List<Unit>> GetAllAsync()
    {
        return await _unitRepository.GetAllAsync();
    }
    public async Task<List<Unit>> GetAllWithEmployeesAsync()
    {
        return await _unitRepository.GetAllWithEmployeesAsync();
    }

    public async Task<Unit?> GetByIdAsync(int id)
    {
        return await _unitRepository.GetByIdAsync(id);
    }

    public async Task<Unit?> GetParentAsync(int unitId)
    {
        return await _unitRepository.GetParentAsync(unitId);
    }

    public async Task<List<Unit>> GetChildrenAsync(int parentUnitId)
    {
        return await _unitRepository.GetChildrenAsync(parentUnitId);
    }

    public async Task<Unit> AddAsync(Unit unit)
    {
        if (unit.ParentUnitId != null)
        {
            var parent = await _unitRepository.GetByIdAsync(unit.ParentUnitId.Value);

            if (parent == null)
            {
                throw new InvalidOperationException("Parent unit does not exist.");
            }
        }

        return await _unitRepository.AddAsync(unit);
    }

    public async Task UpdateAsync(Unit unit)
    {
        if (unit.Id == unit.ParentUnitId)
        {
            throw new InvalidOperationException("Unit cannot be its own parent.");
        }

        if (unit.ParentUnitId != null)
        {
            var parent = await _unitRepository.GetByIdAsync(unit.ParentUnitId.Value);

            if (parent == null)
            {
                throw new InvalidOperationException("Parent unit does not exist.");
            }
        }

        await _unitRepository.UpdateAsync(unit);
    }

    public async Task DeleteAsync(int id)
    {
        var unit = await _unitRepository.GetByIdAsync(id);

        if (unit == null)
        {
            throw new InvalidOperationException("Unit not found.");
        }

        var children = await _unitRepository.GetChildrenAsync(id);

        if (children.Count > 0)
        {
            throw new InvalidOperationException(
                "Unit cannot be deleted because it has child units.");
        }

        await _unitRepository.DeleteAsync(unit);
    }

    public async Task<Unit?> GetRootAsync()
    {
        return await _unitRepository.GetRootAsync();
    }

    public async Task<List<Unit>> GetSiblingsAsync(int unitId)
    {
        return await _unitRepository.GetSiblingsAsync(unitId);
    }
        
    public async Task<List<Unit>> GetLeafUnitsAsync()
    {
        return await _unitRepository.GetLeafUnitsAsync();
    }

    public async Task<List<Unit>> GetUnitsWithChildrenAsync()
    {
        return await _unitRepository.GetUnitsWithChildrenAsync();
    }
    
}
