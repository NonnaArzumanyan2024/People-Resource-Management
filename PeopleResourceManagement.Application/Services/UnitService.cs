using PeopleResourceManagement.Domain.Entities;
using PeopleResourceManagement.Application.Interfaces;
using PeopleResourceManagement.Domain.Repositories;
using PeopleResourceManagement.Application.DTOs;

namespace PeopleResourceManagement.Application.Services;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnitService(
        IUnitRepository unitRepository,
        IUnitOfWork unitOfWork)
    {
        _unitRepository = unitRepository;
        _unitOfWork = unitOfWork;
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

        var addedUnit = await _unitRepository.AddAsync(unit);

        await _unitOfWork.SaveChangesAsync();

        return addedUnit;    
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
        await _unitOfWork.SaveChangesAsync();
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
        await _unitOfWork.SaveChangesAsync();
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
    
    public async Task PatchAsync(int id, UnitPatchDto dto)
    {
        var unit = await _unitRepository.GetByIdAsync(id);

        if (unit == null)
        {
            throw new InvalidOperationException("Unit not found.");
        }

        unit.Name = dto.Name;

        var currentChildren = await _unitRepository.GetChildrenAsync(id);

        var currentChildIds = currentChildren
            .Select(child => child.Id)
            .ToList();

        var childrenToAdd = dto.ChildUnitIds
            .Except(currentChildIds)
            .ToList();

        var childrenToRemove = currentChildIds
            .Except(dto.ChildUnitIds)
            .ToList();

        foreach (var childId in childrenToAdd)
        {
            if (childId == id)
            {
                throw new InvalidOperationException(
                    "Unit cannot be its own child.");
            }

            var child = await _unitRepository.GetByIdAsync(childId);

            if (child == null)
            {
                throw new InvalidOperationException(
                    $"Child unit with id {childId} does not exist.");
            }

            child.ParentUnitId = id;

            await _unitRepository.UpdateAsync(child);
        }

        foreach (var childId in childrenToRemove)
        {
            var child = await _unitRepository.GetByIdAsync(childId);

            if (child != null)
            {
                child.ParentUnitId = null;

                await _unitRepository.UpdateAsync(child);
            }
        }

        await _unitRepository.UpdateAsync(unit);

        await _unitOfWork.SaveChangesAsync();
    }
}

