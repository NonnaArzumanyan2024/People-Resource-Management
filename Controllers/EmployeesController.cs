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
    private readonly IEmployeeExportService _employeeExportService;

    public EmployeesController(
        IEmployeeService service,
        IMapper mapper,
        IEmployeeExportService employeeExportService)
    {
        _service = service;
        _mapper = mapper;
        _employeeExportService = employeeExportService;
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

    [HttpGet("active")]
    public async Task<ActionResult<List<EmployeeDto>>> GetActiveEmployees()
    {
        var employees = await _service.GetActiveEmployeesAsync();

        var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

        return Ok(employeeDtos);
    }

    [HttpGet("inactive")]
    public async Task<ActionResult<List<EmployeeDto>>> GetInactiveEmployees()
    {
        var employees = await _service.GetInactiveEmployeesAsync();

        var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

        return Ok(employeeDtos);
    }

    [HttpGet("department/{department}")]
    public async Task<ActionResult<List<EmployeeDto>>> GetByDepartment(
        string department)
    {
        var employees = await _service.GetByDepartmentAsync(department);

        var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

        return Ok(employeeDtos);
    }

    [HttpGet("position/{position}")]
    public async Task<ActionResult<List<EmployeeDto>>> GetByPosition(
        string position)
    {
        var employees = await _service.GetByPositionAsync(position);

        var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

        return Ok(employeeDtos);
    }

    [HttpGet("hire-date")]
    public async Task<ActionResult<List<EmployeeDto>>> GetByHireDate(
        [FromQuery] DateTime hireDate)
    {
        var employees = await _service.GetByHireDateAsync(hireDate);

        var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

        return Ok(employeeDtos);
    }

    [HttpGet("export/excel")]
    public async Task<IActionResult> ExportToExcel()
    {
        var employees = await _service.GetAllAsync();
        var fileBytes = _employeeExportService.ExportToExcel(employees);

        return File(
            fileBytes,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "employees.xlsx");
    }

    [HttpGet("export/html")]
    public async Task<IActionResult> ExportToHtml()
    {
        var employees = await _service.GetAllAsync();
        var html = _employeeExportService.ExportToHtml(employees);

        return File(
        
        System.Text.Encoding.UTF8.GetBytes(html),
        "text/html",
        "employees.html");
    }
}  