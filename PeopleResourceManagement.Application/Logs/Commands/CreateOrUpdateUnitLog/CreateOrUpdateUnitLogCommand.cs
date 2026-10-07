using MediatR;

namespace PeopleResourceManagement.Application.Logs.Commands.CreateOrUpdateUnitLog;

public record CreateOrUpdateUnitLogCommand(int UnitId, string Descriptor) : IRequest;