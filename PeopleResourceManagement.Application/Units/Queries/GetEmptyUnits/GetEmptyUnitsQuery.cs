using MediatR;
using DomainUnit = PeopleResourceManagement.Domain.Entities.Unit;

namespace PeopleResourceManagement.Application.Units.Queries.GetEmptyUnits;

public record GetEmptyUnitsQuery : IRequest<List<DomainUnit>>;