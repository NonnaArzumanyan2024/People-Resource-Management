using MediatR;
using PeopleResourceManagement.Application.Logs.Commands.CreateOrUpdateUnitLog;
using PeopleResourceManagement.Application.Units.Queries.GetEmptyUnits;
using Microsoft.Extensions.Logging;
using PeopleResourceManagement.Application.Units.Queries.GetPreviouslyEmptyUnitsWithEmployees;

namespace PeopleResourceManagement.Application.Jobs;

public class CheckEmptyUnitsJob
{
    private readonly IMediator _mediator;
    private readonly ILogger<CheckEmptyUnitsJob> _logger;

    public CheckEmptyUnitsJob(IMediator mediator, ILogger<CheckEmptyUnitsJob> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var emptyUnits = await _mediator.Send(
            new GetEmptyUnitsQuery(),
            cancellationToken);

        foreach (var unit in emptyUnits)
        {
            var command = new CreateOrUpdateUnitLogCommand(
                unit.Id,
                "Unit has no employees");

            await _mediator.Send(
                command,
                cancellationToken);
        }

        var unitsWithNewEmployees = await _mediator.Send(new GetPreviouslyEmptyUnitsWithEmployeesQuery(), cancellationToken);

        foreach (var unit in unitsWithNewEmployees)
        {
            foreach (var employee in unit.Employees)
            {
                _logger.LogInformation(
                    "Employee {FirstName} {LastName} is now in Unit {UnitId} - {UnitName}",
                    employee.FirstName,
                    employee.LastName,
                    unit.Id,
                    unit.Name);
            }

            var command = new CreateOrUpdateUnitLogCommand(unit.Id,"Unit now has employees");
            await _mediator.Send(command, cancellationToken);
        }
    }
}