using MediatR;
using PeopleResourceManagement.Domain.Repositories;
using DomainUnit = PeopleResourceManagement.Domain.Entities.Unit;

namespace PeopleResourceManagement.Application.Units.Queries.GetPreviouslyEmptyUnitsWithEmployees;

public class GetPreviouslyEmptyUnitsWithEmployeesQueryHandler
    : IRequestHandler<GetPreviouslyEmptyUnitsWithEmployeesQuery, List<DomainUnit>>
{
    private readonly IUnitRepository _unitRepository;
    private readonly ILogRepository _logRepository;

    public GetPreviouslyEmptyUnitsWithEmployeesQueryHandler(
        IUnitRepository unitRepository,
        ILogRepository logRepository)
    {
        _unitRepository = unitRepository;
        _logRepository = logRepository;
    }

    public async Task<List<DomainUnit>> Handle(
        GetPreviouslyEmptyUnitsWithEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var emptyUnitLogs =
            await _logRepository.GetEmptyUnitLogsAsync();

        var unitsWithEmployees =
            await _unitRepository.GetUnitsWithEmployeesAsync();

        var emptyUnitIds = emptyUnitLogs
            .Select(log => log.UnitId)
            .ToHashSet();

        return unitsWithEmployees
            .Where(unit => emptyUnitIds.Contains(unit.Id))
            .ToList();
    }
}