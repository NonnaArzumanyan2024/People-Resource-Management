using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using People_Specification.Api.DTOs;
using People_Specification.Api.Models;
using People_Specification.Api.Services;

namespace People_Specification.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UnitsController : ControllerBase
{
    private readonly IUnitService _unitService;

    public UnitsController(IUnitService unitService)
    {
        _unitService = unitService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UnitDto>>> GetAll()
    {
        var units = await _unitService.GetAllAsync();

        var result = units.Select(unit => new UnitDto
        {
            Id = unit.Id,
            Name = unit.Name,
            ParentUnitId = unit.ParentUnitId
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnitDto>> GetById(int id)
    {
        var unit = await _unitService.GetByIdAsync(id);

        if (unit == null)
        {
            return NotFound();
        }

        var result = new UnitDto
        {
            Id = unit.Id,
            Name = unit.Name,
            ParentUnitId = unit.ParentUnitId
        };

        return Ok(result);
    }

    [HttpGet("{id}/parent")]
    public async Task<ActionResult<UnitDto>> GetParent(int id)
    {
        var parent = await _unitService.GetParentAsync(id);

        if (parent == null)
        {
            return NotFound();
        }

        var result = new UnitDto
        {
            Id = parent.Id,
            Name = parent.Name,
            ParentUnitId = parent.ParentUnitId
        };

        return Ok(result);
    }

    [HttpGet("{id}/children")]
    public async Task<ActionResult<List<UnitDto>>> GetChildren(int id)
    {
        var children = await _unitService.GetChildrenAsync(id);

        var result = children.Select(unit => new UnitDto
        {
            Id = unit.Id,
            Name = unit.Name,
            ParentUnitId = unit.ParentUnitId
        }).ToList();

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UnitDto>> Create(UnitDto dto)
    {
        var unit = new Unit
        {
            Name = dto.Name,
            ParentUnitId = dto.ParentUnitId
        };

        var createdUnit = await _unitService.AddAsync(unit);

        var result = new UnitDto
        {
            Id = createdUnit.Id,
            Name = createdUnit.Name,
            ParentUnitId = createdUnit.ParentUnitId
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, UnitDto dto)
    {
        var unit = await _unitService.GetByIdAsync(id);

        if (unit == null)
        {
            return NotFound();
        }

        unit.Name = dto.Name;
        unit.ParentUnitId = dto.ParentUnitId;

        await _unitService.UpdateAsync(unit);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _unitService.DeleteAsync(id);

        return NoContent();
    }

    [HttpGet("root")]
    public async Task<ActionResult<UnitDto>> GetRoot()
    {
        var root = await _unitService.GetRootAsync();

        if (root == null)
        {
            return NotFound();
        }

        var result = new UnitDto
        {
        Id = root.Id,
        Name = root.Name,
        ParentUnitId = root.ParentUnitId
        };

        return Ok(result);
    }

    [HttpGet("{id}/siblings")]
    public async Task<ActionResult<List<UnitDto>>> GetSiblings(int id)
    {
        var siblings = await _unitService.GetSiblingsAsync(id);

        var result = siblings.Select(unit => new UnitDto
        {
            Id = unit.Id,
            Name = unit.Name,
            ParentUnitId = unit.ParentUnitId
        }).ToList();

        return Ok(result);
    }

    [HttpGet("leaf")]
    public async Task<ActionResult<List<UnitDto>>> GetLeafUnits()
    {
        var units = await _unitService.GetLeafUnitsAsync();

        var result = units.Select(unit => new UnitDto
        {
            Id = unit.Id,
            Name = unit.Name,
            ParentUnitId = unit.ParentUnitId
        }).ToList();

        return Ok(result);
    }

    
    [HttpGet("with-children")]
    public async Task<ActionResult<List<UnitDto>>> GetUnitsWithChildren()
    {
        var units = await _unitService.GetUnitsWithChildrenAsync();

        var result = units.Select(unit => new UnitDto
        {
            Id = unit.Id,
            Name = unit.Name,
            ParentUnitId = unit.ParentUnitId
        }).ToList();

        return Ok(result);
    }

}
