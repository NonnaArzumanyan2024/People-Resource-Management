using MediatR;
using DomainUnit = PeopleResourceManagement.Domain.Entities.Unit;

namespace PeopleResourceManagement.Application.Units.Queries.GetPreviouslyEmptyUnitsWithEmployees;

public record GetPreviouslyEmptyUnitsWithEmployeesQuery : IRequest<List<DomainUnit>>;