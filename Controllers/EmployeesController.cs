using Microsoft.AspNetCore.Mvc;
using People_Specification.Api.Models;
using People_Specification.Api.Services;
using AutoMapper;
using People_Specification.Api.DTOs;

namespace People_Specification.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeeService service, IMapper mapper)
    {
    _service = service;
    _mapper = mapper;
    }

    [HttpGet]
public async Task<ActionResult<List<EmployeeDto>>> GetAll()
{
    var employees = await _service.GetAllAsync();

    var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

    return Ok(employeeDtos);
}


    [HttpGet("{id}")]
public async Task<ActionResult<EmployeeDto>> GetById(int id)
{
    var employee = await _service.GetByIdAsync(id);

    if (employee == null)
    {
        return NotFound();
    }

    var employeeDto = _mapper.Map<EmployeeDto>(employee);

    return Ok(employeeDto);
}

    [HttpPost]
public async Task<ActionResult<EmployeeDto>> Create(EmployeeDto employeeDto)
{
    var employee = _mapper.Map<Employee>(employeeDto);

    var createdEmployee = await _service.AddAsync(employee);

    var createdEmployeeDto = _mapper.Map<EmployeeDto>(createdEmployee);

    return Ok(createdEmployeeDto);
}

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, EmployeeDto employeeDto)
    {
        var employee = _mapper.Map<Employee>(employeeDto);

        if (id != employee.Id)
        {
            return BadRequest();
        }

        var existingEmployee = await _service.GetByIdAsync(id);

        if (existingEmployee == null)
        {
            return NotFound();
        }

        await _service.UpdateAsync(employee);

        return NoContent();
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _service.GetByIdAsync(id);

        if (employee == null)
        {
            return NotFound();
        }

        await _service.DeleteAsync(id);

        return NoContent();
    }

}