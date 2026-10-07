using MediatR;
using PeopleResourceManagement.Domain.Repositories;
using DomainUnit = PeopleResourceManagement.Domain.Entities.Unit;

namespace PeopleResourceManagement.Application.Units.Queries.GetEmptyUnits;

public class GetEmptyUnitsQueryHandler : IRequestHandler<GetEmptyUnitsQuery, List<DomainUnit>>
{
    private readonly IUnitRepository _unitRepository;

    public GetEmptyUnitsQueryHandler(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public async Task<List<DomainUnit>> Handle(GetEmptyUnitsQuery request, CancellationToken cancellationToken)
    {
        return await _unitRepository.GetEmptyUnitsAsync();
    }
}